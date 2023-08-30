using System.Reflection;
using Xunit.Sdk;

namespace WeatherStation.Tests.TestDataAttributes;

public class NotSupportedTestDataAttribute : DataAttribute
{
  private readonly List<object[]> _data = new()
  {
    new object[]{"GG"},
    new object[]{"[RANDOM]"}
  };
  
  public override IEnumerable<object[]> GetData(MethodInfo testMethod)
  {
    return _data;
  }
}