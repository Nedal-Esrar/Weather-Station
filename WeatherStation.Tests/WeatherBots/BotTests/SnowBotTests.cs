using WeatherStation.Data;
using WeatherStation.WeatherBots;

namespace WeatherStation.Tests.WeatherBots.BotTests;

public class SnowBotTests
{
  [Theory]
  [InlineData(16.6, 20.0, false)]
  [InlineData(15.5, 1.6, true)]
  [InlineData(17.5, null, false)]
  public void Activate_CustomData_ShouldOutputToTheConsole(double temperatureThreshold, double? newTemperature,
    bool shouldOutputToConsole)
  {
    var fixture = new Fixture();

    var botMessage = fixture.Create<string>();
    
    var sut = new SnowBot(botMessage, temperatureThreshold);

    using var consoleOutput = new StringWriter();
    
    Console.SetOut(consoleOutput);
    
    sut.Activate(new WeatherData
    {
      Temperature = newTemperature
    });
    
    var consoleOutputContent = consoleOutput.ToString();

    var expectedOutput = shouldOutputToConsole
      ? $"""
         SnowBot Activated!
         SnowBot: {botMessage}
         
         """
      : "";
    
    Assert.Equal(expectedOutput, consoleOutputContent);
  }
}