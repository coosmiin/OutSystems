using Moq;
using StretchOS.ServiceCenter.Commands;
using StretchOS.ServiceCenter.Commands.Runner;
using StretchOS.ServiceCenter.Validation;
using System;
using System.IO;
using Xunit;

namespace StretchOS.ServiceCenter.UnitTests.Commands.Runner
{
	public class CommandRunnerTests
	{
		[Fact]
		public void Run_CommandIsValid_CommandIsExecuted()
		{
			var commandMock = new Mock<ICommand>();
			commandMock.Setup(c => c.Validate()).Returns(Mock.Of<ValidationResult>(r => r.IsValid == true));

			var commandRunner = new CommandRunner(commandMock.Object, Mock.Of<TextWriter>());
			commandRunner.Run();

			commandMock.Verify(c => c.Execute(), Times.Once);
		}

		[Fact]
		public void Run_CommandIsNotValid_CommandIsNotExecuted()
		{
			var commandMock = new Mock<ICommand>();
			commandMock.Setup(c => c.Validate()).Returns(Mock.Of<ValidationResult>(r => r.IsValid == false));

			var commandRunner = new CommandRunner(commandMock.Object, Mock.Of<TextWriter>());
			commandRunner.Run();

			commandMock.Verify(c => c.Execute(), Times.Never);
		}

		[Fact]
		public void Run_CommandIsNotValid_ValidationTextIsWrittenToOutput()
		{
			var writer = new StringWriter();

			string message = "CMD_VLD_MSG";

			var commandMock = new Mock<ICommand>();
			commandMock
				.Setup(c => c.Validate())
				.Returns(Mock.Of<ValidationResult>(
					r => r.IsValid == false && r.ValidationText == message));

			var commandRunner = new CommandRunner(commandMock.Object, writer);
			commandRunner.Run();

			// Validation message is on first line
			Assert.Equal(message, writer.ToString().Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0]);
		}

		[Fact]
		public void Run_CommandIsValidWithValidationWarning_WarningIsWrittenToOutput()
		{
			var writer = new StringWriter();

			var commandMock = new Mock<ICommand>();
			commandMock
				.Setup(c => c.Validate())
				.Returns(Mock.Of<ValidationResult>(r => r.IsValid == true && r.ValidationText == "WARNING"));

			var commandRunner = new CommandRunner(commandMock.Object, writer);
			commandRunner.Run();

			Assert.Equal("WARNING\r\n", writer.ToString());
		}

		[Fact]
		public void Run_CommandIsNotValid_GetUsageDescriptionIsExecuted()
		{
			var commandMock = new Mock<ICommand>();
			commandMock.Setup(c => c.Validate()).Returns(Mock.Of<ValidationResult>(r => r.IsValid == false));

			var commandRunner = new CommandRunner(commandMock.Object, Mock.Of<TextWriter>());
			commandRunner.Run();

			commandMock.Verify(c => c.GetUsageDescription(), Times.Once);
		}
	}
}
