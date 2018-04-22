using Moq;
using StretchOS.ServiceCenter.Commands;
using StretchOS.ServiceCenter.Domain;
using StretchOS.ServiceCenter.WebProxy;
using Xunit;

namespace StretchOS.ServiceCenter.UnitTests.Commands
{
	public class DownloadCommandTests
	{
		[Fact]
		public void Execute_InnerDownloadErrorLogMethodIsCalled()
		{
			var webProxyMock = new Mock<IServiceCenterWebProxy>();

			var command = new DownloadCommand(webProxyMock.Object, "--error-log");
			command.Execute();

			webProxyMock.Verify(w => w.DownloadErrorLog(It.IsAny<SearchSettings>()), Times.Once);
		}

		[Fact]
		public void Execute_WithNoStartDate_InnerDownloadErrorLogMethodIsCalledWithEmptyParams()
		{
			var webProxyMock = new Mock<IServiceCenterWebProxy>();

			var command = new DownloadCommand(webProxyMock.Object, "--error-log");
			command.Execute();

			webProxyMock.Verify(
				w => w.DownloadErrorLog(
					It.Is<SearchSettings>(s => s.Start == string.Empty && s.End == string.Empty)), 
				Times.Once);
		}

		[Fact]
		public void Execute_WithNoEndDate_InnerDownloadErrorLogMethodIsCalledWithEmptyParamForEndDate()
		{
			var webProxyMock = new Mock<IServiceCenterWebProxy>();

			var command = new DownloadCommand(webProxyMock.Object, "--error-log", "2017-12-01 00:00:00");
			command.Execute();

			webProxyMock.Verify(
				w => w.DownloadErrorLog(
					It.Is<SearchSettings>(s => s.Start != string.Empty && s.End == string.Empty)),
				Times.Once);
		}

		[Fact]
		public void Execute_WithStartAndEndDate_InnerDownloadErrorLogMethodIsCalledWithNonEmptyParams()
		{
			var webProxyMock = new Mock<IServiceCenterWebProxy>();

			var command = new DownloadCommand(webProxyMock.Object, "--error-log", "2017-12-01 00:00:00", "2017-12-30 00:00:00");
			command.Execute();

			webProxyMock.Verify(
				w => w.DownloadErrorLog(
					It.Is<SearchSettings>(s => s.Start != string.Empty && s.End != string.Empty)),
				Times.Once);
		}

		[Fact]
		public void Validate_KnownParam_IsValid()
		{
			var command = new DownloadCommand(Mock.Of<IServiceCenterWebProxy>(), "--error-log");
			var result = command.Validate();

			Assert.True(result.IsValid);
		}

		[Fact]
		public void Validate_MandatoryParamIsMissing_RequiredParamValidationError()
		{
			var command = new DownloadCommand(Mock.Of<IServiceCenterWebProxy>());
			var result = command.Validate();

			Assert.False(result.IsValid);
			Assert.Equal("[target] is required. Possible values: [--error-log].", result.ValidationText);
		}

		[Fact]
		public void Validate_TooManyParameters_IsValidButWithValidationMessageForFirstUnknownParam()
		{
			string unknownParam = "unknown-param";

			var command = 
				new DownloadCommand(
					Mock.Of<IServiceCenterWebProxy>(), "--error-log", "2017-12-01 00:00:00", "2017-12-01 00:00:00", unknownParam, "some other param");

			var result = command.Validate();

			Assert.True(result.IsValid);
			Assert.Contains($"unknown parameter: {unknownParam}", result.ValidationText);
		}

		[Fact]
		public void Validate_InvalidStartDate_StartDateValidationError()
		{
			var command = new DownloadCommand(Mock.Of<IServiceCenterWebProxy>(), "--error-log", "2017-13-01 00:00:00");
			var result = command.Validate();

			Assert.False(result.IsValid);
			Assert.Equal("[start-date] is invalid", result.ValidationText);
		}

		[Fact]
		public void Validate_ValidStartDate_IsValid()
		{
			var command = new DownloadCommand(Mock.Of<IServiceCenterWebProxy>(), "--error-log", "2017-12-01 00:00:00");
			var result = command.Validate();

			Assert.True(result.IsValid);
		}


		[Fact]
		public void Validate_ValidStartDateIn24hFormat_IsValid()
		{
			var command = new DownloadCommand(Mock.Of<IServiceCenterWebProxy>(), "--error-log", "2017-05-01 23:59:59");
			var result = command.Validate();

			Assert.True(result.IsValid);
		}

		[Fact]
		public void Validate_ValidStartDate_InvalidEndDate_EndDateValidationError()
		{
			var command = new DownloadCommand(Mock.Of<IServiceCenterWebProxy>(), "--error-log", "2017-12-01 00:00:00", "2017-13-01 00:00:00");
			var result = command.Validate();

			Assert.False(result.IsValid);
			Assert.Equal("[end-date] is invalid", result.ValidationText);
		}

		[Fact]
		public void Validate_ValidStartAndEndDate_IsValid()
		{
			var command = new DownloadCommand(Mock.Of<IServiceCenterWebProxy>(), "--error-log", "2017-12-01 00:00:00", "2017-12-01 00:00:00");
			var result = command.Validate();

			Assert.True(result.IsValid);
		}
	}
}
