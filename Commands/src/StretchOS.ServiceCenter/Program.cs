using StretchOS.Core.SystemIO;
using StretchOS.Selenium.WebDriver;
using StretchOS.ServiceCenter.Authentication;
using StretchOS.ServiceCenter.Commands;
using StretchOS.ServiceCenter.WebProxy;
using System;

namespace StretchOS.ServiceCenter
{
	class Program
	{
		static void Main(string[] args)
		{
			// TODO: add try catch around web driver such that it doesn't block the app in case of an Exception
			Command command = 
				new DownloadCommand(
					Console.Out, 
					new ServiceCenterWebProxy(
						new OSWebDriver(
							new OSWebDriverSettings(
								args[1] + "/ServiceCenter/",
								AuthActions.LoginAction(args[4], args[5]), AuthActions.LoginCheck)),
						new SystemIOWrapper()), 
					args);

			command.Execute();
		}
	}
} 