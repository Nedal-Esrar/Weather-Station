using WeatherStation.Configuration;
using WeatherStation.Utilities;
using WeatherStation.Utilities.Exceptions;
using WeatherStation.WeatherBots;
using WeatherStation.WeatherBots.Enums;
using WeatherStation.WeatherBots.FactoryProviders;

namespace WeatherStation.BotManager;

public class WeatherBotManager : IWeatherBotManager
{
  private readonly IDictionary<string, BotConfiguration> _botConfigurations;

  private readonly IWeatherBotFactoryProvider _botFactoryProvider;

  public WeatherBotManager(IDictionary<string, BotConfiguration> botConfigurations, 
    IWeatherBotFactoryProvider botFactoryProvider)
  {
    _botConfigurations = botConfigurations;

    _botFactoryProvider = botFactoryProvider;
  }

  public IList<WeatherBot> GetBots()
  {
    var bots = new List<WeatherBot>();

    // at most one instance for each type of bots
    foreach (var (botName, botConfiguration) in _botConfigurations)
    {
      if (botConfiguration.Enabled is null)
      {
        throw new UnknownStateException(StandardMessages.GenerateUnknownStateMessage(botName));
      }
      
      if (!(bool)botConfiguration.Enabled)
      {
        continue;
      }

      if (!Enum.TryParse<WeatherBotType>(botName, true, out var botType))
      {
        throw new ArgumentOutOfRangeException();
      }

      bots.Add(_botFactoryProvider.GetFactoryFor(botType).Create(botConfiguration));
    }

    return bots;
  }
}
