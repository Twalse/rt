using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Microsoft.Win32;

namespace RustTweaker.Optimization
{
	// Token: 0x0200001D RID: 29
	public static class Backups
	{
		// Token: 0x060000EE RID: 238 RVA: 0x001606E4 File Offset: 0x0015DEE4
		private static Backups.CommandResult RunCommand(string fileName, string arguments)
		{
			Process process = P4258EBF.AFA7138A.M6233B19[603]();
			P4258EBF.AFA7138A.M6233B19[132](P4258EBF.AFA7138A.M6233B19[40](process), fileName);
			P4258EBF.AFA7138A.M6233B19[496](P4258EBF.AFA7138A.M6233B19[40](process), arguments);
			P4258EBF.AFA7138A.M6233B19[117](P4258EBF.AFA7138A.M6233B19[40](process), true);
			P4258EBF.AFA7138A.M6233B19[387](P4258EBF.AFA7138A.M6233B19[40](process), true);
			P4258EBF.AFA7138A.M6233B19[584](P4258EBF.AFA7138A.M6233B19[40](process), false);
			P4258EBF.AFA7138A.M6233B19[92](P4258EBF.AFA7138A.M6233B19[40](process), true);
			P4258EBF.AFA7138A.M6233B19[524](process);
			P4258EBF.AFA7138A.M6233B19[341](process);
			string text = P4258EBF.AFA7138A.M6233B19[355](P4258EBF.AFA7138A.M6233B19[399](process));
			string text2 = P4258EBF.AFA7138A.M6233B19[355](P4258EBF.AFA7138A.M6233B19[403](process));
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			P4258EBF.AFA7138A.M6233B19[466](ref defaultInterpolatedStringHandler, 18, 3);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "Command ");
			P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler, fileName);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, " ");
			P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler, arguments);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, " stdout: ");
			P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler, text);
			Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler));
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2;
			P4258EBF.AFA7138A.M6233B19[466](ref defaultInterpolatedStringHandler2, 18, 3);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler2, "Command ");
			P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler2, fileName);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler2, " ");
			P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler2, arguments);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler2, " stderr: ");
			P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler2, text2);
			Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler2));
			return new Backups.CommandResult
			{
				ExitCode = P4258EBF.AFA7138A.M6233B19[373](process),
				StandardOutput = text,
				StandardError = text2
			};
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0015472C File Offset: 0x00151F2C
		public static void CreateRestorePoint(string name)
		{
			using (RegistryKey registryKey = P4258EBF.AFA7138A.M6233B19[120](P4258EBF.AFA7138A.M6233B19[298](), "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\SystemRestore"))
			{
				if (registryKey != null)
				{
					P4258EBF.AFA7138A.M6233B19[554](registryKey, "SystemRestorePointCreationFrequency", 0, RegistryValueKind.DWord);
				}
				Backups.CommandResult commandResult = Backups.RunCommand("powershell", P4258EBF.AFA7138A.M6233B19[64]("-NoProfile -ExecutionPolicy Bypass -Command \"Checkpoint-Computer -Description ", Backups.ToPowerShellString(name), " -RestorePointType 'MODIFY_SETTINGS'\""));
				if (commandResult.ExitCode != 0)
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = P4258EBF.AFA7138A.M6233B19[467](33, 1);
					P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "PowerShell failed with exit code ");
					defaultInterpolatedStringHandler.AppendFormatted<int>(commandResult.ExitCode);
					Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler));
				}
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0014C670 File Offset: 0x00149E70
		public static List<RestorePointDTO> ListRestorePoints()
		{
			Backups.CommandResult commandResult = Backups.RunCommand("powershell", "-NoProfile -ExecutionPolicy Bypass -Command \"Get-ComputerRestorePoint | Select-Object SequenceNumber, Description, CreationTime | ConvertTo-Json -Compress\"");
			if (commandResult.ExitCode != 0)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
				P4258EBF.AFA7138A.M6233B19[466](ref defaultInterpolatedStringHandler, 33, 1);
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "PowerShell failed with exit code ");
				defaultInterpolatedStringHandler.AppendFormatted<int>(commandResult.ExitCode);
				throw P4258EBF.AFA7138A.M6233B19[62](P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler));
			}
			return Backups.ParseRestorePoints(commandResult.StandardOutput);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00006044 File Offset: 0x00004444
		public static List<RestorePointDTO> ListOptimizationRestorePoints()
		{
			return (from point in Backups.ListRestorePoints()
				where P4258EBF.AFA7138A.M6233B19[44](point.Description, "RustTweakerOptimization_", StringComparison.OrdinalIgnoreCase)
				orderby point.CreationTime descending
				select point).ToList<RestorePointDTO>();
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00159E84 File Offset: 0x00157684
		public static RestorePointDTO EnsureOptimizationRestorePointExists()
		{
			RestorePointDTO restorePointDTO = Backups.ListOptimizationRestorePoints().FirstOrDefault<RestorePointDTO>();
			if (restorePointDTO != null)
			{
				return restorePointDTO;
			}
			A3B43D3F a3B43D3F = P4258EBF.AFA7138A.M6233B19[478];
			string text = "RustTweakerOptimization_";
			DateTime dateTime = P4258EBF.AFA7138A.M6233B19[300]();
			string text2 = a3B43D3F(text, P4258EBF.AFA7138A.M6233B19[429](ref dateTime, "yyyy-MM-dd_HH-mm-ss", P4258EBF.AFA7138A.M6233B19[311]()));
			Backups.CreateRestorePoint(text2);
			RestorePointDTO restorePointDTO2 = Backups.ListOptimizationRestorePoints().FirstOrDefault<RestorePointDTO>();
			if (restorePointDTO2 == null)
			{
				throw P4258EBF.AFA7138A.M6233B19[62](P4258EBF.AFA7138A.M6233B19[64]("Restore point ", text2, " was created, but was not found."));
			}
			return restorePointDTO2;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0015590C File Offset: 0x0015310C
		public static void RestoreToPoint(int sequenceNumber)
		{
			string text = "powershell";
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			P4258EBF.AFA7138A.M6233B19[466](ref defaultInterpolatedStringHandler, 93, 1);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "-NoProfile -ExecutionPolicy Bypass -Command \"Restore-Computer -RestorePoint ");
			defaultInterpolatedStringHandler.AppendFormatted<int>(sequenceNumber);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, " -Confirm:$false\"");
			Backups.CommandResult commandResult = Backups.RunCommand(text, P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler));
			if (commandResult.ExitCode != 0)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2;
				P4258EBF.AFA7138A.M6233B19[466](ref defaultInterpolatedStringHandler2, 33, 1);
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler2, "PowerShell failed with exit code ");
				defaultInterpolatedStringHandler2.AppendFormatted<int>(commandResult.ExitCode);
				throw P4258EBF.AFA7138A.M6233B19[62](P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler2));
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00158380 File Offset: 0x00155B80
		public static void RestoreLatestOptimizationPoint()
		{
			RestorePointDTO restorePointDTO = Backups.ListOptimizationRestorePoints().FirstOrDefault<RestorePointDTO>();
			if (restorePointDTO == null)
			{
				throw P4258EBF.AFA7138A.M6233B19[62]("Restore point with name RustTweakerOptimization_* was not found.");
			}
			RestorePointDTO restorePointDTO2 = restorePointDTO;
			Backups.RestoreToPoint((int)restorePointDTO2.SequenceNumber);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00143868 File Offset: 0x00141068
		private static List<RestorePointDTO> ParseRestorePoints(string json)
		{
			List<RestorePointDTO> list = new List<RestorePointDTO>();
			if (P4258EBF.AFA7138A.M6233B19[426](json))
			{
				return list;
			}
			List<RestorePointDTO> list2;
			using (JsonDocument jsonDocument = P4258EBF.AFA7138A.M6233B19[493](json, default(JsonDocumentOptions)))
			{
				JsonElement jsonElement = P4258EBF.AFA7138A.M6233B19[434](jsonDocument);
				if (P4258EBF.AFA7138A.M6233B19[601](ref jsonElement) == JsonValueKind.Array)
				{
					jsonElement = P4258EBF.AFA7138A.M6233B19[434](jsonDocument);
					using (JsonElement.ArrayEnumerator enumerator = P4258EBF.AFA7138A.M6233B19[609](ref jsonElement).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							JsonElement jsonElement2 = enumerator.Current;
							list.Add(Backups.ParseRestorePoint(jsonElement2));
						}
						goto IL_0114;
					}
				}
				jsonElement = P4258EBF.AFA7138A.M6233B19[434](jsonDocument);
				if (P4258EBF.AFA7138A.M6233B19[601](ref jsonElement) == JsonValueKind.Object)
				{
					list.Add(Backups.ParseRestorePoint(P4258EBF.AFA7138A.M6233B19[434](jsonDocument)));
				}
				IL_0114:
				list2 = list;
			}
			return list2;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0014FD84 File Offset: 0x0014D584
		private static RestorePointDTO ParseRestorePoint(JsonElement element)
		{
			JsonElement jsonElement = P4258EBF.AFA7138A.M6233B19[585](ref element, "CreationTime");
			string text = P4258EBF.AFA7138A.M6233B19[480](ref jsonElement) ?? P4258EBF.AFA7138A.M6233B19[280]();
			RestorePointDTO restorePointDTO = new RestorePointDTO();
			jsonElement = P4258EBF.AFA7138A.M6233B19[585](ref element, "SequenceNumber");
			restorePointDTO.SequenceNumber = P4258EBF.AFA7138A.M6233B19[592](ref jsonElement);
			jsonElement = P4258EBF.AFA7138A.M6233B19[585](ref element, "Description");
			restorePointDTO.Description = P4258EBF.AFA7138A.M6233B19[480](ref jsonElement) ?? P4258EBF.AFA7138A.M6233B19[280]();
			restorePointDTO.CreationTime = P4258EBF.AFA7138A.M6233B19[613](text);
			return restorePointDTO;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00160308 File Offset: 0x0015DB08
		private static string ToPowerShellString(string value)
		{
			return P4258EBF.AFA7138A.M6233B19[64]("'", P4258EBF.AFA7138A.M6233B19[114](value, "'", "''"), "'");
		}

		// Token: 0x04000056 RID: 86
		public const string OptimizationRestorePointPrefix = "RustTweakerOptimization_";

		// Token: 0x02000095 RID: 149
		private sealed class CommandResult
		{
			// Token: 0x170000A9 RID: 169
			// (get) Token: 0x06000427 RID: 1063 RVA: 0x0001A45E File Offset: 0x0001885E
			// (set) Token: 0x06000428 RID: 1064 RVA: 0x0001A466 File Offset: 0x00018866
			public int ExitCode { get; set; }

			// Token: 0x170000AA RID: 170
			// (get) Token: 0x06000429 RID: 1065 RVA: 0x0001A46F File Offset: 0x0001886F
			// (set) Token: 0x0600042A RID: 1066 RVA: 0x0001A477 File Offset: 0x00018877
			public string StandardOutput { get; set; } = P4258EBF.AFA7138A.M6233B19[280]();

			// Token: 0x170000AB RID: 171
			// (get) Token: 0x0600042B RID: 1067 RVA: 0x0001A480 File Offset: 0x00018880
			// (set) Token: 0x0600042C RID: 1068 RVA: 0x0001A488 File Offset: 0x00018888
			public string StandardError { get; set; } = P4258EBF.AFA7138A.M6233B19[280]();

			// Token: 0x0600042D RID: 1069 RVA: 0x0015FDE4 File Offset: 0x0015D5E4
			public CommandResult()
			{
				P4258EBF.AFA7138A.M6233B19[130](this);
			}
		}
	}
}
