using StretchOS.Proxy.OutSystemsService;

namespace StretchOS.Proxy.Parsers
{
	public class OsESpacePublishStartedResponseParser : OsContentParserBase<Start1CPResponse, OsESpacePublishStartedResponse>
	{
		protected override OsESpacePublishStartedResponse Build(Start1CPResponse content)
		{
			return new OsESpacePublishStartedResponse
			{
				HasErrors = content.result.HasErrors,
				ESpaceId = content.result.ESpaceId,
				VersionId = content.result.VersionId
			};
		}
	}
}
