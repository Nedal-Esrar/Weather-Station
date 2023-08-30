using WeatherStation.Data;
using WeatherStation.WeatherBots;

namespace WeatherStation.Tests.WeatherBots.BotTests;

public class SnowBotTests
{
  private readonly string _botMessage;
  
  private readonly StringWriter _consoleOutput;

  public SnowBotTests()
  {
    var fixture = new Fixture();

    _botMessage = fixture.Create<string>();
    
    _consoleOutput = new StringWriter();
    
    Console.SetOut(_consoleOutput);
  }

  [Theory]
  [InlineData(16.6, 20.0)]
  [InlineData(17.5, null)]
  public void Activate_GreaterThanThresholdOrNullTemperature_ShouldOutputNothingToTheConsole(double temperatureThreshold, double? newTemperature)
  {
    var sut = new SnowBot(_botMessage, temperatureThreshold);
    
    sut.Activate(new WeatherData
    {
      Temperature = newTemperature
    });
    
    var consoleOutputContent = _consoleOutput.ToString();

    var expectedOutput = string.Empty;
    
    Assert.Equal(expectedOutput, consoleOutputContent);
  }
  
  [Theory]
  [InlineData(15.5, 1.6)]
  public void Activate_GreaterThanThresholdTemperature_ShouldOutputBotActivationMessageToTheConsole(double temperatureThreshold, double newTemperature)
  {
    var sut = new SnowBot(_botMessage, temperatureThreshold);
    
    sut.Activate(new WeatherData
    {
      Temperature = newTemperature
    });
    
    var consoleOutputContent = _consoleOutput.ToString();

    var expectedOutput = $"""
                          SnowBot Activated!
                          SnowBot: {_botMessage}

                          """;
    
    Assert.Equal(expectedOutput, consoleOutputContent);
  }
}