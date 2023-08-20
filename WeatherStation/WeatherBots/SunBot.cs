using WeatherStation.Data;

namespace WeatherStation.WeatherBots;

public class SunBot : WeatherBot
{
  private readonly double _temperatureThreshold;

  public SunBot(string message, double temperatureThreshold) : base(message) =>
    _temperatureThreshold = temperatureThreshold;

  protected override bool ShouldActivate(WeatherData weatherData) => 
    weatherData.Temperature is not null && weatherData.Temperature > _temperatureThreshold;
}