using System.Text;
using HttpRequestToCurl.Exceptions;
using HttpRequestToCurl.Handlers;
using HttpRequestToCurl.Models;

namespace HttpRequestToCurl.Tests.Handlers
{
	public class CommandHandlerTests
	{
		private readonly CommandHandler _sut = new();

		public static IEnumerable<object[]> HttpMethodTypes => new[]
		{
			new object[] { HttpMethod.Get, "--request GET" },
			new object[] { HttpMethod.Post, "--request POST" },
			new object[] { HttpMethod.Put, "--request PUT" },
			new object[] { HttpMethod.Delete, "--request DELETE" }
		};

		[Theory]
		[MemberData(nameof(HttpMethodTypes))]
		public void HttpRequest_ShouldMapToCorrectMethodType(HttpMethod method, string expectedSubstring)
		{
			var request = new HttpRequestMessage
			{
				RequestUri = new Uri("https://www.example.com/"),
				Method = method
			};
			var sb = new StringBuilder();
		
			if (_sut.CanHandle(request))
				_sut.Handle(request, new HttpRequestConverterSettings(), ref sb);
		
			string actual = sb.ToString();

			Assert.NotEmpty(actual);
			Assert.DoesNotContain("--insecure", actual);
			Assert.Contains(expectedSubstring, actual);
		}

		[Fact]
		public void HttpRequestWithNoRequestUri_ShouldThrowNullRequestUriException()
		{
			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Get
			};
			var sb = new StringBuilder();
		
			Assert.Throws<NullRequestUriException>(() => _sut.Handle(request, new HttpRequestConverterSettings(), ref sb));
		}

		[Fact]
		public void HttpRequestConverterSettings_WithAllowInsecureConnections_ShouldContainInsecureInCommand()
		{
			var request = new HttpRequestMessage
			{
				RequestUri = new Uri("https://www.example.com/"),
				Method = HttpMethod.Get
			};

			var settings = new HttpRequestConverterSettings
			{
				AllowInsecureConnections = true
			};
		
			var sb = new StringBuilder();
		
			if (_sut.CanHandle(request))
				_sut.Handle(request, settings, ref sb);
		
			string actual = sb.ToString();
		
			Assert.NotEmpty(actual);
			Assert.Contains("--insecure", actual);
		}
	}
}