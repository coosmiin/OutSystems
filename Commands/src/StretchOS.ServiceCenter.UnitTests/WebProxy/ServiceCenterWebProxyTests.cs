using Moq;
using OpenQA.Selenium;
using StretchOS.Core.SystemIO;
using StretchOS.Selenium.WebDriver;
using StretchOS.ServiceCenter.Domain;
using StretchOS.ServiceCenter.WebProxy;
using Xunit;

namespace StretchOS.ServiceCenter.UnitTests.WebProxy
{
	public class ServiceCenterWebProxyTests
	{
		[Fact]
		public void DownloadErrorLog_DownloadFails_MethodExitsCorrectly()
		{
			var serviceCenterProxy =
				new ServiceCenterWebProxy(
					Mock.Of<IOSWebDriver>(
						d => d.GoTo(It.IsAny<string>()) == d 
							&& d.Fill(It.IsAny<By>(), It.IsAny<string>()) == d),
					Mock.Of<ISystemIOWrapper>(
						w => w.FileExists(It.IsAny<string>()) == false));

			serviceCenterProxy.DownloadErrorLog(new SearchSettings());
		}
	}
}
