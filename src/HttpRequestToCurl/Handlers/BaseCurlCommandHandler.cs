using System.Text;
using HttpRequestToCurl.Extensions;
using HttpRequestToCurl.Models;

namespace HttpRequestToCurl.Handlers;

internal class BaseCurlCommandHandler : IHandler
{
	private const string CurlCommand = "curl ";
	private const string MethodFlag = "--request ";
	
	public bool CanHandle(HttpRequestMessage request) => true;
	
	public string Handle(HttpRequestMessage request, HttpRequestConverterSettings? converterSettings)
	{
		var sb = new StringBuilder();

		sb.Append(CurlCommand);
		sb.Append(MethodFlag);
		sb.Append(request.Method);
		sb.AppendWhitespace();

		sb.Append(request.RequestUri);
		sb.AppendWhitespace();

		return sb.ToString();
	}
}