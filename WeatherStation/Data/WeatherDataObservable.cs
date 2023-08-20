using WeatherStation.BotManager;
using WeatherStation.WeatherBots;

namespace WeatherStation.Data;

public class WeatherDataObservable : IWeatherDataObservable
{
  private readonly IList<WeatherBot> _subscribers;

  private WeatherData _weatherData;

  public WeatherDataObservable(IWeatherBotManager manager)
  {
    _subscribers = manager.GetBots();
  }
  
  public WeatherData WeatherData
  {
    get => _weatherData;
    
    set
    {
      _weatherData = value;
      
      Notify();
    }
  }

  public void Attach(WeatherBot bot)
  {
    _subscribers.Add(bot);
  }

  public void Detach(WeatherBot bot)
  {
    _subscribers.Remove(bot);
  }

  public void Notify()
  {
    foreach (var bot in _subscribers)
    {
      bot.Activate(WeatherData);
    }
  }
}