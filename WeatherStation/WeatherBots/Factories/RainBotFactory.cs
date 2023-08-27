using WeatherStation.Configuration;
using WeatherStation.Utilities;
using WeatherStation.Utilities.Exceptions;

namespace WeatherStation.WeatherBots.Factories;

public class RainBotFactory : IWeatherBotFactory
{
  public WeatherBot Create(BotConfiguration botConfiguration)
  {
    if (botConfiguration.HumidityThreshold is null)
    {
      throw new HumidityNotDefinedException(StandardMessages.HumidityThresholdIsNotDefined);
    }
    return new RainBot(botConfiguration.Message, (double)botConfiguration.HumidityThreshold);
  }
}