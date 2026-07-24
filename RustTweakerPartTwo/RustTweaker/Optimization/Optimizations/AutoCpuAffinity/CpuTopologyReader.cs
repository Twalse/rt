using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Vanara.PInvoke;

namespace RustTweaker.Optimization.Optimizations.AutoCpuAffinity
{
	// Token: 0x02000039 RID: 57
	public static class CpuTopologyReader
	{
		// Token: 0x06000214 RID: 532 RVA: 0x001425B0 File Offset: 0x0013FDB0
		public static List<L3CacheGroupInfo> GetL3CacheGroups()
		{
			List<L3CacheGroupInfo> list = new List<L3CacheGroupInfo>();
			uint num = 0U;
			P4258EBF.AFA7138A.M6233B19[485](Kernel32.LOGICAL_PROCESSOR_RELATIONSHIP.RelationCache, P4258EBF.AFA7138A.M6233B19[500](), ref num);
			if (num == 0U)
			{
				return list;
			}
			IntPtr intPtr = P4258EBF.AFA7138A.M6233B19[594]((int)num);
			try
			{
				if (!P4258EBF.AFA7138A.M6233B19[485](Kernel32.LOGICAL_PROCESSOR_RELATIONSHIP.RelationCache, intPtr, ref num))
				{
					P4258EBF.AFA7138A.M6233B19[138](null);
				}
				IntPtr intPtr2 = intPtr;
				IntPtr intPtr3 = P4258EBF.AFA7138A.M6233B19[340](intPtr, (int)num);
				while (P4258EBF.AFA7138A.M6233B19[101](ref intPtr2) < P4258EBF.AFA7138A.M6233B19[101](ref intPtr3))
				{
					Kernel32.SYSTEM_LOGICAL_PROCESSOR_INFORMATION_EX system_LOGICAL_PROCESSOR_INFORMATION_EX = Marshal.PtrToStructure<Kernel32.SYSTEM_LOGICAL_PROCESSOR_INFORMATION_EX>(intPtr2);
					if (system_LOGICAL_PROCESSOR_INFORMATION_EX.Size == 0U)
					{
						break;
					}
					if (system_LOGICAL_PROCESSOR_INFORMATION_EX.Relationship == Kernel32.LOGICAL_PROCESSOR_RELATIONSHIP.RelationCache)
					{
						Kernel32.CACHE_RELATIONSHIP cache = system_LOGICAL_PROCESSOR_INFORMATION_EX.Cache;
						if (cache.Level == 3)
						{
							list.Add(new L3CacheGroupInfo(cache.GroupMask.Group, cache.CacheSize, cache.Type, cache.LineSize, cache.Associativity, cache.GroupMask.Mask));
						}
					}
					intPtr2 = P4258EBF.AFA7138A.M6233B19[340](intPtr2, (int)system_LOGICAL_PROCESSOR_INFORMATION_EX.Size);
				}
			}
			finally
			{
				P4258EBF.AFA7138A.M6233B19[347](intPtr);
			}
			return list;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000AE68 File Offset: 0x00009268
		public static List<LogicalProcessorL3CacheInfo> GetL3CachePerLogicalProcessor()
		{
			return CpuTopologyReader.GetL3CacheGroups().SelectMany<L3CacheGroupInfo, LogicalProcessorL3CacheInfo>((L3CacheGroupInfo cache) => from logicalProcessorIndex in CpuTopologyReader.GetLogicalProcessors(cache.SharedMask)
				select new LogicalProcessorL3CacheInfo(cache.Group, logicalProcessorIndex, cache.CacheSizeBytes, 3, cache.CacheType, cache.LineSize, cache.Associativity, cache.SharedMask)).ToList<LogicalProcessorL3CacheInfo>();
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000AE98 File Offset: 0x00009298
		public static IEnumerable<int> GetLogicalProcessors(UIntPtr mask)
		{
			CpuTopologyReader.<GetLogicalProcessors>d__2 <GetLogicalProcessors>d__ = new CpuTopologyReader.<GetLogicalProcessors>d__2(-2);
			<GetLogicalProcessors>d__.<>3__mask = mask;
			return <GetLogicalProcessors>d__;
		}
	}
}
