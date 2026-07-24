using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using RustTweaker;
using WpfApp1.Model;

namespace RustTweaker_NET.Services
{
	// Token: 0x02000015 RID: 21
	internal static class BenchmarkBackupRecovery
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x00156300 File Offset: 0x00153B00
		public static bool HasPendingBackupsAtStartup()
		{
			bool flag;
			try
			{
				if (!P4258EBF.AFA7138A.M6233B19[89](BenchmarkBackupRecovery.BackupsDirectory))
				{
					flag = false;
				}
				else
				{
					string selectedRustFolder = BenchmarkBackupRecovery.GetSelectedRustFolder();
					if (P4258EBF.AFA7138A.M6233B19[426](selectedRustFolder))
					{
						flag = false;
					}
					else
					{
						flag = BenchmarkBackupRecovery.GetLatestPendingBackup("client-*.cfg") != null || BenchmarkBackupRecovery.GetLatestPendingBackup("keys-*.cfg") != null || BenchmarkBackupRecovery.GetLatestPendingBackup("launchparams-*.json") != null;
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00143710 File Offset: 0x00140F10
		public static void TryRestorePendingBackupsAtStartup()
		{
			try
			{
				if (P4258EBF.AFA7138A.M6233B19[89](BenchmarkBackupRecovery.BackupsDirectory))
				{
					string selectedRustFolder = BenchmarkBackupRecovery.GetSelectedRustFolder();
					if (P4258EBF.AFA7138A.M6233B19[426](selectedRustFolder))
					{
						Logger.Log("Benchmark backup startup restore skipped: selected Rust folder is empty.");
					}
					else
					{
						FileInfo latestPendingBackup = BenchmarkBackupRecovery.GetLatestPendingBackup("client-*.cfg");
						FileInfo latestPendingBackup2 = BenchmarkBackupRecovery.GetLatestPendingBackup("keys-*.cfg");
						FileInfo latestPendingBackup3 = BenchmarkBackupRecovery.GetLatestPendingBackup("launchparams-*.json");
						if (latestPendingBackup != null || latestPendingBackup2 != null || latestPendingBackup3 != null)
						{
							Logger.Log("Benchmark backup startup restore detected pending backup(s).");
							if (!BenchmarkBackupRecovery.CloseSteamBeforeRestore())
							{
								Logger.Log("Benchmark backup startup restore skipped: Steam is still running.");
							}
							else
							{
								if (latestPendingBackup != null)
								{
									BenchmarkBackupRecovery.RestoreClientBackup(selectedRustFolder, latestPendingBackup);
								}
								if (latestPendingBackup2 != null)
								{
									BenchmarkBackupRecovery.RestoreKeysBackup(selectedRustFolder, latestPendingBackup2);
								}
								if (latestPendingBackup3 != null)
								{
									BenchmarkBackupRecovery.RestoreLaunchParamsBackup(latestPendingBackup3);
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x0015A9C4 File Offset: 0x001581C4
		private static string GetSelectedRustFolder()
		{
			string text = P4258EBF.AFA7138A.M6233B19[158](MainLogic.appDataPath, "paths.json");
			if (!P4258EBF.AFA7138A.M6233B19[627](text))
			{
				return null;
			}
			List<FileActions.PathNode> list = JsonConvert.DeserializeObject<List<FileActions.PathNode>>(P4258EBF.AFA7138A.M6233B19[267](text)) ?? new List<FileActions.PathNode>();
			FileActions.PathNode pathNode = list.FirstOrDefault<FileActions.PathNode>((FileActions.PathNode x) => x.is_select);
			string text2;
			if ((text2 = ((pathNode != null) ? pathNode.folder : null)) == null)
			{
				FileActions.PathNode pathNode2 = list.FirstOrDefault<FileActions.PathNode>((FileActions.PathNode x) => !x.have_warn);
				if ((text2 = ((pathNode2 != null) ? pathNode2.folder : null)) == null)
				{
					FileActions.PathNode pathNode3 = list.FirstOrDefault<FileActions.PathNode>();
					if (pathNode3 == null)
					{
						return null;
					}
					text2 = pathNode3.folder;
				}
			}
			return text2;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x001514CC File Offset: 0x0014ECCC
		private static bool CloseSteamBeforeRestore()
		{
			bool flag;
			try
			{
				if (BenchmarkBackupRecovery.IsSteamFullyStopped())
				{
					Logger.Log("Benchmark backup startup restore: Steam is already closed.");
					flag = true;
				}
				else
				{
					if (P4258EBF.AFA7138A.M6233B19[98]("steam").Length != 0)
					{
						Logger.Log("Benchmark backup startup restore: closing Steam before applying backup.");
						ProcessStartInfo processStartInfo = P4258EBF.AFA7138A.M6233B19[394]();
						N62EB38A.CB1145A6(processStartInfo, "steam://exit");
						O8258311.M5A8918D(processStartInfo, true);
						JC11021F.C827CF8C(processStartInfo);
					}
					Stopwatch stopwatch = P4258EBF.AFA7138A.M6233B19[55]();
					while (!BenchmarkBackupRecovery.IsSteamFullyStopped() && P4258EBF.AFA7138A.M6233B19[358](stopwatch) < 120000L)
					{
						Logger.Log("Benchmark backup startup restore: waiting for Steam to close...");
						P4258EBF.AFA7138A.M6233B19[381](1000);
					}
					if (BenchmarkBackupRecovery.IsSteamFullyStopped())
					{
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = P4258EBF.AFA7138A.M6233B19[467](57, 1);
						P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "Benchmark backup startup restore: Steam closed after ");
						defaultInterpolatedStringHandler.AppendFormatted<long>(P4258EBF.AFA7138A.M6233B19[358](stopwatch));
						P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, " ms.");
						Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler));
						flag = true;
					}
					else
					{
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = P4258EBF.AFA7138A.M6233B19[467](65, 1);
						P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler2, "Benchmark backup startup restore: Steam did not close within ");
						defaultInterpolatedStringHandler2.AppendFormatted<int>(120000);
						P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler2, " ms.");
						Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler2));
						flag = false;
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0015E718 File Offset: 0x0015BF18
		private static bool IsSteamFullyStopped()
		{
			return P4258EBF.AFA7138A.M6233B19[98]("steam").Length == 0 && P4258EBF.AFA7138A.M6233B19[98]("steamwebhelper").Length == 0;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0015D4B8 File Offset: 0x0015ACB8
		private static FileInfo GetLatestPendingBackup(string pattern)
		{
			DirectoryInfo directoryInfo = P4258EBF.AFA7138A.M6233B19[216](BenchmarkBackupRecovery.BackupsDirectory);
			IEnumerable<FileInfo> enumerable = from f in P4258EBF.AFA7138A.M6233B19[416](directoryInfo, pattern)
				orderby P4258EBF.AFA7138A.M6233B19[521](f) descending
				select f;
			Func<FileInfo, bool> func;
			if ((func = BenchmarkBackupRecovery.<>O.<0>__IsPendingBackup) == null)
			{
				func = (BenchmarkBackupRecovery.<>O.<0>__IsPendingBackup = new Func<FileInfo, bool>(BenchmarkBackupRecovery.IsPendingBackup));
			}
			return enumerable.FirstOrDefault<FileInfo>(func);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00159FB8 File Offset: 0x001577B8
		private static bool IsPendingBackup(FileInfo backupFile)
		{
			string text = P4258EBF.AFA7138A.M6233B19[478](P4258EBF.AFA7138A.M6233B19[329](backupFile), ".restore");
			string text2 = P4258EBF.AFA7138A.M6233B19[478](P4258EBF.AFA7138A.M6233B19[329](backupFile), ".restored.txt");
			return !P4258EBF.AFA7138A.M6233B19[627](text) && !P4258EBF.AFA7138A.M6233B19[627](text2);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0015E9A4 File Offset: 0x0015C1A4
		private static void RestoreClientBackup(string rustFolder, FileInfo backupFile)
		{
			string text = P4258EBF.AFA7138A.M6233B19[278](rustFolder, "cfg", "client.cfg");
			if (!P4258EBF.AFA7138A.M6233B19[627](text))
			{
				Logger.Log(P4258EBF.AFA7138A.M6233B19[478]("Benchmark backup startup restore: target not found: ", text));
				return;
			}
			P4258EBF.AFA7138A.M6233B19[94](text, P4258EBF.AFA7138A.M6233B19[267](P4258EBF.AFA7138A.M6233B19[329](backupFile)));
			BenchmarkBackupRecovery.MarkBackupFileRestored(backupFile);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x0014ADFC File Offset: 0x001485FC
		private static void RestoreKeysBackup(string rustFolder, FileInfo backupFile)
		{
			string text = P4258EBF.AFA7138A.M6233B19[278](rustFolder, "cfg", "keys.cfg");
			if (!P4258EBF.AFA7138A.M6233B19[627](text))
			{
				Logger.Log(P4258EBF.AFA7138A.M6233B19[478]("Benchmark backup startup restore: target not found: ", text));
				return;
			}
			P4258EBF.AFA7138A.M6233B19[94](text, P4258EBF.AFA7138A.M6233B19[267](P4258EBF.AFA7138A.M6233B19[329](backupFile)));
			BenchmarkBackupRecovery.MarkBackupFileRestored(backupFile);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x0013F384 File Offset: 0x0013CB84
		private static void RestoreLaunchParamsBackup(FileInfo backupFile)
		{
			string text = P4258EBF.AFA7138A.M6233B19[267](P4258EBF.AFA7138A.M6233B19[329](backupFile));
			List<BenchmarkBackupRecovery.LaunchParamsBackup> list = JsonConvert.DeserializeObject<List<BenchmarkBackupRecovery.LaunchParamsBackup>>(text) ?? new List<BenchmarkBackupRecovery.LaunchParamsBackup>();
			foreach (BenchmarkBackupRecovery.LaunchParamsBackup launchParamsBackup in list)
			{
				if (!J7B43304.P634343F((launchParamsBackup != null) ? launchParamsBackup.PathToLocalconfig : null) && P4258EBF.AFA7138A.M6233B19[627](launchParamsBackup.PathToLocalconfig))
				{
					string text2 = P4258EBF.AFA7138A.M6233B19[267](launchParamsBackup.PathToLocalconfig);
					string text3 = ((launchParamsBackup.LaunchParams != null) ? BenchmarkBackupRecovery.SetLaunchParams(text2, launchParamsBackup.AppId, launchParamsBackup.LaunchParams) : BenchmarkBackupRecovery.RemoveLaunchParams(text2, launchParamsBackup.AppId));
					P4258EBF.AFA7138A.M6233B19[94](launchParamsBackup.PathToLocalconfig, text3);
				}
			}
			BenchmarkBackupRecovery.MarkBackupFileRestored(backupFile);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00005004 File Offset: 0x00003404
		private static string SetLaunchParams(string vdfContent, int steamAppId, string launchParams)
		{
			return BenchmarkBackupRecovery.RewriteLaunchOptions(vdfContent, steamAppId, BenchmarkBackupRecovery.EscapeVdfValue(launchParams), false);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00160344 File Offset: 0x0015DB44
		private static string RemoveLaunchParams(string vdfContent, int steamAppId)
		{
			return BenchmarkBackupRecovery.RewriteLaunchOptions(vdfContent, steamAppId, P4258EBF.AFA7138A.M6233B19[280](), true);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0014C9F0 File Offset: 0x0014A1F0
		private static string RewriteLaunchOptions(string vdfContent, int steamAppId, string launchParams, bool remove)
		{
			BenchmarkBackupRecovery.<>c__DisplayClass16_0 CS$<>8__locals1 = new BenchmarkBackupRecovery.<>c__DisplayClass16_0();
			CS$<>8__locals1.launchParams = launchParams;
			if (P4258EBF.AFA7138A.M6233B19[88](vdfContent))
			{
				return vdfContent;
			}
			MD2D84B4 md2D84B = P4258EBF.AFA7138A.M6233B19[458];
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			P4258EBF.AFA7138A.M6233B19[466](ref defaultInterpolatedStringHandler, 7, 1);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "\"");
			defaultInterpolatedStringHandler.AppendFormatted<int>(steamAppId);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "\"\\s*\\{");
			Match match = md2D84B(vdfContent, P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler), RegexOptions.Singleline);
			if (!P4258EBF.AFA7138A.M6233B19[494](match))
			{
				return vdfContent;
			}
			int num = P4258EBF.AFA7138A.M6233B19[475](vdfContent, "{", P4258EBF.AFA7138A.M6233B19[93](match), StringComparison.Ordinal);
			if (num == -1)
			{
				return vdfContent;
			}
			int num2 = BenchmarkBackupRecovery.FindClosingBrace(vdfContent, num);
			if (num2 == -1)
			{
				return vdfContent;
			}
			string text = P4258EBF.AFA7138A.M6233B19[487](vdfContent, num, num2 - num + 1);
			Regex regex = P4258EBF.AFA7138A.M6233B19[456]("^[ \\t]*\"LaunchOptions\"[ \\t]*\".*\"[ \\t]*(\\r?\\n)?", RegexOptions.Multiline);
			string text2;
			if (remove)
			{
				text2 = P4258EBF.AFA7138A.M6233B19[380](regex, text, P4258EBF.AFA7138A.M6233B19[280](), 1);
			}
			else if (P4258EBF.AFA7138A.M6233B19[545](regex, text))
			{
				text2 = P4258EBF.AFA7138A.M6233B19[410](regex, text, P4258EBF.AFA7138A.M6233B19[247](CS$<>8__locals1, ldftn(<RewriteLaunchOptions>b__0)), 1);
			}
			else
			{
				string text3 = (P4258EBF.AFA7138A.M6233B19[433](vdfContent, "\r\n") ? "\r\n" : "\n");
				int num3 = P4258EBF.AFA7138A.M6233B19[447](text, '}');
				if (num3 == -1)
				{
					return vdfContent;
				}
				text2 = P4258EBF.AFA7138A.M6233B19[296](text, num3, P4258EBF.AFA7138A.M6233B19[259]("\t\t\t\t\"LaunchOptions\"\t\t\"", CS$<>8__locals1.launchParams, "\"", text3));
			}
			return P4258EBF.AFA7138A.M6233B19[296](P4258EBF.AFA7138A.M6233B19[56](vdfContent, num, num2 - num + 1), num, text2);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x0015374C File Offset: 0x00150F4C
		private static int FindClosingBrace(string text, int openingBraceIndex)
		{
			int num = 0;
			bool flag = false;
			bool flag2 = false;
			for (int i = openingBraceIndex; i < P4258EBF.AFA7138A.M6233B19[152](text); i++)
			{
				char c = P4258EBF.AFA7138A.M6233B19[366](text, i);
				if (flag2)
				{
					flag2 = false;
				}
				else if (c == '\\' && flag)
				{
					flag2 = true;
				}
				else if (c == '"')
				{
					flag = !flag;
				}
				else if (!flag)
				{
					if (c == '{')
					{
						num++;
					}
					else if (c == '}')
					{
						num--;
						if (num == 0)
						{
							return i;
						}
					}
				}
			}
			return -1;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0014FE94 File Offset: 0x0014D694
		private static string EscapeVdfValue(string value)
		{
			return C626073A.M880662A(C626073A.M880662A(value ?? P4258EBF.AFA7138A.M6233B19[280](), "\\", "\\\\"), "\"", "\\\"");
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0015CB54 File Offset: 0x0015A354
		private static void MarkBackupFileRestored(FileInfo backupFile)
		{
			string text = P4258EBF.AFA7138A.M6233B19[478](P4258EBF.AFA7138A.M6233B19[329](backupFile), ".restore");
			BA272031 ba = P4258EBF.AFA7138A.M6233B19[94];
			string text2 = text;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			P4258EBF.AFA7138A.M6233B19[466](ref defaultInterpolatedStringHandler, 56, 4);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "Restored from this backup on ");
			defaultInterpolatedStringHandler.AppendFormatted<DateTimeOffset>(P4258EBF.AFA7138A.M6233B19[166](), "O");
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, " UTC by RustTweaker");
			P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler, P4258EBF.AFA7138A.M6233B19[181]());
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "Backup: ");
			P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler, P4258EBF.AFA7138A.M6233B19[31](backupFile));
			P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler, P4258EBF.AFA7138A.M6233B19[181]());
			ba(text2, P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler));
			Logger.Log(P4258EBF.AFA7138A.M6233B19[478]("Benchmark backup restore marker created: ", text));
		}

		// Token: 0x04000044 RID: 68
		private const int SteamShutdownCheckIntervalMs = 1000;

		// Token: 0x04000045 RID: 69
		private const int MaxSteamShutdownWaitMs = 120000;

		// Token: 0x04000046 RID: 70
		private static readonly string BackupsDirectory = P4258EBF.AFA7138A.M6233B19[278](P4258EBF.AFA7138A.M6233B19[54](Environment.SpecialFolder.ApplicationData), "RustTweakerBenchmark", "Backups");

		// Token: 0x02000086 RID: 134
		private sealed class LaunchParamsBackup
		{
			// Token: 0x170000A4 RID: 164
			// (get) Token: 0x06000400 RID: 1024 RVA: 0x0001969A File Offset: 0x00017A9A
			// (set) Token: 0x06000401 RID: 1025 RVA: 0x000196A2 File Offset: 0x00017AA2
			public string PathToLocalconfig { get; set; }

			// Token: 0x170000A5 RID: 165
			// (get) Token: 0x06000402 RID: 1026 RVA: 0x000196AB File Offset: 0x00017AAB
			// (set) Token: 0x06000403 RID: 1027 RVA: 0x000196B3 File Offset: 0x00017AB3
			public int AppId { get; set; }

			// Token: 0x170000A6 RID: 166
			// (get) Token: 0x06000404 RID: 1028 RVA: 0x000196BC File Offset: 0x00017ABC
			// (set) Token: 0x06000405 RID: 1029 RVA: 0x000196C4 File Offset: 0x00017AC4
			public string LaunchParams { get; set; }

			// Token: 0x06000406 RID: 1030 RVA: 0x00160FC4 File Offset: 0x0015E7C4
			public LaunchParamsBackup()
			{
				P4258EBF.AFA7138A.M6233B19[130](this);
			}
		}

		// Token: 0x02000087 RID: 135
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040001CC RID: 460
			public static Func<FileInfo, bool> <0>__IsPendingBackup;
		}
	}
}
