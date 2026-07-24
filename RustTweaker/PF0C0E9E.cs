using System;

// Token: 0x020000F1 RID: 241
internal struct PF0C0E9E
{
	// Token: 0x06000571 RID: 1393 RVA: 0x0071A444 File Offset: 0x00715E44
	public uint EB1EB10F(I00BF691 GA9B579B)
	{
		uint num = 1U;
		uint num2 = 0U;
		for (int i = 0; i < this.B3BC6C25; i++)
		{
			uint num3 = this.IE2B8798[(int)num].DF1D1809(GA9B579B);
			num <<= 1;
			num += num3;
			num2 |= num3 << i;
		}
		return num2;
	}

	// Token: 0x06000572 RID: 1394 RVA: 0x0071A3BC File Offset: 0x00715DBC
	public void H928582E()
	{
		uint num = 1U;
		while ((ulong)num < (ulong)(1L << (this.B3BC6C25 & 31)))
		{
			this.IE2B8798[(int)num].M63722B7();
			num += 1U;
		}
	}

	// Token: 0x06000573 RID: 1395 RVA: 0x0071A3F8 File Offset: 0x00715DF8
	public uint LB92B522(I00BF691 NE3F4FB2)
	{
		uint num = 1U;
		for (int i = this.B3BC6C25; i > 0; i--)
		{
			num = (num << 1) + this.IE2B8798[(int)num].DF1D1809(NE3F4FB2);
		}
		return num - (1U << this.B3BC6C25);
	}

	// Token: 0x06000574 RID: 1396 RVA: 0x0071A494 File Offset: 0x00715E94
	public static uint AC3AB180(FF15D6B0[] B5B7B133, uint KA0A4813, I00BF691 M2A95316, int O397AA9B)
	{
		uint num = 1U;
		uint num2 = 0U;
		for (int i = 0; i < O397AA9B; i++)
		{
			uint num3 = B5B7B133[(int)(KA0A4813 + num)].DF1D1809(M2A95316);
			num <<= 1;
			num += num3;
			num2 |= num3 << i;
		}
		return num2;
	}

	// Token: 0x06000575 RID: 1397 RVA: 0x0071A394 File Offset: 0x00715D94
	public PF0C0E9E(int J2329F3B)
	{
		this.B3BC6C25 = J2329F3B;
		this.IE2B8798 = new FF15D6B0[1 << J2329F3B];
	}

	// Token: 0x04000356 RID: 854
	private readonly FF15D6B0[] IE2B8798;

	// Token: 0x04000357 RID: 855
	private readonly int B3BC6C25;
}
