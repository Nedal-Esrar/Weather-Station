namespace WeatherStation.Parsers.Factories;

public class JsonParserFactory : IParserFactory
{
  public IParser<TInput> Create<TInput>()
  {
    return new JsonParser<TInput>();
  }
}