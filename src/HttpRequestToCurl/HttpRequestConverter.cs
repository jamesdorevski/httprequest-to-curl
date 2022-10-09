using System.Text;
using HttpRequestToCurl.Handlers;
using HttpRequestToCurl.Models;

namespace HttpRequestToCurl;

public static class HttpRequestConverter
{
	private static readonly IEnumerable<IHandler> Handlers = AppDomain.CurrentDomain.GetAssemblies()
		.SelectMany(x => x.GetTypes())
		.Where(x => typeof(IHandler).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
		.Select(x => Activator.CreateInstance(x) as IHandler)!;

	public static string ConvertToCurl(HttpRequestMessage request, HttpRequestConverterSettings? settings = null)
	{
		var sb = new StringBuilder();
		settings ??= new HttpRequestConverterSettings();

		foreach (var handler in Handlers.Where(h => h.CanHandle(request)))
			handler.Handle(request, settings, ref sb);

		return sb.ToString();
	}
}