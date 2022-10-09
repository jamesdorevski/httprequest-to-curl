using System.Net.Mime;
using System.Text;

namespace HttpRequestToCurl.Tests;

public class HttpRequestConverterTests
{
	[Fact]
	public void ConvertsGetRequestSuccessfully()
	{
		var request = new HttpRequestMessage
		{
			RequestUri = new Uri("http://localhost:5002/WeatherForecast"),
			Method = HttpMethod.Post,
			Content = new StringContent("Hi there!", Encoding.UTF8, MediaTypeNames.Text.Plain)
		};

		request.Headers.Add("X-Hello", "world");

		var actual = HttpRequestConverter.ConvertToCurl(request);
		Assert.NotEmpty(actual);
	}
}