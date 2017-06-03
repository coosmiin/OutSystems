using System;
using System.IO;

namespace StretchOS.Core.SystemIO
{
	public class SystemIOWrapper : ISystemIOWrapper
	{
		public void CreateDirectory(string path)
		{
			Directory.CreateDirectory(path);
		}

		public bool DirectoryExists(string path)
		{
			return Directory.Exists(path);
		}

		public void FileDelete(string path)
		{
			File.Delete(path);
		}

		public bool FileExists(string path)
		{
			return File.Exists(path);
		}
	}
}
