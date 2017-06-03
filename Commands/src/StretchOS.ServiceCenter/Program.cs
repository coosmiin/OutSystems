using StretchOS.Core.SystemIO;
using StretchOS.Selenium.WebDriver;
using StretchOS.ServiceCenter.Authentication;
using StretchOS.ServiceCenter.Commands;
using StretchOS.ServiceCenter.Commands.Runner;
using StretchOS.ServiceCenter.WebProxy;
using System;
using System.Linq;

namespace StretchOS.ServiceCenter
{
	class Program
	{
		static void Main(string[] args)
		{
			if (!IsValid(args))
				return;

			try
			{
				ICommand command = CreateCommand(args);

				ICommandRunner commandRunner = new CommandRunner(command, Console.Out); ;
				commandRunner.Run();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"ERROR: {ex.Message}; Details: {ex.InnerException?.Message}");
			}
		}

		// TODO: extract command validation in a testable class
		private static bool IsValid(string[] args)
		{
			if (args.Length == 0)
			{
				Console.WriteLine("Please provide a command.");
				return false;
			}

			if (args[0] != DownloadCommand.COMMAND)
			{
				Console.WriteLine("Unknown command: ", args[0]);
				return false;
			}

			return true;
		}

		private static ICommand CreateCommand(string[] args)
		{
			// TODO: extract global parameters like host and credential
			ICommand command =
				new DownloadCommand(
					new ServiceCenterWebProxy(
						new OSWebDriver(
							new OSWebDriverSettings(
								args[4] + "/ServiceCenter/",
								AuthActions.LoginAction(args[5], args[6]), AuthActions.LoginCheck)),
						new SystemIOWrapper()),
					args.Skip(1).ToArray());

			return command;
		}
	}
} 