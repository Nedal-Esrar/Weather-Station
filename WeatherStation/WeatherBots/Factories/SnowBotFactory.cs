using WeatherStation.Configuration;
using WeatherStation.Utilities;
using WeatherStation.Utilities.Exceptions;

namespace WeatherStation.WeatherBots.Factories;

public class SnowBotFactory : IWeatherBotFactory
{
  public WeatherBot Create(BotConfiguration botConfiguration)
  {
    if (botConfiguration.TemperatureThreshold is null)
    {
      throw new TemperatureThresholdNotDefinedException(StandardMessages.TemperatureThresholdIsNotDefined);
    }
    
    return new SnowBot(botConfiguration.Message, (double)botConfiguration.TemperatureThreshold);
  }
}