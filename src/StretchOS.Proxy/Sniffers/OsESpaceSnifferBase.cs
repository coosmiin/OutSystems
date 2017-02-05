using System;
using System.Threading.Tasks;
using Titanium.Web.Proxy.EventArguments;

namespace StretchOS.Proxy.Sniffers
{
	public abstract class OsESpaceSnifferBase : IWebSniffer
	{
		private const string SOAP_HEADER_NAME = "SOAPAction";

		// TODO: configurable url matcher for each sniffer
		private readonly string _baseUrl = "https://croman.outsystemscloud.com"; // "http://172.28.6.216";

		private readonly string _sniffedServiceMethodName;

		public OsESpaceSnifferBase(string sniffedServiceMethodName)
		{
			_sniffedServiceMethodName = sniffedServiceMethodName;
		}

		protected virtual void HandleResponseContent(SessionEventArgs e, string responseBody) { }
		protected virtual void HandleRequestContent(SessionEventArgs e, string requestBody) { }

		public virtual async Task OnBeforeRequest(SessionEventArgs e)
		{
			if (!IsValidRequest(e))
				return;

			string requestBody = string.Empty;
			try
			{
				requestBody = await e.GetRequestBodyAsString();

				HandleRequestContent(e, requestBody);
			}
			catch (Exception ex)
			{
				// TODO: proper logger should be implemented
				Console.WriteLine(ex.Message);
			}
		}

		public virtual async Task OnBeforeResponse(SessionEventArgs e)
		{
			if (!IsValidRequest(e))
				return;

			string responseBody = string.Empty;
			try
			{
				responseBody = await e.GetResponseBodyAsString();

				HandleResponseContent(e, responseBody);
			}
			catch (Exception ex)
			{
				// TODO: proper logger should be implemented
				Console.WriteLine(ex.Message);
			}
		}

		private bool IsValidRequest(SessionEventArgs e)
		{
			if (!e.WebSession.Request.Url.StartsWith(_baseUrl))
				return false;

			if (!e.WebSession.Request.RequestHeaders.ContainsKey(SOAP_HEADER_NAME)
				|| !e.WebSession.Request.RequestHeaders[SOAP_HEADER_NAME].Value.Contains(_sniffedServiceMethodName))
				return false;

			return true;
		}
	}
}
