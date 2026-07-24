using System;
using System.Diagnostics;

// Token: 0x02000179 RID: 377
[DebuggerDisplay("{2+AFA7138A.ODAA03A6()}")]
public class J6973F37
{
	// Token: 0x060006ED RID: 1773 RVA: 0x0071A708 File Offset: 0x00716108
	public uint FBA4CF3E(uint E7095601)
	{
		uint num = E7095601 ^ this.B2A6641D;
		this.B2A6641D = ((this.B2A6641D << 7) | (this.B2A6641D >> 25)) ^ num;
		return num;
	}

	// Token: 0x060006EE RID: 1774 RVA: 0x0071A6E8 File Offset: 0x007160E8
	public J6973F37()
	{
		this.B2A6641D = 683486257U;
	}

	// Token: 0x0400049A RID: 1178
	private uint B2A6641D;
}
