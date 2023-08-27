using WeatherStation.Data;

namespace WeatherStation.WeatherBots;

public class SnowBot : WeatherBot
{
  private readonly double _temperatureThreshold;
  
  public SnowBot(string message, double temperatureThreshold) : base(message) =>
    _temperatureThreshold = temperatureThreshold;

  protected override bool ShouldActivate(WeatherData weatherData) => 
    weatherData.Temperature is not null && weatherData.Temperature < _temperatureThreshold;
}