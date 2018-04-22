using Moq;
using StretchOS.ServiceCenter.Commands;
using StretchOS.ServiceCenter.WebProxy;
using Xunit;

namespace StretchOS.ServiceCenter.UnitTests.Commands
{
	public class SolutionPublishCommandTests
	{
		[Fact]
		public void Validate_ValidSolutionIdParam_IsValid()
		{
			var command = new SolutionPublishCommand(Mock.Of<IServiceCenterWebProxy>(), "7");
			var result = command.Validate();

			Assert.True(result.IsValid);
		}

		[Fact]
		public void Validate_InvalidSolutionIdParam_CorrectMessageIsShown()
		{
			var command = new SolutionPublishCommand(Mock.Of<IServiceCenterWebProxy>(), "7u");
			var result = command.Validate();

			Assert.False(result.IsValid);
			Assert.Equal("[solutionId] should be an integer", result.ValidationText);
		}

	}
}
