using StretchOS.Proxy.Events;
using System.Collections.Generic;

namespace StretchOS.Proxy.Parsers
{
	public class OsCheckESpacePublishStateResponse
	{
		public OsCheckESpacePublishStateResponse()
		{
			ConsumerNames = new List<string>();
			OutdatedProducerNames = new List<string>();
		}

		public bool HasErrors { get; set; }
		public OsPublishStatus Status { get; set; }
		public string ESpaceName { get; set; }
		public IList<string> ConsumerNames { get; }
		public IList<string> OutdatedProducerNames { get; }
	}
}
