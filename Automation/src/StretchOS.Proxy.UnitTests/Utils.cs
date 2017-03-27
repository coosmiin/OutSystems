using System.IO;
using System.Reflection;

namespace StretchOS.Proxy.UnitTests
{
	public class Utils
	{
		public static string GetTestResourcePath(string relativePath)
		{
			return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), relativePath);
		}
	}
}
