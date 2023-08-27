using WeatherStation.Data;

namespace WeatherStation.WeatherBots;

public class RainBot : WeatherBot
{
  private readonly double _humidityThreshold;
  
  public RainBot(string message, double humidityThreshold) : base(message) =>
    _humidityThreshold = humidityThreshold;

  protected override bool ShouldActivate(WeatherData weatherData) =>
    weatherData.Humidity is not null && weatherData.Humidity > _humidityThreshold;
}