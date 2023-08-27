using System.Reflection;
using Xunit.Sdk;

namespace WeatherStation.Tests.Station.TestDataAttributes;

public class ObservableSetterTestDataAttribute : DataAttribute
{
  private readonly List<object[]> _data = new()
  {
    new object[] { "GG", false },
    new object[] { "[RANDOM]", false },
    new object[]{"""{"gg"; 1234""", false },

    new object[] {
      "<Weather>GG<Werty>>", false
    },

    new object[] {
      """{"Location": "City Name","Temperature": 23.0,"Humidity": 85.0}""", true
    },

    new object[] {
      "<WeatherData><Location>City Name</Location><Temperature>23.0</Temperature><Humidity>85.0</Humidity></WeatherData>",
      true
    }
  };
  
  public override IEnumerable<object[]> GetData(MethodInfo testMethod)
  {
    return _data;
  }
}