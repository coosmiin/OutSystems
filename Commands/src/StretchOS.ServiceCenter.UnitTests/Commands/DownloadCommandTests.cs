using Moq;
using StretchOS.ServiceCenter.Commands;
using StretchOS.ServiceCenter.Domain;
using StretchOS.ServiceCenter.WebProxy;
using System.IO;
using Xunit;

namespace StretchOS.ServiceCenter.UnitTests.Commands
{
	public class DownloadCommandTests
	{
		[Fact]
		public void Validate_KnownParam_NoMessageWrittenToOutput()
		{
			var writer = new StringWriter();

			var command = new DownloadCommand(writer, Mock.Of<IServiceCenterWebProxy>(), "--error");

			Assert.Equal(string.Empty, writer.ToString());
		}

		[Fact]
		public void Validate_UnknownParam_UnknownParamMessageWrittenToOutput()
		{
			var writer = new StringWriter();

			string unknownParam = "unknown-param";

			var command = new DownloadCommand(writer, Mock.Of<IServiceCenterWebProxy>(), unknownParam, "sss");

			Assert.Contains($"Unknown parameter: {unknownParam}\n", writer.ToString());
		}

		[Fact]
		public void Validate_MultipleUnknownParams_OnlyFirstUnknownParamMessageWrittenToOutput()
		{
			var writer = new StringWriter();

			string unknownParam = "unknown-param";

			var command = new DownloadCommand(writer, Mock.Of<IServiceCenterWebProxy>(), unknownParam, "sss");

			Assert.Contains($"Unknown parameter: {unknownParam}\n", writer.ToString());
		}

		[Fact]
		public void Execute_InnerDownloadErrorLogMethodIsCalled()
		{
			var writer = new StringWriter();

			var webProxyMock = new Mock<IServiceCenterWebProxy>();

			var command = new DownloadCommand(writer, webProxyMock.Object, "--error");
			command.Execute();

			webProxyMock.Verify(w => w.DownloadErrorLog(It.IsAny<SearchSettings>()), Times.Once);
		}
	}
}
