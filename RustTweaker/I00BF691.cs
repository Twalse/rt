using System;
using System.Diagnostics;
using System.IO;

// Token: 0x0200003A RID: 58
[DebuggerDisplay("{2+AFA7138A.ODAA03A6()}")]
internal class I00BF691
{
	// Token: 0x06000217 RID: 535 RVA: 0x0071A1F0 File Offset: 0x00715BF0
	public uint D3A188BA(int FF997E2A)
	{
		uint num = this.LA90469D;
		uint num2 = this.AFA60080;
		uint num3 = 0U;
		for (int i = FF997E2A; i > 0; i--)
		{
			num >>= 1;
			uint num4 = num2 - num >> 31;
			num2 -= num & (num4 - 1U);
			num3 = (num3 << 1) | (1U - num4);
			if (num < 16777216U)
			{
				num2 = (num2 << 8) | (uint)((byte)this.HBA5BB05.ReadByte());
				num <<= 8;
			}
		}
		this.LA90469D = num;
		this.AFA60080 = num2;
		return num3;
	}

	// Token: 0x06000218 RID: 536 RVA: 0x0071A1DC File Offset: 0x00715BDC
	public void B928B098()
	{
		this.HBA5BB05 = null;
	}

	// Token: 0x0600021A RID: 538 RVA: 0x0071A18C File Offset: 0x00715B8C
	public void P10C36AF(Stream E205481B)
	{
		this.HBA5BB05 = E205481B;
		this.AFA60080 = 0U;
		this.LA90469D = uint.MaxValue;
		for (int i = 0; i < 5; i++)
		{
			this.AFA60080 = (this.AFA60080 << 8) | (uint)((byte)this.HBA5BB05.ReadByte());
		}
	}

	// Token: 0x040000BC RID: 188
	public uint LA90469D;

	// Token: 0x040000BD RID: 189
	public Stream HBA5BB05;

	// Token: 0x040000BE RID: 190
	public uint AFA60080;

	// Token: 0x040000BF RID: 191
	private uint LD2AA3AF = 1U;
}
