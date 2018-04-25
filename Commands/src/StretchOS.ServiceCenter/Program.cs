using StretchOS.Selenium.WebDriver;
using StretchOS.ServiceCenter.Authentication;
using StretchOS.ServiceCenter.Commands;
using StretchOS.ServiceCenter.Commands.Runner;
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

			IOSWebDriver osWebDriver = null;

			try
			{
				ICommandBuilder commandBuilder = null;
				ICommand command = null;

				if (args[0] == DownloadCommand.COMMAND)
				{
					osWebDriver = CreateOsWebDriver(args[4], args[5], args[6]);

					commandBuilder = new CommandBuilder(osWebDriver);

					command = commandBuilder.CreateDownloadCommand(args.Skip(1).ToArray());
				}

				if (args[0] == SolutionPublishCommand.COMMAND)
				{
					osWebDriver = CreateOsWebDriver(args[2], args[3], args[4]);

					commandBuilder = new CommandBuilder(osWebDriver);

					command = commandBuilder.CreateSolutionPublishCommand(args.Skip(1).ToArray());
				}

				if (command == null)
				{
					Console.WriteLine("Unknown command: ", args[0]);
					return;
				}

				ICommandRunner commandRunner = new CommandRunner(command, Console.Out); ;
				commandRunner.Run();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"ERROR: {ex.Message}; Details: {ex.InnerException?.Message}");
			}
			finally
			{
				if (osWebDriver != null)
					osWebDriver.Close();
			}
		}

		private static bool IsValid(string[] args)
		{
			if (args.Length == 0)
			{
				Console.WriteLine("Please provide a command.");
				return false;
			}

			return true;
		}

		private static IOSWebDriver CreateOsWebDriver(string baseUrl, string username, string password)
		{
			return 
				new OSWebDriver(
					new OSWebDriverSettings(
						$"{baseUrl}/ServiceCenter/",
						AuthActions.LoginAction(username, password), AuthActions.LoginCheck));
		}
	}
} 