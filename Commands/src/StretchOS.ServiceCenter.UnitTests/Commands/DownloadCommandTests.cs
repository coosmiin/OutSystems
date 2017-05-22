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
		public void Validate_KnownParam_IsValid()
		{
			var writer = new StringWriter();

			var command = new DownloadCommand(Mock.Of<IServiceCenterWebProxy>(), "--error-log");
			var result = command.Validate();

			Assert.True(result.IsValid);
		}

		[Fact]
		public void Validate_UnknownParam_IsNotValid()
		{
			var writer = new StringWriter();

			string unknownParam = "unknown-param";

			var command = new DownloadCommand(Mock.Of<IServiceCenterWebProxy>(), unknownParam, "sss");
			var result = command.Validate();

			Assert.False(result.IsValid);
		}

		[Fact]
		public void Validate_MultipleUnknownParams_OnlyFirstUnknownParamMessageWrittenToOutput()
		{
			// TODO: (re)write this test?
			var writer = new StringWriter();

			string unknownParam = "unknown-param";

			var command = new DownloadCommand(Mock.Of<IServiceCenterWebProxy>(), unknownParam, "sss");

			Assert.Contains($"Unknown parameter: {unknownParam}\n", writer.ToString());
		}

		[Fact]
		public void Execute_InnerDownloadErrorLogMethodIsCalled()
		{
			var webProxyMock = new Mock<IServiceCenterWebProxy>();

			var command = new DownloadCommand(webProxyMock.Object, "--error");
			command.Execute();

			webProxyMock.Verify(w => w.DownloadErrorLog(It.IsAny<SearchSettings>()), Times.Once);
		}
	}
}
