using OpenQA.Selenium;
using StretchOS.Selenium.WebDriver;
using StretchOS.ServiceCenter.Domain;
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

		public void DownloadErrorLog(SearchSettings settings)
		{
			string filePath = Path.Combine(AppContext.BaseDirectory, "ErrorLog.xlsx");

			if (File.Exists(filePath))
			{
				File.Delete(filePath);
			}

			_webDriver
				.GoTo("Error_Logs.aspx")
				.Fill(By.CssSelector("input[id$=FromDate]"), settings.Start)
				.Fill(By.CssSelector("input[id*=ToDate]"), settings.End)
				.Click(By.CssSelector(".TableRecords_TopNavigation a"));

			while (!File.Exists(filePath)) ;

			_webDriver.Close();

			// TODO: Logic and tests for empty start and end date texts
			string newFileName =
				$"ErrorLog_{ToFileNameCompliant(settings.Start)}_{ToFileNameCompliant(settings.End)}.xlsx";

			string newFolderPath = Path.Combine(AppContext.BaseDirectory, "ErrorLogs");

			if (!Directory.Exists(newFolderPath))
			{
				Directory.CreateDirectory(newFolderPath);
			}

			string newFilePath = Path.Combine(newFolderPath, newFileName);

			if (File.Exists(newFilePath))
			{
				File.Delete(newFilePath);
			}

			File.Move(filePath, newFilePath);
		}

		// TODO: this is not testable - extract it to its own class :(
		private string ToFileNameCompliant(string dateText)
		{
			return dateText.Replace("-", string.Empty).Replace(" ", "-").Replace(":", string.Empty);
		}
	}
}
