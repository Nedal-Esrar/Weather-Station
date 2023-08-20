namespace WeatherStation.Parsers.Factories;

public interface IParserFactory
{
  IParser<TInput> Create<TInput>();
}