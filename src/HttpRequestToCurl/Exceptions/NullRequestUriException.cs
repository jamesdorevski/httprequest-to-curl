namespace HttpRequestToCurl.Exceptions;

internal class NullRequestUriException : Exception
{
	public NullRequestUriException() : base("RequestUri cannot be null or empty") {}
}