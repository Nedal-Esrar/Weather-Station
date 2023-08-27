using WeatherStation.Parsers.Factories;

namespace WeatherStation.Parsers.FactoryProviders;

public class ParserFactoryProvider : IParserFactoryProvider
{
  private readonly IDictionary<string, IParserFactory> _factories;

  public ParserFactoryProvider(IDictionary<string, IParserFactory> factories)
  {
    _factories = factories;
  }

  public IParserFactory? GetFactoryFor(string input)
  {
    foreach (var (identifyingPrefix, factory) in _factories)
    {
      if (input.StartsWith(identifyingPrefix))
      {
        return factory;
      }
    }

    return null;
  }
}