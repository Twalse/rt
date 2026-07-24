using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Win32;

namespace RustTweaker.Optimization
{
	// Token: 0x0200002D RID: 45
	internal class DisableXboxGameBarOptimization : IOptimization
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600016E RID: 366 RVA: 0x00008299 File Offset: 0x00006699
		public OptimizationId Id
		{
			get
			{
				return OptimizationId.DisableXboxGameBar;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600016F RID: 367 RVA: 0x0000829D File Offset: 0x0000669D
		public bool NeedComputerRestart
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000170 RID: 368 RVA: 0x000082A0 File Offset: 0x000066A0
		public bool NeedSteamRestart
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000171 RID: 369 RVA: 0x001415B0 File Offset: 0x0013EDB0
		public void Apply(OptimizationTargetStatus targetStatus)
		{
			try
			{
				if (targetStatus != OptimizationTargetStatus.Good)
				{
					if (targetStatus != OptimizationTargetStatus.Bad)
					{
						goto IL_039B;
					}
				}
				else
				{
					try
					{
						int num = this.RunCommand("powershell", "-Command \"Get-AppxPackage Microsoft.XboxGamingOverlay | Remove-AppxPackage -ErrorAction Stop\"");
						if (num != 0)
						{
							throw P4258EBF.AFA7138A.M6233B19[62]("Appx removal failed");
						}
						using (RegistryKey registryKey = P4258EBF.AFA7138A.M6233B19[120](Registry.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\GameDVR"))
						{
							P4258EBF.AFA7138A.M6233B19[555](registryKey, "AppCaptureEnabled", 0, RegistryValueKind.DWord);
						}
						using (RegistryKey registryKey2 = P4258EBF.AFA7138A.M6233B19[120](Registry.CurrentUser, "System\\GameConfigStore"))
						{
							P4258EBF.AFA7138A.M6233B19[555](registryKey2, "GameDVR_Enabled", 0, RegistryValueKind.DWord);
						}
						Logger.Log("Successfully removed Xbox Gaming Overlay via AppxPackage");
						goto IL_039B;
					}
					catch
					{
						Logger.Log("AppxPackage removal failed, trying to remove via winget");
						try
						{
							int num2 = this.RunCommand("winget", "uninstall 9nzkpstsnw4p --silent --accept-source-agreements");
							if (num2 != 0)
							{
								throw P4258EBF.AFA7138A.M6233B19[62]("Winget uninstall failed");
							}
							using (RegistryKey registryKey3 = P4258EBF.AFA7138A.M6233B19[120](Registry.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\GameDVR"))
							{
								P4258EBF.AFA7138A.M6233B19[555](registryKey3, "AppCaptureEnabled", 0, RegistryValueKind.DWord);
							}
							using (RegistryKey registryKey4 = P4258EBF.AFA7138A.M6233B19[120](Registry.CurrentUser, "System\\GameConfigStore"))
							{
								P4258EBF.AFA7138A.M6233B19[555](registryKey4, "GameDVR_Enabled", 0, RegistryValueKind.DWord);
							}
							Logger.Log("Successfully removed Xbox Gaming Overlay via winget");
						}
						catch
						{
							Logger.Log("Winget uninstall failed");
						}
						goto IL_039B;
					}
				}
				try
				{
					Process process = P4258EBF.AFA7138A.M6233B19[603]();
					P4258EBF.AFA7138A.M6233B19[132](P4258EBF.AFA7138A.M6233B19[40](process), "winget");
					P4258EBF.AFA7138A.M6233B19[496](P4258EBF.AFA7138A.M6233B19[40](process), "install 9NZKPSTSNW4P --source msstore --accept-source-agreements --accept-package-agreements");
					P4258EBF.AFA7138A.M6233B19[584](P4258EBF.AFA7138A.M6233B19[40](process), false);
					P4258EBF.AFA7138A.M6233B19[92](P4258EBF.AFA7138A.M6233B19[40](process), true);
					P4258EBF.AFA7138A.M6233B19[524](process);
					P4258EBF.AFA7138A.M6233B19[341](process);
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = P4258EBF.AFA7138A.M6233B19[467](33, 1);
					P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "Installation finished with code: ");
					defaultInterpolatedStringHandler.AppendFormatted<int>(P4258EBF.AFA7138A.M6233B19[373](process));
					Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler));
					using (RegistryKey registryKey5 = P4258EBF.AFA7138A.M6233B19[120](P4258EBF.AFA7138A.M6233B19[378](), "Software\\Microsoft\\Windows\\CurrentVersion\\GameDVR"))
					{
						P4258EBF.AFA7138A.M6233B19[555](registryKey5, "AppCaptureEnabled", 1, RegistryValueKind.DWord);
					}
					using (RegistryKey registryKey6 = P4258EBF.AFA7138A.M6233B19[120](Registry.CurrentUser, "System\\GameConfigStore"))
					{
						P4258EBF.AFA7138A.M6233B19[555](registryKey6, "GameDVR_Enabled", 1, RegistryValueKind.DWord);
					}
				}
				catch (Exception ex)
				{
					Logger.Log(ex);
				}
				IL_039B:;
			}
			catch (Exception ex2)
			{
				Logger.Log(ex2);
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x000085EC File Offset: 0x000069EC
		public OptimizationStatus GetStatus()
		{
			bool? flag = null;
			try
			{
				flag = new bool?(DisableXboxGameBarOptimization.XboxGameBarIsInstalled());
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
			}
			if (flag == null)
			{
				return OptimizationStatus.Bad;
			}
			if (flag.GetValueOrDefault())
			{
				return OptimizationStatus.Bad;
			}
			return OptimizationStatus.Good;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0014D1F8 File Offset: 0x0014A9F8
		public static bool XboxGameBarIsInstalled()
		{
			bool flag;
			try
			{
				Process process = P4258EBF.AFA7138A.M6233B19[603]();
				P4258EBF.AFA7138A.M6233B19[132](P4258EBF.AFA7138A.M6233B19[40](process), "powershell");
				P4258EBF.AFA7138A.M6233B19[496](P4258EBF.AFA7138A.M6233B19[40](process), "-Command \"Get-AppxPackage Microsoft.XboxGamingOverlay\"");
				P4258EBF.AFA7138A.M6233B19[117](P4258EBF.AFA7138A.M6233B19[40](process), true);
				P4258EBF.AFA7138A.M6233B19[584](P4258EBF.AFA7138A.M6233B19[40](process), false);
				P4258EBF.AFA7138A.M6233B19[92](P4258EBF.AFA7138A.M6233B19[40](process), true);
				P4258EBF.AFA7138A.M6233B19[524](process);
				string text = P4258EBF.AFA7138A.M6233B19[355](P4258EBF.AFA7138A.M6233B19[399](process));
				P4258EBF.AFA7138A.M6233B19[341](process);
				flag = !P4258EBF.AFA7138A.M6233B19[426](text);
			}
			catch (Exception ex)
			{
				Logger.Log("Failed to check Xbox Game Bar installation status");
				Logger.Log(ex);
				throw;
			}
			return flag;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00153D14 File Offset: 0x00151514
		private int RunCommand(string fileName, string arguments)
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
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			P4258EBF.AFA7138A.M6233B19[466](ref defaultInterpolatedStringHandler, 18, 3);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "Command ");
			P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler, fileName);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, " ");
			P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler, arguments);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, " stdout: ");
			P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler, P4258EBF.AFA7138A.M6233B19[355](P4258EBF.AFA7138A.M6233B19[399](process)));
			Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler));
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2;
			P4258EBF.AFA7138A.M6233B19[466](ref defaultInterpolatedStringHandler2, 18, 3);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler2, "Command ");
			P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler2, fileName);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler2, " ");
			P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler2, arguments);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler2, " stderr: ");
			P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler2, P4258EBF.AFA7138A.M6233B19[355](P4258EBF.AFA7138A.M6233B19[403](process)));
			Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler2));
			return P4258EBF.AFA7138A.M6233B19[373](process);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00160570 File Offset: 0x0015DD70
		public DisableXboxGameBarOptimization()
		{
			P4258EBF.AFA7138A.M6233B19[130](this);
		}
	}
}
