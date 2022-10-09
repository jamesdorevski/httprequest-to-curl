using System.Text;
using HttpRequestToCurl.Extensions;
using HttpRequestToCurl.Models;

namespace HttpRequestToCurl.Handlers;

internal class HeadersHandler : IHandler
{
	private const string HeaderFlag = "--header ";
	
	private static readonly string[] SensitiveHeaders = 
	{
		"Authorization"
	};
	
	public bool CanHandle(HttpRequestMessage request) => request.Headers.Any();

	public string Handle(HttpRequestMessage request, HttpRequestConverterSettings settings)
	{
		var sb = new StringBuilder();
		
		foreach (var header in request.Headers)
			HeaderToString(header, settings, ref sb);

		return sb.ToString();
	}

	private static void HeaderToString(
		KeyValuePair<string, IEnumerable<string>> header,
		HttpRequestConverterSettings settings,
		ref StringBuilder sb)
	{
		if (settings.IgnoreSensitiveInformation && header.Key.EqualsAny(SensitiveHeaders)) return;

		sb.Append(HeaderFlag);
		sb.AppendSingleQuote();
		sb.Append(header.Key + ':');
		sb.AppendWhitespace();

		if (header.Value.Count() > 1)
			foreach (string value in header.Value)
				sb.Append(value + ',');
		else
			sb.Append(header.Value.First());

		sb.AppendSingleQuote();
		sb.AppendWhitespace();
	}
}