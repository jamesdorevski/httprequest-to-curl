using System.Text;
using HttpRequestToCurl.Extensions;
using HttpRequestToCurl.Handlers;
using HttpRequestToCurl.Models;

namespace HttpRequestToCurl;

public static class HttpRequestConverter
{
	private const string CurlCommand = "curl ";
	private const string MethodFlag = "--request ";
	private const string HeaderFlag = "--header ";
	private const string DataFlag = "--data ";

	private static readonly IEnumerable<IHandler> Handlers;
	
	//TODO: add to appsettings
	private static readonly string[] SensitiveHeaders =
	{
		"Authorization"
	};

	public static string ConvertToCurl(HttpRequestMessage request, HttpRequestConverterSettings? settings = null)
	{
		var sb = new StringBuilder();
		settings ??= new HttpRequestConverterSettings();

		sb.Append(CurlCommand);
		sb.Append(MethodFlag);
		sb.Append(request.Method);
		sb.AppendWhitespace();

		sb.Append(request.RequestUri);
		sb.AppendWhitespace();

		if (request.Headers.Any())
			foreach (var header in request.Headers)
			{
				var headerString = HeaderToString(header, settings);
				if (headerString != null) sb.Append(headerString);
			}

		if (request.Content != null) sb.Append(ContentToString(request.Content));

		return sb.ToString();
	}

	private static string? HeaderToString(KeyValuePair<string, IEnumerable<string>> header,
		HttpRequestConverterSettings settings)
	{
		var sb = new StringBuilder();

		if (settings.IgnoreSensitiveInformation && header.Key.EqualsAny(SensitiveHeaders))
			return null;

		sb.Append(HeaderFlag);
		sb.Append('"');
		sb.Append(header.Key + ":");
		sb.AppendWhitespace();

		if (header.Value.Count() > 1)
			foreach (var value in header.Value)
				sb.Append(value + ',');
		else
			sb.Append(header.Value.First());

		sb.Append('"');

		sb.AppendWhitespace();

		return sb.ToString();
	}

	private static string ContentToString(HttpContent content)
	{
		var sb = new StringBuilder();
		var body = content.ReadAsStringAsync().Result;

		// Get headers from HttpContent as well
		if (content.Headers.ContentType != null) sb.Append("Content-Type: " + content.Headers.ContentType.MediaType);

		sb.Append(DataFlag);

		sb.Append('"');
		sb.Append(body);
		sb.Append('"');

		sb.AppendWhitespace();

		return sb.ToString();
	}
}