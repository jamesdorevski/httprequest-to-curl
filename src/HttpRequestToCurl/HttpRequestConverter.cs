﻿using System.Text;

namespace HttpRequestToCurl;

public static class HttpRequestConverter
{
    private const string CurlCommand = "curl ";
    private const string MethodFlag = "--request ";
    private const string HeaderFlag = "--header ";
    
    public static string ConvertToCurl(HttpRequestMessage request)
    {
        var sb = new StringBuilder();

        sb.Append(CurlCommand);
        sb.Append(MethodFlag);
        sb.Append(request.Method.ToString() + ' ');

        if (request.Headers.Any())
        {
            foreach (var header in request.Headers)
            {
                sb.Append(HeaderToString(header));
            }    
        }

        sb.Append(' ' + request.RequestUri!.ToString());

        return sb.ToString();
    }

    private static string HeaderToString(KeyValuePair<string, IEnumerable<string>> header)
    {
        var sb = new StringBuilder();

        sb.Append(HeaderFlag);
        sb.Append('"');
        sb.Append(header.Key + ": ");

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
}