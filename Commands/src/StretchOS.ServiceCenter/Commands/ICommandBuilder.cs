namespace StretchOS.ServiceCenter.Commands
{
	public interface ICommandBuilder
	{
		ICommand CreateDownloadCommand(string baseUrl, string username, string password, string[] commandArguments);
		ICommand CreateSolutionPublishCommand(string baseUrl, string username, string password, string[] commandArguments);
	}
}
