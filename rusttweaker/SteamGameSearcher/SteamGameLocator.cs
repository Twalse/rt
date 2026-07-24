using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SteamGameSearcher
{
	// Token: 0x0200004F RID: 79
	public class SteamGameLocator : ISteamGameLocator
	{
		// Token: 0x060002A8 RID: 680 RVA: 0x0013AB7C File Offset: 0x0013837C
		public SteamGameLocator(ISteamPathResolver pathResolver, IFileSystemService fileSystem)
		{
			P4258EBF.AFA7138A.M6233B19[130](this);
			if (pathResolver == null)
			{
				throw P4258EBF.AFA7138A.M6233B19[562]("pathResolver");
			}
			this._pathResolver = pathResolver;
			if (fileSystem == null)
			{
				throw P4258EBF.AFA7138A.M6233B19[562]("fileSystem");
			}
			this._fileSystem = fileSystem;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00153A4C File Offset: 0x0015124C
		public string FindGameInstallPath(int appId)
		{
			string steamInstallPath = this._pathResolver.GetSteamInstallPath();
			if (P4258EBF.AFA7138A.M6233B19[88](steamInstallPath))
			{
				return null;
			}
			IEnumerable<string> libraryFolders = this._pathResolver.GetLibraryFolders(steamInstallPath);
			using (IEnumerator<string> enumerator = libraryFolders.GetEnumerator())
			{
				while (P4258EBF.AFA7138A.M6233B19[411](enumerator))
				{
					string text = enumerator.Current;
					string text2 = this.TryFindGameInLibrary(text, appId);
					if (text2 != null)
					{
						return text2;
					}
				}
			}
			return null;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0015BC44 File Offset: 0x00159444
		private string TryFindGameInLibrary(string libraryPath, int appId)
		{
			string text = P4258EBF.AFA7138A.M6233B19[278](libraryPath, "steamapps", P4258EBF.AFA7138A.M6233B19[440]("appmanifest_{0}.acf", appId));
			if (!this._fileSystem.FileExists(text))
			{
				return null;
			}
			string text2 = this.ParseInstallDirectory(text);
			if (P4258EBF.AFA7138A.M6233B19[88](text2))
			{
				return null;
			}
			string text3 = P4258EBF.AFA7138A.M6233B19[459](libraryPath, "steamapps", "common", text2);
			if (!this._fileSystem.DirectoryExists(text3))
			{
				return null;
			}
			return text3;
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0015F964 File Offset: 0x0015D164
		private string ParseInstallDirectory(string manifestPath)
		{
			string text = this._fileSystem.ReadAllText(manifestPath);
			Match match = P4258EBF.AFA7138A.M6233B19[444](text, "\"installdir\"\\s+\"([^\"]+)\"");
			if (!P4258EBF.AFA7138A.M6233B19[494](match))
			{
				return null;
			}
			return P4258EBF.AFA7138A.M6233B19[372](P4258EBF.AFA7138A.M6233B19[212](P4258EBF.AFA7138A.M6233B19[614](match), 1));
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00152C4C File Offset: 0x0015044C
		public IEnumerable<InstalledGame> GetAllInstalledGames()
		{
			List<InstalledGame> list = new List<InstalledGame>();
			string steamInstallPath = this._pathResolver.GetSteamInstallPath();
			if (P4258EBF.AFA7138A.M6233B19[88](steamInstallPath))
			{
				return list;
			}
			IEnumerable<string> libraryFolders = this._pathResolver.GetLibraryFolders(steamInstallPath);
			using (IEnumerator<string> enumerator = libraryFolders.GetEnumerator())
			{
				while (P4258EBF.AFA7138A.M6233B19[411](enumerator))
				{
					string text = enumerator.Current;
					string text2 = P4258EBF.AFA7138A.M6233B19[158](text, "steamapps");
					if (this._fileSystem.DirectoryExists(text2))
					{
						string[] array = P4258EBF.AFA7138A.M6233B19[526](text2, "appmanifest_*.acf");
						foreach (string text3 in array)
						{
							InstalledGame installedGame = this.ParseGameManifest(text3, text);
							if (installedGame != null)
							{
								list.Add(installedGame);
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00149518 File Offset: 0x00146D18
		private InstalledGame ParseGameManifest(string manifestPath, string libraryPath)
		{
			InstalledGame installedGame;
			try
			{
				string text = this._fileSystem.ReadAllText(manifestPath);
				Match match = P4258EBF.AFA7138A.M6233B19[444](text, "\"appid\"\\s+\"(\\d+)\"");
				Match match2 = P4258EBF.AFA7138A.M6233B19[444](text, "\"name\"\\s+\"([^\"]+)\"");
				Match match3 = P4258EBF.AFA7138A.M6233B19[444](text, "\"installdir\"\\s+\"([^\"]+)\"");
				if (!P4258EBF.AFA7138A.M6233B19[494](match) || !P4258EBF.AFA7138A.M6233B19[494](match3))
				{
					installedGame = null;
				}
				else
				{
					string text2 = P4258EBF.AFA7138A.M6233B19[372](P4258EBF.AFA7138A.M6233B19[212](P4258EBF.AFA7138A.M6233B19[614](match3), 1));
					string text3 = P4258EBF.AFA7138A.M6233B19[459](libraryPath, "steamapps", "common", text2);
					installedGame = new InstalledGame
					{
						AppId = P4258EBF.AFA7138A.M6233B19[214](P4258EBF.AFA7138A.M6233B19[372](P4258EBF.AFA7138A.M6233B19[212](P4258EBF.AFA7138A.M6233B19[614](match), 1))),
						Name = (P4258EBF.AFA7138A.M6233B19[494](match2) ? P4258EBF.AFA7138A.M6233B19[372](P4258EBF.AFA7138A.M6233B19[212](P4258EBF.AFA7138A.M6233B19[614](match2), 1)) : "Unknown"),
						InstallDir = text2,
						InstallPath = (this._fileSystem.DirectoryExists(text3) ? text3 : null)
					};
				}
			}
			catch
			{
				installedGame = null;
			}
			return installedGame;
		}

		// Token: 0x040000D4 RID: 212
		private readonly ISteamPathResolver _pathResolver;

		// Token: 0x040000D5 RID: 213
		private readonly IFileSystemService _fileSystem;
	}
}
