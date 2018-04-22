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
				ICommandBuilder commandBuilder = new CommandBuilder();
				ICommand command = null;

				if (args[0] == DownloadCommand.COMMAND)
				{
					command =
						commandBuilder.CreateDownloadCommand(args[4], args[5], args[6], args.Skip(1).ToArray());
				}

				if (args[0] == SolutionPublishCommand.COMMAND)
				{
					command =
						commandBuilder.CreateSolutionPublishCommand(args[2], args[3], args[4], args.Skip(1).ToArray());
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
	}
} 