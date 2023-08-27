using System.Reflection;
using Xunit.Sdk;

namespace WeatherStation.Tests.Station.TestDataAttributes;

public class SupportedValidTestDataAttribute : DataAttribute
{
  private readonly List<object[]> _data = new()
  {
    new object[]{"""{"Location": "City Name","Temperature": 23.0,"Humidity": 85.0}"""},
    new object[]{"<WeatherData><Location>City Name</Location><Temperature>23.0</Temperature><Humidity>85.0</Humidity></WeatherData>"}
  };
  
  public override IEnumerable<object[]> GetData(MethodInfo testMethod)
  {
    return _data;
  }
}