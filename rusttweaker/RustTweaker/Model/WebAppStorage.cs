using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using WpfApp1.Model;

namespace RustTweaker.Model
{
	// Token: 0x02000046 RID: 70
	public static class WebAppStorage
	{
		// Token: 0x06000284 RID: 644 RVA: 0x0015F088 File Offset: 0x0015C888
		static WebAppStorage()
		{
			WebAppStorage.EnsureExists();
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0015C9AC File Offset: 0x0015A1AC
		private static void EnsureExists()
		{
			if (!P4258EBF.AFA7138A.M6233B19[627](WebAppStorage._filePath))
			{
				StorageSchema storageSchema = new StorageSchema();
				WebAppStorage.Save(storageSchema);
			}
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0015F868 File Offset: 0x0015D068
		private static void Save(StorageSchema data)
		{
			string text = P4258EBF.AFA7138A.M6233B19[412](data, Formatting.Indented);
			P4258EBF.AFA7138A.M6233B19[94](WebAppStorage._filePath, text);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0015B8B8 File Offset: 0x001590B8
		public static List<string> GetFavouritesCommands()
		{
			List<string> list;
			try
			{
				string text = P4258EBF.AFA7138A.M6233B19[267](WebAppStorage._filePath);
				StorageSchema storageSchema = JsonConvert.DeserializeObject<StorageSchema>(text);
				list = ((storageSchema != null) ? storageSchema.FavouritesCommand : null) ?? new List<string>();
			}
			catch
			{
				list = new List<string>();
			}
			return list;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000D5E8 File Offset: 0x0000B9E8
		public static void UpdateFavouritesCommands(string[] newArray)
		{
			StorageSchema storageSchema = new StorageSchema
			{
				FavouritesCommand = (new List<string>(newArray) ?? new List<string>())
			};
			WebAppStorage.Save(storageSchema);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000D618 File Offset: 0x0000BA18
		public static void UpdateFavouritesCommands(List<string> newArray)
		{
			StorageSchema storageSchema = new StorageSchema
			{
				FavouritesCommand = (newArray ?? new List<string>())
			};
			WebAppStorage.Save(storageSchema);
		}

		// Token: 0x040000CC RID: 204
		private static readonly string _filePath = P4258EBF.AFA7138A.M6233B19[158](MainLogic.appDataPath, "storage.json");
	}
}
