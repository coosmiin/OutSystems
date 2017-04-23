using OpenQA.Selenium;
using StretchOS.Selenium.WebDriver;
using System;
using System.IO;

namespace StretchOS.ServiceCenter.WebProxy
{
	public class ServiceCenterWebProxy : IServiceCenterWebProxy
	{
		private readonly IOSWebDriver _webDriver;

		public ServiceCenterWebProxy(IOSWebDriver webDriver)
		{
			_webDriver = webDriver;
		}

		public void DownloadErrorLog()
		{
			_webDriver
				.GoTo("Error_Logs.aspx")
				.Click(By.CssSelector(".TableRecords_TopNavigation a"));

			string filePath = Path.Combine(AppContext.BaseDirectory, "ErrorLog.xlsx");

			while (!File.Exists(filePath)) ;

			_webDriver.Close();
		}
	}
}
