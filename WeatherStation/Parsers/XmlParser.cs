using System.Text;
using System.Xml.Serialization;

namespace WeatherStation.Parsers;

public class XmlParser<TInput> : IParser<TInput>
{
  public async Task<TInput?> Parse(string input)
  {
    var xmlSerializer = new XmlSerializer(typeof(TInput));

    using var stream = new MemoryStream(Encoding.UTF8.GetBytes(input));
    
    var parsedXml = await Task.Run(() => (TInput?)xmlSerializer.Deserialize(stream));

    return parsedXml;
  }
}