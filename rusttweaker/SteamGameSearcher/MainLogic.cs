using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using RustTweaker;

namespace SteamGameSearcher
{
	// Token: 0x0200004D RID: 77
	public class MainLogic
	{
		// Token: 0x060002A3 RID: 675 RVA: 0x0000D776 File Offset: 0x0000BB76
		public MainLogic()
			: this(MainLogic.CreateGameLocator(), new FileSystemService())
		{
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00154A98 File Offset: 0x00152298
		public MainLogic(ISteamGameLocator gameLocator, IFileSystemService fileSystem)
		{
			P4258EBF.AFA7138A.M6233B19[130](this);
			if (gameLocator == null)
			{
				throw P4258EBF.AFA7138A.M6233B19[562]("gameLocator");
			}
			this._gameLocator = gameLocator;
			if (fileSystem == null)
			{
				throw P4258EBF.AFA7138A.M6233B19[562]("fileSystem");
			}
			this._fileSystem = fileSystem;
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000D7BC File Offset: 0x0000BBBC
		private static ISteamGameLocator CreateGameLocator()
		{
			FileSystemService fileSystemService = new FileSystemService();
			SteamPathResolver steamPathResolver = new SteamPathResolver(fileSystemService);
			return new SteamGameLocator(steamPathResolver, fileSystemService);
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x001584C8 File Offset: 0x00155CC8
		private string HandleSearchResult(string gamePath, int appId)
		{
			if (!P4258EBF.AFA7138A.M6233B19[88](gamePath))
			{
				Logger.Log("Игра найдена!");
				return gamePath;
			}
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			P4258EBF.AFA7138A.M6233B19[466](ref defaultInterpolatedStringHandler, 71, 1);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "Игра с App ID ");
			defaultInterpolatedStringHandler.AppendFormatted<int>(appId);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, " не найдена.\nУбедитесь, что игра установлена через Steam.");
			Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler));
			return null;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000D838 File Offset: 0x0000BC38
		public string FindGame(int appId)
		{
			try
			{
				IEnumerable<InstalledGame> allInstalledGames = this._gameLocator.GetAllInstalledGames();
				string text = this._gameLocator.FindGameInstallPath(appId);
				return this.HandleSearchResult(text, appId);
			}
			catch (Exception ex)
			{
				Logger.Log("Ошибка при поиске игры");
				Logger.Log(ex);
			}
			return null;
		}

		// Token: 0x040000D2 RID: 210
		private readonly ISteamGameLocator _gameLocator;

		// Token: 0x040000D3 RID: 211
		private readonly IFileSystemService _fileSystem;
	}
}
