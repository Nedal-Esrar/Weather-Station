using WeatherStation.Configuration;
using WeatherStation.Utilities.Exceptions;
using WeatherStation.WeatherBots;
using WeatherStation.WeatherBots.Factories;

namespace WeatherStation.Tests.WeatherBots.FactoryTests;

public class SnowBotFactoryTests
{
  private readonly SnowBotFactory _sut = new();

  private readonly BotConfiguration _botConfiguration = new();

  [Fact]
  public void Create_TemperatureThresholdIsNull_ShouldThrowException()
  {
    _botConfiguration.TemperatureThreshold = null;
    
    Action act = () => _sut.Create(_botConfiguration);

    Assert.Throws<TemperatureThresholdNotDefinedException>(act);
  }

  [Fact]
  public void Create_TemperatureThresholdIsNotNull_ShouldReturnSnowBot()
  {
    var fixture = new Fixture();
    
    _botConfiguration.TemperatureThreshold = fixture.Create<double>();

    var returnedBot = _sut.Create(_botConfiguration);

    Assert.IsType<SnowBot>(returnedBot);
  }
}