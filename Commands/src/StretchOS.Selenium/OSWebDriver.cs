using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace StretchOS.Selenium
{
	public class OSWebDriver : IOSWebDriver
	{
		private readonly IWebDriver _driver;

		public OSWebDriver(IWebDriver webDriver = null)
		{
			_driver = webDriver;

			if (_driver == null)
			{
				_driver = new ChromeDriver(AppContext.BaseDirectory);
			}
		}

		public void GoTo(string url)
		{
			_driver.Navigate().GoToUrl(url);
		}
	}
}