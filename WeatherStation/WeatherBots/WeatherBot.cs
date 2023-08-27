using WeatherStation.Utilities;
using WeatherStation.Data;

namespace WeatherStation.WeatherBots;

public abstract class WeatherBot
{
  private readonly string _message;

  protected WeatherBot(string message) => 
    _message = message;

  public void Activate(WeatherData weatherData)
  {
    if (!ShouldActivate(weatherData))
    {
      return;
    }
    
    var activationMessage = StandardMessages.GenerateBotActivationMessage(GetType().Name, _message);
      
    Console.WriteLine(activationMessage);
  }

  protected abstract bool ShouldActivate(WeatherData weatherData);
}