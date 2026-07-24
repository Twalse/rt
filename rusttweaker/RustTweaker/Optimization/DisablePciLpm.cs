using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace RustTweaker.Optimization
{
	// Token: 0x02000028 RID: 40
	public class DisablePciLpm : IOptimization
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00006DB8 File Offset: 0x000051B8
		public OptimizationId Id
		{
			get
			{
				return OptimizationId.DisablePcieLpm;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00006DBB File Offset: 0x000051BB
		public bool NeedComputerRestart
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00006DBE File Offset: 0x000051BE
		public bool NeedSteamRestart
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00006DC1 File Offset: 0x000051C1
		public OptimizationStatus GetStatus()
		{
			if (this.GetACSettingIndex() == ASPMValue.Off && this.GetDCSettingIndex() == ASPMValue.Off)
			{
				return OptimizationStatus.Good;
			}
			return OptimizationStatus.Bad;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00006DD8 File Offset: 0x000051D8
		public void Apply(OptimizationTargetStatus targetStatus)
		{
			try
			{
				if (targetStatus == OptimizationTargetStatus.Good)
				{
					this.SetCurrentACSettingIndex(ASPMValue.Off);
					this.SetCurrentDCSettingIndex(ASPMValue.Off);
				}
				else
				{
					this.SetCurrentACSettingIndex(ASPMValue.Moderate);
					this.SetCurrentDCSettingIndex(ASPMValue.Moderate);
				}
				PowershellTools.powershellExecute("powercfg /setactive SCHEME_CURRENT");
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00006E2C File Offset: 0x0000522C
		public ASPMValue GetACSettingIndex(string registerPath)
		{
			return this.GetACSettingIndex();
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00006E34 File Offset: 0x00005234
		public ASPMValue GetDCSettingIndex(string registerPath)
		{
			return this.GetDCSettingIndex();
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00006E3C File Offset: 0x0000523C
		public ASPMValue GetACSettingIndex()
		{
			return this.GetCurrentSettingIndex(true);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00006E45 File Offset: 0x00005245
		public ASPMValue GetDCSettingIndex()
		{
			return this.GetCurrentSettingIndex(false);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00160620 File Offset: 0x0015DE20
		public void SetCurrentACSettingIndex(ASPMValue value)
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			P4258EBF.AFA7138A.M6233B19[466](ref defaultInterpolatedStringHandler, 61, 1);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "powercfg /setacvalueindex SCHEME_CURRENT SUB_PCIEXPRESS ASPM ");
			defaultInterpolatedStringHandler.AppendFormatted<int>((int)value);
			PowershellTools.powershellExecute(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler));
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0015487C File Offset: 0x0015207C
		public void SetCurrentDCSettingIndex(ASPMValue value)
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			P4258EBF.AFA7138A.M6233B19[466](ref defaultInterpolatedStringHandler, 61, 1);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "powercfg /setdcvalueindex SCHEME_CURRENT SUB_PCIEXPRESS ASPM ");
			defaultInterpolatedStringHandler.AppendFormatted<int>((int)value);
			PowershellTools.powershellExecute(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler));
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0015D184 File Offset: 0x0015A984
		public string GetRegisterPathToStatus()
		{
			string text = "HKLM:\\SYSTEM\\CurrentControlSet\\Control\\Power\\User\\PowerSchemes\\";
			string text2 = PowershellTools.powershellExecute("powercfg /q SCHEME_CURRENT SUB_PCIEXPRESS ASPM");
			MatchCollection matchCollection = P4258EBF.AFA7138A.M6233B19[602](text2, "[0-9a-fA-F\\-]{36}");
			using (IEnumerator enumerator = P4258EBF.AFA7138A.M6233B19[624](matchCollection))
			{
				while (P4258EBF.AFA7138A.M6233B19[411](enumerator))
				{
					Match match = (Match)P4258EBF.AFA7138A.M6233B19[606](enumerator);
					Guid guid;
					if (P4258EBF.AFA7138A.M6233B19[221](P4258EBF.AFA7138A.M6233B19[372](match), ref guid))
					{
						text = P4258EBF.AFA7138A.M6233B19[64](text, guid.ToString(), "\\");
					}
				}
			}
			return text;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0013B354 File Offset: 0x00138B54
		private ASPMValue GetCurrentSettingIndex(bool isAc)
		{
			Guid activePowerSchemeGuid = DisablePciLpm.GetActivePowerSchemeGuid();
			Guid pciExpressSubgroupGuid = DisablePciLpm.PciExpressSubgroupGuid;
			Guid aspmSettingGuid = DisablePciLpm.AspmSettingGuid;
			if (isAc)
			{
				uint num2;
				uint num = DisablePciLpm.PowerReadACValueIndex(P4258EBF.AFA7138A.M6233B19[500](), ref activePowerSchemeGuid, ref pciExpressSubgroupGuid, ref aspmSettingGuid, out num2);
				if (num != 0U)
				{
					throw P4258EBF.AFA7138A.M6233B19[35]((int)num, "Failed to read PCI Express Link State Power Management AC status.");
				}
				return (ASPMValue)num2;
			}
			else
			{
				uint num4;
				uint num3 = DisablePciLpm.PowerReadDCValueIndex(P4258EBF.AFA7138A.M6233B19[500](), ref activePowerSchemeGuid, ref pciExpressSubgroupGuid, ref aspmSettingGuid, out num4);
				if (num3 != 0U)
				{
					throw P4258EBF.AFA7138A.M6233B19[35]((int)num3, "Failed to read PCI Express Link State Power Management DC status.");
				}
				return (ASPMValue)num4;
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0015F3E0 File Offset: 0x0015CBE0
		private static Guid GetActivePowerSchemeGuid()
		{
			IntPtr intPtr;
			uint num = DisablePciLpm.PowerGetActiveScheme(P4258EBF.AFA7138A.M6233B19[500](), out intPtr);
			if (num != 0U)
			{
				throw P4258EBF.AFA7138A.M6233B19[35]((int)num, "Failed to get active power scheme.");
			}
			Guid guid;
			try
			{
				guid = Marshal.PtrToStructure<Guid>(intPtr);
			}
			finally
			{
				DisablePciLpm.LocalFree(intPtr);
			}
			return guid;
		}

		// Token: 0x06000135 RID: 309
		[DllImport("powrprof.dll")]
		private static extern uint PowerGetActiveScheme(IntPtr userRootPowerKey, out IntPtr activePolicyGuid);

		// Token: 0x06000136 RID: 310
		[DllImport("powrprof.dll")]
		private static extern uint PowerReadACValueIndex(IntPtr rootPowerKey, ref Guid schemeGuid, ref Guid subgroupOfPowerSettingsGuid, ref Guid powerSettingGuid, out uint acValueIndex);

		// Token: 0x06000137 RID: 311
		[DllImport("powrprof.dll")]
		private static extern uint PowerReadDCValueIndex(IntPtr rootPowerKey, ref Guid schemeGuid, ref Guid subgroupOfPowerSettingsGuid, ref Guid powerSettingGuid, out uint dcValueIndex);

		// Token: 0x06000138 RID: 312
		[DllImport("kernel32.dll")]
		private static extern IntPtr LocalFree(IntPtr hMem);

		// Token: 0x06000139 RID: 313 RVA: 0x00147AA0 File Offset: 0x001452A0
		public DisablePciLpm()
		{
			P4258EBF.AFA7138A.M6233B19[130](this);
		}

		// Token: 0x04000076 RID: 118
		private static readonly Guid PciExpressSubgroupGuid = P4258EBF.AFA7138A.M6233B19[428]("501a4d13-42af-4429-9fd1-a8218c268e20");

		// Token: 0x04000077 RID: 119
		private static readonly Guid AspmSettingGuid = P4258EBF.AFA7138A.M6233B19[428]("ee12f906-d277-404b-b6da-e5fa1a576df5");
	}
}
