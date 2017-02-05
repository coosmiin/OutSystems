using NUnit.Framework;
using StretchOS.Proxy.ESpaces;
using System.Linq;

namespace StretchOS.Proxy.UnitTests.ESpaces
{
	[TestFixture]
	public class ESpaceCenterTests
	{
		[Test]
		public void AddOrUpdateESpace_ESpaceIsUnknownConsumerForOtherESpaces_BecomesKnownForAll()
		{
			var eSpaceCenter = new ESpaceCenter();

			// preparing

			var eSpaceA = new ESpace(1, "A");

			eSpaceA.AddUnknownConsumer("CW_A");
			eSpaceA.AddUnknownConsumer("CW_X");
			eSpaceA.AddUnknownConsumer("CS_A");

			eSpaceCenter.AddOrUpdateESpace(eSpaceA);

			var eSpaceB = new ESpace(2, "B");

			eSpaceB.AddUnknownConsumer("CW_B");
			eSpaceB.AddUnknownConsumer("CW_X");

			eSpaceCenter.AddOrUpdateESpace(eSpaceB);

			// test

			eSpaceCenter.AddOrUpdateESpace(new ESpace(3, "CW_X"));

			// assert

			CollectionAssert.AreEquivalent(new[] { "CW_A", "CS_A" }, eSpaceA.GetUnknownConsumers());
			CollectionAssert.AreEquivalent(new[] { "CW_B" }, eSpaceB.GetUnknownConsumers());
			Assert.IsNotNull(eSpaceA.GetConsumers().FirstOrDefault(c => c.Name == "CW_X"));
			Assert.IsNotNull(eSpaceB.GetConsumers().FirstOrDefault(c => c.Id == 3));
		}

		[Test]
		public void AddOrUpdateESpace_ESpaceIsAlreadyPresent_DoesNotThrow()
		{
			var eSpaceCenter = new ESpaceCenter();

			// preparing

			var eSpaceA = new ESpace(1, "A");
			eSpaceA.AddUnknownConsumer("CW_A");
			eSpaceCenter.AddOrUpdateESpace(eSpaceA);

			var eSpaceB = new ESpace(2, "B");
			eSpaceB.AddUnknownConsumer("CW_B");
			eSpaceCenter.AddOrUpdateESpace(eSpaceB);

			// test & assert

			Assert.DoesNotThrow(() => eSpaceCenter.AddOrUpdateESpace(new ESpace(1, "A")));
		}

		[Test]
		public void AddOrUpdateESpace_ESpaceChangedName_NameIsChangedForAll()
		{
			var eSpaceCenter = new ESpaceCenter();

			// preparing

			var eSpaceA = new ESpace(1, "A");

			eSpaceA.AddUnknownConsumer("CW_A");
			eSpaceA.AddUnknownConsumer("CW_X");
			eSpaceA.AddUnknownConsumer("CS_A");

			eSpaceCenter.AddOrUpdateESpace(eSpaceA);

			var eSpaceB = new ESpace(2, "B");

			eSpaceB.AddUnknownConsumer("CW_B");
			eSpaceB.AddUnknownConsumer("CW_X");

			eSpaceCenter.AddOrUpdateESpace(eSpaceB);

			var eSpaceX = new ESpace(3, "CW_X");

			eSpaceCenter.AddOrUpdateESpace(eSpaceX);

			// test

			eSpaceCenter.AddOrUpdateESpace(new ESpace(3, "CW_XX"));

			// assert

			Assert.IsNotNull(eSpaceA.GetConsumers().FirstOrDefault(c => c.Name == "CW_XX"));
			Assert.IsNotNull(eSpaceB.GetConsumers().FirstOrDefault(c => c.Name == "CW_XX"));
		}

		[Test]
		public void AddConsumers_ConsumerAlreadyKnown_IsAddedAsKnownConsumer()
		{
			var eSpaceCenter = new ESpaceCenter();

			// preparing

			var eSpaceCWB = new ESpace(1, "CW_B");

			eSpaceCWB.AddUnknownConsumer("CS_B");
			eSpaceCWB.AddUnknownConsumer("IS_X");

			eSpaceCenter.AddOrUpdateESpace(eSpaceCWB);

			var eSpaceB = new ESpace(2, "B");

			eSpaceCenter.AddOrUpdateESpace(eSpaceB);

			// test
			eSpaceCenter.AddConsumers(2, new[] { "CW_B", "CS_B" });

			// assert

			CollectionAssert.AreEquivalent(new[] { "CS_B" }, eSpaceB.GetUnknownConsumers());
			Assert.IsNotNull(eSpaceB.GetConsumers().FirstOrDefault(c => c.Name == "CW_B"));
		}
	}
}
