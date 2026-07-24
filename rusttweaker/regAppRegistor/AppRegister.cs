using System;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace regAppRegistor
{
	// Token: 0x02000051 RID: 81
	public static class AppRegister
	{
		// Token: 0x060002B2 RID: 690 RVA: 0x0013C49C File Offset: 0x00139C9C
		public static bool RegisterOrUpdatePerUser(string schemeName, string applicationExePath, string displayName = null, string iconPath = null)
		{
			if (P4258EBF.AFA7138A.M6233B19[426](schemeName))
			{
				throw P4258EBF.AFA7138A.M6233B19[110]("schemeName не может быть пустым.", "schemeName");
			}
			if (P4258EBF.AFA7138A.M6233B19[426](applicationExePath))
			{
				throw P4258EBF.AFA7138A.M6233B19[110]("applicationExePath не может быть пустым.", "applicationExePath");
			}
			string text;
			try
			{
				text = P4258EBF.AFA7138A.M6233B19[597](P4258EBF.AFA7138A.M6233B19[224](applicationExePath));
			}
			catch (Exception ex)
			{
				throw P4258EBF.AFA7138A.M6233B19[110](P4258EBF.AFA7138A.M6233B19[478]("applicationExePath недействителен: ", P4258EBF.AFA7138A.M6233B19[551](ex)), "applicationExePath");
			}
			if (P4258EBF.AFA7138A.M6233B19[88](displayName))
			{
				displayName = P4258EBF.AFA7138A.M6233B19[64]("URL:", schemeName, " Protocol");
			}
			bool flag = false;
			string text2 = P4258EBF.AFA7138A.M6233B19[478]("Software\\Classes\\", schemeName);
			using (RegistryKey registryKey = P4258EBF.AFA7138A.M6233B19[589](P4258EBF.AFA7138A.M6233B19[378](), text2, true) ?? P4258EBF.AFA7138A.M6233B19[120](P4258EBF.AFA7138A.M6233B19[378](), text2))
			{
				if (registryKey == null)
				{
					throw P4258EBF.AFA7138A.M6233B19[115](P4258EBF.AFA7138A.M6233B19[478]("Не удалось открыть или создать ключ реестра: ", text2));
				}
				string text3 = P4258EBF.AFA7138A.M6233B19[451](registryKey, null) as string;
				if (P4258EBF.AFA7138A.M6233B19[593](text3, displayName))
				{
					P4258EBF.AFA7138A.M6233B19[198](registryKey, null, displayName);
					flag = true;
				}
				if (P4258EBF.AFA7138A.M6233B19[451](registryKey, "URL Protocol") == null)
				{
					P4258EBF.AFA7138A.M6233B19[198](registryKey, "URL Protocol", P4258EBF.AFA7138A.M6233B19[280]());
					flag = true;
				}
				if (!P4258EBF.AFA7138A.M6233B19[426](iconPath))
				{
					using (RegistryKey registryKey2 = P4258EBF.AFA7138A.M6233B19[589](registryKey, "DefaultIcon", true) ?? P4258EBF.AFA7138A.M6233B19[120](registryKey, "DefaultIcon"))
					{
						if (registryKey2 == null)
						{
							throw P4258EBF.AFA7138A.M6233B19[115]("Не удалось создать/открыть DefaultIcon.");
						}
						string text4 = P4258EBF.AFA7138A.M6233B19[451](registryKey2, null) as string;
						if (P4258EBF.AFA7138A.M6233B19[593](text4, iconPath))
						{
							P4258EBF.AFA7138A.M6233B19[198](registryKey2, null, iconPath);
							flag = true;
						}
					}
				}
				using (RegistryKey registryKey3 = P4258EBF.AFA7138A.M6233B19[589](registryKey, "shell\\open\\command", true) ?? P4258EBF.AFA7138A.M6233B19[120](registryKey, "shell\\open\\command"))
				{
					if (registryKey3 == null)
					{
						throw P4258EBF.AFA7138A.M6233B19[115]("Не удалось создать/открыть shell\\open\\command.");
					}
					string text5 = P4258EBF.AFA7138A.M6233B19[64]("\"", text, "\" \"%1\"");
					object obj = P4258EBF.AFA7138A.M6233B19[451](registryKey3, null);
					string text6 = obj as string;
					string text7 = null;
					if (!P4258EBF.AFA7138A.M6233B19[88](text6))
					{
						Match match = P4258EBF.AFA7138A.M6233B19[444](P4258EBF.AFA7138A.M6233B19[597](text6), "^\\\"?([^\\\"\\s]+(?:\\\\[^\\\"\\s]+)*)\\\"?");
						if (P4258EBF.AFA7138A.M6233B19[494](match))
						{
							text7 = P4258EBF.AFA7138A.M6233B19[372](P4258EBF.AFA7138A.M6233B19[212](P4258EBF.AFA7138A.M6233B19[614](match), 1));
							try
							{
								text7 = P4258EBF.AFA7138A.M6233B19[224](text7);
							}
							catch
							{
							}
						}
					}
					bool flag2 = false;
					if (!P4258EBF.AFA7138A.M6233B19[88](text7))
					{
						try
						{
							string text8 = N29EB3AD.I08B2A1C(P4258EBF.AFA7138A.M6233B19[224](text7), new char[]
							{
								P4258EBF.AFA7138A.M6233B19[107](),
								P4258EBF.AFA7138A.M6233B19[21]()
							});
							string text9 = N29EB3AD.I08B2A1C(P4258EBF.AFA7138A.M6233B19[224](text), new char[]
							{
								P4258EBF.AFA7138A.M6233B19[107](),
								P4258EBF.AFA7138A.M6233B19[21]()
							});
							flag2 = P4258EBF.AFA7138A.M6233B19[492](text8, text9, StringComparison.OrdinalIgnoreCase);
						}
						catch
						{
							flag2 = P4258EBF.AFA7138A.M6233B19[492](text7, text, StringComparison.OrdinalIgnoreCase);
						}
					}
					if (!flag2 || P4258EBF.AFA7138A.M6233B19[593](text6, text5))
					{
						P4258EBF.AFA7138A.M6233B19[198](registryKey3, null, text5);
						flag = true;
					}
				}
			}
			return flag;
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x001583C0 File Offset: 0x00155BC0
		public static void UnregisterPerUser(string schemeName)
		{
			if (P4258EBF.AFA7138A.M6233B19[426](schemeName))
			{
				throw P4258EBF.AFA7138A.M6233B19[263]("schemeName");
			}
			P4258EBF.AFA7138A.M6233B19[34](Registry.CurrentUser, P4258EBF.AFA7138A.M6233B19[478]("Software\\Classes\\", schemeName), false);
		}
	}
}
