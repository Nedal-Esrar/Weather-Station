using FluentAssertions;
using WeatherStation.Data;
using WeatherStation.WeatherBots;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace WeatherStation.Tests.WeatherBots.BotTests;

public class RainBotTests
{
  private readonly string _botMessage;
  
  private readonly StringWriter _consoleOutput;

  public RainBotTests()
  {
    var fixture = new Fixture();

    _botMessage = fixture.Create<string>();
    
    _consoleOutput = new StringWriter();
    
    Console.SetOut(_consoleOutput);
  }
  
  [Theory]
  [InlineData(15.5, 1.6)]
  [InlineData(17.5, null)]
  public void Activate_LessThanThresholdOrNullHumidity_ShouldOutputNothingToTheConsole(double humidityThreshold, double? newHumidity)
  {
    var sut = new RainBot(_botMessage, humidityThreshold);
    
    sut.Activate(new WeatherData
    {
      Humidity = newHumidity
    });
    
    var consoleOutputContent = _consoleOutput.ToString();
    
    var expectedOutput = string.Empty;
    
    consoleOutputContent.Should().Be(expectedOutput);
  }
  
  [Theory]
  [InlineData(16.6, 20.0)]
  public void Activate_GreaterThanThreshold_ShouldOutputBotActivationMessageToTheConsole(double humidityThreshold, double newHumidity)
  {
    var sut = new RainBot(_botMessage, humidityThreshold);
    
    sut.Activate(new WeatherData
    {
      Humidity = newHumidity
    });
    
    var consoleOutputContent = _consoleOutput.ToString();
    
    var expectedOutput = $"""
                          RainBot Activated!
                          RainBot: {_botMessage}

                          """;
    
    consoleOutputContent.Should().Be(expectedOutput);
  }
}