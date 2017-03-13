using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace StretchOS.Proxy.ESpaces
{
	public class ESpace : IESpace
	{
		public int Id { get; }
		public string Name { get; set; }

		[JsonProperty]
		private IDictionary<int, IESpace> _consumers { get; }
		[JsonProperty]
		private IList<string> _unknownConsumers { get; }

		public ESpace(int id, string name)
		{
			Id = id;
			Name = name;

			_consumers = new Dictionary<int, IESpace>();
			_unknownConsumers = new List<string>();
		}

		public void AddConsumer(IESpace eSpace)
		{
			if (_consumers.ContainsKey(eSpace.Id))
				return;

			_unknownConsumers.Remove(eSpace.Name);
			_consumers.Add(eSpace.Id, eSpace);
		}

		public void AddUnknownConsumer(string eSpaceName)
		{
			if (_unknownConsumers.Contains(eSpaceName))
				return;

			_unknownConsumers.Add(eSpaceName);
		}

		public void UpdateUnknownConsumer(string oldName, string newName)
		{
			_unknownConsumers.Remove(oldName);
			_unknownConsumers.Add(newName);
		}

		public IEnumerable<IESpace> GetConsumers()
		{
			return _consumers.Select(d => d.Value).ToList();
		}

		public IEnumerable<string> GetUnknownConsumers()
		{
			return _unknownConsumers;
		}
	}
}
