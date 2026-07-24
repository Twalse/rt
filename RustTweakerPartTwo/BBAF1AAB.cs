using System;
using System.Diagnostics;

// Token: 0x02000556 RID: 1366
[DebuggerDisplay("{2+AFA7138A.ODAA03A6()}")]
public class BBAF1AAB
{
	// Token: 0x0600185C RID: 6236 RVA: 0x001038A0 File Offset: 0x001010A0
	public byte[] PC2A2FB0(byte[] M8A440BB)
	{
		int num = this.K793DEA2.Length;
		byte[] array = new byte[num];
		for (int i = 0; i < num; i++)
		{
			array[i] = this.K793DEA2[i];
		}
		num = M8A440BB.Length;
		byte[] array2 = new byte[num];
		int num2 = 0;
		int num3 = 0;
		for (int j = 0; j < num; j++)
		{
			num2 = (num2 + 1) % 256;
			num3 = ((int)array[num2] + num3) % 256;
			byte b = array[num2];
			array[num2] = array[num3];
			array[num3] = b;
			array2[j] = M8A440BB[j] ^ array[(int)(array[num2] + array[num3]) % 256];
		}
		return array2;
	}

	// Token: 0x0600185D RID: 6237 RVA: 0x00103810 File Offset: 0x00101010
	public BBAF1AAB(byte[] A083CD30)
	{
		for (int i = 0; i < 256; i++)
		{
			this.K793DEA2[i] = (byte)i;
		}
		int num = 0;
		int num2 = A083CD30.Length;
		for (int j = 0; j < 256; j++)
		{
			num = ((int)(A083CD30[j % num2] + this.K793DEA2[j]) + num) % 256;
			byte b = this.K793DEA2[j];
			this.K793DEA2[j] = this.K793DEA2[num];
			this.K793DEA2[num] = b;
		}
	}

	// Token: 0x04000562 RID: 1378
	private byte[] K793DEA2 = new byte[256];
}
