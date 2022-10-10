namespace HttpRequestToCurl.Exceptions;

public class UnsupportedContentTypeException : Exception
{
	public UnsupportedContentTypeException() : base("Unsupported HttpRequestMessage Content Type. Only StringContent is supported") {}
}