using System.Net.Http.Headers;
using System.Text;
using HttpRequestToCurl.Extensions;
using HttpRequestToCurl.Models;

namespace HttpRequestToCurl.Handlers;

internal class ContentHandler : IHandler
{
	private const string DataFlag = "--data ";
	
	public bool CanHandle(HttpRequestMessage request) => request.Content != null;

	public string Handle(HttpRequestMessage request, HttpRequestConverterSettings settings)
	{
		var sb = new StringBuilder();

		if (request.Content != null) ContentHeadersToString(request.Content.Headers, ref sb);
		ContentToString(request.Content!, ref sb);

		return sb.ToString();
	}

	private static void ContentHeadersToString(HttpContentHeaders contentHeaders, ref StringBuilder sb)
	{
		sb.Append(Constants.HeaderFlag);

		sb.Append('"');
		sb.Append("Content-Type: " + contentHeaders.ContentType?.MediaType);
		sb.Append('"');
		
		sb.AppendWhitespace();
	}

	private static void ContentToString(HttpContent content, ref StringBuilder sb)
	{
		string body = content.ReadAsStringAsync().Result;

		sb.Append(DataFlag);

		sb.Append('"');
		sb.Append(body);
		sb.Append('"');
		
		sb.AppendWhitespace();
	}
}