using OpenQA.Selenium;
using StretchOS.Core.SystemIO;
using StretchOS.Selenium.WebDriver;
using StretchOS.ServiceCenter.Domain;
using System;
using System.IO;

namespace StretchOS.ServiceCenter.WebProxy
{
	public class ServiceCenterWebProxy : IServiceCenterWebProxy
	{
		private readonly IOSWebDriver _webDriver;
		private readonly ISystemIOWrapper _systemIOWrapper;

		public ServiceCenterWebProxy(IOSWebDriver webDriver, ISystemIOWrapper systemIOWrapper)
		{
			_webDriver = webDriver;
			_systemIOWrapper = systemIOWrapper;
		}

		public void DownloadErrorLog(SearchSettings settings)
		{
			string filePath = Path.Combine(AppContext.BaseDirectory, "ErrorLog.xlsx");

			if (_systemIOWrapper.FileExists(filePath))
			{
				_systemIOWrapper.FileDelete(filePath);
			}

			_webDriver
				.GoTo("Error_Logs.aspx")
				.Fill(By.CssSelector("input[id$=FromDate]"), settings.Start)
				.Fill(By.CssSelector("input[id*=ToDate]"), settings.End)
				.Click(By.CssSelector(".TableRecords_TopNavigation a"));

			// TODO: What if the file will never exist?
			while (!_systemIOWrapper.FileExists(filePath)) ;

			_webDriver.Close();

			// TODO: Logic and tests for empty start and end date texts
			string newFileName =
				$"ErrorLog_{ToFileNameCompliant(settings.Start)}_{ToFileNameCompliant(settings.End)}.xlsx";

			string newFolderPath = Path.Combine(AppContext.BaseDirectory, "ErrorLogs");

			if (!_systemIOWrapper.DirectoryExists(newFolderPath))
			{
				_systemIOWrapper.CreateDirectory(newFolderPath);
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
