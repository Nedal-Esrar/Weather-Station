using WeatherStation.Data;
using WeatherStation.WeatherBots;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace WeatherStation.Tests.WeatherBots.BotTests;

public class RainBotTests
{
  [Theory]
  [InlineData(16.6, 20.0, true)]
  [InlineData(15.5, 1.6, false)]
  [InlineData(17.5, null, false)]
  public void Activate_CustomData_ShouldOutputToTheConsole(double humidityThreshold, double? newHumidity,
    bool shouldOutputToConsole)
  {
    var fixture = new Fixture();
    
    var botMessage = fixture.Create<string>();
    
    var sut = new RainBot(botMessage, humidityThreshold);

    using var consoleOutput = new StringWriter();
    
    Console.SetOut(consoleOutput);
    
    sut.Activate(new WeatherData
    {
      Humidity = newHumidity
    });
    
    var consoleOutputContent = consoleOutput.ToString();
    
    var expectedOutput = shouldOutputToConsole
      ? $"""
         RainBot Activated!
         RainBot: {botMessage}

         """
      : "";
    
    Assert.Equal(expectedOutput, consoleOutputContent);
  }
}