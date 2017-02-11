using NUnit.Framework;
using StretchOS.Proxy.OsEvents;
using StretchOS.Proxy.Parsers;
using System.IO;

namespace StretchOS.Proxy.UnitTests.Parsers
{
	[TestFixture]
	public class OsCheckESpacePublishStateResponseParserTests
	{
		IRequestParser<OsCheckESpacePublishStateResponse> _parser = new OsCheckESpacePublishStateResponseParser();

		[Test]
		public void Parse_NoRelevantResult_StatusIsStarted()
		{
			var result = _parser.Parse(File.ReadAllText(Utils.GetTestResourcePath(@"Parsers\Data\CheckPublishAsyncResponse_OneIrrelevantMessage.xml")));

			Assert.IsTrue(result.Status == OsPublishStatus.Started);
		}

		[Test]
		public void Parse_CompileFinished_StatusIsCompiled()
		{
			var result = _parser.Parse(File.ReadAllText(Utils.GetTestResourcePath(@"Parsers\Data\CheckPublishAsyncResponse_CompileFinished.xml")));

			Assert.IsTrue(result.Status == OsPublishStatus.Compiled);
		}

		[Test]
		public void Parse_CompileFinished_ESpaceNameIsParsed()
		{
			var result = _parser.Parse(File.ReadAllText(Utils.GetTestResourcePath(@"Parsers\Data\CheckPublishAsyncResponse_CompileFinished.xml")));

			Assert.IsTrue(result.ESpaceName == "CS_B");
		}

		[Test]
		public void Parse_CompileFinished_StatusIsDeployed()
		{
			var result = _parser.Parse(File.ReadAllText(Utils.GetTestResourcePath(@"Parsers\Data\CheckPublishAsyncResponse_DeployFinished.xml")));

			Assert.IsTrue(result.Status == OsPublishStatus.Deployed);
		}

		[Test]
		public void Parse_CompileFinished_ConsumersAreParsed()
		{
			var result = _parser.Parse(File.ReadAllText(Utils.GetTestResourcePath(@"Parsers\Data\CheckPublishAsyncResponse_DeployFinished.xml")));

			CollectionAssert.AreEquivalent(new[] { "B", "CS_A1", "CW_B" }, result.ConsumerNames);
		}

		[Test]
		public void Parse_CompileFinished_OutdatedProducersAreParsed()
		{
			var result = _parser.Parse(File.ReadAllText(Utils.GetTestResourcePath(@"Parsers\Data\CheckPublishAsyncResponse_OutdatedProducer.xml")));

			CollectionAssert.AreEquivalent(new[] { "CW_A" }, result.OutdatedProducerNames);
		}
	}
}