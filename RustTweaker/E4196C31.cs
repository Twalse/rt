using System;
using System.Collections.Generic;
using System.Diagnostics;

// Token: 0x02000621 RID: 1569
[DebuggerDisplay("{2+AFA7138A.ODAA03A6()}")]
public class E4196C31
{
	// Token: 0x06001A55 RID: 6741 RVA: 0x00160D68 File Offset: 0x0015E568
	public E4196C31()
	{
		new H52867B7().C68FE888(new object[] { this }, 101714);
	}

	// Token: 0x06001A56 RID: 6742 RVA: 0x0015A24C File Offset: 0x00157A4C
	private unsafe byte[] C7063C0C(IntPtr EF89CA18, int C8B0EBA2, uint N4267715)
	{
		short num = 17374;
		num = (short)((1625241 >> (int)num) % ~((int)num ^ ((int)num % 1585747087)) - 733728148 - -733736866);
		byte[] array;
		for (;;)
		{
			short num2;
			int num3;
			ushort num4;
			switch (num % 3)
			{
			default:
				num = (short)(((((uint)(-(uint)num) >> 29 < 2711675949U) ? 1 : 0) ^ (num >> (int)num)) - -17373);
				array = new byte[C8B0EBA2];
				num2 = (short)(~(short)((int)num | (1855908997 * (int)num % (1845656071 / (int)num))));
				continue;
			case 1:
			{
				byte* ptr = (byte*)(void*)EF89CA18;
				num = (short)((int)(((num - num) ^ num) / num2) % 999707571);
				num3 = ((-452166892 % (906367390 % (int)num2)) ^ (int)num) - 2834;
				break;
			}
			case 2:
			{
				if (num3 >= C8B0EBA2)
				{
					return array;
				}
				byte* ptr;
				array[num3] = (byte)((ulong)ptr[num3] ^ ((ulong)((N4267715 << num3) | (N4267715 >> 32 - num3)) + (ulong)((long)num3)));
				num4 = 0;
				goto IL_00D1;
			}
			}
			IL_00EE:
			if (((num2 % ~(0 / num2 != 0)) & num) == 0)
			{
				continue;
			}
			IL_00D1:
			num3 += ((-1362119861 % (int)(~(int)num4)) & (int)(-(int)num4)) - -1;
			num2 = 7201;
			num = 2;
			goto IL_00EE;
		}
		return array;
	}

	// Token: 0x040005D9 RID: 1497
	private readonly Dictionary<string, uint> OC9768A1;
}
