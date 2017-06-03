using System;

namespace StretchOS.ServiceCenter.Validation
{
	public class ValidationRule
	{
		public Func<string, bool> Rule { get; set; }
		public string ErrorMessageFormat { get; set; } = string.Empty;
	}
}
