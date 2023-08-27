using WeatherStation.Data;
using WeatherStation.WeatherBots;

namespace WeatherStation.Tests.WeatherBots.BotTests;

public class SunBotTests
{
  [Theory]
  [InlineData(16.6, 20.0, true)]
  [InlineData(15.5, 1.6, false)]
  [InlineData(17.5, null, false)]
  public void Activate_CustomData_ShouldOutputToTheConsole(double temperatureThreshold, double? newTemperature,
    bool shouldOutputToConsole)
  {
    var fixture = new Fixture();

    var botMessage = fixture.Create<string>();
    
    var sut = new SunBot(botMessage, temperatureThreshold);

    using var consoleOutput = new StringWriter();
    
    Console.SetOut(consoleOutput);
    
    sut.Activate(new WeatherData
    {
      Temperature = newTemperature
    });
    
    var consoleOutputContent = consoleOutput.ToString();
    
    var expectedOutput = shouldOutputToConsole
      ? $"""
         SunBot Activated!
         SunBot: {botMessage}

         """
      : "";
    
    Assert.Equal(expectedOutput, consoleOutputContent);
  }
}