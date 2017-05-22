namespace StretchOS.ServiceCenter.Commands
{
	public interface ICommand
	{
		CommandValidationResult Validate();
		void Execute();
	}
}
