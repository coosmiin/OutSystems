using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StretchOS.Proxy.ESpaces
{
	public class ESpaceCenter : IESpaceCenter
	{
		private readonly IESpaceRepository _repository;

		private readonly IDictionary<int, IESpace> _eSpaces = new Dictionary<int, IESpace>();

		public ESpaceCenter(IESpaceRepository repository)
		{
			_repository = repository;
		}

		public void AddOrUpdateESpace(IESpace eSpace)
		{
			if (_eSpaces.ContainsKey(eSpace.Id))
			{
				_eSpaces[eSpace.Id].Name = eSpace.Name;
				return;
			}

			AddESpace(eSpace);
		}

		public void AddConsumers(int eSpaceId, IList<string> consumerNames)
		{
			foreach (string eSpaceName in consumerNames)
			{
				IESpace eSpace = _eSpaces.Values.FirstOrDefault(v => v.Name.Equals(eSpaceName));
				if (eSpace != null)
				{
					_eSpaces[eSpaceId].AddConsumer(eSpace);
				}
				else
				{
					_eSpaces[eSpaceId].AddUnknownConsumer(eSpaceName);
				}
			}
		}

		public void SaveState()
		{
			_repository.Save(_eSpaces);
		}

		private void AddESpace(IESpace eSpace)
		{
			foreach (IESpace eSpaceItem in _eSpaces.Values)
			{
				string consumerName = eSpaceItem.GetUnknownConsumers().FirstOrDefault(c => c.Equals(eSpace.Name));

				if (!string.IsNullOrEmpty(consumerName))
				{
					eSpaceItem.AddConsumer(eSpace);
				}
			}

			_eSpaces.Add(eSpace.Id, eSpace);
		}
	}
}
