using OpenQA.Selenium;

namespace StretchOS.Selenium.WebDriver
{
	public interface IOSWebDriver
	{
		IOSWebDriver GoTo(string relativeUrl);
		IOSWebDriver Fill(By by, string text);
		IOSWebDriver Click(By by);

		bool ElementExists(By by, string value = null);
		string GetPageTitle();

		void Close();
	}
}