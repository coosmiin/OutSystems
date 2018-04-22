using StretchOS.ServiceCenter.Domain;
using StretchOS.ServiceCenter.Validation;
using StretchOS.ServiceCenter.WebProxy;
using System;
using System.Globalization;
using System.Linq;

namespace StretchOS.ServiceCenter.Commands
{
	public class DownloadCommand : CommandBase
	{
		public const string COMMAND = "download";

		private const string START_DATE_PARAM_NAME = "start-date";
		private const string END_DATE_PARAM_NAME = "end-date";

		private readonly IServiceCenterWebProxy _webProxy;
		private readonly string[] _targetAllowedValues = new[] { "--error-log" };

		public DownloadCommand(IServiceCenterWebProxy webProxy, params string[] parameters)
			: base(parameters)
		{
			_webProxy = webProxy;
		}

		public override void Execute()
		{
			_webProxy.DownloadErrorLog(
				new SearchSettings
				{
					Start = GetParamAt(1),
					End = GetParamAt(2)
				});
		}

		protected override CommandParameter[] GetCommandParameters()
		{
			return
				new[]
				{
					// param[0]
					new CommandParameter
					{
						Name = "target",
						AllowedValues = _targetAllowedValues,
						ValidationRule = new ValidationRule()
						{
							Rule = param => _targetAllowedValues.Contains(param),
							ErrorMessageFormat = "Unknown parameter: {0}"
						},
						Mandatory = true
					},
					// param[1]
					new CommandParameter
					{
						Name = START_DATE_PARAM_NAME,
						ValidationRule = new ValidationRule()
						{
							Rule = ValidateDate,
							ErrorMessageFormat = $"[{START_DATE_PARAM_NAME}] is invalid"
						}
					},
					// param[2]
					new CommandParameter
					{
						Name = END_DATE_PARAM_NAME,
						ValidationRule = new ValidationRule()
						{
							Rule = ValidateDate,
							ErrorMessageFormat = $"[{END_DATE_PARAM_NAME}] is invalid"
						}
					}
				};
		}

		protected override string GetCommandName()
		{
			return COMMAND;
		}

		private static bool ValidateDate(string param)
		{
			return DateTime.TryParseExact(param, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date);
		}
	}
}
