using OpenQA.Selenium;
using StretchOS.Core.SystemIO;
using StretchOS.Selenium.WebDriver;
using StretchOS.ServiceCenter.Domain;
using StretchOS.Core.Utils;
using System;
using System.IO;
using System.Threading.Tasks;

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

			Task downloadTask = Task.Run(
				() => DowloadErrorLog(_webDriver, settings.Start, settings.End, filePath));

			if (!downloadTask.Wait(TimeSpan.FromSeconds(5)))
			{
				throw new Exception("ErrorLog.xlsx download failed");
			};

			string newFileName =
				$"ErrorLog_{settings.Start.ToFileNameCompliant()}_{settings.End.ToFileNameCompliant()}.xlsx";

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

		private void DowloadErrorLog(IOSWebDriver webDriver, string start, string end, string filePath)
		{
			webDriver
				.GoTo("Error_Logs.aspx")
				.Fill(By.CssSelector("input[id$=FromDate]"), start)
				.Fill(By.CssSelector("input[id*=ToDate]"), end)
				.Click(By.CssSelector(".TableRecords_TopNavigation a"));

			while (!_systemIOWrapper.FileExists(filePath)) ;

			webDriver.Close();
		}
	}
}
