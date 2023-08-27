using WeatherStation.WeatherBots;

namespace WeatherStation.Data;

public interface IWeatherDataObservable
{
  WeatherData WeatherData { get; set; }

  void Attach(WeatherBot bot);

  void Detach(WeatherBot bot);

  void Notify();
}