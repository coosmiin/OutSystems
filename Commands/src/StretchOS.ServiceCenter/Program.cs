using StretchOS.Core.SystemIO;
using StretchOS.Selenium.WebDriver;
using StretchOS.ServiceCenter.Authentication;
using StretchOS.ServiceCenter.Commands;
using StretchOS.ServiceCenter.Commands.Runner;
using StretchOS.ServiceCenter.WebProxy;
using System;

namespace StretchOS.ServiceCenter
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				ICommandRunner commandRunner = CreateCommandRunner(args);
				commandRunner.Run();
			}
			catch (Exception ex)
			{
				Console.WriteLine("ERROR: {0}", ex.Message);
			}
		}

		private static ICommandRunner CreateCommandRunner(string[] args)
		{
			ICommand command =
				new DownloadCommand(
					new ServiceCenterWebProxy(
						new OSWebDriver(
							new OSWebDriverSettings(
								args[1] + "/ServiceCenter/",
								AuthActions.LoginAction(args[4], args[5]), AuthActions.LoginCheck)),
						new SystemIOWrapper()),
					args);

			return new CommandRunner(command, Console.Out);
		}
	}
} 