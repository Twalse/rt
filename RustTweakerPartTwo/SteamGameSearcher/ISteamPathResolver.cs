using System;
using System.Collections.Generic;

namespace SteamGameSearcher
{
	// Token: 0x02000049 RID: 73
	public interface ISteamPathResolver
	{
		// Token: 0x06000292 RID: 658
		string GetSteamInstallPath();

		// Token: 0x06000293 RID: 659
		IEnumerable<string> GetLibraryFolders(string steamPath);
	}
}
