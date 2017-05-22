using StretchOS.ServiceCenter.Domain;
using StretchOS.ServiceCenter.WebProxy;

namespace StretchOS.ServiceCenter.Commands
{
	public class DownloadCommand : ICommand
	{
		private readonly IServiceCenterWebProxy _webProxy;
		private readonly string _knownParameter = "--error-log";
		private readonly string[] _parameters;

		public DownloadCommand(IServiceCenterWebProxy webProxy, params string[] parameters)
		{
			_webProxy = webProxy;
			_parameters = parameters;
		}

		public void Execute()
		{
			// TODO: Write a test for the conditional operator?
			_webProxy.DownloadErrorLog(
				new SearchSettings
				{
					Start = _parameters.Length > 2 ? _parameters[2] : string.Empty,
					End = _parameters.Length > 3 ? _parameters[3] : string.Empty
				});
		}

		protected string GetDescription()
		{
			// TODO: This is not an error message. It should describe the usage.
			return $"Unknown parameter: {_parameters[0]}\n";
		}

		public CommandValidationResult Validate()
		{
			// TODO: Parameters parsing & validation
			return
				new CommandValidationResult
				{
					IsValid = _knownParameter.Equals(_parameters[0])
				};		
		}
	}
}
