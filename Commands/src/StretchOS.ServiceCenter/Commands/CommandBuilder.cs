using StretchOS.Core.SystemIO;
using StretchOS.Selenium.WebDriver;
using StretchOS.ServiceCenter.Authentication;
using StretchOS.ServiceCenter.WebProxy;

namespace StretchOS.ServiceCenter.Commands
{
	public class CommandBuilder : ICommandBuilder
	{
		public ICommand CreateDownloadCommand(
			string baseUrl, string username, string password, string[] commandArguments)
		{
			ICommand command =
				new DownloadCommand(
					CreateServiceCenterWebProxy(baseUrl, username, password), commandArguments);

			return command;
		}

		public ICommand CreateSolutionPublishCommand(
			string baseUrl, string username, string password, string[] commandArguments)
		{
			ICommand command =
				new SolutionPublishCommand(
					CreateServiceCenterWebProxy(baseUrl, username, password), commandArguments);

			return command;
		}

		private IServiceCenterWebProxy CreateServiceCenterWebProxy(string baseUrl, string username, string password)
		{
			return
				new ServiceCenterWebProxy(
					new OSWebDriver(
						new OSWebDriverSettings(
							$"{baseUrl}/ServiceCenter/",
							AuthActions.LoginAction(username, password), AuthActions.LoginCheck)),
					new SystemIOWrapper());
		}
	}
}
