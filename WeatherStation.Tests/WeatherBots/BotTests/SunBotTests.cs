using FluentAssertions;
using WeatherStation.Data;
using WeatherStation.WeatherBots;

namespace WeatherStation.Tests.WeatherBots.BotTests;

public class SunBotTests
{
  private readonly string _botMessage;
  
  private readonly StringWriter _consoleOutput;

  public SunBotTests()
  {
    var fixture = new Fixture();

    _botMessage = fixture.Create<string>();
    
    _consoleOutput = new StringWriter();
    
    Console.SetOut(_consoleOutput);
  }

  [Theory]
  [InlineData(15.5, 1.6)]
  [InlineData(17.5, null)]
  public void Activate_LessThanThresholdOrNullTemperature_ShouldOutputNothingToTheConsole(double temperatureThreshold, double? newTemperature)
  {
    var sut = new SunBot(_botMessage, temperatureThreshold);
    
    sut.Activate(new WeatherData
    {
      Temperature = newTemperature
    });
    
    var consoleOutputContent = _consoleOutput.ToString();

    var expectedOutput = string.Empty;

    consoleOutputContent.Should().Be(expectedOutput);
  }
  
  [Theory]
  [InlineData(16.6, 20.0)]
  public void Activate_GreaterThanThresholdTemperature_ShouldOutputBotActivationMessageToTheConsole(double temperatureThreshold, double newTemperature)
  {
    var sut = new SunBot(_botMessage, temperatureThreshold);
    
    sut.Activate(new WeatherData
    {
      Temperature = newTemperature
    });
    
    var consoleOutputContent = _consoleOutput.ToString();

    var expectedOutput = $"""
                          SunBot Activated!
                          SunBot: {_botMessage}

                          """;
    
    consoleOutputContent.Should().Be(expectedOutput);
  }
}