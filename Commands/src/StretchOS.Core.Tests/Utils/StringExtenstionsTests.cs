using StretchOS.Core.Utils;
using System;
using Xunit;

namespace StretchOS.Core.Tests.Utils
{
	public class StringExtenstionsTests
	{
		[Fact]
		public void ToFileNameCompliant_NullString_DoesThrowException()
		{
			string text = null;

			Exception ex = Record.Exception(
				() => { try { text.ToFileNameCompliant(); } catch (Exception e) { throw e; } });

			Assert.Null(ex);
		}
	}
}
