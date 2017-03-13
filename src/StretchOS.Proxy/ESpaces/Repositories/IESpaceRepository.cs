using System.Collections.Generic;

namespace StretchOS.Proxy.ESpaces.Repositories
{
	public interface IESpaceRepository
	{
		void Save(IDictionary<int, IESpace> _eSpaces);
		IDictionary<int, IESpace> LoadState();
	}
}