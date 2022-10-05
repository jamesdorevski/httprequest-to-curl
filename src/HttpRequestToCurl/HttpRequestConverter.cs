using System.Text;
using HttpRequestToCurl.Extensions;
using HttpRequestToCurl.Models;

namespace HttpRequestToCurl;

public static class HttpRequestConverter
{
    private const string CurlCommand = "curl ";
    private const string MethodFlag = "--request ";
    private const string HeaderFlag = "--header ";
    private const string DataFlag = "--data ";
    //TODO: to remove
    private static readonly string[] SensitiveHeaders = 
    {
        "Authorization"
    };

    public static string ConvertToCurl(HttpRequestMessage request, HttpRequestConverterSettings? settings = null)
    {
        var sb = new StringBuilder();
        settings ??= new HttpRequestConverterSettings();
        
        sb.Append(CurlCommand);
        sb.Append(MethodFlag);
        sb.Append(request.Method);
        sb.AddWhitespace();

        if (request.Headers.Any())
        {
            foreach (var header in request.Headers)
            {
                string? headerString = HeaderToString(header, settings);                
                if (headerString != null) sb.Append(headerString);
            }    
        }

        if (request.Content != null)
            sb.Append(ContentToString(request.Content));

        sb.AddWhitespace();
        sb.Append(request.RequestUri);

        return sb.ToString();
    }

    private static string? HeaderToString(KeyValuePair<string, IEnumerable<string>> header, HttpRequestConverterSettings settings)
    {
        var sb = new StringBuilder();
        
        if (settings.IgnoreSensitiveInformation && header.Key.EqualsAny(SensitiveHeaders))
            return null;

        sb.Append(HeaderFlag);
        sb.Append('"');
        sb.Append(header.Key + ":");
        sb.AddWhitespace();

        if (header.Value.Count() > 1)
        {
            foreach (string value in header.Value)
            {
                sb.Append(value + ',');
            }
        }
        else
        {
            sb.Append(header.Value.First());
        }

        sb.Append('"');

        return sb.ToString();
    }

    private static string ContentToString(HttpContent content)
    {
        string body = content.ReadAsStringAsync().Result;

        throw new NotImplementedException();
    }
}
