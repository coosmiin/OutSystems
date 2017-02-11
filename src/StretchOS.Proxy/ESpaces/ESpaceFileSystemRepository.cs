using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace StretchOS.Proxy.ESpaces
{
	public class ESpaceFileSystemRepository : IESpaceRepository
	{
		private readonly string _eSpacefilePath;

		public ESpaceFileSystemRepository(string eSpacefilePath)
		{
			_eSpacefilePath = eSpacefilePath;
		}

		public void Save(IDictionary<int, IESpace> _eSpaces)
		{
			File.WriteAllText(_eSpacefilePath, JsonConvert.SerializeObject(_eSpaces));
		}
	}
}