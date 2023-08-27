using WeatherStation.WeatherBots.Enums;

namespace WeatherStation.WeatherBots.FactoryProviders;

public class WeatherBotFactoryProvider : IWeatherBotFactoryProvider
{
  private readonly IDictionary<WeatherBotType, Factories.IWeatherBotFactory> _factories;

  public WeatherBotFactoryProvider(IDictionary<WeatherBotType, Factories.IWeatherBotFactory> factories)
  {
    _factories = factories;
  }
  
  public Factories.IWeatherBotFactory GetFactoryFor(WeatherBotType botType)
  {
    if (_factories.TryGetValue(botType, out var botFactory))
    {
      return botFactory;
    }

    throw new ArgumentOutOfRangeException();
  }
}