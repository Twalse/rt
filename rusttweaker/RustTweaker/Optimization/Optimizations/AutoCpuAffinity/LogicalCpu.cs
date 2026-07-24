using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace RustTweaker.Optimization.Optimizations.AutoCpuAffinity
{
	// Token: 0x02000035 RID: 53
	public readonly struct LogicalCpu : IEquatable<LogicalCpu>
	{
		// Token: 0x060001B6 RID: 438 RVA: 0x00009D9A File Offset: 0x0000819A
		public LogicalCpu(ushort Group, int Index)
		{
			this.Group = Group;
			this.Index = Index;
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00009DAA File Offset: 0x000081AA
		// (set) Token: 0x060001B8 RID: 440 RVA: 0x00009DB2 File Offset: 0x000081B2
		public ushort Group { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00009DBB File Offset: 0x000081BB
		// (set) Token: 0x060001BA RID: 442 RVA: 0x00009DC3 File Offset: 0x000081C3
		public int Index { get; set; }

		// Token: 0x060001BB RID: 443 RVA: 0x0014F348 File Offset: 0x0014CB48
		public override string ToString()
		{
			if (this.Group != 0)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
				P4258EBF.AFA7138A.M6233B19[466](ref defaultInterpolatedStringHandler, 1, 2);
				defaultInterpolatedStringHandler.AppendFormatted<ushort>(this.Group);
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, ":");
				defaultInterpolatedStringHandler.AppendFormatted<int>(this.Index);
				return P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler);
			}
			int index = this.Index;
			return P4258EBF.AFA7138A.M6233B19[24](ref index);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0013A30C File Offset: 0x00137B0C
		[CompilerGenerated]
		private bool PrintMembers(StringBuilder builder)
		{
			P4258EBF.AFA7138A.M6233B19[468](builder, "Group = ");
			P4258EBF.AFA7138A.M6233B19[468](builder, this.Group.ToString());
			P4258EBF.AFA7138A.M6233B19[468](builder, ", Index = ");
			P4258EBF.AFA7138A.M6233B19[468](builder, this.Index.ToString());
			return true;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00009E84 File Offset: 0x00008284
		[CompilerGenerated]
		public static bool operator !=(LogicalCpu left, LogicalCpu right)
		{
			return !(left == right);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00009E90 File Offset: 0x00008290
		[CompilerGenerated]
		public static bool operator ==(LogicalCpu left, LogicalCpu right)
		{
			return left.Equals(right);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00009E9A File Offset: 0x0000829A
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return EqualityComparer<ushort>.Default.GetHashCode(this.<Group>k__BackingField) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Index>k__BackingField);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00009EC3 File Offset: 0x000082C3
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return obj is LogicalCpu && this.Equals((LogicalCpu)obj);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00009EDB File Offset: 0x000082DB
		[CompilerGenerated]
		public bool Equals(LogicalCpu other)
		{
			return EqualityComparer<ushort>.Default.Equals(this.<Group>k__BackingField, other.<Group>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Index>k__BackingField, other.<Index>k__BackingField);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00009F0D File Offset: 0x0000830D
		[CompilerGenerated]
		public void Deconstruct(out ushort Group, out int Index)
		{
			Group = this.Group;
			Index = this.Index;
		}
	}
}
