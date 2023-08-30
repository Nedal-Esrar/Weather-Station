using System.Reflection;
using Xunit.Sdk;

namespace WeatherStation.Tests.TestDataAttributes;

public class SupportedValidXmlTestDataAttribute : DataAttribute
{
  private readonly List<object[]> _data = new()
  {
    new object[]{"<WeatherData><Location>City Name</Location><Temperature>23.0</Temperature><Humidity>85.0</Humidity></WeatherData>"},
    new object[]{"<WeatherData><Location>Jenin</Location><Temperature>100.0</Temperature><Humidity>50.0</Humidity></WeatherData>"}
  };
  
  public override IEnumerable<object[]> GetData(MethodInfo testMethod)
  {
    return _data;
  }
}