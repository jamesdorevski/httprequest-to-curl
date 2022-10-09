using System.Text;
using HttpRequestToCurl.Extensions;
using HttpRequestToCurl.Handlers;
using HttpRequestToCurl.Models;

namespace HttpRequestToCurl;

public static class HttpRequestConverter
{
	// TODO - inject this 
	private static readonly IEnumerable<IHandler> Handlers = new IHandler[]
	{
		new CommandHandler(),
		new HeadersHandler(),
		new ContentHandler(),
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
		
		return sb.ToString();
	}
}