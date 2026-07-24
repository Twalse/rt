using System;

// Token: 0x02000194 RID: 404
public static class M20B2738
{
	// Token: 0x06000715 RID: 1813 RVA: 0x001641E8 File Offset: 0x0015FBE8
	public unsafe static uint M1BBABB5(IntPtr E22786A4, uint I10E9CB1)
	{
		byte b = 167;
		uint num = (uint)((-1281877110 / (int)b + 1471175301) % (b >> (int)b)) >> (int)b;
		if (-358527605 * (int)b / (int)b / 386549130 == 0)
		{
			b = (byte)(4294966944U ^ ((uint)b * (~((uint)b >> (int)b) << (int)(b ^ b))));
			for (;;)
			{
				uint num2;
				uint num4;
				switch (b % 3)
				{
				default:
				{
					b = (byte)((-5 * (int)b) ^ -255);
					byte* ptr = (byte*)E22786A4.ToPointer();
					num2 = (uint)((ushort)(b + b >> (int)b));
					int num3 = (int)(b ^ b);
					break;
				}
				case 1:
				{
					b = (byte)(~((uint)b & ((num2 % 415060024U) | num4)) - 4294967126U);
					int num3;
					if ((long)num3 >= (long)((ulong)I10E9CB1))
					{
						num4 = (((-2108509197 << (int)num4) + (int)b > 0) ? 1U : 0U);
						continue;
					}
					byte* ptr;
					num = M20B2738.N4A64734[(int)((byte)((uint)ptr[num3] ^ num))] ^ (num >> 8);
					num3++;
					b = 167;
					num2 = 2U;
					break;
				}
				case 2:
					goto IL_012F;
				}
				num4 = (uint)(((-878756339 >> (int)num2 == -586514151) ? 1 : 0) / ((1059227532 >> (int)num2) % 2016609557));
				if ((uint)(~(uint)b) / num2 == 0U)
				{
					break;
				}
				b = (byte)((1260884751 << (int)(3348266932U * num4)) - 1260884564);
			}
		}
		IL_012F:
		return ~num;
	}

	// Token: 0x06000716 RID: 1814 RVA: 0x00164000 File Offset: 0x0015FA00
	static M20B2738()
	{
		sbyte b = -64;
		int num = -538382 ^ ~(1102604846 % (int)b - (int)b >> 11);
		if (b * b == 0)
		{
			goto IL_009D;
		}
		IL_0035:
		uint num2;
		ushort num3;
		int num4;
		int num5;
		switch (b % 3)
		{
		default:
			goto IL_0182;
		case 1:
			b = (sbyte)((uint)(~b) / ~((uint)(b % b) >> (int)b >> (int)b));
			num2 >>= (int)(b - -1);
			goto IL_00F6;
		case 2:
			b = (sbyte)(4294967169U ^ (0U * (2919440030U >> (int)b)));
			if (num >= M20B2738.N4A64734.Length)
			{
				return;
			}
			num3 = 544;
			num2 = (uint)num;
			num4 = (int)(((uint)(~(uint)num3) >> (((int)num3 < -274250722) ? 1 : 0) * (1771909155 / (int)num3)) % ~((uint)num3 * 336753940U / (uint)num3 / 3649899956U));
			num5 = 2147483375 ^ num4;
			if (-23726302 / (int)num3 == 0)
			{
				goto IL_00B5;
			}
			break;
		}
		IL_009D:
		goto IL_0115;
		IL_00B5:
		IL_00F6:
		uint num6 = 747176826U;
		num5 += (int)(3547790471U + num6);
		num3 = 544;
		num4 = 2147483375;
		IL_0115:
		if ((int)(num3 / num3 % num3 / num3) != num4)
		{
			if (num5 >= (int)(8 + (~num3 ^ num3) * (ushort)((1141976471 > num4) ? 1 : 0)))
			{
				if (1393669531U >> (1304180012 | (num4 % (int)(~(int)num3))) == 0U)
				{
					M20B2738.N4A64734[num] = num2;
					num4 = (int)num3 ^ 345748256;
					num += 179600721 % (num4 / ((int)(-(int)num3) % num4));
					b = -64;
					goto IL_0182;
				}
			}
			else if ((num2 & 1U) == 1U)
			{
				num2 = (num2 >> 1) ^ 3988292384U;
				goto IL_00B5;
			}
			b = 0;
			b = (sbyte)((int)(b * b) % 1038648057 - -58);
			goto IL_0035;
		}
		goto IL_009D;
		IL_0182:
		b = (sbyte)(1479965569 - ((470098848 % (int)b >> (int)b == -2004557436) ? 1 : 0));
		b = (sbyte)(((int)b | -528272612) - -134);
		goto IL_0035;
	}

	// Token: 0x040004A0 RID: 1184
	private static uint[] N4A64734 = new uint[256];
}
