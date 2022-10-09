using System.Net.Http.Headers;
using System.Text;
using HttpRequestToCurl.Extensions;
using HttpRequestToCurl.Models;

namespace HttpRequestToCurl.Handlers;

internal class ContentHandler : IHandler
{
	private const string DataFlag = "--data ";
	
	public bool CanHandle(HttpRequestMessage request) => request.Content != null;

	public void Handle(HttpRequestMessage request, HttpRequestConverterSettings settings, ref StringBuilder sb)
	{
		if (request.Content != null) ContentHeadersToString(request.Content.Headers, ref sb);
		ContentToString(request.Content!, ref sb);
	}

	private static void ContentHeadersToString(HttpContentHeaders contentHeaders, ref StringBuilder sb)
	{
		sb.Append(Constants.HeaderFlag);

		sb.AppendSingleQuote();
		sb.Append("Content-Type: " + contentHeaders.ContentType?.MediaType);
		sb.AppendSingleQuote();
		
		sb.AppendWhitespace();
	}

	private static void ContentToString(HttpContent content, ref StringBuilder sb)
	{
		string body = content.ReadAsStringAsync().Result;

		sb.Append(DataFlag);

		sb.AppendSingleQuote();
		sb.AppendDoubleQuote();
		sb.Append(body);
		sb.AppendDoubleQuote();
		sb.AppendSingleQuote();
		
		sb.AppendWhitespace();
	}
}