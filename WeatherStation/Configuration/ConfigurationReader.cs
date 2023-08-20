using WeatherStation.Parsers;
using WeatherStation.Utilities;

namespace WeatherStation.Configuration;

public class ConfigurationReader : IConfigurationReader
{
  private readonly IParser<Dictionary<string, BotConfiguration>> _parser;

  public ConfigurationReader(IParser<Dictionary<string, BotConfiguration>> parser)
    => _parser = parser;

  public async Task<Dictionary<string, BotConfiguration>> Read(string filePath)
  {
    var rawConfiguration = await File.ReadAllTextAsync(filePath);

    var parsedConfiguration = await _parser.Parse(rawConfiguration) 
                              ?? throw new Exception(StandardMessages.InvalidConfigurationFile);

    return parsedConfiguration;
  }
}