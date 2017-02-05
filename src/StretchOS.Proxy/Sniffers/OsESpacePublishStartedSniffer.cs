using StretchOS.Proxy.Events;
using StretchOS.Proxy.Parsers;
using Titanium.Web.Proxy.EventArguments;

namespace StretchOS.Proxy.Sniffers
{
	public class OsESpacePublishStartedSniffer : OsESpaceSnifferBase
	{
		private const string START1CP_METHOD_NAME = "Start1CP";

		private readonly IRequestParser<OsESpacePublishStartedResponse> _responseParser;
		private readonly IOsEventHub _osEventHub;

		public OsESpacePublishStartedSniffer(
			IRequestParser<OsESpacePublishStartedResponse> responseParser, IOsEventHub osEventHub)
			: base(START1CP_METHOD_NAME)
		{
			_responseParser = responseParser;
			_osEventHub = osEventHub;
		}

		protected override void HandleResponseContent(SessionEventArgs e, string responseBody)
		{
			OsESpacePublishStartedResponse buildResponse = _responseParser.Parse(responseBody);

			if (!buildResponse.HasErrors)
				_osEventHub.Publish(
					new OsPublishStartedEvent
					{
						VersionId = buildResponse.VersionId,
						ESpaceId = buildResponse.ESpaceId
					});
		}
	}
}
