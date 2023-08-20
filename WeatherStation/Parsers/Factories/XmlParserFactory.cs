namespace WeatherStation.Parsers.Factories;

public class XmlParserFactory : IParserFactory
{
  public IParser<TInput> Create<TInput>()
  {
    return new XmlParser<TInput>();
  }
}