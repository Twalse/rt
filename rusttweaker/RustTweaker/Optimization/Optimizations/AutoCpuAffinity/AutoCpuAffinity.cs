using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using RustTweaker.Model;
using RustTweakerDemo;
using WpfApp1;
using WpfApp1.Model;

namespace RustTweaker.Optimization.Optimizations.AutoCpuAffinity
{
	// Token: 0x0200003B RID: 59
	public sealed class AutoCpuAffinity : IOptimization
	{
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600021B RID: 539 RVA: 0x0000AEA8 File Offset: 0x000092A8
		public OptimizationId Id
		{
			get
			{
				return OptimizationId.AutoCpuAffinity;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600021C RID: 540 RVA: 0x0000AEAB File Offset: 0x000092AB
		public bool NeedComputerRestart
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600021D RID: 541 RVA: 0x0000AEAE File Offset: 0x000092AE
		public bool NeedSteamRestart
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600021E RID: 542 RVA: 0x001506E8 File Offset: 0x0014DEE8
		public OptimizationStatus GetStatus()
		{
			try
			{
				string clientCfgPath = AutoCpuAffinity.GetClientCfgPath();
				if (!P4258EBF.AFA7138A.M6233B19[426](clientCfgPath) && P4258EBF.AFA7138A.M6233B19[627](clientCfgPath))
				{
					string[] array = P4258EBF.AFA7138A.M6233B19[65](clientCfgPath);
					int num = AutoCpuAffinity.FindClientCfgLineIndex(array);
					if (num != -1)
					{
						return AutoCpuAffinity.IsEnabledLine(array[num]) ? OptimizationStatus.Good : OptimizationStatus.Middle;
					}
				}
				string currentParamsLaunch = new JsBridge().getCurrentParamsLaunch();
				string[] array2 = P4258EBF.AFA7138A.M6233B19[141](currentParamsLaunch, ' ', StringSplitOptions.None);
				string goodCpuAffinity = AutoCpuAffinity.GetGoodCpuAffinity();
				if (!P4258EBF.AFA7138A.M6233B19[433](currentParamsLaunch, "-cpu.affinity"))
				{
					return OptimizationStatus.Bad;
				}
				int num2 = array2.IndexOf("-cpu.affinity");
				if (num2 != -1 && num2 + 1 < array2.Length && JC012E8B.N1A97104(IC88751F.D71FA4BE(array2[num2 + 1], new char[] { '"', '\\' }), goodCpuAffinity))
				{
					return OptimizationStatus.Good;
				}
				return OptimizationStatus.Middle;
			}
			catch (Exception ex)
			{
				Logger.Log("Failed to get AutoCpuAffinity status");
				Logger.Log(ex);
			}
			return OptimizationStatus.Unsupported;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0014F4B4 File Offset: 0x0014CCB4
		public void Apply(OptimizationTargetStatus targetStatus)
		{
			try
			{
				string configPathToLastUser = MainLogic.SteamParser.GetConfigPathToLastUser();
				string clientCfgPath = AutoCpuAffinity.GetClientCfgPath();
				List<string> list = ((!P4258EBF.AFA7138A.M6233B19[426](clientCfgPath) && P4258EBF.AFA7138A.M6233B19[627](clientCfgPath)) ? P4258EBF.AFA7138A.M6233B19[65](clientCfgPath).ToList<string>() : new List<string>());
				string goodCpuAffinity = AutoCpuAffinity.GetGoodCpuAffinity();
				RustTweakerViewModel rustTweakerViewModel = new RustTweakerViewModel();
				string currentParamsLaunch = new JsBridge().getCurrentParamsLaunch();
				List<string> list2 = P4258EBF.AFA7138A.M6233B19[141](currentParamsLaunch, ' ', StringSplitOptions.None).ToList<string>();
				int num = list2.IndexOf("-cpu.affinity");
				AutoCpuAffinity.AutoCpuAffinityState autoCpuAffinityState;
				AutoCpuAffinity.LaunchArgumentState launchArgumentState;
				if (targetStatus == OptimizationTargetStatus.Good)
				{
					OptimizationOriginalStateStore.SaveOrReplace<AutoCpuAffinity.AutoCpuAffinityState>(OptimizationId.AutoCpuAffinity, new AutoCpuAffinity.AutoCpuAffinityState
					{
						Launch = new AutoCpuAffinity.LaunchArgumentState
						{
							Exists = (num != -1 && num + 1 < list2.Count),
							Value = ((num != -1 && num + 1 < list2.Count) ? list2[num + 1] : null)
						},
						ClientCfg = AutoCpuAffinity.CaptureClientCfgState(list)
					});
					if (num == -1)
					{
						list2.Add("-cpu.affinity");
						list2.Add(goodCpuAffinity);
					}
					else if (num + 1 < list2.Count)
					{
						list2[num + 1] = goodCpuAffinity;
					}
					else
					{
						list2.Add(goodCpuAffinity);
					}
					AutoCpuAffinity.ReplaceClientCfgLine(list, AutoCpuAffinity.FindClientCfgLineIndex(list), "system.auto_cpu_affinity \"False\"");
				}
				else if (OptimizationOriginalStateStore.TryRead<AutoCpuAffinity.AutoCpuAffinityState>(OptimizationId.AutoCpuAffinity, out autoCpuAffinityState) && autoCpuAffinityState != null && (autoCpuAffinityState.Launch != null || autoCpuAffinityState.ClientCfg != null))
				{
					if (autoCpuAffinityState.Launch != null)
					{
						AutoCpuAffinity.RestoreLaunchArgument(list2, "-cpu.affinity", autoCpuAffinityState.Launch);
					}
					else if (num != -1)
					{
						AutoCpuAffinity.RemoveLaunchArgument(list2, num);
					}
					if (autoCpuAffinityState.ClientCfg != null)
					{
						AutoCpuAffinity.RestoreClientCfgLine(list, autoCpuAffinityState.ClientCfg);
					}
					else
					{
						AutoCpuAffinity.RemoveClientCfgLines(list);
					}
				}
				else if (OptimizationOriginalStateStore.TryRead<AutoCpuAffinity.LaunchArgumentState>(OptimizationId.AutoCpuAffinity, out launchArgumentState) && launchArgumentState != null)
				{
					AutoCpuAffinity.RestoreLaunchArgument(list2, "-cpu.affinity", launchArgumentState);
					AutoCpuAffinity.RemoveClientCfgLines(list);
				}
				else
				{
					if (num != -1)
					{
						AutoCpuAffinity.RemoveLaunchArgument(list2, num);
					}
					AutoCpuAffinity.RemoveClientCfgLines(list);
				}
				rustTweakerViewModel.UpdateLocalConfig(configPathToLastUser, string.Join<string>(' ', list2));
				if (!P4258EBF.AFA7138A.M6233B19[426](clientCfgPath))
				{
					P4258EBF.AFA7138A.M6233B19[213](clientCfgPath, list);
				}
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
			}
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00152028 File Offset: 0x0014F828
		private static void RestoreLaunchArgument(List<string> words, string argumentName, AutoCpuAffinity.LaunchArgumentState state)
		{
			int num = words.IndexOf(argumentName);
			if (!state.Exists)
			{
				if (num != -1)
				{
					AutoCpuAffinity.RemoveLaunchArgument(words, num);
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

		// Token: 0x06000221 RID: 545 RVA: 0x0000B24C File Offset: 0x0000964C
		private static void RemoveLaunchArgument(List<string> words, int index)
		{
			int num = ((index + 1 < words.Count) ? 2 : 1);
			words.RemoveRange(index, num);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000B274 File Offset: 0x00009674
		private static AutoCpuAffinity.ClientCfgState CaptureClientCfgState(IReadOnlyList<string> lines)
		{
			int num = AutoCpuAffinity.FindClientCfgLineIndex(lines);
			return new AutoCpuAffinity.ClientCfgState
			{
				Exists = (num != -1),
				Index = num,
				Line = ((num != -1) ? lines[num] : null)
			};
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0013E5C8 File Offset: 0x0013BDC8
		private static void ApplyClientCfg(OptimizationTargetStatus targetStatus)
		{
			string clientCfgPath = AutoCpuAffinity.GetClientCfgPath();
			if (P4258EBF.AFA7138A.M6233B19[426](clientCfgPath))
			{
				return;
			}
			List<string> list = (P4258EBF.AFA7138A.M6233B19[627](clientCfgPath) ? P4258EBF.AFA7138A.M6233B19[65](clientCfgPath).ToList<string>() : new List<string>());
			int num = AutoCpuAffinity.FindClientCfgLineIndex(list);
			AutoCpuAffinity.ClientCfgState clientCfgState;
			if (targetStatus == OptimizationTargetStatus.Good)
			{
				OptimizationOriginalStateStore.SaveIfMissing<AutoCpuAffinity.ClientCfgState>(OptimizationId.AutoCpuAffinity, new AutoCpuAffinity.ClientCfgState
				{
					Exists = (num != -1),
					Index = num,
					Line = ((num != -1) ? list[num] : null)
				});
				AutoCpuAffinity.ReplaceClientCfgLine(list, num, "system.auto_cpu_affinity \"True\"");
			}
			else if (OptimizationOriginalStateStore.TryRead<AutoCpuAffinity.ClientCfgState>(OptimizationId.AutoCpuAffinity, out clientCfgState) && clientCfgState != null)
			{
				AutoCpuAffinity.RestoreClientCfgLine(list, clientCfgState);
			}
			else
			{
				AutoCpuAffinity.RemoveClientCfgLines(list);
			}
			P4258EBF.AFA7138A.M6233B19[213](clientCfgPath, list);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0015B5F8 File Offset: 0x00158DF8
		[NullableContext(2)]
		private static string GetClientCfgPath()
		{
			string currentSelectedFolder = Configs.getCurrentSelectedFolder();
			if (P4258EBF.AFA7138A.M6233B19[426](currentSelectedFolder))
			{
				return null;
			}
			return P4258EBF.AFA7138A.M6233B19[278](currentSelectedFolder, "cfg", "client.cfg");
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000B390 File Offset: 0x00009790
		private static int FindClientCfgLineIndex(IReadOnlyList<string> lines)
		{
			for (int i = 0; i < lines.Count; i++)
			{
				if (AutoCpuAffinity.IsClientCfgKeyLine(lines[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x001466C4 File Offset: 0x00143EC4
		[NullableContext(2)]
		private static bool IsClientCfgKeyLine(string line)
		{
			return !P4258EBF.AFA7138A.M6233B19[426](line) && P4258EBF.AFA7138A.M6233B19[44](P4258EBF.AFA7138A.M6233B19[127](line), "system.auto_cpu_affinity", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00145670 File Offset: 0x00142E70
		private static bool IsEnabledLine(string line)
		{
			return P4258EBF.AFA7138A.M6233B19[492](P4258EBF.AFA7138A.M6233B19[597](line), "system.auto_cpu_affinity \"False\"", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000B3EF File Offset: 0x000097EF
		private static void RemoveClientCfgLines(List<string> lines)
		{
			Predicate<string> predicate;
			if ((predicate = AutoCpuAffinity.<>O.<0>__IsClientCfgKeyLine) == null)
			{
				predicate = (AutoCpuAffinity.<>O.<0>__IsClientCfgKeyLine = new Predicate<string>(AutoCpuAffinity.IsClientCfgKeyLine));
			}
			lines.RemoveAll(predicate);
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000B413 File Offset: 0x00009813
		private static void ReplaceClientCfgLine(List<string> lines, int index, string line)
		{
			AutoCpuAffinity.RemoveClientCfgLines(lines);
			if (index < 0 || index > lines.Count)
			{
				lines.Add(line);
				return;
			}
			lines.Insert(index, line);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0014C970 File Offset: 0x0014A170
		private static void RestoreClientCfgLine(List<string> lines, AutoCpuAffinity.ClientCfgState state)
		{
			AutoCpuAffinity.RemoveClientCfgLines(lines);
			if (!state.Exists)
			{
				return;
			}
			string text = (P4258EBF.AFA7138A.M6233B19[426](state.Line) ? "system.auto_cpu_affinity \"False\"" : state.Line);
			if (state.Index < 0 || state.Index > lines.Count)
			{
				lines.Add(text);
				return;
			}
			lines.Insert(state.Index, text);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000B49C File Offset: 0x0000989C
		private static CpuAffinityRecommendation BuildRecommendation()
		{
			string processorName = AutoCpuAffinity.GetProcessorName();
			AutoCpuAffinity.CpuSetInfo[] array = (from cpu in AutoCpuAffinity.GetCpuSets()
				orderby cpu.Group, cpu.LogicalProcessorIndex
				select cpu).ToArray<AutoCpuAffinity.CpuSetInfo>();
			L3CacheGroupInfo[] array2 = (from cache in CpuTopologyReader.GetL3CacheGroups()
				orderby cache.Group, cache.CacheSizeBytes descending, cache.SharedMask
				select cache).ToArray<L3CacheGroupInfo>();
			LogicalCpu[] performanceLogicalCpus = AutoCpuAffinity.GetPerformanceLogicalCpus(array);
			LogicalCpu[] largeL3LogicalCpus = AutoCpuAffinity.GetLargeL3LogicalCpus(array2, array);
			LogicalCpu[] array3 = (from cpu in performanceLogicalCpus.Intersect<LogicalCpu>(largeL3LogicalCpus)
				orderby cpu.Group, cpu.Index
				select cpu).ToArray<LogicalCpu>();
			if (array3.Length == 0)
			{
				array3 = (from cpu in array.Select<AutoCpuAffinity.CpuSetInfo, LogicalCpu>((AutoCpuAffinity.CpuSetInfo cpu) => cpu.LogicalCpu).Distinct<LogicalCpu>()
					orderby cpu.Group, cpu.Index
					select cpu).ToArray<LogicalCpu>();
			}
			LogicalCpu[] oneLogicalCpuPerPhysicalCore = AutoCpuAffinity.GetOneLogicalCpuPerPhysicalCore(array, array3);
			return new CpuAffinityRecommendation(processorName, array, array2, performanceLogicalCpus, largeL3LogicalCpus, array3, oneLogicalCpuPerPhysicalCore);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00139650 File Offset: 0x00136E50
		private static string GetProcessorName()
		{
			object obj = P4258EBF.AFA7138A.M6233B19[180]("HKEY_LOCAL_MACHINE\\HARDWARE\\DESCRIPTION\\System\\CentralProcessor\\0", "ProcessorNameString", null);
			string text = ((obj != null) ? obj.ToString() : null);
			if (!P4258EBF.AFA7138A.M6233B19[426](text))
			{
				return P4258EBF.AFA7138A.M6233B19[597](text);
			}
			return P4258EBF.AFA7138A.M6233B19[400]().ToString();
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000B6C4 File Offset: 0x00009AC4
		private static string GetGoodCpuAffinity()
		{
			CpuAffinityRecommendation cpuAffinityRecommendation = AutoCpuAffinity.BuildRecommendation();
			return AutoCpuAffinity.FormatCpuList(cpuAffinityRecommendation.OneLogicalCpuPerPhysicalCore);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000B6E4 File Offset: 0x00009AE4
		private static LogicalCpu[] GetPerformanceLogicalCpus(IReadOnlyCollection<AutoCpuAffinity.CpuSetInfo> cpuSets)
		{
			AutoCpuAffinity.<>c__DisplayClass26_0 CS$<>8__locals1 = new AutoCpuAffinity.<>c__DisplayClass26_0();
			if (cpuSets.Count == 0)
			{
				return Array.Empty<LogicalCpu>();
			}
			CS$<>8__locals1.efficiencyClasses = cpuSets.Select<AutoCpuAffinity.CpuSetInfo, byte>((AutoCpuAffinity.CpuSetInfo cpu) => cpu.EfficiencyClass).Distinct<byte>().Order<byte>()
				.ToArray<byte>();
			AutoCpuAffinity.<>c__DisplayClass26_0 CS$<>8__locals2 = CS$<>8__locals1;
			byte[] efficiencyClasses = CS$<>8__locals1.efficiencyClasses;
			CS$<>8__locals2.performanceClass = efficiencyClasses[efficiencyClasses.Length - 1];
			return (from cpu in (from cpu in cpuSets
					where CS$<>8__locals1.efficiencyClasses.Length == 1 || cpu.EfficiencyClass == CS$<>8__locals1.performanceClass
					select cpu.LogicalCpu).Distinct<LogicalCpu>()
				orderby cpu.Group, cpu.Index
				select cpu).ToArray<LogicalCpu>();
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000B7DC File Offset: 0x00009BDC
		private static LogicalCpu[] GetLargeL3LogicalCpus(IReadOnlyCollection<L3CacheGroupInfo> l3Caches, IReadOnlyCollection<AutoCpuAffinity.CpuSetInfo> cpuSets)
		{
			AutoCpuAffinity.<>c__DisplayClass27_0 CS$<>8__locals1 = new AutoCpuAffinity.<>c__DisplayClass27_0();
			if (l3Caches.Count == 0)
			{
				return (from cpu in cpuSets.Select<AutoCpuAffinity.CpuSetInfo, LogicalCpu>((AutoCpuAffinity.CpuSetInfo cpu) => cpu.LogicalCpu).Distinct<LogicalCpu>()
					orderby cpu.Group, cpu.Index
					select cpu).ToArray<LogicalCpu>();
			}
			CS$<>8__locals1.cacheSizes = l3Caches.Select<L3CacheGroupInfo, uint>((L3CacheGroupInfo cache) => cache.CacheSizeBytes).Distinct<uint>().Order<uint>()
				.ToArray<uint>();
			AutoCpuAffinity.<>c__DisplayClass27_0 CS$<>8__locals2 = CS$<>8__locals1;
			uint[] cacheSizes = CS$<>8__locals1.cacheSizes;
			CS$<>8__locals2.largestCacheSize = cacheSizes[cacheSizes.Length - 1];
			return (from cpu in l3Caches.Where<L3CacheGroupInfo>((L3CacheGroupInfo cache) => CS$<>8__locals1.cacheSizes.Length == 1 || cache.CacheSizeBytes == CS$<>8__locals1.largestCacheSize).SelectMany<L3CacheGroupInfo, LogicalCpu>((L3CacheGroupInfo cache) => cache.LogicalCpus).Distinct<LogicalCpu>()
				orderby cpu.Group, cpu.Index
				select cpu).ToArray<LogicalCpu>();
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000B944 File Offset: 0x00009D44
		private static LogicalCpu[] GetOneLogicalCpuPerPhysicalCore(IReadOnlyCollection<AutoCpuAffinity.CpuSetInfo> cpuSets, IReadOnlyCollection<LogicalCpu> selectedLogicalCpus)
		{
			HashSet<LogicalCpu> selected = selectedLogicalCpus.ToHashSet<LogicalCpu>();
			return (from cpu in cpuSets
				where selected.Contains(cpu.LogicalCpu)
				group cpu by new { cpu.Group, cpu.CoreIndex } into @group
				select @group.OrderByDescending<AutoCpuAffinity.CpuSetInfo, byte>((AutoCpuAffinity.CpuSetInfo cpu) => cpu.LogicalProcessorIndex).First<AutoCpuAffinity.CpuSetInfo>().LogicalCpu into cpu
				orderby cpu.Group, cpu.Index
				select cpu).ToArray<LogicalCpu>();
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00161688 File Offset: 0x0015EE88
		private static string FormatCpuList(IEnumerable<LogicalCpu> logicalCpus)
		{
			return D2B9D912.A91E8BBB(",", from cpu in logicalCpus
				orderby cpu.Group, cpu.Index
				select cpu.ToString());
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0015194C File Offset: 0x0014F14C
		private static UIntPtr BuildAffinityMask(IEnumerable<LogicalCpu> logicalCpus)
		{
			UIntPtr uintPtr = (UIntPtr)((IntPtr)0);
			using (IEnumerator<LogicalCpu> enumerator = logicalCpus.GetEnumerator())
			{
				while (P4258EBF.AFA7138A.M6233B19[411](enumerator))
				{
					LogicalCpu logicalCpu = enumerator.Current;
					uintPtr |= (UIntPtr)((IntPtr)1 << (logicalCpu.Index & (sizeof(UIntPtr) * 8 - 1)));
				}
			}
			return uintPtr;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0015CCA0 File Offset: 0x0015A4A0
		private static string FormatBinaryMask(UIntPtr mask, int maxLogicalProcessorIndex)
		{
			int num = P4258EBF.AFA7138A.M6233B19[264](1, maxLogicalProcessorIndex + 1);
			char[] array = new char[num];
			for (int i = 0; i < num; i++)
			{
				array[num - i - 1] = (((mask & (UIntPtr)((IntPtr)1 << (i & (sizeof(UIntPtr) * 8 - 1)))) != 0) ? '1' : '0');
			}
			return P4258EBF.AFA7138A.M6233B19[303](array);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000BB40 File Offset: 0x00009F40
		public static IEnumerable<AutoCpuAffinity.CpuSetInfo> GetCpuSets()
		{
			return new AutoCpuAffinity.<GetCpuSets>d__32(-2);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00140F50 File Offset: 0x0013E750
		public AutoCpuAffinity()
		{
			P4258EBF.AFA7138A.M6233B19[130](this);
		}

		// Token: 0x040000C0 RID: 192
		private const string ClientCfgKey = "system.auto_cpu_affinity";

		// Token: 0x020000A9 RID: 169
		private sealed class LaunchArgumentState
		{
			// Token: 0x170000B8 RID: 184
			// (get) Token: 0x0600046C RID: 1132 RVA: 0x0001A897 File Offset: 0x00018C97
			// (set) Token: 0x0600046D RID: 1133 RVA: 0x0001A89F File Offset: 0x00018C9F
			public bool Exists { get; set; }

			// Token: 0x170000B9 RID: 185
			// (get) Token: 0x0600046E RID: 1134 RVA: 0x0001A8A8 File Offset: 0x00018CA8
			// (set) Token: 0x0600046F RID: 1135 RVA: 0x0001A8B0 File Offset: 0x00018CB0
			public string Value { get; set; }

			// Token: 0x06000470 RID: 1136 RVA: 0x0015F120 File Offset: 0x0015C920
			public LaunchArgumentState()
			{
				P4258EBF.AFA7138A.M6233B19[130](this);
			}
		}

		// Token: 0x020000AA RID: 170
		[Nullable(0)]
		[NullableContext(2)]
		private sealed class ClientCfgState
		{
			// Token: 0x170000BA RID: 186
			// (get) Token: 0x06000471 RID: 1137 RVA: 0x0001A8C1 File Offset: 0x00018CC1
			// (set) Token: 0x06000472 RID: 1138 RVA: 0x0001A8C9 File Offset: 0x00018CC9
			public bool Exists { get; set; }

			// Token: 0x170000BB RID: 187
			// (get) Token: 0x06000473 RID: 1139 RVA: 0x0001A8D2 File Offset: 0x00018CD2
			// (set) Token: 0x06000474 RID: 1140 RVA: 0x0001A8DA File Offset: 0x00018CDA
			public int Index { get; set; }

			// Token: 0x170000BC RID: 188
			// (get) Token: 0x06000475 RID: 1141 RVA: 0x0001A8E3 File Offset: 0x00018CE3
			// (set) Token: 0x06000476 RID: 1142 RVA: 0x0001A8EB File Offset: 0x00018CEB
			public string Line { get; set; }

			// Token: 0x06000477 RID: 1143 RVA: 0x001584A8 File Offset: 0x00155CA8
			public ClientCfgState()
			{
				P4258EBF.AFA7138A.M6233B19[130](this);
			}
		}

		// Token: 0x020000AC RID: 172
		[NullableContext(2)]
		[Nullable(0)]
		private sealed class AutoCpuAffinityState
		{
			// Token: 0x170000BD RID: 189
			// (get) Token: 0x06000479 RID: 1145 RVA: 0x0001A8FC File Offset: 0x00018CFC
			// (set) Token: 0x0600047A RID: 1146 RVA: 0x0001A904 File Offset: 0x00018D04
			public AutoCpuAffinity.LaunchArgumentState Launch { get; set; }

			// Token: 0x170000BE RID: 190
			// (get) Token: 0x0600047B RID: 1147 RVA: 0x0001A90D File Offset: 0x00018D0D
			// (set) Token: 0x0600047C RID: 1148 RVA: 0x0001A915 File Offset: 0x00018D15
			public AutoCpuAffinity.ClientCfgState ClientCfg { get; set; }

			// Token: 0x0600047D RID: 1149 RVA: 0x00160E24 File Offset: 0x0015E624
			public AutoCpuAffinityState()
			{
				P4258EBF.AFA7138A.M6233B19[130](this);
			}
		}

		// Token: 0x020000AD RID: 173
		[NullableContext(1)]
		[Nullable(0)]
		public sealed class CpuSetInfo : IEquatable<AutoCpuAffinity.CpuSetInfo>
		{
			// Token: 0x0600047E RID: 1150 RVA: 0x0015D5AC File Offset: 0x0015ADAC
			public CpuSetInfo(uint Id, ushort Group, byte LogicalProcessorIndex, byte CoreIndex, byte LastLevelCacheIndex, byte NumaNodeIndex, byte EfficiencyClass)
			{
				this.Id = Id;
				this.Group = Group;
				this.LogicalProcessorIndex = LogicalProcessorIndex;
				this.CoreIndex = CoreIndex;
				this.LastLevelCacheIndex = LastLevelCacheIndex;
				this.NumaNodeIndex = NumaNodeIndex;
				this.EfficiencyClass = EfficiencyClass;
				P4258EBF.AFA7138A.M6233B19[130](this);
			}

			// Token: 0x170000BF RID: 191
			// (get) Token: 0x0600047F RID: 1151 RVA: 0x001546C0 File Offset: 0x00151EC0
			[CompilerGenerated]
			private Type EqualityContract
			{
				[CompilerGenerated]
				get
				{
					return P4258EBF.AFA7138A.M6233B19[22](typeof(AutoCpuAffinity.CpuSetInfo).TypeHandle);
				}
			}

			// Token: 0x170000C0 RID: 192
			// (get) Token: 0x06000480 RID: 1152 RVA: 0x0001A96F File Offset: 0x00018D6F
			// (set) Token: 0x06000481 RID: 1153 RVA: 0x0001A977 File Offset: 0x00018D77
			public uint Id { get; set; }

			// Token: 0x170000C1 RID: 193
			// (get) Token: 0x06000482 RID: 1154 RVA: 0x0001A980 File Offset: 0x00018D80
			// (set) Token: 0x06000483 RID: 1155 RVA: 0x0001A988 File Offset: 0x00018D88
			public ushort Group { get; set; }

			// Token: 0x170000C2 RID: 194
			// (get) Token: 0x06000484 RID: 1156 RVA: 0x0001A991 File Offset: 0x00018D91
			// (set) Token: 0x06000485 RID: 1157 RVA: 0x0001A999 File Offset: 0x00018D99
			public byte LogicalProcessorIndex { get; set; }

			// Token: 0x170000C3 RID: 195
			// (get) Token: 0x06000486 RID: 1158 RVA: 0x0001A9A2 File Offset: 0x00018DA2
			// (set) Token: 0x06000487 RID: 1159 RVA: 0x0001A9AA File Offset: 0x00018DAA
			public byte CoreIndex { get; set; }

			// Token: 0x170000C4 RID: 196
			// (get) Token: 0x06000488 RID: 1160 RVA: 0x0001A9B3 File Offset: 0x00018DB3
			// (set) Token: 0x06000489 RID: 1161 RVA: 0x0001A9BB File Offset: 0x00018DBB
			public byte LastLevelCacheIndex { get; set; }

			// Token: 0x170000C5 RID: 197
			// (get) Token: 0x0600048A RID: 1162 RVA: 0x0001A9C4 File Offset: 0x00018DC4
			// (set) Token: 0x0600048B RID: 1163 RVA: 0x0001A9CC File Offset: 0x00018DCC
			public byte NumaNodeIndex { get; set; }

			// Token: 0x170000C6 RID: 198
			// (get) Token: 0x0600048C RID: 1164 RVA: 0x0001A9D5 File Offset: 0x00018DD5
			// (set) Token: 0x0600048D RID: 1165 RVA: 0x0001A9DD File Offset: 0x00018DDD
			public byte EfficiencyClass { get; set; }

			// Token: 0x170000C7 RID: 199
			// (get) Token: 0x0600048E RID: 1166 RVA: 0x0001A9E6 File Offset: 0x00018DE6
			public LogicalCpu LogicalCpu
			{
				get
				{
					return new LogicalCpu(this.Group, (int)this.LogicalProcessorIndex);
				}
			}

			// Token: 0x0600048F RID: 1167 RVA: 0x001548D8 File Offset: 0x001520D8
			[CompilerGenerated]
			public override string ToString()
			{
				StringBuilder stringBuilder = P4258EBF.AFA7138A.M6233B19[353]();
				P4258EBF.AFA7138A.M6233B19[468](stringBuilder, "CpuSetInfo");
				P4258EBF.AFA7138A.M6233B19[468](stringBuilder, " { ");
				if (this.PrintMembers(stringBuilder))
				{
					P4258EBF.AFA7138A.M6233B19[168](stringBuilder, ' ');
				}
				P4258EBF.AFA7138A.M6233B19[168](stringBuilder, '}');
				return stringBuilder.ToString();
			}

			// Token: 0x06000490 RID: 1168 RVA: 0x00161010 File Offset: 0x0015E810
			[CompilerGenerated]
			private bool PrintMembers(StringBuilder builder)
			{
				P4258EBF.AFA7138A.M6233B19[253]();
				P4258EBF.AFA7138A.M6233B19[468](builder, "Id = ");
				P4258EBF.AFA7138A.M6233B19[468](builder, this.Id.ToString());
				P4258EBF.AFA7138A.M6233B19[468](builder, ", Group = ");
				P4258EBF.AFA7138A.M6233B19[468](builder, this.Group.ToString());
				P4258EBF.AFA7138A.M6233B19[468](builder, ", LogicalProcessorIndex = ");
				P4258EBF.AFA7138A.M6233B19[468](builder, this.LogicalProcessorIndex.ToString());
				P4258EBF.AFA7138A.M6233B19[468](builder, ", CoreIndex = ");
				P4258EBF.AFA7138A.M6233B19[468](builder, this.CoreIndex.ToString());
				P4258EBF.AFA7138A.M6233B19[468](builder, ", LastLevelCacheIndex = ");
				P4258EBF.AFA7138A.M6233B19[468](builder, this.LastLevelCacheIndex.ToString());
				P4258EBF.AFA7138A.M6233B19[468](builder, ", NumaNodeIndex = ");
				P4258EBF.AFA7138A.M6233B19[468](builder, this.NumaNodeIndex.ToString());
				P4258EBF.AFA7138A.M6233B19[468](builder, ", EfficiencyClass = ");
				P4258EBF.AFA7138A.M6233B19[468](builder, this.EfficiencyClass.ToString());
				P4258EBF.AFA7138A.M6233B19[468](builder, ", LogicalCpu = ");
				P4258EBF.AFA7138A.M6233B19[468](builder, this.LogicalCpu.ToString());
				return true;
			}

			// Token: 0x06000491 RID: 1169 RVA: 0x0001AB93 File Offset: 0x00018F93
			[CompilerGenerated]
			[NullableContext(2)]
			public static bool operator !=(AutoCpuAffinity.CpuSetInfo left, AutoCpuAffinity.CpuSetInfo right)
			{
				return !(left == right);
			}

			// Token: 0x06000492 RID: 1170 RVA: 0x0001AB9F File Offset: 0x00018F9F
			[CompilerGenerated]
			[NullableContext(2)]
			public static bool operator ==(AutoCpuAffinity.CpuSetInfo left, AutoCpuAffinity.CpuSetInfo right)
			{
				return left == right || (left != null && left.Equals(right));
			}

			// Token: 0x06000493 RID: 1171 RVA: 0x0001ABB4 File Offset: 0x00018FB4
			[CompilerGenerated]
			public override int GetHashCode()
			{
				return ((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<uint>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<ushort>.Default.GetHashCode(this.<Group>k__BackingField)) * -1521134295 + EqualityComparer<byte>.Default.GetHashCode(this.<LogicalProcessorIndex>k__BackingField)) * -1521134295 + EqualityComparer<byte>.Default.GetHashCode(this.<CoreIndex>k__BackingField)) * -1521134295 + EqualityComparer<byte>.Default.GetHashCode(this.<LastLevelCacheIndex>k__BackingField)) * -1521134295 + EqualityComparer<byte>.Default.GetHashCode(this.<NumaNodeIndex>k__BackingField)) * -1521134295 + EqualityComparer<byte>.Default.GetHashCode(this.<EfficiencyClass>k__BackingField);
			}

			// Token: 0x06000494 RID: 1172 RVA: 0x0001AC72 File Offset: 0x00019072
			[NullableContext(2)]
			[CompilerGenerated]
			public override bool Equals(object obj)
			{
				return this.Equals(obj as AutoCpuAffinity.CpuSetInfo);
			}

			// Token: 0x06000495 RID: 1173 RVA: 0x00155BE0 File Offset: 0x001533E0
			[CompilerGenerated]
			[NullableContext(2)]
			public bool Equals(AutoCpuAffinity.CpuSetInfo other)
			{
				return this == other || (other != null && P4258EBF.AFA7138A.M6233B19[125](this.EqualityContract, other.EqualityContract) && EqualityComparer<uint>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<ushort>.Default.Equals(this.<Group>k__BackingField, other.<Group>k__BackingField) && EqualityComparer<byte>.Default.Equals(this.<LogicalProcessorIndex>k__BackingField, other.<LogicalProcessorIndex>k__BackingField) && EqualityComparer<byte>.Default.Equals(this.<CoreIndex>k__BackingField, other.<CoreIndex>k__BackingField) && EqualityComparer<byte>.Default.Equals(this.<LastLevelCacheIndex>k__BackingField, other.<LastLevelCacheIndex>k__BackingField) && EqualityComparer<byte>.Default.Equals(this.<NumaNodeIndex>k__BackingField, other.<NumaNodeIndex>k__BackingField) && EqualityComparer<byte>.Default.Equals(this.<EfficiencyClass>k__BackingField, other.<EfficiencyClass>k__BackingField));
			}

			// Token: 0x06000497 RID: 1175 RVA: 0x0015E10C File Offset: 0x0015B90C
			[CompilerGenerated]
			private CpuSetInfo(AutoCpuAffinity.CpuSetInfo original)
			{
				P4258EBF.AFA7138A.M6233B19[130](this);
				this.Id = original.<Id>k__BackingField;
				this.Group = original.<Group>k__BackingField;
				this.LogicalProcessorIndex = original.<LogicalProcessorIndex>k__BackingField;
				this.CoreIndex = original.<CoreIndex>k__BackingField;
				this.LastLevelCacheIndex = original.<LastLevelCacheIndex>k__BackingField;
				this.NumaNodeIndex = original.<NumaNodeIndex>k__BackingField;
				this.EfficiencyClass = original.<EfficiencyClass>k__BackingField;
			}

			// Token: 0x06000498 RID: 1176 RVA: 0x0001ADCF File Offset: 0x000191CF
			[CompilerGenerated]
			public void Deconstruct(out uint Id, out ushort Group, out byte LogicalProcessorIndex, out byte CoreIndex, out byte LastLevelCacheIndex, out byte NumaNodeIndex, out byte EfficiencyClass)
			{
				Id = this.Id;
				Group = this.Group;
				LogicalProcessorIndex = this.LogicalProcessorIndex;
				CoreIndex = this.CoreIndex;
				LastLevelCacheIndex = this.LastLevelCacheIndex;
				NumaNodeIndex = this.NumaNodeIndex;
				EfficiencyClass = this.EfficiencyClass;
			}
		}

		// Token: 0x020000AE RID: 174
		private enum CpuSetInformationType
		{
			// Token: 0x04000239 RID: 569
			CpuSetInformation
		}

		// Token: 0x020000AF RID: 175
		private struct SystemCpuSetInformation
		{
			// Token: 0x0400023A RID: 570
			public uint Size;

			// Token: 0x0400023B RID: 571
			public AutoCpuAffinity.CpuSetInformationType Type;

			// Token: 0x0400023C RID: 572
			public uint Id;

			// Token: 0x0400023D RID: 573
			public ushort Group;

			// Token: 0x0400023E RID: 574
			public byte LogicalProcessorIndex;

			// Token: 0x0400023F RID: 575
			public byte CoreIndex;

			// Token: 0x04000240 RID: 576
			public byte LastLevelCacheIndex;

			// Token: 0x04000241 RID: 577
			public byte NumaNodeIndex;

			// Token: 0x04000242 RID: 578
			public byte EfficiencyClass;

			// Token: 0x04000243 RID: 579
			public byte AllFlags;

			// Token: 0x04000244 RID: 580
			public uint Reserved;

			// Token: 0x04000245 RID: 581
			public ulong AllocationTag;
		}

		// Token: 0x020000B0 RID: 176
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000246 RID: 582
			public static Predicate<string> <0>__IsClientCfgKeyLine;
		}
	}
}
