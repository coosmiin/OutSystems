using System.Collections.Generic;

namespace StretchOS.Proxy.ESpaces
{
	public interface IESpaceRepository
	{
		void Save(IDictionary<int, IESpace> _eSpaces);
	}
}