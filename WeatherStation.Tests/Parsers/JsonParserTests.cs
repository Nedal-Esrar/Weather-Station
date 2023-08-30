using System.Text.Json;
using FluentAssertions;
using FluentAssertions.Execution;
using WeatherStation.Data;
using WeatherStation.Parsers;
using WeatherStation.Tests.TestDataAttributes;

namespace WeatherStation.Tests.Parsers;

public class JsonParserTests
{
  private readonly JsonParser<WeatherData> _sut = new();
  
  [Theory]
  [NotSupportedTestData]
  [SupportedButInvalidTestData]
  [SupportedValidXmlTestData]
  public async Task Parse_NotJsonWeatherData_ShouldThrowException(string input)
  {
    Func<Task> act = async () => await _sut.Parse(input);

    await act.Should().ThrowAsync<Exception>();
  }

  [Theory]
  [InlineData("""{"Location": "City Name","Temperature": 23.0,"Humidity": 85.0}""", "City Name", 23.0, 85.0)]
  [InlineData("""{"Location": "City Name","Humidity": 85.0}""", "City Name", null, 85.0)] 
  public async Task Parse_JsonWeatherData_ShouldReturnTheExpectedData(string json, 
    string expectedLocation, double? expectedTemperature, double? expectedHumidity)
  {
    var weatherData = await _sut.Parse(json);

    weatherData.Should().NotBeNull();

    using (new AssertionScope())
    {
      weatherData.Location.Should().Be(expectedLocation);
      weatherData.Temperature.Should().BeApproximately(expectedTemperature, 0.01);
      weatherData.Humidity.Should().BeApproximately(expectedHumidity, 0.01);
    }
  }
}