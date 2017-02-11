using System.Collections.Generic;

namespace StretchOS.Proxy.OsEvents
{
	public class OsPublishCompletedEvent : OsEventBase
	{
		public override OsEventType Type { get; } = OsEventType.PublishCompleted;
		public string ESpaceName { get; set; }
		public IList<string> ConsumerNames { get; set; }
	}
}
