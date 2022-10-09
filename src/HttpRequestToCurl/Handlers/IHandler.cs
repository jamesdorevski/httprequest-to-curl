using HttpRequestToCurl.Models;

namespace HttpRequestToCurl.Handlers;

internal interface IHandler
{
	public bool CanHandle(HttpRequestMessage request);
	public string Handle(HttpRequestMessage request, HttpRequestConverterSettings? converterSettings = default);
}