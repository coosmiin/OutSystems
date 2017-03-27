using Newtonsoft.Json;
using NUnit.Framework;
using StretchOS.Proxy.ESpaces;
using StretchOS.Proxy.ESpaces.Serialization;
using System.Collections.Generic;

namespace StretchOS.Proxy.UnitTests.ESpaces.Serialization
{
	[TestFixture]
	public class ESpaceJsonConverterTests
	{
		[Test]
		public void WriteJson_ESpaceDictionaryIsSerializedCorrectly()
		{
			var eSpaceA = new ESpace(1, "A");
			eSpaceA.AddUnknownConsumer("CW_X");

			var eSpaceCWA = new ESpace(2, "CW_A");
			eSpaceCWA.AddUnknownConsumer("CW_B");
			eSpaceCWA.AddConsumer(eSpaceA);

			var eSpaceCSA = new ESpace(3, "CS_A");
			eSpaceCSA.AddUnknownConsumer("CS_B");
			eSpaceCSA.AddConsumer(eSpaceCWA);
			eSpaceCSA.AddConsumer(eSpaceA);

			IDictionary<int, IESpace> eSpaces =
				new Dictionary<int, IESpace>
				{
					{ eSpaceCSA.Id, eSpaceCSA },
					{ eSpaceCWA.Id, eSpaceCWA },
					{ eSpaceA.Id, eSpaceA }
				};

			JsonConvert.SerializeObject(eSpaces, Formatting.Indented, new ESpaceJsonConverter(typeof(ESpace)));
		}
	}
}
