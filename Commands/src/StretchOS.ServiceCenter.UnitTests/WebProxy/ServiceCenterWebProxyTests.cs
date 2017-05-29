using Moq;
using OpenQA.Selenium;
using StretchOS.Core.SystemIO;
using StretchOS.Selenium.WebDriver;
using StretchOS.ServiceCenter.Domain;
using StretchOS.ServiceCenter.WebProxy;
using System;
using Xunit;

namespace StretchOS.ServiceCenter.UnitTests.WebProxy
{
	public class ServiceCenterWebProxyTests
	{
		[Fact]
		public void DownloadErrorLog_DownloadFails_ExceptionIsThrown()
		{
			var serviceCenterProxy =
				new ServiceCenterWebProxy(
					Mock.Of<IOSWebDriver>(
						d => d.GoTo(It.IsAny<string>()) == d 
							&& d.Fill(It.IsAny<By>(), It.IsAny<string>()) == d),
					Mock.Of<ISystemIOWrapper>(
						w => w.FileExists(It.IsAny<string>()) == false),
					0 /* timeout */);

			Exception ex = Record.Exception(() => serviceCenterProxy.DownloadErrorLog(new SearchSettings()));

			Assert.NotNull(ex);
			Assert.True(ex.Message.Contains("download failed"));
		}
	}
}
