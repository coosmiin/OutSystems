using NUnit.Framework;
using StretchOS.Proxy.Parsers;
using System.IO;

namespace StretchOS.Proxy.UnitTests.Parsers
{
	[TestFixture]
	public class OsESpacePublishStartedResponseParserTests
	{
		[Test]
		public void Parse()
		{
			var parser = new OsESpacePublishStartedResponseParser();

			parser.Parse(File.ReadAllText(Utils.GetTestResourcePath(@"Parsers\Data\Start1CPResponse.xml")));
		}
	}
}
