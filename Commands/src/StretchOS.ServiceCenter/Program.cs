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
			Command command = 
				new DownloadCommand(
					Console.Out, 
					new ServiceCenterWebProxy(
						new OSWebDriver(
							new OSWebDriverSettings(
								args[1] + "/ServiceCenter/",
								AuthActions.LoginAction(args[2], args[3]), AuthActions.LoginCheck))), 
					args);

			command.Execute();
		}
	}
} 