using StretchOS.ServiceCenter.Validation;
using System.IO;

namespace StretchOS.ServiceCenter.Commands.Runner
{
	public class CommandRunner : ICommandRunner
	{
		private readonly ICommand _command;
		private readonly TextWriter _textWriter;

		public CommandRunner(ICommand command, TextWriter textWriter)
		{
			_command = command;
			_textWriter = textWriter;
		}

		public void Run()
		{
			ValidationResult validationResult = _command.Validate();

			if (!string.IsNullOrEmpty(validationResult.ValidationText))
			{
				_textWriter.WriteLine(validationResult.ValidationText);
			}

			if (validationResult.IsValid)
			{
				_command.Execute();
			}
			else
			{
				_textWriter.WriteLine(_command.GetUsageDescription());
			}
		}
	}
}
