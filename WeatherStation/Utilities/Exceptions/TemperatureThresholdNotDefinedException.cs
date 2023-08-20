namespace WeatherStation.Utilities.Exceptions;

public class TemperatureThresholdNotDefinedException : Exception
{
  public TemperatureThresholdNotDefinedException(string message) : base(message) { }
}