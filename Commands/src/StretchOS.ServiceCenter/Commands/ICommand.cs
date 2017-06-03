using StretchOS.ServiceCenter.Validation;

namespace StretchOS.ServiceCenter.Commands
{
	public interface ICommand
	{
		ValidationResult Validate();
		void Execute();
		string GetUsageDescription();
	}
}
