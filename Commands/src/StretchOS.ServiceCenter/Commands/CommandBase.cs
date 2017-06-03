using StretchOS.ServiceCenter.Validation;
using System.Linq;

namespace StretchOS.ServiceCenter.Commands
{
	public abstract class CommandBase : ICommand
	{
		private readonly string[] _inputParameters;
		private readonly CommandParameter[] _commandParameters;

		protected CommandParameter[] CommandParameters => _commandParameters;

		public CommandBase(params string[] inputParameters)
		{
			_inputParameters = inputParameters;
			_commandParameters = GetCommandParameters();
		}

		public abstract void Execute();

		protected abstract CommandParameter[] GetCommandParameters();
		protected abstract string GetCommandName();

		public ValidationResult Validate()
		{
			int mandatoryParamsCount = _commandParameters.Count(p => p.Mandatory);
			if (mandatoryParamsCount > _inputParameters.Length)
			{
				CommandParameter firstRequiredParam = _commandParameters[_inputParameters.Length];
				string allowedValues = string.Join(", ", firstRequiredParam.AllowedValues);

				return
					new ValidationResult
					{
						IsValid = false,
						ValidationText = $"[{firstRequiredParam.Name}] is required. Possible values: [{allowedValues}]."
					};
			}

			for (int index = 0; index < _inputParameters.Length; index++)
			{
				// If we have more input parameters than command parameters
				if (index == _commandParameters.Length)
				{
					return
						new ValidationResult
						{
							IsValid = true,
							ValidationText = $"Warning, unknown parameter: {_inputParameters[index]}"
						};
				}

				var commandParam = _commandParameters[index];

				bool isValid = commandParam.ValidationRule.Rule(_inputParameters[index]);

				if (!isValid)
				{
					return
						new ValidationResult
						{
							IsValid = false,
							ValidationText = string.Format(commandParam.ValidationRule.ErrorMessageFormat, _inputParameters[index])
						};
				}
			}

			return new ValidationResult { IsValid = true };
		}

		public string GetUsageDescription()
		{
			return
				string.Format(
					"{0} {1}", 
					GetCommandName(), 
					string.Join(
						" ",
						CommandParameters.Select(
							p => p.AllowedValues.Any()
								? string.Format("[{0}]", string.Join(" | ", p.AllowedValues))
								: $"[{p.Name}]")));
		}

		protected string GetParamAt(int index)
		{
			return _inputParameters.Length > index ? _inputParameters[index] : string.Empty;
		}
	}
}
