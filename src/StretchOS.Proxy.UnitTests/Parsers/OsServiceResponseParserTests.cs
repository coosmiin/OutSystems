using NUnit.Framework;
using StretchOS.Proxy.Parsers;
using System.IO;

namespace StretchOS.Proxy.UnitTests.Parsers
{
	[TestFixture]
	public class OsServiceResponseParserTests
	{
		[Test]
		public void Parse_WithOutsystemsNamespaceInRoot()
		{
			var parser = new OsEspaceBuildResponseParser();

			parser.Parse(File.ReadAllText(Utils.GetTestResourcePath(@"Parsers\Data\Start1CPResponse.xml")));
		}
	}
}
