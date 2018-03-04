using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace StretchOS.Proxy.ESpaces.Repositories
{
	public class ESpaceFileSystemRepository : IESpaceRepository
	{
		private readonly string _eSpacefilePath;

		private readonly JsonSerializerSettings _jsonSettings = 
			new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.All };

		public ESpaceFileSystemRepository(string eSpacefilePath)
		{
			_eSpacefilePath = eSpacefilePath;
		}

		public void Save(IDictionary<int, IESpace> _eSpaces)
		{
			File.WriteAllText(_eSpacefilePath, JsonConvert.SerializeObject(_eSpaces, _jsonSettings));
		}

		public IDictionary<int, IESpace> LoadState()
		{
			if (!File.Exists(_eSpacefilePath))
				return new Dictionary<int, IESpace>();

			return 
				JsonConvert.DeserializeObject<Dictionary<int, ESpace>>(File.ReadAllText(_eSpacefilePath)) 
				as IDictionary<int, IESpace>;
		}
	}
}