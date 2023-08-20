namespace WeatherStation.Parsers;

public interface IParser<TInput>
{
  Task<TInput?> Parse(string input);
}