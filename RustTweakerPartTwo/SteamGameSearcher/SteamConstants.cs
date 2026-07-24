using System;

namespace SteamGameSearcher
{
	// Token: 0x0200004E RID: 78
	public static class SteamConstants
	{
		// Token: 0x020000DD RID: 221
		public static class Registry
		{
			// Token: 0x0400031E RID: 798
			public static readonly string[] InstallPathKeys = new string[] { "HKEY_LOCAL_MACHINE\\SOFTWARE\\Wow6432Node\\Valve\\Steam", "HKEY_LOCAL_MACHINE\\SOFTWARE\\Valve\\Steam" };

			// Token: 0x0400031F RID: 799
			public const string InstallPathValue = "InstallPath";
		}

		// Token: 0x020000DE RID: 222
		public static class Paths
		{
			// Token: 0x04000320 RID: 800
			public static readonly string[] LibraryFoldersLocations = new string[] { "steamapps\\libraryfolders.vdf", "config\\libraryfolders.vdf" };

			// Token: 0x04000321 RID: 801
			public const string SteamAppsFolder = "steamapps";

			// Token: 0x04000322 RID: 802
			public const string CommonFolder = "common";

			// Token: 0x04000323 RID: 803
			public const string AppManifestTemplate = "appmanifest_{0}.acf";
		}

		// Token: 0x020000DF RID: 223
		public static class VdfKeys
		{
			// Token: 0x04000324 RID: 804
			public const string Path = "path";

			// Token: 0x04000325 RID: 805
			public const string InstallDir = "installdir";

			// Token: 0x04000326 RID: 806
			public const string AppId = "appid";

			// Token: 0x04000327 RID: 807
			public const string Name = "name";
		}

		// Token: 0x020000E1 RID: 225
		public static class Regex
		{
			// Token: 0x04000328 RID: 808
			public const string PathPattern = "\"path\"\\s+\"([^\"]+)\"";

			// Token: 0x04000329 RID: 809
			public const string InstallDirPattern = "\"installdir\"\\s+\"([^\"]+)\"";
		}
	}
}
