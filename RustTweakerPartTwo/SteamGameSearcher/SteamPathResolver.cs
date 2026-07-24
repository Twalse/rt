using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SteamGameSearcher
{
	// Token: 0x02000050 RID: 80
	internal class SteamPathResolver : ISteamPathResolver
	{
		// Token: 0x060002AE RID: 686 RVA: 0x0015F3B8 File Offset: 0x0015CBB8
		public SteamPathResolver(IFileSystemService fileSystem)
		{
			P4258EBF.AFA7138A.M6233B19[130](this);
			this._fileSystem = fileSystem;
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00150368 File Offset: 0x0014DB68
		public string GetSteamInstallPath()
		{
			foreach (string text in SteamConstants.Registry.InstallPathKeys)
			{
				string text2 = P4258EBF.AFA7138A.M6233B19[180](text, "InstallPath", null) as string;
				if (!P4258EBF.AFA7138A.M6233B19[88](text2))
				{
					return text2;
				}
			}
			return null;
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000DC1C File Offset: 0x0000C01C
		public IEnumerable<string> GetLibraryFolders(string steamPath)
		{
			List<string> list = new List<string> { steamPath };
			string text = SteamConstants.Paths.LibraryFoldersLocations.Select<string, string>((string loc) => P4258EBF.AFA7138A.M6233B19[158](steamPath, loc)).FirstOrDefault<string>(new Func<string, bool>(this._fileSystem.FileExists));
			if (text != null)
			{
				string text2 = this._fileSystem.ReadAllText(text);
				list.AddRange(this.ParseLibraryPaths(text2));
			}
			return list.Distinct<string>();
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00159DA4 File Offset: 0x001575A4
		private IEnumerable<string> ParseLibraryPaths(string vdfContent)
		{
			MatchCollection matchCollection = P4258EBF.AFA7138A.M6233B19[602](vdfContent, "\"path\"\\s+\"([^\"]+)\"");
			return (from Match m in matchCollection
				select P4258EBF.AFA7138A.M6233B19[114](P4258EBF.AFA7138A.M6233B19[372](P4258EBF.AFA7138A.M6233B19[212](P4258EBF.AFA7138A.M6233B19[614](m), 1)), "\\\\", "\\")).Where<string>(new Func<string, bool>(this._fileSystem.DirectoryExists));
		}

		// Token: 0x040000D6 RID: 214
		private readonly IFileSystemService _fileSystem;
	}
}
