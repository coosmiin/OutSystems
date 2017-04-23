using OpenQA.Selenium;
using Moq;
using Xunit;
using StretchOS.Selenium.WebDriver;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace StretchOS.Selenium.UnitTests.WebDriver
{
	public class OSWebDriverTests
	{
		[Fact]
		public void GoTo_GoToUrlOnInnerDriverIsCalledWithCorrectAbosultePath()
		{
			var navigateMock = new Mock<INavigation>();

			var seleniumWebDriverStub =
				Mock.Of<IWebDriver>(m => m.Navigate() == navigateMock.Object);

			var osWebDriver =
				new OSWebDriver(
					GetDefaultOSWebDriverSettings(), seleniumWebDriverStub);
			osWebDriver.GoTo("relativeUrl");

			navigateMock.Verify(m => m.GoToUrl(It.Is<string>(u => u == "http://someurl/relativeUrl")), Times.Once());
		}

		[Fact]
		public void GoTo_NonAuthenticated_PerformsLogin()
		{
			var osSettingsMock = new Mock<IOSWebDriverSettings>();
			osSettingsMock.SetupGet(s => s.BaseUrl).Returns("http://someurl");
			osSettingsMock.SetupGet(s => s.Login).Returns(d => { });
			osSettingsMock.SetupGet(s => s.CheckIsLogin).Returns(d => { return true; });

			var osWebDriver = 
				new OSWebDriver(
					osSettingsMock.Object, 
					Mock.Of<IWebDriver>(d => d.Navigate() == Mock.Of<INavigation>()));

			osWebDriver.GoTo("someUrl");

			osSettingsMock.Verify(s => s.CheckIsLogin, Times.Once);
			osSettingsMock.Verify(s => s.Login, Times.Once);
		}

		[Fact]
		public void GoTo_Authenticated_DoesNotPerformLogin()
		{
			var osSettingsMock = new Mock<IOSWebDriverSettings>();
			osSettingsMock.SetupGet(s => s.BaseUrl).Returns("http://someurl");
			osSettingsMock.SetupGet(s => s.Login).Returns(d => { });
			osSettingsMock.SetupGet(s => s.CheckIsLogin).Returns(d => { return false; });

			var osWebDriver =
				new OSWebDriver(
					osSettingsMock.Object,
					Mock.Of<IWebDriver>(d => d.Navigate() == Mock.Of<INavigation>()));

			osWebDriver.GoTo("someUrl");

			osSettingsMock.Verify(s => s.CheckIsLogin, Times.Once);
			osSettingsMock.Verify(s => s.Login, Times.Never);
		}

		[Fact]
		public void ElementExists_ElementsFound_NoValueProvided_ReturnsTrue()
		{
			var osWebDriver =
				new OSWebDriver(
					GetDefaultOSWebDriverSettings(),
					Mock.Of<IWebDriver>(
						d => d.FindElements(It.IsAny<By>()) == new ReadOnlyCollection<IWebElement>(new IWebElement[] { null, null })));

			var result = osWebDriver.ElementExists(By.Id(string.Empty));

			Assert.True(result);
		}

		[Fact]
		public void ElementExists_NoElementsFound_ReturnsFalse()
		{
			var osWebDriver =
				new OSWebDriver(
					GetDefaultOSWebDriverSettings(),
					Mock.Of<IWebDriver>(
						d => d.FindElements(It.IsAny<By>()) == new ReadOnlyCollection<IWebElement>(new IWebElement[] { })));

			var result = osWebDriver.ElementExists(By.Id(string.Empty));

			Assert.False(result);
		}

		[Fact]
		public void ElementExists_ValueProvided_ElementsFound_ValueIsMatched_ReturnsTrue()
		{
			string value = "someValue";

			var osWebDriver =
				new OSWebDriver(
					GetDefaultOSWebDriverSettings(),
					Mock.Of<IWebDriver>(
						d => d.FindElements(
							It.IsAny<By>()) == new ReadOnlyCollection<IWebElement>(
								new IWebElement[] { Mock.Of<IWebElement>(), Mock.Of<IWebElement>(e => e.Text == value) })));

			var result = osWebDriver.ElementExists(By.Id(string.Empty), value);

			Assert.True(result);
		}

		[Fact]
		public void ElementExists_ValueProvided_ElementsFound_ValueNotMatched_ReturnsFalse()
		{
			var osWebDriver =
				new OSWebDriver(
					GetDefaultOSWebDriverSettings(),
					Mock.Of<IWebDriver>(
						d => d.FindElements(
							It.IsAny<By>()) == new ReadOnlyCollection<IWebElement>(
								new IWebElement[] { Mock.Of<IWebElement>(), Mock.Of<IWebElement>() })));

			var result = osWebDriver.ElementExists(By.Id(string.Empty), "someValue");

			Assert.False(result);
		}

		private static OSWebDriverSettings GetDefaultOSWebDriverSettings()
		{
			return new OSWebDriverSettings("http://someurl", null, _ => { return false; });
		}
	}
}
