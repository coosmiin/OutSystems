using OpenQA.Selenium;
using Moq;
using Xunit;

namespace StretchOS.Selenium.UnitTests
{
	public class OSWebDriverTests
	{
		[Fact]
		public void GoTo_CallsNavigateOnInnerDriver()
		{
			var navigateMock = new Mock<INavigation>();

			var seleniumWebDriverStub =
				Mock.Of<IWebDriver>(m => m.Navigate() == navigateMock.Object);

			var osWebDriver = new OSWebDriver(/*seleniumWebDriverStub*/);
			osWebDriver.GoTo("http://www.google.com");

			navigateMock.Verify(m => m.GoToUrl(It.IsAny<string>()), Times.Once());
		}
	}
}
