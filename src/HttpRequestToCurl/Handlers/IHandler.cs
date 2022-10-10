using HttpRequestToCurl.Models;

namespace HttpRequestToCurl.Handlers;

internal interface IHandler
{
	public bool CanHandle(HttpRequestMessage request);
	public void Handle(HttpRequestMessage request, HttpRequestConverterSettings settings, ref StringBuilder sb);
}