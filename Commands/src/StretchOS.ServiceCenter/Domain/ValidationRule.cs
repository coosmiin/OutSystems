using System;

namespace StretchOS.ServiceCenter.Domain
{
	public class ValidationRule
	{
		public Func<string, bool> Rule { get; set; }
		public string ErrorMessageFormat { get; set; }
	}
}
