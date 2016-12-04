using StretchOS.Proxy.Domain;
using StretchOS.Proxy.Events;
using StretchOS.Proxy.Observer;
using StretchOS.Proxy.Parsers;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using Titanium.Web.Proxy.EventArguments;
using Titanium.Web.Proxy.Models;

namespace StretchOS.Proxy.Sniffers
{
	public class OsBuildRequestSniffer : IWebSniffer
	{
		private const string SOAP_HEADER_NAME = "SOAPAction";
		private const string CHECK_PUBLISH_METHOD_NAME = "CheckPublishAsync";

		// TODO: configurable url matcher for each sniffer
		private readonly string _baseUrl = "https://croman.outsystemscloud.com"; // "http://172.28.6.216";

		private readonly IRequestParser _requestParser;
		private readonly IEventNotifier _notifier;

		public OsBuildRequestSniffer(IEventNotifier notifier, IRequestParser requestParser)
		{
			_notifier = notifier;
			_requestParser = requestParser;
		}

		public async Task OnBeforeRequest(SessionEventArgs e)
		{
			if (e.WebSession.Request.Method != HttpMethod.Post.Method)
				return;

			if (!e.WebSession.Request.Url.StartsWith(_baseUrl))
				return;

			string requestBody = string.Empty;
			try
			{
				requestBody = await e.GetRequestBodyAsString();
			}
			catch (Exception ex)
			{
				// TODO: proper logger should be implemented
				Console.WriteLine(ex.Message);
			}

			OsRequest request = _requestParser.Parse(requestBody);

			_notifier.Notify(request);
		}

		public async Task OnBeforeResponse(SessionEventArgs e)
		{
			if (!e.WebSession.Request.Url.StartsWith(_baseUrl))
				return;

			if (!e.WebSession.Request.RequestHeaders.ContainsKey(SOAP_HEADER_NAME)
				|| !e.WebSession.Request.RequestHeaders[SOAP_HEADER_NAME].Value.Contains(CHECK_PUBLISH_METHOD_NAME))
				return;

			string responseBody = string.Empty;
			try
			{
				responseBody = await e.GetResponseBodyAsString();
			}
			catch (Exception ex)
			{
				// TODO: proper logger should be implemented
				Console.WriteLine(ex.Message);
			}

			Console.WriteLine(e.WebSession.Request.Url);
		}
	}
}
