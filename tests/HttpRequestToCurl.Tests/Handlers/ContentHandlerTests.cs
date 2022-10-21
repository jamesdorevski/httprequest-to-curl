using System.Net.Mime;
using System.Text;
using HttpRequestToCurl.Exceptions;
using HttpRequestToCurl.Handlers;
using HttpRequestToCurl.Models;

namespace HttpRequestToCurl.Tests.Handlers
{
	// TODO: test all content types + encodings
	public class ContentHandlerTests
	{
		private readonly ContentHandler _sut = new();

		public static IEnumerable<object[]> ValidContentTestCases => new[]
		{
			new object[]
			{
				new StringContent("Hello world!", Encoding.UTF8, MediaTypeNames.Application.Json), "--header 'Content-Type: application/json' --data '\"Hello world!\"'",
			},
			new object[]
			{
				new StringContent(new {Hello = "World", Complex = "Object"}.ToString(), Encoding.UTF8, MediaTypeNames.Application.Json), "--header 'Content-Type: application/json' --data '\"{ Hello = World, Complex = Object }\"' "
			}
		};

		[Theory]
		[MemberData(nameof(ValidContentTestCases))]
		public void HttpRequestMessageWithContent_IsParsedCorrectly(HttpContent content, string expectedContentString)
		{
			var request = new HttpRequestMessage
			{
				Content = content
			};

			var sb = new StringBuilder();
		
			if (_sut.CanHandle(request))
				_sut.Handle(request, new HttpRequestConverterSettings(), ref sb);

			string actual = sb.ToString();
		
			Assert.NotEmpty(actual);
			Assert.Contains(expectedContentString, actual);
		}

		[Fact]
		public void UnsupportedContent_ShouldThrowUnsupportedContentTypeException()
		{
			var request = new HttpRequestMessage
			{
				Content = new StreamContent(new MemoryStream(), 1)
			};

			var sb = new StringBuilder();

			Assert.Throws<UnsupportedContentTypeException>(() => _sut.Handle(request, new HttpRequestConverterSettings(), ref sb));
		}
	}
}