using HttpRequestToCurl.Handlers;
using HttpRequestToCurl.Models;

namespace HttpRequestToCurl;

public static class HttpRequestConverter
{
	private static readonly IEnumerable<IHandler> Handlers = new IHandler[]
	{
		new CommandHandler(),
		new HeadersHandler(),
		new ContentHandler()
	};

	public static string ConvertToCurl(HttpRequestMessage request, HttpRequestConverterSettings? settings = null)
	{
		var sb = new StringBuilder();
		settings ??= new HttpRequestConverterSettings();

		foreach (var handler in Handlers.Where(h => h.CanHandle(request)))
			handler.Handle(request, settings, ref sb);

		return sb.ToString();
	}
}