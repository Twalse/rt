using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WpfApp1.Model
{
	// Token: 0x02000062 RID: 98
	public static class FileActions
	{
		// Token: 0x06000373 RID: 883 RVA: 0x001482E4 File Offset: 0x00145AE4
		public static FileActions.FOLDER_ERROS checkFolder(string path)
		{
			if (!P4258EBF.AFA7138A.M6233B19[89](path))
			{
				return FileActions.FOLDER_ERROS.DIR_NOTFOUND;
			}
			if (!P4258EBF.AFA7138A.M6233B19[627](P4258EBF.AFA7138A.M6233B19[478](path, "\\Rust.exe")))
			{
				return FileActions.FOLDER_ERROS.RUSTEXE_NOTFOUND;
			}
			if (!P4258EBF.AFA7138A.M6233B19[627](P4258EBF.AFA7138A.M6233B19[478](path, "\\cfg\\client.cfg")))
			{
				return FileActions.FOLDER_ERROS.CLIENTCFG_NOTFOUND;
			}
			if (!P4258EBF.AFA7138A.M6233B19[627](P4258EBF.AFA7138A.M6233B19[478](path, "\\cfg\\keys.cfg")))
			{
				return FileActions.FOLDER_ERROS.KEYSCFG_NOTFOUND;
			}
			return FileActions.FOLDER_ERROS.ALLGOOD;
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0014494C File Offset: 0x0014214C
		public static void ApplyParams(string PathToFile, Params.Node[] Params)
		{
			if (!P4258EBF.AFA7138A.M6233B19[627](PathToFile))
			{
				throw new FileDoesNotExist();
			}
			if (!FileActions.IsFileAccessible(PathToFile))
			{
				throw new FileNotAccessible();
			}
			if (Params == null || Params.Length == 0)
			{
				throw new BadParams();
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (string text in P4258EBF.AFA7138A.M6233B19[65](PathToFile))
			{
				if (!P4258EBF.AFA7138A.M6233B19[250](P4258EBF.AFA7138A.M6233B19[597](text), ""))
				{
					string[] array2 = P4258EBF.AFA7138A.M6233B19[141](text, ' ', StringSplitOptions.None);
					dictionary.Add(array2[0], array2[1]);
				}
			}
			foreach (Params.Node node in Params)
			{
				if (dictionary.ContainsKey(node.key))
				{
					dictionary[node.key] = node.value;
				}
				else
				{
					dictionary.Add(node.key, node.value);
				}
			}
			string text2 = D2B9D912.A91E8BBB("\n", dictionary.Select<KeyValuePair<string, string>, string>((KeyValuePair<string, string> i) => P4258EBF.AFA7138A.M6233B19[64](i.Key, " ", i.Value)));
			P4258EBF.AFA7138A.M6233B19[94](PathToFile, text2);
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0014C170 File Offset: 0x00149970
		public static void RemoveParams(string PathToFile, Params.Node[] Params)
		{
			if (!P4258EBF.AFA7138A.M6233B19[627](PathToFile))
			{
				throw new FileDoesNotExist();
			}
			if (!FileActions.IsFileAccessible(PathToFile))
			{
				throw new FileNotAccessible();
			}
			if (Params == null || P4258EBF.AFA7138A.M6233B19[88](PathToFile))
			{
				throw new BadParams();
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (string text in P4258EBF.AFA7138A.M6233B19[65](PathToFile))
			{
				if (!P4258EBF.AFA7138A.M6233B19[250](P4258EBF.AFA7138A.M6233B19[597](text), ""))
				{
					string[] array2 = P4258EBF.AFA7138A.M6233B19[141](text, ' ', StringSplitOptions.None);
					dictionary.Add(array2[0], array2[1]);
				}
			}
			foreach (Params.Node node in Params)
			{
				if (dictionary.ContainsKey(node.key))
				{
					dictionary.Remove(node.key);
				}
			}
			string text2 = D2B9D912.A91E8BBB("\n", dictionary.Select<KeyValuePair<string, string>, string>((KeyValuePair<string, string> i) => P4258EBF.AFA7138A.M6233B19[64](i.Key, " ", i.Value)));
			P4258EBF.AFA7138A.M6233B19[94](PathToFile, text2);
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0013C16C File Offset: 0x0013996C
		public static bool IsFileAccessible(string filePath)
		{
			bool flag;
			try
			{
				using (P4258EBF.AFA7138A.M6233B19[558](filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
				{
					flag = true;
				}
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0200011C RID: 284
		public class PathNode
		{
			// Token: 0x060005ED RID: 1517 RVA: 0x0016000C File Offset: 0x0015D80C
			public PathNode()
			{
				P4258EBF.AFA7138A.M6233B19[130](this);
			}

			// Token: 0x040003FD RID: 1021
			public bool is_select;

			// Token: 0x040003FE RID: 1022
			public bool have_warn;

			// Token: 0x040003FF RID: 1023
			public string folder;
		}

		// Token: 0x0200011E RID: 286
		public enum FOLDER_ERROS
		{
			// Token: 0x04000407 RID: 1031
			RUSTEXE_NOTFOUND,
			// Token: 0x04000408 RID: 1032
			CLIENTCFG_NOTFOUND,
			// Token: 0x04000409 RID: 1033
			KEYSCFG_NOTFOUND,
			// Token: 0x0400040A RID: 1034
			DIR_NOTFOUND,
			// Token: 0x0400040B RID: 1035
			ALLGOOD
		}
	}
}
