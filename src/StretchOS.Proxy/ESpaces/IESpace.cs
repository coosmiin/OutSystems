using System.Collections.Generic;

namespace StretchOS.Proxy.ESpaces
{
	public interface IESpace
	{
		int Id { get; }
		string Name { get; set; }

		void AddConsumer(IESpace eSpace);
		void AddUnknownConsumer(string eSpaceName);
		void UpdateUnknownConsumer(string oldName, string newName);

		IEnumerable<IESpace> GetConsumers();
		IEnumerable<string> GetUnknownConsumers();
	}
}