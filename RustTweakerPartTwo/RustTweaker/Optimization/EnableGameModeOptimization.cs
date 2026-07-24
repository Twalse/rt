using System;
using System.Runtime.CompilerServices;
using Microsoft.Win32;

namespace RustTweaker.Optimization
{
	// Token: 0x0200002E RID: 46
	public sealed class EnableGameModeOptimization : IOptimization
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000176 RID: 374 RVA: 0x0000880E File Offset: 0x00006C0E
		public OptimizationId Id
		{
			get
			{
				return OptimizationId.EnableGameMode;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00008811 File Offset: 0x00006C11
		public bool NeedComputerRestart
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00008814 File Offset: 0x00006C14
		public bool NeedSteamRestart
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x001503C8 File Offset: 0x0014DBC8
		public void Apply(OptimizationTargetStatus targetStatus)
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			P4258EBF.AFA7138A.M6233B19[466](ref defaultInterpolatedStringHandler, 37, 1);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "Requested Game Mode status change to ");
			defaultInterpolatedStringHandler.AppendFormatted<OptimizationTargetStatus>(targetStatus);
			Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler));
			using (RegistryKey registryKey = P4258EBF.AFA7138A.M6233B19[589](P4258EBF.AFA7138A.M6233B19[378](), "Software\\Microsoft\\GameBar", true))
			{
				try
				{
					if (registryKey != null)
					{
						object obj = P4258EBF.AFA7138A.M6233B19[451](registryKey, "AllowAutoGameMode");
						object obj2 = P4258EBF.AFA7138A.M6233B19[451](registryKey, "AutoGameModeEnabled");
						if (obj != null || obj2 != null)
						{
							if (targetStatus == OptimizationTargetStatus.Good)
							{
								P4258EBF.AFA7138A.M6233B19[555](registryKey, "AllowAutoGameMode", "1", RegistryValueKind.String);
								P4258EBF.AFA7138A.M6233B19[555](registryKey, "AutoGameModeEnabled", "1", RegistryValueKind.String);
								Logger.Log("Game Mode registry values were set to enabled");
							}
							else if (targetStatus == OptimizationTargetStatus.Bad)
							{
								P4258EBF.AFA7138A.M6233B19[555](registryKey, "AllowAutoGameMode", "0", RegistryValueKind.String);
								P4258EBF.AFA7138A.M6233B19[555](registryKey, "AutoGameModeEnabled", "0", RegistryValueKind.String);
								Logger.Log("Game Mode registry values were set to disabled");
							}
						}
					}
				}
				catch (Exception ex)
				{
					Logger.Log("Failed to set Game Mode registry values");
					Logger.Log(ex);
				}
			}
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2;
			P4258EBF.AFA7138A.M6233B19[466](ref defaultInterpolatedStringHandler2, 30, 1);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler2, "Game Mode effective status is ");
			defaultInterpolatedStringHandler2.AppendFormatted<OptimizationStatus>(this.GetStatus());
			Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler2));
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00008950 File Offset: 0x00006D50
		public OptimizationStatus GetStatus()
		{
			return this.GetGameModeStatus();
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0013B3F4 File Offset: 0x00138BF4
		private OptimizationStatus GetGameModeStatus()
		{
			OptimizationStatus optimizationStatus;
			using (RegistryKey registryKey = P4258EBF.AFA7138A.M6233B19[415](P4258EBF.AFA7138A.M6233B19[378](), "Software\\Microsoft\\GameBar"))
			{
				try
				{
					if (registryKey != null)
					{
						object obj = P4258EBF.AFA7138A.M6233B19[451](registryKey, "AllowAutoGameMode");
						string text = ((obj != null) ? obj.ToString() : null);
						object obj2 = P4258EBF.AFA7138A.M6233B19[451](registryKey, "AutoGameModeEnabled");
						string text2 = ((obj2 != null) ? obj2.ToString() : null);
						if (text != null || text2 != null)
						{
							if (P4258EBF.AFA7138A.M6233B19[250](text, "0") || P4258EBF.AFA7138A.M6233B19[250](text2, "0"))
							{
								Logger.Log("Game Mode is disabled based on registry values");
								return OptimizationStatus.Bad;
							}
							if (P4258EBF.AFA7138A.M6233B19[250](text, "1") || P4258EBF.AFA7138A.M6233B19[250](text2, "1"))
							{
								Logger.Log("Game Mode is enabled based on registry values");
								return OptimizationStatus.Good;
							}
							Logger.Log("Registry values indicate mixed Game Mode state");
							return OptimizationStatus.Middle;
						}
					}
				}
				catch (Exception ex)
				{
					Logger.Log("Failed to read Game Mode registry values");
					Logger.Log(ex);
				}
				Logger.Log("Game Mode status fell back to unsupported");
				optimizationStatus = OptimizationStatus.Unsupported;
			}
			return optimizationStatus;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0015D308 File Offset: 0x0015AB08
		public EnableGameModeOptimization()
		{
			P4258EBF.AFA7138A.M6233B19[130](this);
		}

		// Token: 0x0400008B RID: 139
		private const string RegistryPath = "Software\\Microsoft\\GameBar";

		// Token: 0x0400008C RID: 140
		private const string ValueName1 = "AllowAutoGameMode";

		// Token: 0x0400008D RID: 141
		private const string ValueName2 = "AutoGameModeEnabled";

		// Token: 0x0400008E RID: 142
		private const string BadValue1 = "0";

		// Token: 0x0400008F RID: 143
		private const string BadValue2 = "0";

		// Token: 0x04000090 RID: 144
		private const string GoodValue1 = "1";

		// Token: 0x04000091 RID: 145
		private const string GoodValue2 = "1";
	}
}
