using NUnit.Framework;
using Service.Weather;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Tests.IntegrationTests
{
    [TestFixture]
    public class Weather_forecast_query_handler_tests
    {
        [Test]
        public async Task Get_weather_forecasts()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/weatherforecast");
            var (response, content) = await SUT.SendHttpRequest<WeatherForecastResponse>(request, new { });

            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(content.WeatherForecasts, Is.Not.Null);
                CollectionAssert.IsNotEmpty(content.WeatherForecasts);
            });
        }
    }
}
