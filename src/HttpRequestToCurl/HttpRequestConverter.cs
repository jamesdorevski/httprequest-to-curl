using System.Text;
using HttpRequestToCurl.Extensions;
using HttpRequestToCurl.Handlers;
using HttpRequestToCurl.Models;

namespace HttpRequestToCurl;

public static class HttpRequestConverter
{
	private const string DataFlag = "--data ";

	// TODO - inject this 
	private static readonly IEnumerable<IHandler> Handlers = new IHandler[]
	{
		new CommandHandler(),
		new HeadersHandler(),
	};
	
	public static string ConvertToCurl(HttpRequestMessage request, HttpRequestConverterSettings? settings = null)
	{
		var sb = new StringBuilder();
		settings ??= new HttpRequestConverterSettings();
		
		foreach (var handler in Handlers.Where(h => h.CanHandle(request)))
		{
			string result = handler.Handle(request, settings);
			sb.Append(result);
		}
		
		if (request.Content != null) sb.Append(ContentToString(request.Content));

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