using StretchOS.Core.SystemIO;
using StretchOS.Selenium.WebDriver;
using StretchOS.ServiceCenter.WebProxy;

namespace StretchOS.ServiceCenter.Commands
{
	public class CommandBuilder : ICommandBuilder
	{
		private readonly IOSWebDriver _osWebDriver;

		public CommandBuilder(IOSWebDriver osWebDriver)
		{
			_osWebDriver = osWebDriver;
		}

		public ICommand CreateDownloadCommand(string[] commandArguments)
		{
			ICommand command =
				new DownloadCommand(CreateServiceCenterWebProxy(), commandArguments);

			return command;
		}

		public ICommand CreateSolutionPublishCommand(string[] commandArguments)
		{
			ICommand command =
				new SolutionPublishCommand(CreateServiceCenterWebProxy(), commandArguments);

			return command;
		}

		private IServiceCenterWebProxy CreateServiceCenterWebProxy()
		{
			return new ServiceCenterWebProxy(_osWebDriver, new SystemIOWrapper());
		}
	}
}
