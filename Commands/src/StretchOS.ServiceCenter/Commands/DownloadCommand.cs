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
			_webProxy.DownloadErrorLog();
		}

		protected override string GetDescription()
		{
			return $"Unknown parameter: {Parameters[0]}\n";
		}

		protected override bool IsValid()
		{
			return _knownParameter.Equals(Parameters[0]);
		}
	}
}
