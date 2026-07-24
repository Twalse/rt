using System;
using System.Runtime.CompilerServices;
using Microsoft.Win32;

namespace RustTweaker.Optimization
{
	// Token: 0x02000026 RID: 38
	public sealed class DisableHVCI : IOptimization
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00006C88 File Offset: 0x00005088
		public OptimizationId Id
		{
			get
			{
				return OptimizationId.DisableHvci;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00006C8B File Offset: 0x0000508B
		public bool NeedComputerRestart
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00006C8E File Offset: 0x0000508E
		public bool NeedSteamRestart
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0015C8E0 File Offset: 0x0015A0E0
		public void Apply(OptimizationTargetStatus targetStatus)
		{
			try
			{
				using (RegistryKey registryKey = P4258EBF.AFA7138A.M6233B19[591](P4258EBF.AFA7138A.M6233B19[298](), "SYSTEM\\CurrentControlSet\\Control\\DeviceGuard\\Scenarios\\HypervisorEnforcedCodeIntegrity", true))
				{
					if (registryKey == null)
					{
						Logger.Log("HVCI registry key could not be created");
					}
					else
					{
						P4258EBF.AFA7138A.M6233B19[555](registryKey, "Enabled", (targetStatus > OptimizationTargetStatus.Good) ? 1 : 0, RegistryValueKind.DWord);
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Log("Failed to change HVCI registry value");
				Logger.Log(ex);
			}
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00147BEC File Offset: 0x001453EC
		public OptimizationStatus GetStatus()
		{
			using (RegistryKey registryKey = P4258EBF.AFA7138A.M6233B19[589](P4258EBF.AFA7138A.M6233B19[298](), "SYSTEM\\CurrentControlSet\\Control\\DeviceGuard\\Scenarios\\HypervisorEnforcedCodeIntegrity", false))
			{
				if (registryKey == null)
				{
					Logger.Log("HVCI registry key was not found");
					return OptimizationStatus.Middle;
				}
				object obj = P4258EBF.AFA7138A.M6233B19[451](registryKey, "Enabled");
				int num = ((obj != null) ? P4258EBF.AFA7138A.M6233B19[228](obj) : 0);
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = P4258EBF.AFA7138A.M6233B19[467](24, 1);
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "HVCI enabled raw value: ");
				defaultInterpolatedStringHandler.AppendFormatted<int>(num);
				Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler));
				if (num > 0)
				{
					return OptimizationStatus.Bad;
				}
			}
			return OptimizationStatus.Good;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0015E8E4 File Offset: 0x0015C0E4
		public DisableHVCI()
		{
			P4258EBF.AFA7138A.M6233B19[130](this);
		}

		// Token: 0x04000070 RID: 112
		private const string _path = "SYSTEM\\CurrentControlSet\\Control\\DeviceGuard\\Scenarios\\HypervisorEnforcedCodeIntegrity";

		// Token: 0x04000071 RID: 113
		private const string _key = "Enabled";
	}
}
