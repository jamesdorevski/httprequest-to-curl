using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using HttpRequestToCurl.Handlers;
using HttpRequestToCurl.Models;

namespace HttpRequestToCurl
{
	public static class HttpRequestConverter
    {
    	private static readonly IEnumerable<IHandler> Handlers = new IHandler[]
    	{
    		new CommandHandler(),
    		new HeadersHandler(),
    		new ContentHandler()
    	};
    
        /// <summary>
        /// Converts a HttpRequestMessageObject to its cURL equivalent.
        /// </summary>
        /// <param name="request">HttpRequestMessage to convert to cURL.</param>
        /// <param name="settings">Optional settings object.</param>
        /// <returns>cURL string equivalent of the input.</returns>
    	public static string ConvertToCurl(HttpRequestMessage request, HttpRequestConverterSettings? settings = null)
    	{
    		var sb = new StringBuilder();
    		settings ??= new HttpRequestConverterSettings();
    
    		foreach (var handler in Handlers.Where(h => h.CanHandle(request)))
    			handler.Handle(request, settings, ref sb);
    
    		return sb.ToString();
    	}
    }
}
