using StretchOS.Proxy.Domain;
using StretchOS.Proxy.OutSystemsService;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace StretchOS.Proxy.Parsers
{
	public class OsEspaceBuildResponseParser : IResponseParser<OsESpaceBuildResponse>
	{
		public OsESpaceBuildResponse Parse(string responseBody)
		{
			responseBody = responseBody.Replace(" xmlns=\"http://www.outsystems.com\"", string.Empty);

			XDocument xDoc = XDocument.Parse(responseBody);

			XNode start1CPResponseNode = 
				xDoc.Descendants((XNamespace)"http://schemas.xmlsoap.org/soap/envelope/" + "Body").First().FirstNode;

			XmlSerializer oXmlSerializer = new XmlSerializer(typeof(Start1CPResponse));

			var buildResponse = (Start1CPResponse)oXmlSerializer.Deserialize(start1CPResponseNode.CreateReader());

			return new OsESpaceBuildResponse
			{
				HasErrors = buildResponse.result.HasErrors,
				ESpaceId = buildResponse.result.ESpaceId,
				VersionId = buildResponse.result.VersionId
			};
		}
	}
}
