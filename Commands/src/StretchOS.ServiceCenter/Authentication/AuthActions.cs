using OpenQA.Selenium;
using StretchOS.Selenium.WebDriver;
using System;

namespace StretchOS.ServiceCenter.Authentication
{
	public static class AuthActions
	{
		public static Func<string, string, Action<IOSWebDriver>> LoginAction =
			(username, password) =>
			{
				return
					driver =>
						driver
							.Fill(By.Id("wtInput1"), username)
							.Fill(By.Id("wtInputPass1"), password)
							.Click(By.Id("wtButton1"));
			};

		public static Func<IOSWebDriver, bool> LoginCheck = driver => { return driver.GetPageTitle().Contains(" - Login"); };
	}
}