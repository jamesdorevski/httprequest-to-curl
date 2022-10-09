namespace HttpRequestToCurl.Extensions;

public static class StringExtensions
{
	public static bool EqualsAny(this string target, params string[] comparisons)
	{
		return comparisons.Contains(target);
	}
}