using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Vanara.PInvoke;

namespace RustTweaker.Optimization.Optimizations.AutoCpuAffinity
{
	// Token: 0x02000037 RID: 55
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class L3CacheGroupInfo : IEquatable<L3CacheGroupInfo>
	{
		// Token: 0x060001E0 RID: 480 RVA: 0x0015FA00 File Offset: 0x0015D200
		public L3CacheGroupInfo(ushort Group, uint CacheSizeBytes, Kernel32.PROCESSOR_CACHE_TYPE CacheType, ushort LineSize, byte Associativity, UIntPtr SharedMask)
		{
			this.Group = Group;
			this.CacheSizeBytes = CacheSizeBytes;
			this.CacheType = CacheType;
			this.LineSize = LineSize;
			this.Associativity = Associativity;
			this.SharedMask = SharedMask;
			P4258EBF.AFA7138A.M6233B19[130](this);
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00161B70 File Offset: 0x0015F370
		[CompilerGenerated]
		private Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return P4258EBF.AFA7138A.M6233B19[22](typeof(L3CacheGroupInfo).TypeHandle);
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x0000A4E7 File Offset: 0x000088E7
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x0000A4EF File Offset: 0x000088EF
		public ushort Group { get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x0000A4F8 File Offset: 0x000088F8
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x0000A500 File Offset: 0x00008900
		public uint CacheSizeBytes { get; set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x0000A509 File Offset: 0x00008909
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x0000A511 File Offset: 0x00008911
		public Kernel32.PROCESSOR_CACHE_TYPE CacheType { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x0000A51A File Offset: 0x0000891A
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x0000A522 File Offset: 0x00008922
		public ushort LineSize { get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001EA RID: 490 RVA: 0x0000A52B File Offset: 0x0000892B
		// (set) Token: 0x060001EB RID: 491 RVA: 0x0000A533 File Offset: 0x00008933
		public byte Associativity { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001EC RID: 492 RVA: 0x0000A53C File Offset: 0x0000893C
		// (set) Token: 0x060001ED RID: 493 RVA: 0x0000A544 File Offset: 0x00008944
		public UIntPtr SharedMask { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001EE RID: 494 RVA: 0x0000A54D File Offset: 0x0000894D
		[Nullable(0)]
		public IReadOnlyList<LogicalCpu> LogicalCpus
		{
			[NullableContext(0)]
			get
			{
				return (from index in CpuTopologyReader.GetLogicalProcessors(this.SharedMask)
					select new LogicalCpu(this.Group, index)).ToArray<LogicalCpu>();
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0015F8BC File Offset: 0x0015D0BC
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = P4258EBF.AFA7138A.M6233B19[353]();
			P4258EBF.AFA7138A.M6233B19[468](stringBuilder, "L3CacheGroupInfo");
			P4258EBF.AFA7138A.M6233B19[468](stringBuilder, " { ");
			if (this.PrintMembers(stringBuilder))
			{
				P4258EBF.AFA7138A.M6233B19[168](stringBuilder, ' ');
			}
			P4258EBF.AFA7138A.M6233B19[168](stringBuilder, '}');
			return stringBuilder.ToString();
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00157EA4 File Offset: 0x001556A4
		[CompilerGenerated]
		private bool PrintMembers(StringBuilder builder)
		{
			P4258EBF.AFA7138A.M6233B19[253]();
			P4258EBF.AFA7138A.M6233B19[468](builder, "Group = ");
			P4258EBF.AFA7138A.M6233B19[468](builder, this.Group.ToString());
			P4258EBF.AFA7138A.M6233B19[468](builder, ", CacheSizeBytes = ");
			P4258EBF.AFA7138A.M6233B19[468](builder, this.CacheSizeBytes.ToString());
			P4258EBF.AFA7138A.M6233B19[468](builder, ", CacheType = ");
			P4258EBF.AFA7138A.M6233B19[468](builder, this.CacheType.ToString());
			P4258EBF.AFA7138A.M6233B19[468](builder, ", LineSize = ");
			P4258EBF.AFA7138A.M6233B19[468](builder, this.LineSize.ToString());
			P4258EBF.AFA7138A.M6233B19[468](builder, ", Associativity = ");
			P4258EBF.AFA7138A.M6233B19[468](builder, this.Associativity.ToString());
			P4258EBF.AFA7138A.M6233B19[468](builder, ", SharedMask = ");
			P4258EBF.AFA7138A.M6233B19[468](builder, this.SharedMask.ToString());
			P4258EBF.AFA7138A.M6233B19[468](builder, ", LogicalCpus = ");
			P4258EBF.AFA7138A.M6233B19[27](builder, this.LogicalCpus);
			return true;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000A6D3 File Offset: 0x00008AD3
		[CompilerGenerated]
		[NullableContext(2)]
		public static bool operator !=(L3CacheGroupInfo left, L3CacheGroupInfo right)
		{
			return !(left == right);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000A6DF File Offset: 0x00008ADF
		[CompilerGenerated]
		[NullableContext(2)]
		public static bool operator ==(L3CacheGroupInfo left, L3CacheGroupInfo right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000A6F4 File Offset: 0x00008AF4
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<ushort>.Default.GetHashCode(this.<Group>k__BackingField)) * -1521134295 + EqualityComparer<uint>.Default.GetHashCode(this.<CacheSizeBytes>k__BackingField)) * -1521134295 + EqualityComparer<Kernel32.PROCESSOR_CACHE_TYPE>.Default.GetHashCode(this.<CacheType>k__BackingField)) * -1521134295 + EqualityComparer<ushort>.Default.GetHashCode(this.<LineSize>k__BackingField)) * -1521134295 + EqualityComparer<byte>.Default.GetHashCode(this.<Associativity>k__BackingField)) * -1521134295 + EqualityComparer<UIntPtr>.Default.GetHashCode(this.<SharedMask>k__BackingField);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000A79B File Offset: 0x00008B9B
		[CompilerGenerated]
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as L3CacheGroupInfo);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00130124 File Offset: 0x0012D924
		[CompilerGenerated]
		[NullableContext(2)]
		public bool Equals(L3CacheGroupInfo other)
		{
			return this == other || (other != null && P4258EBF.AFA7138A.M6233B19[125](this.EqualityContract, other.EqualityContract) && EqualityComparer<ushort>.Default.Equals(this.<Group>k__BackingField, other.<Group>k__BackingField) && EqualityComparer<uint>.Default.Equals(this.<CacheSizeBytes>k__BackingField, other.<CacheSizeBytes>k__BackingField) && EqualityComparer<Kernel32.PROCESSOR_CACHE_TYPE>.Default.Equals(this.<CacheType>k__BackingField, other.<CacheType>k__BackingField) && EqualityComparer<ushort>.Default.Equals(this.<LineSize>k__BackingField, other.<LineSize>k__BackingField) && EqualityComparer<byte>.Default.Equals(this.<Associativity>k__BackingField, other.<Associativity>k__BackingField) && EqualityComparer<UIntPtr>.Default.Equals(this.<SharedMask>k__BackingField, other.<SharedMask>k__BackingField));
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00160D94 File Offset: 0x0015E594
		[CompilerGenerated]
		private L3CacheGroupInfo(L3CacheGroupInfo original)
		{
			P4258EBF.AFA7138A.M6233B19[130](this);
			this.Group = original.<Group>k__BackingField;
			this.CacheSizeBytes = original.<CacheSizeBytes>k__BackingField;
			this.CacheType = original.<CacheType>k__BackingField;
			this.LineSize = original.<LineSize>k__BackingField;
			this.Associativity = original.<Associativity>k__BackingField;
			this.SharedMask = original.<SharedMask>k__BackingField;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000A8D3 File Offset: 0x00008CD3
		[CompilerGenerated]
		public void Deconstruct(out ushort Group, out uint CacheSizeBytes, out Kernel32.PROCESSOR_CACHE_TYPE CacheType, out ushort LineSize, out byte Associativity, out UIntPtr SharedMask)
		{
			Group = this.Group;
			CacheSizeBytes = this.CacheSizeBytes;
			CacheType = this.CacheType;
			LineSize = this.LineSize;
			Associativity = this.Associativity;
			SharedMask = this.SharedMask;
		}
	}
}
