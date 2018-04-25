namespace StretchOS.ServiceCenter.Commands
{
	public interface ICommandBuilder
	{
		ICommand CreateDownloadCommand(string[] commandArguments);
		ICommand CreateSolutionPublishCommand(string[] commandArguments);
	}
}
