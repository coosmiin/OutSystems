using StretchOS.Proxy.Domain;

namespace StretchOS.Proxy.Parsers
{
	public class OsServiceRequestParser : IRequestParser
	{
		public OsRequest Parse(string requestBody)
		{
			return new OsRequest
			{
				Type = OsRequestType.Build
			};
		}
	}
}
