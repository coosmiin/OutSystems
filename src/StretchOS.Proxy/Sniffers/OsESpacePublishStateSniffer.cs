using StretchOS.Proxy.Events;
using StretchOS.Proxy.Parsers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Titanium.Web.Proxy.EventArguments;

namespace StretchOS.Proxy.Sniffers
{
	public class OsESpacePublishStateSniffer : OsESpaceSnifferBase
	{
		private const string CHECK_PUBLISH_ASYNC_METHOD_NAME = "CheckPublishAsync";

		private readonly IRequestParser<OsCheckESpacePublishStateRequest> _requestParser;
		private readonly IRequestParser<OsCheckESpacePublishStateResponse> _responseParser;
		private readonly IOsEventHub _osEventHub;

		private readonly ConcurrentDictionary<Guid, int> _requestVersionIds = new ConcurrentDictionary<Guid, int>();

		public OsESpacePublishStateSniffer(
			IRequestParser<OsCheckESpacePublishStateRequest> requestParser,
			IRequestParser<OsCheckESpacePublishStateResponse> responseParser, IOsEventHub osEventHub)
			: base(CHECK_PUBLISH_ASYNC_METHOD_NAME)
		{
			_requestParser = requestParser;
			_responseParser = responseParser;
			_osEventHub = osEventHub;
		}

		protected override void HandleRequestContent(SessionEventArgs e, string requestBody)
		{
			OsCheckESpacePublishStateRequest checkRequest = _requestParser.Parse(requestBody);

			_requestVersionIds.TryAdd(e.Id, checkRequest.VersionId);
		}

		protected override void HandleResponseContent(SessionEventArgs e, string responseBody)
		{
			OsCheckESpacePublishStateResponse checkResponse = _responseParser.Parse(responseBody);

			if (checkResponse.Status == OsPublishStatus.Deployed)
			{
				_osEventHub.Publish(
					new OsPublishCompletedEvent
					{
						ESpaceName = checkResponse.ESpaceName,
						ConsumerNames = checkResponse.ConsumerNames,
						VersionId = _requestVersionIds[e.Id]
					});
			}
		}
	}
}
