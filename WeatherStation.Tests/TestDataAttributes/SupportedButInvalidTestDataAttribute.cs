using System.Reflection;
using Xunit.Sdk;

namespace WeatherStation.Tests.TestDataAttributes;

public class SupportedButInvalidTestDataAttribute : DataAttribute
{
  private readonly List<object[]> _data = new()
  {
    new object[]{"""{"gg"; 1234"""},
    new object[]{"<Weather>GG<Werty>>"}
  };
  
  public override IEnumerable<object[]> GetData(MethodInfo testMethod)
  {
    return _data;
  }
}