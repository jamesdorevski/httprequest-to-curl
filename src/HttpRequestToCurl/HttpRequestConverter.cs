using System.Text;
using HttpRequestToCurl.Extensions;

namespace HttpRequestToCurl;

public static class HttpRequestConverter
{
    private const string CurlCommand = "curl ";
    private const string MethodFlag = "--request ";
    private const string HeaderFlag = "--header ";
    private const string DataFlag = "--data ";
    
    public static string ConvertToCurl(HttpRequestMessage request)
    {
        var sb = new StringBuilder();

        sb.Append(CurlCommand);
        sb.Append(MethodFlag);
        sb.Append(request.Method);
        sb.AddWhitespace();

        if (request.Headers.Any())
        {
            foreach (var header in request.Headers)
            {
                sb.Append(HeaderToString(header));
            }    
        }

        if (request.Content != null)
            sb.Append(ContentToString(request.Content));

        sb.AddWhitespace();
        sb.Append(request.RequestUri);

        return sb.ToString();
    }

    private static string HeaderToString(KeyValuePair<string, IEnumerable<string>> header)
    {
        var sb = new StringBuilder();

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
