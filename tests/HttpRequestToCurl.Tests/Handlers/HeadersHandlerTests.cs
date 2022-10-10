using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using HttpRequestToCurl.Handlers;
using HttpRequestToCurl.Models;

namespace HttpRequestToCurl.Tests.Handlers;

public class HeadersHandlerTests
{
	private readonly HeadersHandler _sut = new();

	[Fact]
	public void SensitiveHeader_ShouldBeExcluded_IfIgnoreSensitiveInformationIsTrue()
	{
		var request = new HttpRequestMessage
		{
			Headers =
			{
				Authorization = new AuthenticationHeaderValue("Bearer", "bear"),
				Accept = { new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json) }
			}
		};

		var settings = new HttpRequestConverterSettings
		{
			IgnoreSensitiveInformation = true
		};

		var sb = new StringBuilder();
		
		if (_sut.CanHandle(request))
			_sut.Handle(request, settings, ref sb);

		string actual = sb.ToString();
		
		Assert.NotEmpty(actual);
		Assert.Contains("--header 'Accept: application/json'", actual);
		Assert.DoesNotContain("--header 'Authorization: Bearer bear'", actual);
	}

	[Fact]
	public void HeaderWithMultipleValues_IsParsedCorrectly()
	{
		var request = new HttpRequestMessage();
		request.Headers.Add("Accept", new []
		{
			MediaTypeNames.Application.Json, MediaTypeNames.Image.Jpeg, MediaTypeNames.Text.Plain
		});

		var sb = new StringBuilder();
		
		if (_sut.CanHandle(request))
			_sut.Handle(request, new HttpRequestConverterSettings(), ref sb);

		string actual = sb.ToString();
		
		Assert.NotEmpty(actual);
		Assert.Contains("--header 'Accept: application/json,image/jpeg,text/plain,'", actual);
	}

	[Fact]
	public void HttpRequestMessageWithNoHeaders_ShouldNotProcess()
	{
		var request = new HttpRequestMessage();

		var sb = new StringBuilder();
		
		if (_sut.CanHandle(request))
			_sut.Handle(request, new HttpRequestConverterSettings(), ref sb);

		string actual = sb.ToString();
		Assert.Empty(actual);
	}
}