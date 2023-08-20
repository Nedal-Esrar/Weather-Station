using WeatherStation.Parsers.Factories;

namespace WeatherStation.Parsers.FactoryProviders;

public interface IParserFactoryProvider
{
  IParserFactory? GetFactoryFor(string input);
}