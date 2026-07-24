using System;

// Token: 0x02000246 RID: 582
internal struct FF15D6B0
{
	// Token: 0x0600082C RID: 2092 RVA: 0x0071A2A0 File Offset: 0x00715CA0
	public uint DF1D1809(I00BF691 E3845F83)
	{
		uint num = (E3845F83.LA90469D >> 11) * this.K4133F12;
		if (E3845F83.AFA60080 < num)
		{
			E3845F83.LA90469D = num;
			this.K4133F12 += 2048U - this.K4133F12 >> 5;
			if (E3845F83.LA90469D < 16777216U)
			{
				E3845F83.AFA60080 = (E3845F83.AFA60080 << 8) | (uint)((byte)E3845F83.HBA5BB05.ReadByte());
				E3845F83.LA90469D <<= 8;
			}
			return 0U;
		}
		E3845F83.LA90469D -= num;
		E3845F83.AFA60080 -= num;
		this.K4133F12 -= this.K4133F12 >> 5;
		if (E3845F83.LA90469D < 16777216U)
		{
			E3845F83.AFA60080 = (E3845F83.AFA60080 << 8) | (uint)((byte)E3845F83.HBA5BB05.ReadByte());
			E3845F83.LA90469D <<= 8;
		}
		return 1U;
	}

	// Token: 0x0600082D RID: 2093 RVA: 0x0071A288 File Offset: 0x00715C88
	public void M63722B7()
	{
		this.K4133F12 = 1024U;
	}

	// Token: 0x040004D2 RID: 1234
	private uint K4133F12;
}
