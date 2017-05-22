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
			CommandValidationResult validationResult = _command.Validate();

			if (!validationResult.IsValid)
			{
				_textWriter.WriteLine(validationResult.ValidationText);
				return;
			}

			_command.Execute();
		}
	}
}
