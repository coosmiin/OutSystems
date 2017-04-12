using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace StretchOS.Selenium
{
	public class OSWebDriver : IOSWebDriver
	{
		private readonly IWebDriver _driver;

		public OSWebDriver(IWebDriver webDriver)
		{
			_driver = webDriver;

			if (_driver == null)
			{
				_driver = new ChromeDriver();
			}
		}

		public void GoTo(string url)
		{
			_driver.Navigate().GoToUrl(url);
		}
	}
}