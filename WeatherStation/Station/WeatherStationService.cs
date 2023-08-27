using WeatherStation.Data;
using WeatherStation.Parsers.FactoryProviders;
using WeatherStation.Utilities;

namespace WeatherStation.Station;

public class WeatherStationService : IWeatherStationService
{
  private readonly IWeatherDataObservable _weatherDataObservable;

  private readonly IParserFactoryProvider _inputFactories;

  public WeatherStationService(IWeatherDataObservable weatherDataObservable, 
    IParserFactoryProvider inputFactories)
  {
    _weatherDataObservable = weatherDataObservable;
    
    _inputFactories = inputFactories;
  }

  public async Task Run()
  {
    while (true)
    {
      Console.WriteLine(StandardMessages.EnterWeatherData);

      var input = Console.ReadLine()?.Trim();

      if (string.IsNullOrWhiteSpace(input))
      {
        continue;
      }

      await ProcessInput(input);
    }
  }
  
  public async Task ProcessInput(string input)
  {
    var inputParser = _inputFactories.GetFactoryFor(input)?.Create<WeatherData>();

    if (inputParser is null)
    {
      Console.WriteLine(StandardMessages.InvalidInput);
      
      return;
    }

    try
    {
      var weatherData = await inputParser.Parse(input);
      
      _weatherDataObservable.WeatherData = weatherData;
    }
    catch (Exception exception)
    {
      Console.WriteLine(StandardMessages.GenerateParsingErrorMessage(exception.Message));
    }
  }
}