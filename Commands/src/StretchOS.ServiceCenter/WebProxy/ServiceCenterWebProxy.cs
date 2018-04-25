using OpenQA.Selenium;
using StretchOS.Core.SystemIO;
using StretchOS.Selenium.WebDriver;
using StretchOS.ServiceCenter.Domain;
using StretchOS.Core.Utils;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Threading;

namespace StretchOS.ServiceCenter.WebProxy
{
	public class ServiceCenterWebProxy : IServiceCenterWebProxy
	{
		private readonly IOSWebDriver _webDriver;
		private readonly ISystemIOWrapper _systemIOWrapper;
		private readonly int _timeoutSeconds;

		public ServiceCenterWebProxy(IOSWebDriver webDriver, ISystemIOWrapper systemIOWrapper, int timeoutSeconds = 5 * 60)
		{
			_webDriver = webDriver;
			_systemIOWrapper = systemIOWrapper;
			_timeoutSeconds = timeoutSeconds;
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

			if (!downloadTask.Wait(TimeSpan.FromMinutes(_timeoutSeconds)))
			{
				throw new Exception($"ErrorLog.xlsx download failed or timed out in {_timeoutSeconds} seconds");
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

		public void PublishSolution(int solutionId)
		{
			_webDriver
				.GoTo($"Solution_Edit.aspx?SolutionId={solutionId}")
				.Click(By.CssSelector(".NoWrap input[type=checkbox]"))
				.Click(By.CssSelector("input[id^=wtListVersions][value=Publish]"));

			_webDriver.SwitchToAlert().Accept();

			Thread.Sleep(5000);
		}

		private void DowloadErrorLog(IOSWebDriver webDriver, string start, string end, string filePath)
		{
			webDriver
				.GoTo("Error_Logs.aspx")
				.Fill(By.CssSelector("input[id$=FromDate]"), start)
				.Fill(By.CssSelector("input[id*=ToDate]"), end)
				.Click(By.CssSelector(".TableRecords_TopNavigation a"));

			while (!_systemIOWrapper.FileExists(filePath)) ;
		}
	}
}
