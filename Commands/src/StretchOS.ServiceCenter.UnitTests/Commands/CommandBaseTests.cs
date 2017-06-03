using Moq;
using Moq.Protected;
using StretchOS.ServiceCenter.Commands;
using StretchOS.ServiceCenter.Validation;
using Xunit;

namespace StretchOS.ServiceCenter.UnitTests.Commands
{
	public class CommandBaseTests
	{
		[Fact]
		public void Validate_UnknownParam_IsNotValid()
		{
			string unknownParam = "unknown-param";

			var commandStub = new Mock<CommandBase>(unknownParam, "sss");
			commandStub.Protected()
				.Setup<CommandParameter[]>("GetCommandParameters")
				.Returns(new[] { new CommandParameter { ValidationRule = new ValidationRule() { Rule = _ => false } } });

			var result = commandStub.Object.Validate();

			Assert.False(result.IsValid);
		}

		[Fact]
		public void Validate_MultipleUnknownParams_ValidationErrorForFirstUnknownParam()
		{
			string unknownParam = "unknown-param";

			var commandStub = new Mock<CommandBase>(unknownParam, "sss");
			commandStub.Protected()
				.Setup<CommandParameter[]>("GetCommandParameters")
				.Returns(
					new[] {
						new CommandParameter
						{
							ValidationRule = new ValidationRule()
							{
								Rule = _ => false,
								ErrorMessageFormat =  "Unknown parameter: {0}"
							}
						}
					});

			var result = commandStub.Object.Validate();

			Assert.False(result.IsValid);
			Assert.Contains($"Unknown parameter: {unknownParam}", result.ValidationText);
		}

		[Fact]
		public void GetUsageDescription_DescriptionContainsCommandAndParams()
		{
			string command = "myCommand", paramA = "paramA", paramB = "paramB";

			var commandStub = new Mock<CommandBase>();
			commandStub.Protected()
				.Setup<CommandParameter[]>("GetCommandParameters")
				.Returns(new[] { new CommandParameter { Name = paramA }, new CommandParameter { Name = paramB } });

			commandStub.Protected()
				.Setup<string>("GetCommandName").Returns(command);

			var result = commandStub.Object.GetUsageDescription();

			Assert.Equal($"{command} [{paramA}] [{paramB}]", result);
		}

		[Fact]
		public void GetUsageDescription_WithFixedValuesParams_DescriptionContainsFixedValues()
		{
			string command = "myCommand", paramA = "paramA", paramB = "paramB";
			string[] fixedParamValues = new[] { "value1", "value2" };

			var commandStub = new Mock<CommandBase>();
			commandStub.Protected()
				.Setup<CommandParameter[]>("GetCommandParameters")
				.Returns(
					new[] 
					{
						new CommandParameter { Name = "Whatever", AllowedValues = fixedParamValues },
						new CommandParameter { Name = paramA },
						new CommandParameter { Name = paramB }
					});

			commandStub.Protected()
				.Setup<string>("GetCommandName").Returns(command);

			var result = commandStub.Object.GetUsageDescription();
			var fixedParamsExpectedDescription = string.Join(" | ", fixedParamValues);

			Assert.Equal($"{command} [{fixedParamsExpectedDescription}] [{paramA}] [{paramB}]", result);
		}
	}
}
