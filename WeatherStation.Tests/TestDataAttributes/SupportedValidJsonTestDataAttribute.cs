using System.Reflection;
using Xunit.Sdk;

namespace WeatherStation.Tests.TestDataAttributes;

public class SupportedValidJsonTestDataAttribute : DataAttribute
{
  private readonly List<object[]> _data = new()
  {
    new object[]{"""{"Location": "City Name","Temperature": 23.0,"Humidity": 85.0}"""},
    new object[]{"""{"LOCAtion": "Jenin", "TEMpEratUre": 35.5, "HumidIty": 60.0}"""}
  };
  
  public override IEnumerable<object[]> GetData(MethodInfo testMethod)
  {
    return _data;
  }
}