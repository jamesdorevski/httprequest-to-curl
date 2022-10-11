using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using HttpRequestToCurl.Exceptions;
using HttpRequestToCurl.Extensions;
using HttpRequestToCurl.Models;

namespace HttpRequestToCurl.Handlers
{
	// Only support StringContent ATM, throw exception if otherwise
	internal class ContentHandler : IHandler
	{
		private const string DataFlag = "--data ";

		public bool CanHandle(HttpRequestMessage request) => request.Content != null;

		public void Handle(HttpRequestMessage request, HttpRequestConverterSettings settings, ref StringBuilder sb)
		{
			if (request.Content.GetType().ToString() != "System.Net.Http.StringContent")
				throw new UnsupportedContentTypeException();
		
			if (request.Content?.Headers != null)
				ContentHeadersToString(request.Content.Headers, ref sb);
		
			ContentToString(request.Content!, ref sb);
		}

		// TODO: map available content headers and strings to dictionary, enumerate over
		private static void ContentHeadersToString(HttpContentHeaders contentHeaders, ref StringBuilder sb)
		{
			sb.Append(Constants.HeaderFlag);

			sb.AppendSingleQuote();
			sb.Append("Content-Type: " + contentHeaders.ContentType?.MediaType);
			sb.AppendSingleQuote();

			sb.AppendWhitespace();
		}

		private static void ContentToString(HttpContent content, ref StringBuilder sb)
		{
			string body = content.ReadAsStringAsync().Result;

			sb.Append(DataFlag);

			sb.AppendSingleQuote();
			sb.AppendDoubleQuote();
			sb.Append(body);
			sb.AppendDoubleQuote();
			sb.AppendSingleQuote();

			sb.AppendWhitespace();
		}
	}
}