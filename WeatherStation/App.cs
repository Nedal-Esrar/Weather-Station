using Microsoft.Extensions.DependencyInjection;
using WeatherStation.BotManager;
using WeatherStation.Configuration;
using WeatherStation.Data;
using WeatherStation.Parsers;
using WeatherStation.Parsers.Factories;
using WeatherStation.Parsers.FactoryProviders;
using WeatherStation.Station;
using WeatherStation.WeatherBots.Enums;
using WeatherStation.WeatherBots.Factories;
using WeatherStation.WeatherBots.FactoryProviders;

var jsonParser = new JsonParser<Dictionary<string, BotConfiguration>>();

var configurationReader = new ConfigurationReader(jsonParser);

IDictionary<string, BotConfiguration> configurations = await configurationReader.Read("appsettings.json");

IDictionary<WeatherBotType, IWeatherBotFactory> botFactories = 
  new Dictionary<WeatherBotType, IWeatherBotFactory>
  {
    { WeatherBotType.RainBot, new RainBotFactory() },
    { WeatherBotType.SnowBot, new SnowBotFactory() },
    { WeatherBotType.SunBot, new SunBotFactory() }
  };

IDictionary<string, IParserFactory> parserFactories = 
  new Dictionary<string, IParserFactory>
  {
    { "{", new JsonParserFactory() },
    { "<", new XmlParserFactory() }
  };

var serviceProvider = new ServiceCollection()
  .AddSingleton(botFactories)
  .AddSingleton(parserFactories)
  .AddSingleton(configurations)
  .AddScoped<IWeatherBotFactoryProvider, WeatherBotFactoryProvider>()
  .AddScoped<IParserFactoryProvider, ParserFactoryProvider>()
  .AddScoped<IWeatherBotManager, WeatherBotManager>()
  .AddScoped<IWeatherDataObservable, WeatherDataObservable>()
  .AddScoped<IWeatherStationService, WeatherStationService>();

var app = serviceProvider
  .BuildServiceProvider()
  .GetRequiredService<IWeatherStationService>();

await app.Run();