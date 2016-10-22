using System;
using System.Net;
using System.Threading.Tasks;
using Titanium.Web.Proxy;
using Titanium.Web.Proxy.EventArguments;
using Titanium.Web.Proxy.Models;

namespace StretchOS.Proxy
{
	public class OsProxy
	{
		public void StartProxy()
		{
			var proxyServer = new ProxyServer();
			proxyServer.TrustRootCertificate = true;

			proxyServer.BeforeRequest += ProxyServer_BeforeRequest;

			var explicitEndPoint = new ExplicitProxyEndPoint(IPAddress.Any, 8000, true);

			proxyServer.AddEndPoint(explicitEndPoint);

			proxyServer.Start();

			proxyServer.SetAsSystemHttpProxy(explicitEndPoint);
			proxyServer.SetAsSystemHttpsProxy(explicitEndPoint);
		}

		private async Task ProxyServer_BeforeRequest(object sender, SessionEventArgs e)
		{
			Console.WriteLine(e.WebSession.Request.Url);
		}
	}
}
