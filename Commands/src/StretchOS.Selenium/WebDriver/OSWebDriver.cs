using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StretchOS.Selenium.WebDriver
{
	public class OSWebDriver : IOSWebDriver
	{
		private readonly IWebDriver _driver;
		private readonly IOSWebDriverSettings _settings;
		private readonly Uri _baseUri;

		public OSWebDriver(IOSWebDriverSettings settings, IWebDriver webDriver = null)
		{
			_driver = webDriver;
			_settings = settings;

			if (_driver == null)
			{
				var options = new ChromeOptions();
				options.AddUserProfilePreference("download.default_directory", AppContext.BaseDirectory);
				options.AddArgument("--start-maximized");

				_driver = new ChromeDriver(AppContext.BaseDirectory, options);
			}

			_baseUri = new Uri(_settings.BaseUrl);
		}

		public IOSWebDriver GoTo(string relativeUrl)
		{
			_driver.Navigate().GoToUrl(new Uri(_baseUri, relativeUrl).AbsoluteUri);

			if (_settings.CheckIsLogin(this))
			{
				_settings.Login(this);
			}

			return this;
		}

		public IOSWebDriver Fill(By by, string text)
		{
			_driver.FindElement(by).SendKeys(text);

			return this;
		}

		public IOSWebDriver Click(By by)
		{
			_driver.FindElement(by).Click();

			return this;
		}

		public bool ElementExists(By by, string value = null)
		{
			IEnumerable<IWebElement> elements = _driver.FindElements(by);

			if (!elements.Any())
				return false;

			if (string.IsNullOrEmpty(value))
				return true;

			return elements.Any(e => e.Text == value);
		}

		public string GetPageTitle()
		{
			return _driver.Title;
		}

		public void Close()
		{
			_driver.Quit();
		}
	}
}