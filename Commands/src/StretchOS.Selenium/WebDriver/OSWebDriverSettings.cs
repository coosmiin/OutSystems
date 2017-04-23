using System;

namespace StretchOS.Selenium.WebDriver
{
	public class OSWebDriverSettings : IOSWebDriverSettings
	{
		public string BaseUrl { get; private set; }
		public Action<IOSWebDriver> Login { get; private set; }
		public Func<IOSWebDriver, bool> CheckIsLogin { get; private set; }

		public OSWebDriverSettings(string baseUrl, Action<IOSWebDriver> login, Func<IOSWebDriver, bool> checkIsLogin)
		{
			BaseUrl = baseUrl;
			Login = login;
			CheckIsLogin = checkIsLogin;
		}
	}
}