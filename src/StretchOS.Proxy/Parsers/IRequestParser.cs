using StretchOS.Proxy.Domain;

namespace StretchOS.Proxy.Parsers
{
	public interface IRequestParser
	{
		OsRequest Parse(string requestBody);
	}
}
