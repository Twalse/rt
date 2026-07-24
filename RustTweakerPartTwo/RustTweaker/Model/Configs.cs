using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using WpfApp1.Model;

namespace RustTweaker.Model
{
	// Token: 0x02000041 RID: 65
	public static class Configs
	{
		// Token: 0x0600025D RID: 605 RVA: 0x00149E1C File Offset: 0x0014761C
		public static bool CreateBackupToConfig(string path)
		{
			try
			{
				string text = P4258EBF.AFA7138A.M6233B19[158](MainLogic.appDataPath, "configs_backup.json");
				string text2 = P4258EBF.AFA7138A.M6233B19[158](MainLogic.appDataPath, "configs.json");
				List<Configs.ConfigGameBackupNode> list;
				if (!P4258EBF.AFA7138A.M6233B19[627](text))
				{
					list = new List<Configs.ConfigGameBackupNode>();
				}
				else
				{
					list = JsonConvert.DeserializeObject<List<Configs.ConfigGameBackupNode>>(P4258EBF.AFA7138A.M6233B19[267](text));
				}
				if (!P4258EBF.AFA7138A.M6233B19[89](Configs.BACKUP_FOLDER))
				{
					P4258EBF.AFA7138A.M6233B19[111](Configs.BACKUP_FOLDER);
				}
				if (!P4258EBF.AFA7138A.M6233B19[627](text2))
				{
					Logger.Log(P4258EBF.AFA7138A.M6233B19[478]("File not found then create backup: ", text2));
					return false;
				}
				List<Configs.ConfigNode> list2 = JsonConvert.DeserializeObject<List<Configs.ConfigNode>>(P4258EBF.AFA7138A.M6233B19[267](text2));
				Configs.ConfigNode configNode = list2.Find((Configs.ConfigNode x) => P4258EBF.AFA7138A.M6233B19[250](x.content, path));
				string text3 = ((configNode != null) ? configNode.launch_params : null);
				string text4 = P4258EBF.AFA7138A.M6233B19[124](path);
				L083B68C l083B68C = P4258EBF.AFA7138A.M6233B19[158];
				string backup_FOLDER = Configs.BACKUP_FOLDER;
				Guid guid = P4258EBF.AFA7138A.M6233B19[476]();
				string text5 = l083B68C(backup_FOLDER, P4258EBF.AFA7138A.M6233B19[478](guid.ToString(), text4));
				DateTimeOffset dateTimeOffset = P4258EBF.AFA7138A.M6233B19[446]();
				DateTimeOffset dateTimeOffset2;
				P4258EBF.AFA7138A.M6233B19[560](ref dateTimeOffset2, P4258EBF.AFA7138A.M6233B19[50](ref dateTimeOffset), P4258EBF.AFA7138A.M6233B19[413](ref dateTimeOffset), P4258EBF.AFA7138A.M6233B19[518](ref dateTimeOffset), 0, 0, 0, P4258EBF.AFA7138A.M6233B19[530](ref dateTimeOffset));
				DateTimeOffset dateTimeOffset3 = P4258EBF.AFA7138A.M6233B19[390](ref dateTimeOffset2, 1.0);
				long startTs = P4258EBF.AFA7138A.M6233B19[74](ref dateTimeOffset2);
				long endTs = P4258EBF.AFA7138A.M6233B19[74](ref dateTimeOffset3);
				Configs.ConfigGameBackupNode configGameBackupNode = list.Find((Configs.ConfigGameBackupNode x) => P4258EBF.AFA7138A.M6233B19[250](x.original_file, path) && (long)x.data >= startTs && (long)x.data < endTs);
				if (configGameBackupNode != null)
				{
					P4258EBF.AFA7138A.M6233B19[534](path, text5);
					configGameBackupNode.data = (int)P4258EBF.AFA7138A.M6233B19[74](ref dateTimeOffset);
					configGameBackupNode.backup_launch_params = text3;
				}
				else
				{
					P4258EBF.AFA7138A.M6233B19[534](path, text5);
					List<Configs.ConfigGameBackupNode> list3 = list;
					Configs.ConfigGameBackupNode configGameBackupNode2 = new Configs.ConfigGameBackupNode();
					configGameBackupNode2.original_file = path;
					configGameBackupNode2.backup_file = text5;
					configGameBackupNode2.backup_launch_params = text3;
					DateTimeOffset dateTimeOffset4 = P4258EBF.AFA7138A.M6233B19[166]();
					configGameBackupNode2.data = (int)P4258EBF.AFA7138A.M6233B19[74](ref dateTimeOffset4);
					list3.Add(configGameBackupNode2);
				}
				P4258EBF.AFA7138A.M6233B19[94](text, P4258EBF.AFA7138A.M6233B19[412](list, Formatting.Indented));
				return true;
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
			}
			return false;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0015249C File Offset: 0x0014FC9C
		public static bool CreateBackupToBind(string path)
		{
			try
			{
				string text = P4258EBF.AFA7138A.M6233B19[158](MainLogic.appDataPath, "keys_backup.json");
				string text2 = P4258EBF.AFA7138A.M6233B19[158](MainLogic.appDataPath, "configs.json");
				List<Configs.ConfigKeysBackupNode> list;
				if (!P4258EBF.AFA7138A.M6233B19[627](text))
				{
					list = new List<Configs.ConfigKeysBackupNode>();
				}
				else
				{
					list = JsonConvert.DeserializeObject<List<Configs.ConfigKeysBackupNode>>(P4258EBF.AFA7138A.M6233B19[267](text));
				}
				if (!P4258EBF.AFA7138A.M6233B19[89](Configs.BACKUP_FOLDER))
				{
					P4258EBF.AFA7138A.M6233B19[111](Configs.BACKUP_FOLDER);
				}
				if (!P4258EBF.AFA7138A.M6233B19[627](text2))
				{
					Logger.Log(P4258EBF.AFA7138A.M6233B19[478]("File not found then create backup: ", text2));
					return false;
				}
				List<Configs.ConfigNode> list2 = JsonConvert.DeserializeObject<List<Configs.ConfigNode>>(P4258EBF.AFA7138A.M6233B19[267](text2));
				DateTimeOffset dateTimeOffset = P4258EBF.AFA7138A.M6233B19[166]();
				int num = (int)P4258EBF.AFA7138A.M6233B19[74](ref dateTimeOffset);
				string text3 = P4258EBF.AFA7138A.M6233B19[124](path);
				L083B68C l083B68C = P4258EBF.AFA7138A.M6233B19[158];
				string backup_FOLDER = Configs.BACKUP_FOLDER;
				Guid guid = P4258EBF.AFA7138A.M6233B19[476]();
				string text4 = l083B68C(backup_FOLDER, P4258EBF.AFA7138A.M6233B19[478](guid.ToString(), text3));
				DateTimeOffset dateTimeOffset2 = P4258EBF.AFA7138A.M6233B19[446]();
				DateTimeOffset dateTimeOffset3;
				P4258EBF.AFA7138A.M6233B19[560](ref dateTimeOffset3, P4258EBF.AFA7138A.M6233B19[50](ref dateTimeOffset2), P4258EBF.AFA7138A.M6233B19[413](ref dateTimeOffset2), P4258EBF.AFA7138A.M6233B19[518](ref dateTimeOffset2), 0, 0, 0, P4258EBF.AFA7138A.M6233B19[530](ref dateTimeOffset2));
				DateTimeOffset dateTimeOffset4 = P4258EBF.AFA7138A.M6233B19[390](ref dateTimeOffset3, 1.0);
				long startTs = P4258EBF.AFA7138A.M6233B19[74](ref dateTimeOffset3);
				long endTs = P4258EBF.AFA7138A.M6233B19[74](ref dateTimeOffset4);
				Configs.ConfigKeysBackupNode configKeysBackupNode = list.Find((Configs.ConfigKeysBackupNode x) => P4258EBF.AFA7138A.M6233B19[250](x.original_file, path) && (long)x.data >= startTs && (long)x.data < endTs);
				if (configKeysBackupNode != null)
				{
					P4258EBF.AFA7138A.M6233B19[534](path, text4);
					configKeysBackupNode.data = (int)P4258EBF.AFA7138A.M6233B19[74](ref dateTimeOffset2);
				}
				else
				{
					P4258EBF.AFA7138A.M6233B19[534](path, text4);
					List<Configs.ConfigKeysBackupNode> list3 = list;
					Configs.ConfigKeysBackupNode configKeysBackupNode2 = new Configs.ConfigKeysBackupNode();
					configKeysBackupNode2.original_file = path;
					configKeysBackupNode2.backup_file = text4;
					dateTimeOffset = P4258EBF.AFA7138A.M6233B19[166]();
					configKeysBackupNode2.data = (int)P4258EBF.AFA7138A.M6233B19[74](ref dateTimeOffset);
					list3.Add(configKeysBackupNode2);
				}
				P4258EBF.AFA7138A.M6233B19[94](text, P4258EBF.AFA7138A.M6233B19[412](list, Formatting.Indented));
				return true;
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
			}
			return false;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00151B38 File Offset: 0x0014F338
		public static Configs.PathsNode[] ParsePathsConfig(string path_to_file = null)
		{
			if (P4258EBF.AFA7138A.M6233B19[88](path_to_file))
			{
				path_to_file = P4258EBF.AFA7138A.M6233B19[158](MainLogic.appDataPath, "paths.json");
			}
			if (P4258EBF.AFA7138A.M6233B19[88](path_to_file))
			{
				return null;
			}
			Configs.PathsNode[] array;
			try
			{
				string text = P4258EBF.AFA7138A.M6233B19[267](path_to_file);
				array = JsonConvert.DeserializeObject<List<Configs.PathsNode>>(text).ToArray();
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
				array = null;
			}
			return array;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00147B48 File Offset: 0x00145348
		public static Configs.ConfigNode[] ParseConfigsConfig(string path_to_file = null)
		{
			if (P4258EBF.AFA7138A.M6233B19[88](path_to_file))
			{
				path_to_file = P4258EBF.AFA7138A.M6233B19[158](MainLogic.appDataPath, "configs.json");
			}
			if (P4258EBF.AFA7138A.M6233B19[88](path_to_file))
			{
				return null;
			}
			Configs.ConfigNode[] array;
			try
			{
				string text = P4258EBF.AFA7138A.M6233B19[267](path_to_file);
				array = JsonConvert.DeserializeObject<List<Configs.ConfigNode>>(text).ToArray();
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
				array = null;
			}
			return array;
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000CFD0 File Offset: 0x0000B3D0
		public static string getCurrentSelectedFolder()
		{
			string text;
			try
			{
				Configs.PathsNode pathsNode = Configs.ParsePathsConfig(null).FirstOrDefault<Configs.PathsNode>((Configs.PathsNode x) => x.is_select);
				text = ((pathsNode != null) ? pathsNode.folder : null);
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
				text = null;
			}
			return text;
		}

		// Token: 0x040000C5 RID: 197
		public static string BACKUP_FOLDER = P4258EBF.AFA7138A.M6233B19[158](MainLogic.appDataPath, "backups");

		// Token: 0x020000CB RID: 203
		public class PathsNode
		{
			// Token: 0x06000505 RID: 1285 RVA: 0x0015F4A0 File Offset: 0x0015CCA0
			public PathsNode()
			{
				P4258EBF.AFA7138A.M6233B19[130](this);
			}

			// Token: 0x040002CC RID: 716
			public bool is_select;

			// Token: 0x040002CD RID: 717
			public bool have_warn;

			// Token: 0x040002CE RID: 718
			public string folder;
		}

		// Token: 0x020000CC RID: 204
		public class ConfigNode
		{
			// Token: 0x06000506 RID: 1286 RVA: 0x00161610 File Offset: 0x0015EE10
			public ConfigNode()
			{
				P4258EBF.AFA7138A.M6233B19[130](this);
			}

			// Token: 0x040002CF RID: 719
			public bool is_select;

			// Token: 0x040002D0 RID: 720
			public string name;

			// Token: 0x040002D1 RID: 721
			public string content;

			// Token: 0x040002D2 RID: 722
			public string launch_params;
		}

		// Token: 0x020000CD RID: 205
		public class BindNode
		{
			// Token: 0x06000507 RID: 1287 RVA: 0x00144C8C File Offset: 0x0014248C
			public BindNode()
			{
				P4258EBF.AFA7138A.M6233B19[130](this);
			}

			// Token: 0x040002D3 RID: 723
			public bool is_select;

			// Token: 0x040002D4 RID: 724
			public string name;

			// Token: 0x040002D5 RID: 725
			public string content;
		}

		// Token: 0x020000CE RID: 206
		public class ConfigGameBackupNode
		{
			// Token: 0x06000508 RID: 1288 RVA: 0x0015E01C File Offset: 0x0015B81C
			public ConfigGameBackupNode()
			{
				P4258EBF.AFA7138A.M6233B19[130](this);
			}

			// Token: 0x040002D6 RID: 726
			public string original_file;

			// Token: 0x040002D7 RID: 727
			public string backup_file;

			// Token: 0x040002D8 RID: 728
			public string backup_launch_params;

			// Token: 0x040002D9 RID: 729
			public int data;
		}

		// Token: 0x020000CF RID: 207
		public class ConfigKeysBackupNode
		{
			// Token: 0x06000509 RID: 1289 RVA: 0x0015F634 File Offset: 0x0015CE34
			public ConfigKeysBackupNode()
			{
				P4258EBF.AFA7138A.M6233B19[130](this);
			}

			// Token: 0x040002DA RID: 730
			public string original_file;

			// Token: 0x040002DB RID: 731
			public string backup_file;

			// Token: 0x040002DC RID: 732
			public int data;
		}
	}
}
