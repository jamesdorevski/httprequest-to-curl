using System.Net.Mime;
using System.Text;
using HttpRequestToCurl.Models;

namespace HttpRequestToCurl.Tests
{
	public class HttpRequestConverterTests
	{
		[Fact]
		public void ConvertsGetRequestSuccessfully()
		{
			var request = new HttpRequestMessage
			{
				RequestUri = new Uri("https://localhost:7126/WeatherForecast"),
				Method = HttpMethod.Post,
				Content = new StringContent("Hi there!", Encoding.UTF8, MediaTypeNames.Application.Json)
			};

			request.Headers.Add("X-Hello", "world");

			var actual = HttpRequestConverter.ConvertToCurl(request, new HttpRequestConverterSettings
			{
				AllowInsecureConnections = true
			});

			Assert.NotEmpty(actual);
		}
	}
}