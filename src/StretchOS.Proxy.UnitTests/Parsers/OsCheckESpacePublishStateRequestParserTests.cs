using NUnit.Framework;
using StretchOS.Proxy.Parsers;
using System.IO;

namespace StretchOS.Proxy.UnitTests.Parsers
{
	[TestFixture]
	public class OsCheckESpacePublishStateRequestParserTests
	{
		IRequestParser<OsCheckESpacePublishStateRequest> _parser = new OsCheckESpacePublishStateRequestParser();

		[Test]
		public void Parse()
		{
			var result = _parser.Parse(File.ReadAllText(Utils.GetTestResourcePath(@"Parsers\Data\CheckPublishAsyncRequest.xml")));
		}
	}
}