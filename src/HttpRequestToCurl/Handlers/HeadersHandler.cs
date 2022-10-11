using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using HttpRequestToCurl.Extensions;
using HttpRequestToCurl.Models;

namespace HttpRequestToCurl.Handlers
{
	internal class HeadersHandler : IHandler
	{
		private static readonly string[] SensitiveHeaders =
		{
			"Authorization"
		};

		public bool CanHandle(HttpRequestMessage request) => request.Headers.Any();

		public void Handle(HttpRequestMessage request, HttpRequestConverterSettings settings, ref StringBuilder sb)
		{
			foreach (var header in request.Headers)
				HeaderToString(header, settings, ref sb);
		}

		private static void HeaderToString(
			KeyValuePair<string, IEnumerable<string>> header,
			HttpRequestConverterSettings settings,
			ref StringBuilder sb)
		{
			if (settings.IgnoreSensitiveInformation && header.Key.EqualsAny(SensitiveHeaders)) return;

			sb.Append(Constants.HeaderFlag);
			sb.AppendSingleQuote();
			sb.Append(header.Key + ':');
			sb.AppendWhitespace();

			if (header.Value.Count() > 1)
				foreach (var value in header.Value)
					sb.Append(value + ',');
			else
				sb.Append(header.Value.First());

			sb.AppendSingleQuote();
			sb.AppendWhitespace();
		}
	}
}