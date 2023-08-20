using WeatherStation.WeatherBots.Enums;

namespace WeatherStation.WeatherBots.FactoryProviders;

public interface IWeatherBotFactoryProvider
{
  Factories.IWeatherBotFactory GetFactoryFor(WeatherBotType botType);
}