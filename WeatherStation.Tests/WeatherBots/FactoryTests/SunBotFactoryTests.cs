using WeatherStation.Configuration;
using WeatherStation.Utilities.Exceptions;
using WeatherStation.WeatherBots;
using WeatherStation.WeatherBots.Factories;

namespace WeatherStation.Tests.WeatherBots.FactoryTests;

public class SunBotFactoryTests
{
  private readonly SunBotFactory _sut = new();

  private readonly BotConfiguration _botConfiguration = new();

  [Fact]
  public void Create_TemperatureThresholdIsNull_ShouldThrowException()
  {
    _botConfiguration.TemperatureThreshold = null;

    Assert.Throws<TemperatureThresholdNotDefinedException>(() => _sut.Create(_botConfiguration));
  }

  [Fact]
  public void Create_TemperatureThresholdIsNotNull_ShouldReturnSunBot()
  {
    var fixture = new Fixture();
    
    _botConfiguration.TemperatureThreshold = fixture.Create<double>();

    var returnedBot = _sut.Create(_botConfiguration);

    Assert.IsType<SunBot>(returnedBot);
  }
}