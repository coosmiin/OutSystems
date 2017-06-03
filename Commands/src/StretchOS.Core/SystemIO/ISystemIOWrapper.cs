namespace StretchOS.Core.SystemIO
{
	public interface ISystemIOWrapper
	{
		bool FileExists(string path);
		void FileDelete(string path);
		bool DirectoryExists(string path);
		void CreateDirectory(string path);
	}
}