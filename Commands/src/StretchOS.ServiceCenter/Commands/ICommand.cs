namespace StretchOS.ServiceCenter.Commands
{
	public interface ICommand
	{
		CommandValidationResult Validate();
		void Execute();
		// TODO: GetUsageDescription to be used every time the validation fails
	}
}
