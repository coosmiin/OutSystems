using StretchOS.ServiceCenter.Domain;

namespace StretchOS.ServiceCenter.WebProxy
{
	public interface IServiceCenterWebProxy
	{
		void DownloadErrorLog(SearchSettings settings);
	}
}