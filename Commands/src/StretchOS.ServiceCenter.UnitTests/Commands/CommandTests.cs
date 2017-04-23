using System;
using StretchOS.ServiceCenter.Commands;
using Xunit;
using System.IO;
using Moq;
using Moq.Protected;

namespace StretchOS.ServiceCenter.UnitTests.Commands
{
	public class CommandTests
	{
		[Fact]
		public void Ctor_ValidationPasses_NoExplanationIsWrittenToOutput()
		{
			var writer = new StringWriter();

			var commandStub = new Mock<Command>(writer);

			commandStub.Protected().Setup<bool>("IsValid").Returns(true);
			commandStub.Protected().Setup<string>("GetDescription").Returns("CMD_DESCR");

			var mockObject = commandStub.Object;

			Assert.Equal(string.Empty, writer.ToString());
		}

		[Fact]
		public void Ctor_ValidationFails_DescriptionIsWrittenToOutput()
		{
			var writer = new StringWriter();

			var commandStub = new Mock<Command>(writer);

			string description = "CMD_DESCR";

			commandStub.Protected().Setup<bool>("IsValid").Returns(false);
			commandStub.Protected().Setup<string>("GetDescription").Returns(description);

			var mockObject = commandStub.Object;

			Assert.Equal(description, writer.ToString());
		}
	}
}
