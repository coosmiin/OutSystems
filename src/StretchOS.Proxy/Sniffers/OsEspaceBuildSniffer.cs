using StretchOS.Proxy.Domain;
using StretchOS.Proxy.Events;
using StretchOS.Proxy.Parsers;
using StretchOS.Proxy.ServiceProxy;
using System;
using System.Threading.Tasks;
using Titanium.Web.Proxy.EventArguments;

namespace StretchOS.Proxy.Sniffers
{
	public class OsESpaceBuildSniffer : IWebSniffer
	{
		private const string SOAP_HEADER_NAME = "SOAPAction";
		private const string START1CP_METHOD_NAME = "Start1CP";

		// TODO: configurable url matcher for each sniffer
		private readonly string _baseUrl = "https://croman.outsystemscloud.com"; // "http://172.28.6.216";

		private readonly IResponseParser<OsESpaceBuildResponse> _responseParser;
		private readonly IServiceCenterProxy _serviceCenterProxy;
		private readonly IOsEventHub _osEventHub;

		public OsESpaceBuildSniffer(
			IServiceCenterProxy serviceCenterProxy, IResponseParser<OsESpaceBuildResponse> responseParser, 
			IOsEventHub osEventHub)
		{
			_serviceCenterProxy = serviceCenterProxy;
			_responseParser = responseParser;
			_osEventHub = osEventHub;
		}

		public Task OnBeforeRequest(SessionEventArgs e)
		{
			throw new NotImplementedException();
		}

		public async Task OnBeforeResponse(SessionEventArgs e)
		{
			if (!e.WebSession.Request.Url.StartsWith(_baseUrl))
				return;

			if (!e.WebSession.Request.RequestHeaders.ContainsKey(SOAP_HEADER_NAME)
				|| !e.WebSession.Request.RequestHeaders[SOAP_HEADER_NAME].Value.Contains(START1CP_METHOD_NAME))
				return;

			string responseBody = string.Empty;
			try
			{
				responseBody = await e.GetResponseBodyAsString();

				OsESpaceBuildResponse response = _responseParser.Parse(responseBody);

				if (!response.HasErrors)
					_osEventHub.Publish(new OsBuildStartedEvent { ESpaceId = response.ESpaceId });
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
