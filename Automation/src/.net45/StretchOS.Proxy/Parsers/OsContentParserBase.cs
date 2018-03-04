using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace StretchOS.Proxy.Parsers
{
	public abstract class OsContentParserBase<T, U> : IRequestParser<U>
	{
		readonly XmlRootAttribute _customRootAttribute;

		public OsContentParserBase(XmlRootAttribute customRootAttribute = null)
		{
			_customRootAttribute = customRootAttribute;
		}

		protected abstract U Build(T content);

		public U Parse(string responseBody)
		{
			XDocument xDoc = XDocument.Parse(responseBody);

			XNode contentNode = 
				xDoc.Descendants((XNamespace)"http://schemas.xmlsoap.org/soap/envelope/" + "Body").First().FirstNode;

			XmlSerializer oXmlSerializer = 
				new XmlSerializer(typeof(T), null, null, _customRootAttribute, "http://www.outsystems.com");

			var content = (T)oXmlSerializer.Deserialize(contentNode.CreateReader());

			return Build(content);
		}
	}
}
