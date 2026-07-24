using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Vanara.PInvoke;

namespace RustTweaker.Optimization.Optimizations.AutoCpuAffinity
{
	// Token: 0x02000036 RID: 54
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class LogicalProcessorL3CacheInfo : IEquatable<LogicalProcessorL3CacheInfo>
	{
		// Token: 0x060001C3 RID: 451 RVA: 0x0015E48C File Offset: 0x0015BC8C
		public LogicalProcessorL3CacheInfo(ushort Group, int LogicalProcessorIndex, uint CacheSizeBytes, byte CacheLevel, Kernel32.PROCESSOR_CACHE_TYPE CacheType, ushort LineSize, byte Associativity, UIntPtr SharedMask)
		{
			this.Group = Group;
			this.LogicalProcessorIndex = LogicalProcessorIndex;
			this.CacheSizeBytes = CacheSizeBytes;
			this.CacheLevel = CacheLevel;
			this.CacheType = CacheType;
			this.LineSize = LineSize;
			this.Associativity = Associativity;
			this.SharedMask = SharedMask;
			P4258EBF.AFA7138A.M6233B19[130](this);
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x0015E5A0 File Offset: 0x0015BDA0
		[CompilerGenerated]
		private Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return P4258EBF.AFA7138A.M6233B19[22](typeof(LogicalProcessorL3CacheInfo).TypeHandle);
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00009F7C File Offset: 0x0000837C
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x00009F84 File Offset: 0x00008384
		public ushort Group { get; set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00009F8D File Offset: 0x0000838D
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x00009F95 File Offset: 0x00008395
		public int LogicalProcessorIndex { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00009F9E File Offset: 0x0000839E
		// (set) Token: 0x060001CA RID: 458 RVA: 0x00009FA6 File Offset: 0x000083A6
		public uint CacheSizeBytes { get; set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00009FAF File Offset: 0x000083AF
		// (set) Token: 0x060001CC RID: 460 RVA: 0x00009FB7 File Offset: 0x000083B7
		public byte CacheLevel { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00009FC0 File Offset: 0x000083C0
		// (set) Token: 0x060001CE RID: 462 RVA: 0x00009FC8 File Offset: 0x000083C8
		public Kernel32.PROCESSOR_CACHE_TYPE CacheType { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00009FD1 File Offset: 0x000083D1
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x00009FD9 File Offset: 0x000083D9
		public ushort LineSize { get; set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x00009FE2 File Offset: 0x000083E2
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x00009FEA File Offset: 0x000083EA
		public byte Associativity { get; set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00009FF3 File Offset: 0x000083F3
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x00009FFB File Offset: 0x000083FB
		public UIntPtr SharedMask { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x0000A004 File Offset: 0x00008404
		public LogicalCpu LogicalCpu
		{
			get
			{
				return new LogicalCpu(this.Group, this.LogicalProcessorIndex);
			}
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00156854 File Offset: 0x00154054
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = P4258EBF.AFA7138A.M6233B19[353]();
			P4258EBF.AFA7138A.M6233B19[468](stringBuilder, "LogicalProcessorL3CacheInfo");
			P4258EBF.AFA7138A.M6233B19[468](stringBuilder, " { ");
			if (this.PrintMembers(stringBuilder))
			{
				P4258EBF.AFA7138A.M6233B19[168](stringBuilder, ' ');
			}
			P4258EBF.AFA7138A.M6233B19[168](stringBuilder, '}');
			return stringBuilder.ToString();
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00156920 File Offset: 0x00154120
		[CompilerGenerated]
		private bool PrintMembers(StringBuilder builder)
		{
			P4258EBF.AFA7138A.M6233B19[253]();
			P4258EBF.AFA7138A.M6233B19[468](builder, "Group = ");
			P4258EBF.AFA7138A.M6233B19[468](builder, this.Group.ToString());
			P4258EBF.AFA7138A.M6233B19[468](builder, ", LogicalProcessorIndex = ");
			P4258EBF.AFA7138A.M6233B19[468](builder, this.LogicalProcessorIndex.ToString());
			P4258EBF.AFA7138A.M6233B19[468](builder, ", CacheSizeBytes = ");
			P4258EBF.AFA7138A.M6233B19[468](builder, this.CacheSizeBytes.ToString());
			P4258EBF.AFA7138A.M6233B19[468](builder, ", CacheLevel = ");
			P4258EBF.AFA7138A.M6233B19[468](builder, this.CacheLevel.ToString());
			P4258EBF.AFA7138A.M6233B19[468](builder, ", CacheType = ");
			P4258EBF.AFA7138A.M6233B19[468](builder, this.CacheType.ToString());
			P4258EBF.AFA7138A.M6233B19[468](builder, ", LineSize = ");
			P4258EBF.AFA7138A.M6233B19[468](builder, this.LineSize.ToString());
			P4258EBF.AFA7138A.M6233B19[468](builder, ", Associativity = ");
			P4258EBF.AFA7138A.M6233B19[468](builder, this.Associativity.ToString());
			P4258EBF.AFA7138A.M6233B19[468](builder, ", SharedMask = ");
			P4258EBF.AFA7138A.M6233B19[468](builder, this.SharedMask.ToString());
			P4258EBF.AFA7138A.M6233B19[468](builder, ", LogicalCpu = ");
			P4258EBF.AFA7138A.M6233B19[468](builder, this.LogicalCpu.ToString());
			return true;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000A1D9 File Offset: 0x000085D9
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(LogicalProcessorL3CacheInfo left, LogicalProcessorL3CacheInfo right)
		{
			return !(left == right);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000A1E5 File Offset: 0x000085E5
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(LogicalProcessorL3CacheInfo left, LogicalProcessorL3CacheInfo right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000A1FC File Offset: 0x000085FC
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<ushort>.Default.GetHashCode(this.<Group>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<LogicalProcessorIndex>k__BackingField)) * -1521134295 + EqualityComparer<uint>.Default.GetHashCode(this.<CacheSizeBytes>k__BackingField)) * -1521134295 + EqualityComparer<byte>.Default.GetHashCode(this.<CacheLevel>k__BackingField)) * -1521134295 + EqualityComparer<Kernel32.PROCESSOR_CACHE_TYPE>.Default.GetHashCode(this.<CacheType>k__BackingField)) * -1521134295 + EqualityComparer<ushort>.Default.GetHashCode(this.<LineSize>k__BackingField)) * -1521134295 + EqualityComparer<byte>.Default.GetHashCode(this.<Associativity>k__BackingField)) * -1521134295 + EqualityComparer<UIntPtr>.Default.GetHashCode(this.<SharedMask>k__BackingField);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000A2D1 File Offset: 0x000086D1
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as LogicalProcessorL3CacheInfo);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00155AD0 File Offset: 0x001532D0
		[NullableContext(2)]
		[CompilerGenerated]
		public bool Equals(LogicalProcessorL3CacheInfo other)
		{
			return this == other || (other != null && P4258EBF.AFA7138A.M6233B19[125](this.EqualityContract, other.EqualityContract) && EqualityComparer<ushort>.Default.Equals(this.<Group>k__BackingField, other.<Group>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<LogicalProcessorIndex>k__BackingField, other.<LogicalProcessorIndex>k__BackingField) && EqualityComparer<uint>.Default.Equals(this.<CacheSizeBytes>k__BackingField, other.<CacheSizeBytes>k__BackingField) && EqualityComparer<byte>.Default.Equals(this.<CacheLevel>k__BackingField, other.<CacheLevel>k__BackingField) && EqualityComparer<Kernel32.PROCESSOR_CACHE_TYPE>.Default.Equals(this.<CacheType>k__BackingField, other.<CacheType>k__BackingField) && EqualityComparer<ushort>.Default.Equals(this.<LineSize>k__BackingField, other.<LineSize>k__BackingField) && EqualityComparer<byte>.Default.Equals(this.<Associativity>k__BackingField, other.<Associativity>k__BackingField) && EqualityComparer<UIntPtr>.Default.Equals(this.<SharedMask>k__BackingField, other.<SharedMask>k__BackingField));
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0014F2C8 File Offset: 0x0014CAC8
		[CompilerGenerated]
		private LogicalProcessorL3CacheInfo(LogicalProcessorL3CacheInfo original)
		{
			P4258EBF.AFA7138A.M6233B19[130](this);
			this.Group = original.<Group>k__BackingField;
			this.LogicalProcessorIndex = original.<LogicalProcessorIndex>k__BackingField;
			this.CacheSizeBytes = original.<CacheSizeBytes>k__BackingField;
			this.CacheLevel = original.<CacheLevel>k__BackingField;
			this.CacheType = original.<CacheType>k__BackingField;
			this.LineSize = original.<LineSize>k__BackingField;
			this.Associativity = original.<Associativity>k__BackingField;
			this.SharedMask = original.<SharedMask>k__BackingField;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000A454 File Offset: 0x00008854
		[CompilerGenerated]
		public void Deconstruct(out ushort Group, out int LogicalProcessorIndex, out uint CacheSizeBytes, out byte CacheLevel, out Kernel32.PROCESSOR_CACHE_TYPE CacheType, out ushort LineSize, out byte Associativity, out UIntPtr SharedMask)
		{
			Group = this.Group;
			LogicalProcessorIndex = this.LogicalProcessorIndex;
			CacheSizeBytes = this.CacheSizeBytes;
			CacheLevel = this.CacheLevel;
			CacheType = this.CacheType;
			LineSize = this.LineSize;
			Associativity = this.Associativity;
			SharedMask = this.SharedMask;
		}
	}
}
