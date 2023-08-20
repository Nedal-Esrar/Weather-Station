namespace WeatherStation.Utilities.Exceptions;

public class UnknownStateException : Exception
{
  public UnknownStateException(string message) : base(message) { }
}