using System;

namespace StretchOS.Selenium.WebDriver
{
	public interface IOSWebDriverSettings
	{
		string BaseUrl { get; }
		Func<IOSWebDriver, bool> CheckIsLogin { get; }
		Action<IOSWebDriver> Login { get; }
	}
}