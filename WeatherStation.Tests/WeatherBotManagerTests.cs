using WeatherStation.BotManager;
using WeatherStation.Configuration;
using WeatherStation.Utilities.Exceptions;
using WeatherStation.WeatherBots.Enums;
using WeatherStation.WeatherBots.Factories;
using WeatherStation.WeatherBots.FactoryProviders;

namespace WeatherStation.Tests;

public class WeatherBotManagerTests
{
  private readonly IDictionary<string, BotConfiguration> _botConfigurations;

  private readonly WeatherBotManager _sut;

  public WeatherBotManagerTests()
  {
    _botConfigurations = new Dictionary<string, BotConfiguration>();
    
    var botFactoryProviderMock = new Mock<IWeatherBotFactoryProvider>();

    var botFactoryMock = new Mock<IWeatherBotFactory>();

    botFactoryProviderMock
      .Setup(obj => obj.GetFactoryFor(It.IsAny<WeatherBotType>()))
      .Returns(botFactoryMock.Object);
    
    _sut = new WeatherBotManager(_botConfigurations, botFactoryProviderMock.Object);
  }

  private void AddBotConfigurations(IDictionary<string, BotConfiguration> botConfigurations)
  {
    foreach (var (key, value) in botConfigurations)
    {
      _botConfigurations.Add(key, value);
    }
  }
  
  [Fact]
  public void GetBots_UnknownEnabledState_ShouldThrowUnknownStateException()
  {
    AddBotConfigurations(new Dictionary<string, BotConfiguration>
    {
      {"RainBot", new BotConfiguration
      {
        Enabled = null
      }}
    });

    Assert.Throws<UnknownStateException>(() => _sut.GetBots());
  }

  [Fact]
  public void GetBots_OutOfBotTypeRange_ShouldThrowArgumentOutOfRangeException()
  {
    AddBotConfigurations(new Dictionary<string, BotConfiguration>
    {
      {"gg bot which is the best", new BotConfiguration
      {
        Enabled = true
      }}
    });

    Assert.Throws<ArgumentOutOfRangeException>(() => _sut.GetBots());
  }

  [Fact]
  public void GetBots_ValidConfigurations_ShouldReturnAListOfEnabledBots()
  {
    AddBotConfigurations(new Dictionary<string, BotConfiguration>
    {
      {"RainBot", new BotConfiguration
      {
        Enabled = true,
        TemperatureThreshold = 10,
        HumidityThreshold = 20,
      }},
      {"Sunbot", new BotConfiguration
      {
        Enabled = false,
        TemperatureThreshold = 10,
        HumidityThreshold = 20,
      }},
      {"snowBot", new BotConfiguration
      {
        Enabled = false,
        TemperatureThreshold = 10,
        HumidityThreshold = 20,
      }}
    });

    var returnedList = _sut.GetBots();

    Assert.Equal(1, returnedList.Count);
  }
}