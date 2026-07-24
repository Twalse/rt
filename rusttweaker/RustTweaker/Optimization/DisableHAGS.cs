using System;
using System.Runtime.CompilerServices;
using Microsoft.Win32;

namespace RustTweaker.Optimization
{
	// Token: 0x02000025 RID: 37
	public sealed class DisableHAGS : IOptimization
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00006ADE File Offset: 0x00004EDE
		public OptimizationId Id
		{
			get
			{
				return OptimizationId.DisableHags;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00006AE1 File Offset: 0x00004EE1
		public bool NeedComputerRestart
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00006AE4 File Offset: 0x00004EE4
		public bool NeedSteamRestart
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00151C8C File Offset: 0x0014F48C
		public void Apply(OptimizationTargetStatus targetStatus)
		{
			try
			{
				using (RegistryKey registryKey = P4258EBF.AFA7138A.M6233B19[120](P4258EBF.AFA7138A.M6233B19[298](), "System\\CurrentControlSet\\Control\\GraphicsDrivers"))
				{
					if (registryKey != null && P4258EBF.AFA7138A.M6233B19[451](registryKey, "HwSchMode") != null)
					{
						if (targetStatus == OptimizationTargetStatus.Good)
						{
							P4258EBF.AFA7138A.M6233B19[555](registryKey, "HwSchMode", 1, RegistryValueKind.DWord);
						}
						else
						{
							P4258EBF.AFA7138A.M6233B19[555](registryKey, "HwSchMode", 2, RegistryValueKind.DWord);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Log("Failed to change HAGS registry value");
				Logger.Log(ex);
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00006B7C File Offset: 0x00004F7C
		public OptimizationStatus GetStatus()
		{
			return this.GetHAGSStatus();
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0015B698 File Offset: 0x00158E98
		private OptimizationStatus GetHAGSStatus()
		{
			try
			{
				using (RegistryKey registryKey = P4258EBF.AFA7138A.M6233B19[415](P4258EBF.AFA7138A.M6233B19[298](), "System\\CurrentControlSet\\Control\\GraphicsDrivers"))
				{
					if (registryKey != null)
					{
						object obj = P4258EBF.AFA7138A.M6233B19[451](registryKey, "HwSchMode");
						if (obj != null)
						{
							if (obj is int)
							{
								int num = (int)obj;
								if (num == 1)
								{
									return OptimizationStatus.Good;
								}
								if (num == 2)
								{
									return OptimizationStatus.Bad;
								}
								DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = P4258EBF.AFA7138A.M6233B19[467](29, 1);
								P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "Unknown HAGS registry value: ");
								defaultInterpolatedStringHandler.AppendFormatted<int>(num);
								Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler));
								return OptimizationStatus.Middle;
							}
							else
							{
								DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = P4258EBF.AFA7138A.M6233B19[467](37, 1);
								P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler2, "Unexpected HAGS registry value type: ");
								defaultInterpolatedStringHandler2.AppendFormatted<Type>(P4258EBF.AFA7138A.M6233B19[171](obj));
								Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler2));
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Log("Failed to read HAGS registry value");
				Logger.Log(ex);
			}
			return OptimizationStatus.Unsupported;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00144310 File Offset: 0x00141B10
		public DisableHAGS()
		{
			P4258EBF.AFA7138A.M6233B19[130](this);
		}

		// Token: 0x0400006C RID: 108
		private const string RegistryPath = "System\\CurrentControlSet\\Control\\GraphicsDrivers";

		// Token: 0x0400006D RID: 109
		private const string KeyName = "HwSchMode";

		// Token: 0x0400006E RID: 110
		private const int GoodValue = 1;

		// Token: 0x0400006F RID: 111
		private const int BadValue = 2;
	}
}
