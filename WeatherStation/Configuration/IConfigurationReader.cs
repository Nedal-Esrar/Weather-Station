namespace WeatherStation.Configuration;

public interface IConfigurationReader
{
  Task<Dictionary<string, BotConfiguration>> Read(string filePath);
}