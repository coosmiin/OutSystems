using StretchOS.ServiceCenter.Domain;
using StretchOS.ServiceCenter.WebProxy;
using System.IO;

namespace StretchOS.ServiceCenter.Commands
{
	public class DownloadCommand : Command
	{
		private readonly IServiceCenterWebProxy _webProxy;
		private readonly string _knownParameter = "--error";

		public DownloadCommand(TextWriter textWriter, IServiceCenterWebProxy webProxy, params string[] parameters) 
			: base(textWriter, parameters)
		{
			_webProxy = webProxy;
		}

		public override void Execute()
		{
			// TODO: Write a test for the conditional operator?
			_webProxy.DownloadErrorLog(
				new SearchSettings
				{
					Start = Parameters.Length > 2 ? Parameters[2] : string.Empty,
					End = Parameters.Length > 3 ? Parameters[3] : string.Empty
				});
		}

		protected override string GetDescription()
		{
			// TODO: This is not an error message. It should describe the usage.
			return $"Unknown parameter: {Parameters[0]}\n";
		}

		protected override bool IsValid()
		{
			// TODO: Parameters parsing & validation
			return _knownParameter.Equals(Parameters[0]);
		}
	}
}
