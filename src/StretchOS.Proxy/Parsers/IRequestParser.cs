namespace StretchOS.Proxy.Parsers
{
	public interface IRequestParser<T>
	{
		T Parse(string responseBody);
	}
}
