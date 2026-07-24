using System;

namespace SteamGameSearcher
{
	// Token: 0x0200004A RID: 74
	public interface IFileSystemService
	{
		// Token: 0x06000294 RID: 660
		bool FileExists(string path);

		// Token: 0x06000295 RID: 661
		bool DirectoryExists(string path);

		// Token: 0x06000296 RID: 662
		string ReadAllText(string path);

		// Token: 0x06000297 RID: 663
		void OpenFolderInExplorer(string path);
	}
}
