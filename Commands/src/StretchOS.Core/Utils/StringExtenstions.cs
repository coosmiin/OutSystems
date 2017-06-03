namespace StretchOS.Core.Utils
{
	public static class StringExtenstions
	{
		public static string ToFileNameCompliant(this string text)
		{
			if (string.IsNullOrEmpty(text))
				return string.Empty;

			return text.Replace("-", string.Empty).Replace(" ", "-").Replace(":", string.Empty);
		}
	}
}
