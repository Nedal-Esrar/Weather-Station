namespace WeatherStation.Utilities.Exceptions;

public class HumidityNotDefinedException : Exception
{
  public HumidityNotDefinedException(string message) : base(message) { }
}