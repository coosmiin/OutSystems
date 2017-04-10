using OpenQA.Selenium;

namespace StretchOS.Selenium
{
	public class OSWebDriver
	{
		private readonly IWebDriver _driver;

		public OSWebDriver(IWebDriver webDriver)
		{
			_driver = webDriver;
		}

		public void GoTo(string url)
		{
			_driver.Navigate().GoToUrl(url);
		}
	}
}
