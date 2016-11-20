using StretchOS.Proxy.Sniffers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Titanium.Web.Proxy;
using Titanium.Web.Proxy.EventArguments;
using Titanium.Web.Proxy.Models;

namespace StretchOS.Proxy.WebProxy
{
	public class OsWebProxy : IWebProxy
	{
		private readonly ProxyServer _proxyServer;
		private readonly IList<IRequestSniffer> _requestSniffers = new List<IRequestSniffer>();

		public OsWebProxy()
		{
			_proxyServer = new ProxyServer();
		}

		public void StartProxy()
		{
			_proxyServer.TrustRootCertificate = true;
			_proxyServer.BeforeRequest += OnBeforeRequest;
			_proxyServer.BeforeResponse += OnBeforeResponse;

			// TODO: Port should be read from configuration
			var explicitEndPoint = new ExplicitProxyEndPoint(IPAddress.Any, 8000, true);
			_proxyServer.AddEndPoint(explicitEndPoint);

			// Set Fiddler as UpStream proxy
			//_proxyServer.UpStreamHttpProxy = new ExternalProxy { HostName = "localhost", Port = 8888 };
			//_proxyServer.UpStreamHttpsProxy = new ExternalProxy { HostName = "localhost", Port = 8888 };

			_proxyServer.Start();

			_proxyServer.SetAsSystemHttpProxy(explicitEndPoint);
			_proxyServer.SetAsSystemHttpsProxy(explicitEndPoint);
		}

		public void StopProxy()
		{
			_proxyServer.BeforeRequest -= OnBeforeRequest;
			_proxyServer.BeforeResponse -= OnBeforeResponse;

			_proxyServer.Stop();
		}

		public void RegisterSniffer(IRequestSniffer sniffer)
		{
			_requestSniffers.Add(sniffer);
		}

		private async Task OnBeforeRequest(object sender, SessionEventArgs e)
		{
			foreach (IRequestSniffer sniffer in _requestSniffers)
			{
				await sniffer.OnBeforeRequest(e);
			}
		}

		private async Task OnBeforeResponse(object sender, SessionEventArgs e)
		{
			foreach (IRequestSniffer sniffer in _requestSniffers)
			{
				await sniffer.OnBeforeResponse(e);
			}
		}
	}
}
