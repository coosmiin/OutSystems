using Moq;
using StretchOS.ServiceCenter.Commands;
using StretchOS.ServiceCenter.Commands.Runner;
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
			commandMock.Setup(c => c.Validate()).Returns(Mock.Of<CommandValidationResult>(r => r.IsValid == true));

			var commandRunner = new CommandRunner(commandMock.Object, Mock.Of<TextWriter>());
			commandRunner.Run();

			commandMock.Verify(c => c.Execute(), Times.Once);
		}

		[Fact]
		public void Run_CommandIsNotValid_CommandIsNotExecuted()
		{
			var commandMock = new Mock<ICommand>();
			commandMock.Setup(c => c.Validate()).Returns(Mock.Of<CommandValidationResult>(r => r.IsValid == false));

			var commandRunner = new CommandRunner(commandMock.Object, Mock.Of<TextWriter>());
			commandRunner.Run();

			commandMock.Verify(c => c.Execute(), Times.Never);
		}

		[Fact]
		public void Run_CommandIsNotValid_DescriptionIsWrittenToOutput()
		{
			var writer = new StringWriter();

			var commandMock = new Mock<ICommand>();
			commandMock
				.Setup(c => c.Validate())
				.Returns(Mock.Of<CommandValidationResult>(
					r => r.IsValid == false && r.ValidationText == "CMD_DESCR"));

			var commandRunner = new CommandRunner(commandMock.Object, writer);
			commandRunner.Run();

			Assert.Equal("CMD_DESCR\r\n", writer.ToString());
		}

		[Fact]
		public void Run_CommandIsValidWithValidationWarning_WarningIsWrittenToOutput()
		{
			var writer = new StringWriter();

			var commandMock = new Mock<ICommand>();
			commandMock
				.Setup(c => c.Validate())
				.Returns(Mock.Of<CommandValidationResult>(r => r.IsValid == true && r.ValidationText == "WARNING"));

			var commandRunner = new CommandRunner(commandMock.Object, writer);
			commandRunner.Run();

			Assert.Equal("WARNING\r\n", writer.ToString());
		}
	}
}
