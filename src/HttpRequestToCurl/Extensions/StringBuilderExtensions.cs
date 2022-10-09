using System.Text;

namespace HttpRequestToCurl.Extensions;

internal static class StringBuilderExtensions
{
	public static void AppendWhitespace(this StringBuilder sb)
	{
		sb.Append(' ');
	}
}