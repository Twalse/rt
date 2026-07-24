using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Runtime.CompilerServices;
using RustTweakerDemo;
using WpfApp1;
using WpfApp1.Model;

namespace RustTweaker.Optimization
{
	// Token: 0x02000022 RID: 34
	public class AutoGcBuffer : IOptimization
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000103 RID: 259 RVA: 0x000064AD File Offset: 0x000048AD
		public OptimizationId Id
		{
			get
			{
				return OptimizationId.AutoGcBuffer;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000104 RID: 260 RVA: 0x000064B1 File Offset: 0x000048B1
		public bool NeedComputerRestart
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000105 RID: 261 RVA: 0x000064B4 File Offset: 0x000048B4
		public bool NeedSteamRestart
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0015DC2C File Offset: 0x0015B42C
		public OptimizationStatus GetStatus()
		{
			int goodBcBuffer = AutoGcBuffer.getGoodBcBuffer(AutoGcBuffer.getRamCount(), AutoGcBuffer.getLogicalCores());
			string currentParamsLaunch = new JsBridge().getCurrentParamsLaunch();
			string[] array = P4258EBF.AFA7138A.M6233B19[141](currentParamsLaunch, ' ', StringSplitOptions.None);
			int num = Array.IndexOf<string>(array, "-gc.buffer");
			int num2;
			if (num != -1 && num + 1 < array.Length && HA0ACF96.H3B2540B(IC88751F.D71FA4BE(array[num + 1], new char[] { '"', '\\' }), ref num2) && num2 >= goodBcBuffer)
			{
				return OptimizationStatus.Good;
			}
			return OptimizationStatus.Bad;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0014B2E0 File Offset: 0x00148AE0
		public void Apply(OptimizationTargetStatus targetStatus)
		{
			int goodBcBuffer = AutoGcBuffer.getGoodBcBuffer(AutoGcBuffer.getRamCount(), AutoGcBuffer.getLogicalCores());
			RustTweakerViewModel rustTweakerViewModel = new RustTweakerViewModel();
			string configPathToLastUser = MainLogic.SteamParser.GetConfigPathToLastUser();
			string currentParamsLaunch = new JsBridge().getCurrentParamsLaunch();
			List<string> list = P4258EBF.AFA7138A.M6233B19[141](currentParamsLaunch, ' ', StringSplitOptions.None).ToList<string>();
			int num = list.IndexOf("-gc.buffer");
			AutoGcBuffer.LaunchArgumentState launchArgumentState;
			if (targetStatus == OptimizationTargetStatus.Good)
			{
				OptimizationOriginalStateStore.SaveOrReplace<AutoGcBuffer.LaunchArgumentState>(this.Id, new AutoGcBuffer.LaunchArgumentState
				{
					Exists = (num != -1 && num + 1 < list.Count),
					Value = ((num != -1 && num + 1 < list.Count) ? list[num + 1] : null)
				});
				if (num != -1)
				{
					if (num + 1 < list.Count)
					{
						list[num + 1] = P4258EBF.AFA7138A.M6233B19[24](ref goodBcBuffer);
					}
					else
					{
						list.Add(P4258EBF.AFA7138A.M6233B19[24](ref goodBcBuffer));
					}
				}
				else
				{
					list.Add("-gc.buffer");
					list.Add(P4258EBF.AFA7138A.M6233B19[24](ref goodBcBuffer));
				}
			}
			else if (OptimizationOriginalStateStore.TryRead<AutoGcBuffer.LaunchArgumentState>(this.Id, out launchArgumentState) && launchArgumentState != null)
			{
				AutoGcBuffer.RestoreLaunchArgument(list, "-gc.buffer", launchArgumentState);
			}
			else if (num != -1)
			{
				AutoGcBuffer.RemoveLaunchArgument(list, num);
			}
			rustTweakerViewModel.UpdateLocalConfig(configPathToLastUser, string.Join<string>(' ', list));
		}

		// Token: 0x06000108 RID: 264 RVA: 0x0014BA40 File Offset: 0x00149240
		private static void RestoreLaunchArgument(List<string> words, string argumentName, AutoGcBuffer.LaunchArgumentState state)
		{
			int num = words.IndexOf(argumentName);
			if (!state.Exists)
			{
				if (num != -1)
				{
					AutoGcBuffer.RemoveLaunchArgument(words, num);
				}
				return;
			}
			if (num == -1)
			{
				words.Add(argumentName);
				words.Add(state.Value ?? P4258EBF.AFA7138A.M6233B19[280]());
				return;
			}
			if (num + 1 < words.Count)
			{
				words[num + 1] = state.Value ?? P4258EBF.AFA7138A.M6233B19[280]();
				return;
			}
			words.Add(state.Value ?? P4258EBF.AFA7138A.M6233B19[280]());
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000066F0 File Offset: 0x00004AF0
		private static void RemoveLaunchArgument(List<string> words, int index)
		{
			int num = ((index + 1 < words.Count) ? 2 : 1);
			words.RemoveRange(index, num);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0015D350 File Offset: 0x0015AB50
		private static int getRamCount()
		{
			try
			{
				using (ManagementObjectSearcher managementObjectSearcher = P4258EBF.AFA7138A.M6233B19[84]("SELECT TotalPhysicalMemory FROM Win32_ComputerSystem"))
				{
					using (ManagementObjectCollection.ManagementObjectEnumerator managementObjectEnumerator = P4258EBF.AFA7138A.M6233B19[122](P4258EBF.AFA7138A.M6233B19[343](managementObjectSearcher)))
					{
						if (managementObjectEnumerator.MoveNext())
						{
							ManagementObject managementObject = (ManagementObject)managementObjectEnumerator.Current;
							long num = P4258EBF.AFA7138A.M6233B19[104](P4258EBF.AFA7138A.M6233B19[491](managementObject, "TotalPhysicalMemory"));
							return (int)P4258EBF.AFA7138A.M6233B19[177]((double)num / 1073741824.0);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
			}
			return 0;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x001570EC File Offset: 0x001548EC
		private static int getLogicalCores()
		{
			try
			{
				using (ManagementObjectSearcher managementObjectSearcher = P4258EBF.AFA7138A.M6233B19[84]("SELECT NumberOfLogicalProcessors FROM Win32_Processor"))
				{
					using (ManagementObjectCollection.ManagementObjectEnumerator managementObjectEnumerator = P4258EBF.AFA7138A.M6233B19[122](P4258EBF.AFA7138A.M6233B19[343](managementObjectSearcher)))
					{
						if (managementObjectEnumerator.MoveNext())
						{
							ManagementObject managementObject = (ManagementObject)managementObjectEnumerator.Current;
							return P4258EBF.AFA7138A.M6233B19[228](P4258EBF.AFA7138A.M6233B19[491](managementObject, "NumberOfLogicalProcessors"));
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
			}
			return 0;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x001447D8 File Offset: 0x00141FD8
		public static int getGoodBcBuffer(int ramCount, int logicalCores)
		{
			float num = (float)((ramCount >= 32) ? 4096 : ((ramCount >= 24) ? 3072 : ((ramCount >= 16) ? 2048 : ((ramCount >= 12) ? 1024 : 512))));
			float num2 = ((logicalCores >= 8) ? 1.15f : ((logicalCores > 4) ? 1f : 0.75f));
			int num3 = (int)P4258EBF.AFA7138A.M6233B19[177]((double)(num * num2));
			if (num3 < 512)
			{
				num3 = 512;
			}
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			P4258EBF.AFA7138A.M6233B19[466](ref defaultInterpolatedStringHandler, 46, 5);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "Gc buf (RAM: ");
			defaultInterpolatedStringHandler.AppendFormatted<int>(ramCount);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "GB (");
			defaultInterpolatedStringHandler.AppendFormatted<float>(num);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "MB), core: ");
			defaultInterpolatedStringHandler.AppendFormatted<int>(logicalCores);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, " (x");
			defaultInterpolatedStringHandler.AppendFormatted<float>(num2);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, ")): -gc.buffer=");
			defaultInterpolatedStringHandler.AppendFormatted<int>(num3);
			Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler));
			return num3;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0015E650 File Offset: 0x0015BE50
		public AutoGcBuffer()
		{
			P4258EBF.AFA7138A.M6233B19[130](this);
		}

		// Token: 0x02000097 RID: 151
		private sealed class LaunchArgumentState
		{
			// Token: 0x170000AC RID: 172
			// (get) Token: 0x06000432 RID: 1074 RVA: 0x0001A4DE File Offset: 0x000188DE
			// (set) Token: 0x06000433 RID: 1075 RVA: 0x0001A4E6 File Offset: 0x000188E6
			public bool Exists { get; set; }

			// Token: 0x170000AD RID: 173
			// (get) Token: 0x06000434 RID: 1076 RVA: 0x0001A4EF File Offset: 0x000188EF
			// (set) Token: 0x06000435 RID: 1077 RVA: 0x0001A4F7 File Offset: 0x000188F7
			public string Value { get; set; }

			// Token: 0x06000436 RID: 1078 RVA: 0x00156098 File Offset: 0x00153898
			public LaunchArgumentState()
			{
				P4258EBF.AFA7138A.M6233B19[130](this);
			}
		}
	}
}
