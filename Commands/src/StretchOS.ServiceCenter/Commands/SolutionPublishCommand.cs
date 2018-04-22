using StretchOS.ServiceCenter.Validation;
using StretchOS.ServiceCenter.WebProxy;
using System;
using System.Globalization;

namespace StretchOS.ServiceCenter.Commands
{
	public class SolutionPublishCommand : CommandBase
	{
		public const string COMMAND = "solution-publish";
		private const string SOLUTION_ID_PARAM_NAME = "solutionId";

		private readonly IServiceCenterWebProxy _webProxy;

		public SolutionPublishCommand(IServiceCenterWebProxy webProxy, params string[] parameters)
			: base(parameters)
		{
			_webProxy = webProxy;
		}

		public override void Execute()
		{
			_webProxy.PublishSolution(int.Parse(GetParamAt(0)));
		}

		protected override CommandParameter[] GetCommandParameters()
		{
			return
				new[]
				{
					// param[0]
					new CommandParameter
					{
						Name = SOLUTION_ID_PARAM_NAME,
						ValidationRule = new ValidationRule()
						{
							Rule = param => int.TryParse(param, out int result),
							ErrorMessageFormat = $"[{SOLUTION_ID_PARAM_NAME}] should be an integer"
						},
						Mandatory = true
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
