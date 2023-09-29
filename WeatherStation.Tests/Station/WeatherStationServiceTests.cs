using FluentAssertions;
using WeatherStation.Data;
using WeatherStation.Parsers.Factories;
using WeatherStation.Parsers.FactoryProviders;
using WeatherStation.Station;
using WeatherStation.Tests.TestDataAttributes;

namespace WeatherStation.Tests.Station;

public class WeatherStationServiceTests
{
  private readonly Mock<IWeatherDataObservable> _weatherDataObservableMock;

  private readonly WeatherStationService _sut;
  
  private readonly StringWriter _consoleOutput;

  public WeatherStationServiceTests()
  {
    var parserFactoryProviderMock = new Mock<IParserFactoryProvider>();

    parserFactoryProviderMock
      .Setup(provider => provider.GetFactoryFor(It.Is<string>(input => input.StartsWith("{"))))
      .Returns(new JsonParserFactory());

    parserFactoryProviderMock
      .Setup(provider => provider.GetFactoryFor(It.Is<string>(input => input.StartsWith("<"))))
      .Returns(new XmlParserFactory());

    _weatherDataObservableMock = new Mock<IWeatherDataObservable>();

    _sut = new WeatherStationService(_weatherDataObservableMock.Object, parserFactoryProviderMock.Object);
    
    _consoleOutput = new StringWriter();
    
    Console.SetOut(_consoleOutput);
  }
  
  [Theory]
  [NotSupportedTestData]
  public async Task ProcessInput_NotSupportedData_ShouldDisplayInvalidMessage(string input)
  {
    await _sut.ProcessInput(input);
    
    var expectedOutput = "Invalid input.\n";
    
    var consoleOutputContent = _consoleOutput.ToString();

    consoleOutputContent.Should().Be(expectedOutput);
  }

  [Theory]
  [SupportedButInvalidTestData]
  public async Task ProcessInput_SupportedButInvalidData_ShouldDisplayErrorWhileParsingMessage(string input)
  {
    await _sut.ProcessInput(input);
    
    var expectedOutputPrefix = "An error has occurred while parsing input:";
    
    var consoleOutputContent = _consoleOutput.ToString();

    consoleOutputContent.Should().StartWith(expectedOutputPrefix);
  }

  [Theory]
  [SupportedValidJsonTestData, SupportedValidXmlTestData]
  public async Task ProcessInput_SupportedValidData_ShouldNotDisplayErrorMessages(string input)
  {
    await _sut.ProcessInput(input);
    
    var expectedOutput = "";
    
    var consoleOutputContent = _consoleOutput.ToString();

    consoleOutputContent.Should().Be(expectedOutput);
  }

  [Theory]
  [NotSupportedTestData]
  [SupportedButInvalidTestData]
  public async Task ProcessInput_IncompatibleData_SetterShouldNotBeInvoked(string input)
  {
    await _sut.ProcessInput(input);
    
    _weatherDataObservableMock.VerifySet(observable => observable.WeatherData = It.IsAny<WeatherData>(), 
      Times.Never);
  }
  
  [Theory]
  [SupportedValidJsonTestData]
  [SupportedValidXmlTestData]
  public async Task ProcessInput_CompatibleData_SetterShouldBeInvokedOnce(string input)
  {
    await _sut.ProcessInput(input);
    
    _weatherDataObservableMock.VerifySet(observable => observable.WeatherData = It.IsAny<WeatherData>(), 
      Times.Once);
  }
}