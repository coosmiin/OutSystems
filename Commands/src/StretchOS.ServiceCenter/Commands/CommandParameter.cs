using StretchOS.ServiceCenter.Validation;
using System.Linq;

namespace StretchOS.ServiceCenter.Commands
{
	public class CommandParameter
	{
		public string Name { get; set; }
		public string[] AllowedValues { get; set; } = Enumerable.Empty<string>().ToArray();
		public ValidationRule ValidationRule { get; set; }
		public bool Mandatory { get; set; }
	}
}
