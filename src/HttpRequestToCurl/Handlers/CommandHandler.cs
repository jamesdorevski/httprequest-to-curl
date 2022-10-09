using System.Text;
using HttpRequestToCurl.Extensions;
using HttpRequestToCurl.Models;

namespace HttpRequestToCurl.Handlers;

internal class CommandHandler : IHandler
{
	private const string CurlCommand = "curl ";
	private const string InsecureFlag = "--insecure ";
	private const string MethodFlag = "--request ";
	
	public bool CanHandle(HttpRequestMessage request) => true;
	
	public string Handle(HttpRequestMessage request, HttpRequestConverterSettings settings)
	{
		var sb = new StringBuilder();

		sb.Append(CurlCommand);
		
		if (settings.AllowInsecureConnection) sb.Append(InsecureFlag);
		
		sb.Append(MethodFlag);
		sb.Append(request.Method);
		sb.AppendWhitespace();

		sb.Append(request.RequestUri);
		sb.AppendWhitespace();

		return sb.ToString();
	}
}