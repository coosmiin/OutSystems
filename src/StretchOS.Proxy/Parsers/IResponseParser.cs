namespace StretchOS.Proxy.Parsers
{
	public interface IResponseParser<T>
	{
		T Parse(string responseBody);
	}
}
