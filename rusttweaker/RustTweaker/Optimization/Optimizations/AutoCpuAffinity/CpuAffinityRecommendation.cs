using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace RustTweaker.Optimization.Optimizations.AutoCpuAffinity
{
	// Token: 0x02000038 RID: 56
	internal sealed class CpuAffinityRecommendation : IEquatable<CpuAffinityRecommendation>
	{
		// Token: 0x060001FA RID: 506 RVA: 0x0014DF24 File Offset: 0x0014B724
		public CpuAffinityRecommendation(string ProcessorName, IReadOnlyList<AutoCpuAffinity.CpuSetInfo> CpuSets, IReadOnlyList<L3CacheGroupInfo> L3Caches, IReadOnlyList<LogicalCpu> PerformanceLogicalCpus, IReadOnlyList<LogicalCpu> LargeL3LogicalCpus, IReadOnlyList<LogicalCpu> SelectedLogicalCpus, IReadOnlyList<LogicalCpu> OneLogicalCpuPerPhysicalCore)
		{
			this.ProcessorName = ProcessorName;
			this.CpuSets = CpuSets;
			this.L3Caches = L3Caches;
			this.PerformanceLogicalCpus = PerformanceLogicalCpus;
			this.LargeL3LogicalCpus = LargeL3LogicalCpus;
			this.SelectedLogicalCpus = SelectedLogicalCpus;
			this.OneLogicalCpuPerPhysicalCore = OneLogicalCpuPerPhysicalCore;
			P4258EBF.AFA7138A.M6233B19[130](this);
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001FB RID: 507 RVA: 0x0015A1BC File Offset: 0x001579BC
		[Nullable(1)]
		[CompilerGenerated]
		private Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return P4258EBF.AFA7138A.M6233B19[22](typeof(CpuAffinityRecommendation).TypeHandle);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001FC RID: 508 RVA: 0x0000A95F File Offset: 0x00008D5F
		// (set) Token: 0x060001FD RID: 509 RVA: 0x0000A967 File Offset: 0x00008D67
		public string ProcessorName { get; set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001FE RID: 510 RVA: 0x0000A970 File Offset: 0x00008D70
		// (set) Token: 0x060001FF RID: 511 RVA: 0x0000A978 File Offset: 0x00008D78
		public IReadOnlyList<AutoCpuAffinity.CpuSetInfo> CpuSets { get; set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000200 RID: 512 RVA: 0x0000A981 File Offset: 0x00008D81
		// (set) Token: 0x06000201 RID: 513 RVA: 0x0000A989 File Offset: 0x00008D89
		public IReadOnlyList<L3CacheGroupInfo> L3Caches { get; set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000202 RID: 514 RVA: 0x0000A992 File Offset: 0x00008D92
		// (set) Token: 0x06000203 RID: 515 RVA: 0x0000A99A File Offset: 0x00008D9A
		public IReadOnlyList<LogicalCpu> PerformanceLogicalCpus { get; set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000204 RID: 516 RVA: 0x0000A9A3 File Offset: 0x00008DA3
		// (set) Token: 0x06000205 RID: 517 RVA: 0x0000A9AB File Offset: 0x00008DAB
		public IReadOnlyList<LogicalCpu> LargeL3LogicalCpus { get; set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000206 RID: 518 RVA: 0x0000A9B4 File Offset: 0x00008DB4
		// (set) Token: 0x06000207 RID: 519 RVA: 0x0000A9BC File Offset: 0x00008DBC
		public IReadOnlyList<LogicalCpu> SelectedLogicalCpus { get; set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000208 RID: 520 RVA: 0x0000A9C5 File Offset: 0x00008DC5
		// (set) Token: 0x06000209 RID: 521 RVA: 0x0000A9CD File Offset: 0x00008DCD
		public IReadOnlyList<LogicalCpu> OneLogicalCpuPerPhysicalCore { get; set; }

		// Token: 0x0600020A RID: 522 RVA: 0x001601F0 File Offset: 0x0015D9F0
		[CompilerGenerated]
		[NullableContext(1)]
		public override string ToString()
		{
			StringBuilder stringBuilder = P4258EBF.AFA7138A.M6233B19[353]();
			P4258EBF.AFA7138A.M6233B19[468](stringBuilder, "CpuAffinityRecommendation");
			P4258EBF.AFA7138A.M6233B19[468](stringBuilder, " { ");
			if (this.PrintMembers(stringBuilder))
			{
				P4258EBF.AFA7138A.M6233B19[168](stringBuilder, ' ');
			}
			P4258EBF.AFA7138A.M6233B19[168](stringBuilder, '}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00161CF4 File Offset: 0x0015F4F4
		[NullableContext(1)]
		[CompilerGenerated]
		private bool PrintMembers(StringBuilder builder)
		{
			P4258EBF.AFA7138A.M6233B19[253]();
			P4258EBF.AFA7138A.M6233B19[468](builder, "ProcessorName = ");
			P4258EBF.AFA7138A.M6233B19[27](builder, this.ProcessorName);
			P4258EBF.AFA7138A.M6233B19[468](builder, ", CpuSets = ");
			P4258EBF.AFA7138A.M6233B19[27](builder, this.CpuSets);
			P4258EBF.AFA7138A.M6233B19[468](builder, ", L3Caches = ");
			P4258EBF.AFA7138A.M6233B19[27](builder, this.L3Caches);
			P4258EBF.AFA7138A.M6233B19[468](builder, ", PerformanceLogicalCpus = ");
			P4258EBF.AFA7138A.M6233B19[27](builder, this.PerformanceLogicalCpus);
			P4258EBF.AFA7138A.M6233B19[468](builder, ", LargeL3LogicalCpus = ");
			P4258EBF.AFA7138A.M6233B19[27](builder, this.LargeL3LogicalCpus);
			P4258EBF.AFA7138A.M6233B19[468](builder, ", SelectedLogicalCpus = ");
			P4258EBF.AFA7138A.M6233B19[27](builder, this.SelectedLogicalCpus);
			P4258EBF.AFA7138A.M6233B19[468](builder, ", OneLogicalCpuPerPhysicalCore = ");
			P4258EBF.AFA7138A.M6233B19[27](builder, this.OneLogicalCpuPerPhysicalCore);
			return true;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000AAE6 File Offset: 0x00008EE6
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(CpuAffinityRecommendation left, CpuAffinityRecommendation right)
		{
			return !(left == right);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000AAF2 File Offset: 0x00008EF2
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(CpuAffinityRecommendation left, CpuAffinityRecommendation right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000AB08 File Offset: 0x00008F08
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ProcessorName>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<AutoCpuAffinity.CpuSetInfo>>.Default.GetHashCode(this.<CpuSets>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<L3CacheGroupInfo>>.Default.GetHashCode(this.<L3Caches>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<LogicalCpu>>.Default.GetHashCode(this.<PerformanceLogicalCpus>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<LogicalCpu>>.Default.GetHashCode(this.<LargeL3LogicalCpus>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<LogicalCpu>>.Default.GetHashCode(this.<SelectedLogicalCpus>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<LogicalCpu>>.Default.GetHashCode(this.<OneLogicalCpuPerPhysicalCore>k__BackingField);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000ABC6 File Offset: 0x00008FC6
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as CpuAffinityRecommendation);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0015A8D0 File Offset: 0x001580D0
		[CompilerGenerated]
		[NullableContext(2)]
		public bool Equals(CpuAffinityRecommendation other)
		{
			return this == other || (other != null && P4258EBF.AFA7138A.M6233B19[125](this.EqualityContract, other.EqualityContract) && EqualityComparer<string>.Default.Equals(this.<ProcessorName>k__BackingField, other.<ProcessorName>k__BackingField) && EqualityComparer<IReadOnlyList<AutoCpuAffinity.CpuSetInfo>>.Default.Equals(this.<CpuSets>k__BackingField, other.<CpuSets>k__BackingField) && EqualityComparer<IReadOnlyList<L3CacheGroupInfo>>.Default.Equals(this.<L3Caches>k__BackingField, other.<L3Caches>k__BackingField) && EqualityComparer<IReadOnlyList<LogicalCpu>>.Default.Equals(this.<PerformanceLogicalCpus>k__BackingField, other.<PerformanceLogicalCpus>k__BackingField) && EqualityComparer<IReadOnlyList<LogicalCpu>>.Default.Equals(this.<LargeL3LogicalCpus>k__BackingField, other.<LargeL3LogicalCpus>k__BackingField) && EqualityComparer<IReadOnlyList<LogicalCpu>>.Default.Equals(this.<SelectedLogicalCpus>k__BackingField, other.<SelectedLogicalCpus>k__BackingField) && EqualityComparer<IReadOnlyList<LogicalCpu>>.Default.Equals(this.<OneLogicalCpuPerPhysicalCore>k__BackingField, other.<OneLogicalCpuPerPhysicalCore>k__BackingField));
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00160CF4 File Offset: 0x0015E4F4
		[CompilerGenerated]
		private CpuAffinityRecommendation([Nullable(1)] CpuAffinityRecommendation original)
		{
			P4258EBF.AFA7138A.M6233B19[130](this);
			this.ProcessorName = original.<ProcessorName>k__BackingField;
			this.CpuSets = original.<CpuSets>k__BackingField;
			this.L3Caches = original.<L3Caches>k__BackingField;
			this.PerformanceLogicalCpus = original.<PerformanceLogicalCpus>k__BackingField;
			this.LargeL3LogicalCpus = original.<LargeL3LogicalCpus>k__BackingField;
			this.SelectedLogicalCpus = original.<SelectedLogicalCpus>k__BackingField;
			this.OneLogicalCpuPerPhysicalCore = original.<OneLogicalCpuPerPhysicalCore>k__BackingField;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000AD23 File Offset: 0x00009123
		[CompilerGenerated]
		public void Deconstruct(out string ProcessorName, out IReadOnlyList<AutoCpuAffinity.CpuSetInfo> CpuSets, out IReadOnlyList<L3CacheGroupInfo> L3Caches, out IReadOnlyList<LogicalCpu> PerformanceLogicalCpus, out IReadOnlyList<LogicalCpu> LargeL3LogicalCpus, out IReadOnlyList<LogicalCpu> SelectedLogicalCpus, out IReadOnlyList<LogicalCpu> OneLogicalCpuPerPhysicalCore)
		{
			ProcessorName = this.ProcessorName;
			CpuSets = this.CpuSets;
			L3Caches = this.L3Caches;
			PerformanceLogicalCpus = this.PerformanceLogicalCpus;
			LargeL3LogicalCpus = this.LargeL3LogicalCpus;
			SelectedLogicalCpus = this.SelectedLogicalCpus;
			OneLogicalCpuPerPhysicalCore = this.OneLogicalCpuPerPhysicalCore;
		}
	}
}
