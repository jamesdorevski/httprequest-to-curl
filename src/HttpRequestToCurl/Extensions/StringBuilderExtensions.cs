using System.Text;

namespace HttpRequestToCurl.Extensions;

internal static class StringBuilderExtensions
{
	internal static void AddWhitespace(this StringBuilder sb)
	{
		sb.Append(' ');
	}
}