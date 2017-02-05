using NUnit.Framework;
using StretchOS.Proxy.ESpaces;

namespace StretchOS.Proxy.UnitTests.ESpaces
{
	[TestFixture]
	public class ESpaceTests
	{
		[Test]
		public void AddConsumer_UnknownConsumerWithSameNamePresent_UnknownConsumerIsRemoved()
		{
			var eSpace = new ESpace(1, "A");

			eSpace.AddUnknownConsumer("CW_A");
			eSpace.AddUnknownConsumer("CW_B");
			eSpace.AddUnknownConsumer("CW_C");

			eSpace.AddConsumer(new ESpace(2, "CW_B"));

			CollectionAssert.AreEquivalent(new[] { "CW_A", "CW_C" }, eSpace.GetUnknownConsumers());
		}

		[Test]
		public void UpdateUnknownConsumer_ConsumerNameIsUpdated()
		{
			var eSpace = new ESpace(1, "A");

			eSpace.AddUnknownConsumer("CW_A");
			eSpace.AddUnknownConsumer("CW_B");

			eSpace.UpdateUnknownConsumer("CW_A", "CW_A1");

			CollectionAssert.AreEquivalent(new[] { "CW_A1", "CW_B" }, eSpace.GetUnknownConsumers());
		}
	}
}
