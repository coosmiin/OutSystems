using StretchOS.Proxy.OutSystemsService;
using System.Xml.Serialization;

namespace StretchOS.Proxy.Parsers
{
	public class OsCheckESpacePublishStateRequestParser 
		: OsContentParserBase<CheckPublishAsyncRequest, OsCheckESpacePublishStateRequest>
	{
		public OsCheckESpacePublishStateRequestParser()
			: base(new XmlRootAttribute("CheckPublishAsync"))
		{
		}

		protected override OsCheckESpacePublishStateRequest Build(CheckPublishAsyncRequest content)
		{
			return new OsCheckESpacePublishStateRequest { VersionId = content.versionid };
		}
	}
}
