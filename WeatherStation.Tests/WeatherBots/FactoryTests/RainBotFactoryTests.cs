using WeatherStation.Configuration;
using WeatherStation.Utilities.Exceptions;
using WeatherStation.WeatherBots;
using WeatherStation.WeatherBots.Factories;

namespace WeatherStation.Tests.WeatherBots.FactoryTests;

public class RainBotFactoryTests
{
  private readonly RainBotFactory _sut = new();

  private readonly BotConfiguration _botConfiguration = new();

  [Fact]
  public void Create_HumidityThresholdIsNull_ShouldThrowException()
  {
    _botConfiguration.HumidityThreshold = null;

    Action act = () => _sut.Create(_botConfiguration);

    Assert.Throws<HumidityNotDefinedException>(act);
  }

  [Fact]
  public void Create_Create_HumidityThresholdIsNotNull_ShouldReturnRainBot()
  {
    var fixture = new Fixture();
    
    _botConfiguration.HumidityThreshold = fixture.Create<double>();

    var returnedBot = _sut.Create(_botConfiguration);

    Assert.IsType<RainBot>(returnedBot);
  }
}