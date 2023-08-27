using WeatherStation.WeatherBots;

namespace WeatherStation.BotManager;

public interface IWeatherBotManager
{
  IList<WeatherBot> GetBots();
}