using System.Text;
using System.Text.Json;

namespace WeatherStation.Parsers;

public class JsonParser<TInput> : IParser<TInput>
{
  public async Task<TInput?> Parse(string input)
  {
    using var stream = new MemoryStream(Encoding.UTF8.GetBytes(input));
    
    var options = new JsonSerializerOptions
    {
      PropertyNameCaseInsensitive = true
    };

    var parsedJson = await JsonSerializer.DeserializeAsync<TInput>(stream, options);

    return parsedJson;
  }
}