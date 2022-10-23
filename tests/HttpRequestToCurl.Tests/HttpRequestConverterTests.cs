using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using HttpRequestToCurl.Models;

namespace HttpRequestToCurl.Tests
{
	public class HttpRequestConverterTests
	{
		[Fact]
		public void ConvertsPostRequest()
		{
			string expected = "curl --insecure --request POST 'https://localhost:7126/WeatherForecast' --header 'X-Hello: world' --header 'Content-Type: application/json' --data '\"Hi there!\"' ";
			
			var request = new HttpRequestMessage
			{
				RequestUri = new Uri("https://localhost:7126/WeatherForecast"),
				Method = HttpMethod.Post,
				Content = new StringContent("Hi there!", Encoding.UTF8, MediaTypeNames.Application.Json)
			};

			request.Headers.Add("X-Hello", "world");

			var settings = new HttpRequestConverterSettings
			{
				AllowInsecureConnections = true,
			};

			var actual = HttpRequestConverter.ConvertToCurl(request, settings);

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void ConvertsGetRequest()
		{
			string expected = "curl --request GET 'https://www.example.com/v1/hi' ";
			
			var request = new HttpRequestMessage
			{
				RequestUri = new Uri("https://www.example.com/v1/hi"),
				Method = HttpMethod.Get
			};

			var actual = HttpRequestConverter.ConvertToCurl(request);
			
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void OmittingHttpRequestConverterSettings_SetsDefaultSettings()
		{
			var request = new HttpRequestMessage
			{
				RequestUri = new Uri("https://www.example.com"),
				Method = HttpMethod.Get,
				Headers =
				{
					Authorization = new AuthenticationHeaderValue("Bearer", "bear")
				}
			};

			var actual = HttpRequestConverter.ConvertToCurl(request);
			
			Assert.DoesNotContain("--insecure", actual);
			Assert.DoesNotContain("--header 'Authorization: Bearer bear'", actual);
		}
	}
}