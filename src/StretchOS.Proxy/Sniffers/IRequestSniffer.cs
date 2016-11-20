using System.Threading.Tasks;
using Titanium.Web.Proxy.EventArguments;

namespace StretchOS.Proxy.Sniffers
{
	public interface IRequestSniffer
	{
		Task OnBeforeRequest(SessionEventArgs e);
		Task OnBeforeResponse(SessionEventArgs e);
	}
}
