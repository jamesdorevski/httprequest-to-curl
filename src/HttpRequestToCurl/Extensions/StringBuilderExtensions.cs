namespace HttpRequestToCurl.Extensions;

internal static class StringBuilderExtensions
{
	public static void AppendWhitespace(this StringBuilder sb) => sb.Append(' ');
	public static void AppendSingleQuote(this StringBuilder sb) => sb.Append('\'');
	public static void AppendDoubleQuote(this StringBuilder sb) => sb.Append('"');
}