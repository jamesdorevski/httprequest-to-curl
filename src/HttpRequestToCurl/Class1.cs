using System.Text;

namespace HttpRequestToCurl;

public static class HttpConverter
{
    public const string CurlCommand = "curl";
    public const string MethodFlag = "--request";
    public const string HeaderFlag = "--header";
    
    public static string ConvertToCurl(HttpRequestMessage request)
    {
        var sb = new StringBuilder();

        sb.Append(CurlCommand);
        sb.Append(MethodFlag);
        sb.Append(request.Method);

        if (request.Headers.Any())
        {
            foreach (var header in request.Headers)
            {
                sb.Append(HeaderToString(header));
            }    
        }

        sb.Append(request.RequestUri);

        return sb.ToString();
    }

    private static string HeaderToString(KeyValuePair<string, IEnumerable<string>> keyValuePair)
    {
        
    }
}
