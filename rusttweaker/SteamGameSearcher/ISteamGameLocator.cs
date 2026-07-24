using System;
using System.Collections.Generic;

namespace SteamGameSearcher
{
	// Token: 0x0200004C RID: 76
	public interface ISteamGameLocator
	{
		// Token: 0x060002A1 RID: 673
		string FindGameInstallPath(int appId);

		// Token: 0x060002A2 RID: 674
		IEnumerable<InstalledGame> GetAllInstalledGames();
	}
}
