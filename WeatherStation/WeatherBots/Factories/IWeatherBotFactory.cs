using WeatherStation.Configuration;

namespace WeatherStation.WeatherBots.Factories;

public interface IWeatherBotFactory
{
  WeatherBot Create(BotConfiguration botConfiguration);
}