using System.Collections.Generic;

namespace StretchOS.Proxy.ESpaces
{
	public interface IESpaceCenter
	{
		void AddOrUpdateESpace(IESpace eSpace);
		void AddConsumers(int eSpaceId, IList<string> consumerNames);
		void SaveState();
	}
}
