using System;
using System.Diagnostics;
using System.IO;

// Token: 0x02000354 RID: 852
[DebuggerDisplay("{2+AFA7138A.ODAA03A6()}")]
public class ND1CA726
{
	// Token: 0x060009CE RID: 2510 RVA: 0x0071A550 File Offset: 0x00715F50
	public void OD389F35()
	{
		this.D31A8BAA();
		this.ED142E98 = null;
	}

	// Token: 0x060009CF RID: 2511 RVA: 0x0071A518 File Offset: 0x00715F18
	public void PAB0EF91(Stream HDB6A21C, bool FA89F195)
	{
		this.OD389F35();
		this.ED142E98 = HDB6A21C;
		if (!FA89F195)
		{
			this.A59BDB2E = 0U;
			this.C82AD0A7 = 0U;
			this.DAB65219 = 0U;
		}
	}

	// Token: 0x060009D0 RID: 2512 RVA: 0x0071A654 File Offset: 0x00716054
	public void I2981D0B(byte JB0548B6)
	{
		byte[] kf98F = this.KF98F215;
		uint c82AD0A = this.C82AD0A7;
		this.C82AD0A7 = c82AD0A + 1U;
		kf98F[(int)c82AD0A] = JB0548B6;
		if (this.C82AD0A7 >= this.D03F6798)
		{
			this.D31A8BAA();
		}
	}

	// Token: 0x060009D2 RID: 2514 RVA: 0x0071A694 File Offset: 0x00716094
	public byte CF3B211D(uint HD188485)
	{
		uint num = this.C82AD0A7 - HD188485 - 1U;
		if (num >= this.D03F6798)
		{
			num += this.D03F6798;
		}
		return this.KF98F215[(int)num];
	}

	// Token: 0x060009D3 RID: 2515 RVA: 0x0071A5CC File Offset: 0x00715FCC
	public void G999423F(uint C3096E34, uint J0AB25B0)
	{
		uint num = this.C82AD0A7 - C3096E34 - 1U;
		if (num >= this.D03F6798)
		{
			num += this.D03F6798;
		}
		while (J0AB25B0 > 0U)
		{
			if (num >= this.D03F6798)
			{
				num = 0U;
			}
			byte[] kf98F = this.KF98F215;
			uint c82AD0A = this.C82AD0A7;
			this.C82AD0A7 = c82AD0A + 1U;
			kf98F[(int)c82AD0A] = this.KF98F215[(int)num++];
			if (this.C82AD0A7 >= this.D03F6798)
			{
				this.D31A8BAA();
			}
			J0AB25B0 -= 1U;
		}
	}

	// Token: 0x060009D4 RID: 2516 RVA: 0x0071A56C File Offset: 0x00715F6C
	public void D31A8BAA()
	{
		uint num = this.C82AD0A7 - this.A59BDB2E;
		if (num == 0U)
		{
			return;
		}
		this.ED142E98.Write(this.KF98F215, (int)this.A59BDB2E, (int)num);
		if (this.C82AD0A7 >= this.D03F6798)
		{
			this.C82AD0A7 = 0U;
		}
		this.A59BDB2E = this.C82AD0A7;
	}

	// Token: 0x060009D5 RID: 2517 RVA: 0x0071A4DC File Offset: 0x00715EDC
	public void D59A8A04(uint PC117229)
	{
		if (this.D03F6798 != PC117229)
		{
			this.KF98F215 = new byte[PC117229];
		}
		this.D03F6798 = PC117229;
		this.C82AD0A7 = 0U;
		this.A59BDB2E = 0U;
	}

	// Token: 0x040004F4 RID: 1268
	private uint A59BDB2E;

	// Token: 0x040004F5 RID: 1269
	private uint C82AD0A7;

	// Token: 0x040004F6 RID: 1270
	private Stream ED142E98;

	// Token: 0x040004F7 RID: 1271
	public uint DAB65219;

	// Token: 0x040004F8 RID: 1272
	private uint GE2552BF = 1U;

	// Token: 0x040004F9 RID: 1273
	private uint D03F6798;

	// Token: 0x040004FA RID: 1274
	private byte[] KF98F215;
}
