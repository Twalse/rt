using System;
using System.Diagnostics;

// Token: 0x020001C8 RID: 456
[DebuggerDisplay("{2+AFA7138A.ODAA03A6()}")]
internal abstract class L0BE8811
{
	// Token: 0x06000768 RID: 1896 RVA: 0x00719A68 File Offset: 0x00715468
	public static uint K013720D(uint A80ABF26)
	{
		A80ABF26 -= 2U;
		if (A80ABF26 < 4U)
		{
			return A80ABF26;
		}
		return 3U;
	}

	// Token: 0x02000476 RID: 1142
	public struct H6BFD73D
	{
		// Token: 0x0600169E RID: 5790 RVA: 0x0071B14C File Offset: 0x00716B4C
		public void HDB06392()
		{
			this.J22E253D = ((this.J22E253D < 7U) ? 9U : 11U);
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x0071B124 File Offset: 0x00716B24
		public void I1A8533A()
		{
			this.J22E253D = ((this.J22E253D < 7U) ? 8U : 11U);
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x0071B0B0 File Offset: 0x00716AB0
		public void B390A231()
		{
			if (this.J22E253D < 4U)
			{
				this.J22E253D = 0U;
				return;
			}
			if (this.J22E253D < 10U)
			{
				this.J22E253D -= 3U;
				return;
			}
			this.J22E253D -= 6U;
		}

		// Token: 0x060016A1 RID: 5793 RVA: 0x0071B0FC File Offset: 0x00716AFC
		public void FB2CBDB2()
		{
			this.J22E253D = ((this.J22E253D < 7U) ? 7U : 10U);
		}

		// Token: 0x060016A2 RID: 5794 RVA: 0x0071B174 File Offset: 0x00716B74
		public bool FEA8EA3B()
		{
			return this.J22E253D < 7U;
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x0071B09C File Offset: 0x00716A9C
		public void FE9C919E()
		{
			this.J22E253D = 0U;
		}

		// Token: 0x0400052E RID: 1326
		public uint J22E253D;
	}
}
