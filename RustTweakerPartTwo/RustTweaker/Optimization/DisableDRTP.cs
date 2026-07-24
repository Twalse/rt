using System;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace RustTweaker.Optimization
{
	// Token: 0x02000023 RID: 35
	public sealed class DisableDRTP : IOptimization
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00006968 File Offset: 0x00004D68
		public OptimizationId Id
		{
			get
			{
				return OptimizationId.DisableDrtp;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600010F RID: 271 RVA: 0x0000696C File Offset: 0x00004D6C
		public bool NeedComputerRestart
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000110 RID: 272 RVA: 0x0000696F File Offset: 0x00004D6F
		public bool NeedSteamRestart
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00006974 File Offset: 0x00004D74
		public void Apply(OptimizationTargetStatus targetStatus)
		{
			try
			{
				DisableDRTPInfo disableDRTPInfo = DisableDRTP.GetDisableDRTPInfo();
				if (disableDRTPInfo != null && !disableDRTPInfo.IsTamperProtected)
				{
					if (targetStatus == OptimizationTargetStatus.Good)
					{
						PowershellTools.powershellExecute("Set-MpPreference -DisableRealtimeMonitoring $false");
					}
					else
					{
						PowershellTools.powershellExecute("Set-MpPreference -DisableRealtimeMonitoring $true");
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000069C8 File Offset: 0x00004DC8
		public OptimizationStatus GetStatus()
		{
			DisableDRTPInfo disableDRTPInfo = DisableDRTP.GetDisableDRTPInfo();
			if (disableDRTPInfo == null || !disableDRTPInfo.IsTamperProtected)
			{
				if (disableDRTPInfo != null && disableDRTPInfo.RealTimeProtectionEnabled)
				{
					return OptimizationStatus.Bad;
				}
				if (disableDRTPInfo != null && !disableDRTPInfo.RealTimeProtectionEnabled)
				{
					return OptimizationStatus.Good;
				}
			}
			return OptimizationStatus.Unsupported;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0015BD70 File Offset: 0x00159570
		[NullableContext(2)]
		public static DisableDRTPInfo GetDisableDRTPInfo()
		{
			try
			{
				string text = PowershellTools.powershellExecute("Get-MpComputerStatus | Select-Object IsTamperProtected | ConvertTo-Json");
				Logger.Log(P4258EBF.AFA7138A.M6233B19[478]("RAW JSON: ", text));
				DisableDRTP.TamperStatusDto tamperStatusDto = JsonSerializer.Deserialize<DisableDRTP.TamperStatusDto>(text, null);
				string text2 = PowershellTools.powershellExecute("Get-MpComputerStatus | Select-Object RealTimeProtectionEnabled | ConvertTo-Json");
				Logger.Log(P4258EBF.AFA7138A.M6233B19[478]("RAW JSON: ", text2));
				DisableDRTP.RealtimeProtectStatusDto realtimeProtectStatusDto = JsonSerializer.Deserialize<DisableDRTP.RealtimeProtectStatusDto>(text2, null);
				return new DisableDRTPInfo
				{
					IsTamperProtected = tamperStatusDto.IsTamperProtected,
					RealTimeProtectionEnabled = realtimeProtectStatusDto.RealTimeProtectionEnabled
				};
			}
			catch (Exception ex)
			{
				Logger.Log(P4258EBF.AFA7138A.M6233B19[551](ex));
				Logger.Log(P4258EBF.AFA7138A.M6233B19[414](ex));
			}
			return null;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0015B834 File Offset: 0x00159034
		public DisableDRTP()
		{
			P4258EBF.AFA7138A.M6233B19[130](this);
		}

		// Token: 0x02000098 RID: 152
		public class RealtimeProtectStatusDto
		{
			// Token: 0x170000AE RID: 174
			// (get) Token: 0x06000437 RID: 1079 RVA: 0x0001A508 File Offset: 0x00018908
			// (set) Token: 0x06000438 RID: 1080 RVA: 0x0001A510 File Offset: 0x00018910
			public bool RealTimeProtectionEnabled { get; set; }

			// Token: 0x06000439 RID: 1081 RVA: 0x00161578 File Offset: 0x0015ED78
			public RealtimeProtectStatusDto()
			{
				P4258EBF.AFA7138A.M6233B19[130](this);
			}
		}

		// Token: 0x02000099 RID: 153
		private class TamperStatusDto
		{
			// Token: 0x170000AF RID: 175
			// (get) Token: 0x0600043A RID: 1082 RVA: 0x0001A521 File Offset: 0x00018921
			// (set) Token: 0x0600043B RID: 1083 RVA: 0x0001A529 File Offset: 0x00018929
			public bool IsTamperProtected { get; set; }

			// Token: 0x0600043C RID: 1084 RVA: 0x001560F0 File Offset: 0x001538F0
			public TamperStatusDto()
			{
				P4258EBF.AFA7138A.M6233B19[130](this);
			}
		}
	}
}
