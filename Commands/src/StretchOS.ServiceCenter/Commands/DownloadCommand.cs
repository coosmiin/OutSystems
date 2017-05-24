using StretchOS.ServiceCenter.Domain;
using StretchOS.ServiceCenter.WebProxy;
using System;
using System.Globalization;

namespace StretchOS.ServiceCenter.Commands
{
	public class DownloadCommand : ICommand
	{
		private readonly IServiceCenterWebProxy _webProxy;
		private readonly string[] _parameters;

		private const string TARGET_PARAM_ERROR_LOG = "--error-log";

		private ValidationRule[] _validationRules = GetValidationRules();

		public DownloadCommand(IServiceCenterWebProxy webProxy, params string[] parameters)
		{
			_webProxy = webProxy;
			_parameters = parameters;
		}

		public void Execute()
		{
			_webProxy.DownloadErrorLog(
				new SearchSettings
				{
					Start = GetParamAt(1),
					End = GetParamAt(2)
				});
		}

		public CommandValidationResult Validate()
		{
			for (int index = 0; index < _parameters.Length; index++)
			{
				if (index == _validationRules.Length)
				{
					return
						new CommandValidationResult
						{
							IsValid = true,
							ValidationText = $"Warning, unknown parameter: {_parameters[index]}"
						};
				}

				var validationRule = _validationRules[index];

				bool isValid = validationRule.Rule(_parameters[index]);

				if (!isValid)
				{
					return
						new CommandValidationResult
						{
							IsValid = false,
							ValidationText = string.Format(validationRule.ErrorMessageFormat, _parameters[index])
						};
				}
			}

			return new CommandValidationResult { IsValid = true };
		}

		#region Private Methods

		private string GetParamAt(int index)
		{
			return _parameters.Length > index ? _parameters[index] : string.Empty;
		}

		private static bool ValidateDate(string param)
		{
			return DateTime.TryParseExact(param, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date);
		}

		private static ValidationRule[] GetValidationRules()
		{
			return
				new[]
				{
					// param[0]
					new ValidationRule
					{
						Rule = param => param.Equals(TARGET_PARAM_ERROR_LOG),
						ErrorMessageFormat = "Unknown parameter: {0}"
					},
					// param[1]
					new ValidationRule
					{
						Rule = ValidateDate,
						ErrorMessageFormat = "StartDate is invalid"
					},
					// param[2]
					new ValidationRule
					{
						Rule = ValidateDate,
						ErrorMessageFormat = "EndDate is invalid"
					},
				};
		}

		#endregion Private Methods
	}
}
