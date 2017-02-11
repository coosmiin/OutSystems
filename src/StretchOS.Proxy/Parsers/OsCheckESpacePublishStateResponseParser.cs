using StretchOS.Proxy.OsEvents;
using StretchOS.Proxy.OutSystemsService;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace StretchOS.Proxy.Parsers
{
	public class OsCheckESpacePublishStateResponseParser 
		: OsContentParserBase<CheckPublishAsyncResponse, OsCheckESpacePublishStateResponse>
	{
		private static Regex _simpleQuoteWordExtractorRegex = new Regex("'(\\w+)'", RegexOptions.Compiled);

		protected override OsCheckESpacePublishStateResponse Build(CheckPublishAsyncResponse content)
		{
			if (content.messages.Length == 0)
				throw new Exception("The 'CheckPublishAsyncResponse' returned no messages");

			var response = new OsCheckESpacePublishStateResponse();

			foreach (HEMessage heMessage in content.messages)
			{
				if (heMessage.Type == "ok")
				{
					response.Status = OsPublishStatus.Started;
					continue;
				}

				if (heMessage.Type == "Info")
				{
					if (heMessage.Id == "CompileFinished")
					{
						response.Status = OsPublishStatus.Compiled;
						response.ESpaceName = _simpleQuoteWordExtractorRegex.Match(heMessage.Detail).Groups[1].Value;
						continue;
					}

					if (heMessage.Id == "DeployFinished")
					{
						response.Status = OsPublishStatus.Deployed;
						continue;
					}
					throw new Exception($"Unknown 'CheckPublishAsyncResponse' message: '{heMessage.Id}' with type '{heMessage.Type}'");
				}

				if (heMessage.Type == "Warning")
				{
					if (heMessage.Id == "ConsumerOutdated")
					{
						response.ConsumerNames.Add(_simpleQuoteWordExtractorRegex.Match(heMessage.Detail).Groups[1].Value);
						continue;
					}

					if (heMessage.Id == "ReferenceOutdated")
					{
						response.OutdatedProducerNames.Add(_simpleQuoteWordExtractorRegex.Match(heMessage.Detail).Groups[1].Value);
						continue;
					}
					throw new Exception($"Unknown 'CheckPublishAsyncResponse' message: '{heMessage.Id}' with type '{heMessage.Type}'");
				}

				throw new Exception($"Unknown 'CheckPublishAsyncResponse' message type: '{heMessage.Type}'");
			}

			return response;
		}
	}
}
