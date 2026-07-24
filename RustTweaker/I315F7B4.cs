using System;
using System.Reflection;
using System.Runtime.InteropServices;

// Token: 0x020000D2 RID: 210
internal static class I315F7B4
{
	// Token: 0x0600050F RID: 1295 RVA: 0x0071AA68 File Offset: 0x00716468
	public static bool LE1F481C(IntPtr L71AB88D, UIntPtr E837AEBA, I315F7B4.D091D1BB K9B4F211)
	{
		return (bool)new H52867B7().FCA6C832(new object[] { L71AB88D, E837AEBA, K9B4F211 }, 7529099);
	}

	// Token: 0x06000510 RID: 1296 RVA: 0x0071AB9C File Offset: 0x0071659C
	public static bool B0AFB831()
	{
		return (bool)new H52867B7().FCA6C832(null, 7533573);
	}

	// Token: 0x06000511 RID: 1297 RVA: 0x0071A900 File Offset: 0x00716300
	public static byte[] I3092BAB(uint NCB919BE, uint LBBAD5BA)
	{
		return (byte[])new H52867B7().FCA6C832(new object[] { NCB919BE, LBBAD5BA }, 7523779);
	}

	// Token: 0x06000512 RID: 1298 RVA: 0x0071AA18 File Offset: 0x00716418
	public static IntPtr LB273903(IntPtr KE2CCBB9, UIntPtr E0A0CABD, I315F7B4.K71039A2 DE21319D, I315F7B4.O504ED39 N615F417)
	{
		return (IntPtr)new H52867B7().FCA6C832(new object[] { KE2CCBB9, E0A0CABD, DE21319D, N615F417 }, 7527402);
	}

	// Token: 0x06000513 RID: 1299 RVA: 0x0071B06C File Offset: 0x00716A6C
	public static string JF37C1B5(Module K9965995)
	{
		return (string)new H52867B7().FCA6C832(new object[] { K9965995 }, 7540966);
	}

	// Token: 0x06000514 RID: 1300 RVA: 0x0071A860 File Offset: 0x00716260
	private unsafe static uint D8AA6A2C(I315F7B4.F9144AB1 IEB12414, out object C22276A3, uint D9896D87, out uint I89EFF9A)
	{
		H52867B7 h52867B = new H52867B7();
		object[] array = new object[4];
		array[0] = IEB12414;
		int num = 1;
		TypedReference typedReference = __makeref(C22276A3);
		array[num] = &typedReference;
		array[2] = D9896D87;
		int num2 = 3;
		TypedReference typedReference2 = __makeref(I89EFF9A);
		array[num2] = &typedReference2;
		return (uint)h52867B.FCA6C832(array, 7477092);
	}

	// Token: 0x06000515 RID: 1301 RVA: 0x0071AAB0 File Offset: 0x007164B0
	public unsafe static IntPtr B324378A(IntPtr MF9C55A2, I315F7B4.O504ED39 C7107436, ulong PB03A61F, ref UIntPtr IA9ABD00, out IntPtr M21CDF0E)
	{
		H52867B7 h52867B = new H52867B7();
		object[] array = new object[5];
		array[0] = MF9C55A2;
		array[1] = C7107436;
		array[2] = PB03A61F;
		int num = 3;
		TypedReference typedReference = __makeref(IA9ABD00);
		array[num] = &typedReference;
		int num2 = 4;
		TypedReference typedReference2 = __makeref(M21CDF0E);
		array[num2] = &typedReference2;
		return (IntPtr)h52867B.FCA6C832(array, 7529613);
	}

	// Token: 0x06000516 RID: 1302 RVA: 0x0071A974 File Offset: 0x00716374
	public static IntPtr C23CB02F(string HE342F03, I315F7B4.DB953005 F2244A0D, uint C2292C05)
	{
		return (IntPtr)new H52867B7().FCA6C832(new object[] { HE342F03, F2244A0D, C2292C05 }, 7516256);
	}

	// Token: 0x06000517 RID: 1303 RVA: 0x0071AFC4 File Offset: 0x007169C4
	public static byte[] EE346A3C(string O725AB3D)
	{
		return (byte[])new H52867B7().FCA6C832(new object[] { O725AB3D }, 7504931);
	}

	// Token: 0x06000518 RID: 1304 RVA: 0x0071AFA0 File Offset: 0x007169A0
	public static string OF34A5B5()
	{
		return (string)new H52867B7().FCA6C832(null, 7503137);
	}

	// Token: 0x06000519 RID: 1305 RVA: 0x0071ABC0 File Offset: 0x007165C0
	private static bool J7041A90(byte[] C9A2663D)
	{
		int num = C9A2663D.Length;
		for (int i = 0; i < num; i++)
		{
			if (i + 3 < num && C9A2663D[i] == 81 && C9A2663D[i + 1] == 69 && C9A2663D[i + 2] == 77 && C9A2663D[i + 3] == 85)
			{
				return true;
			}
			if (i + 5 < num && C9A2663D[i] == 79 && C9A2663D[i + 1] == 114 && C9A2663D[i + 2] == 97 && C9A2663D[i + 3] == 99 && C9A2663D[i + 4] == 108 && C9A2663D[i + 5] == 101)
			{
				return true;
			}
			if (i + 6 < num && C9A2663D[i] == 105 && C9A2663D[i + 1] == 110 && C9A2663D[i + 2] == 110 && C9A2663D[i + 3] == 111 && C9A2663D[i + 4] == 116 && C9A2663D[i + 5] == 101 && C9A2663D[i + 6] == 107)
			{
				return true;
			}
			if (i + 9 < num && C9A2663D[i] == 86 && C9A2663D[i + 1] == 105 && C9A2663D[i + 2] == 114 && C9A2663D[i + 3] == 116 && C9A2663D[i + 4] == 117 && C9A2663D[i + 5] == 97 && C9A2663D[i + 6] == 108 && C9A2663D[i + 7] == 66 && C9A2663D[i + 8] == 111 && C9A2663D[i + 9] == 120)
			{
				return true;
			}
			if (i + 15 < num && C9A2663D[i] == 86 && C9A2663D[i + 1] == 105 && C9A2663D[i + 2] == 114 && C9A2663D[i + 3] == 116 && C9A2663D[i + 4] == 117 && C9A2663D[i + 5] == 97 && C9A2663D[i + 6] == 108 && C9A2663D[i + 7] == 32 && C9A2663D[i + 8] == 80 && C9A2663D[i + 9] == 108 && C9A2663D[i + 10] == 97 && C9A2663D[i + 11] == 116 && C9A2663D[i + 12] == 102 && C9A2663D[i + 13] == 111 && C9A2663D[i + 14] == 114 && C9A2663D[i + 15] == 109)
			{
				return true;
			}
			if (i + 5 < num && C9A2663D[i] == 86 && C9A2663D[i + 1] == 77 && C9A2663D[i + 2] == 119 && C9A2663D[i + 3] == 97 && C9A2663D[i + 4] == 114 && C9A2663D[i + 5] == 101)
			{
				return true;
			}
			if (i + 8 < num && C9A2663D[i] == 80 && C9A2663D[i + 1] == 97 && C9A2663D[i + 2] == 114 && C9A2663D[i + 3] == 97 && C9A2663D[i + 4] == 108 && C9A2663D[i + 5] == 108 && C9A2663D[i + 6] == 101 && C9A2663D[i + 7] == 108 && C9A2663D[i + 8] == 115)
			{
				return true;
			}
			if (i + 5 < num && C9A2663D[i] == 55 && C9A2663D[i + 1] == 55 && C9A2663D[i + 2] == 55 && C9A2663D[i + 3] == 55 && C9A2663D[i + 4] == 55 && C9A2663D[i + 5] == 55)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600051A RID: 1306 RVA: 0x0071AB28 File Offset: 0x00716528
	public static bool L09EDCAE(IntPtr N78492B5, UIntPtr D23FC10C)
	{
		return (bool)new H52867B7().FCA6C832(new object[] { N78492B5, D23FC10C }, 7517549);
	}

	// Token: 0x0600051B RID: 1307 RVA: 0x0071A77C File Offset: 0x0071617C
	private static IntPtr J7ADBD29(uint LFBE4EBF)
	{
		return (IntPtr)new H52867B7().FCA6C832(new object[] { LFBE4EBF }, 7467238);
	}

	// Token: 0x0600051C RID: 1308 RVA: 0x0071B048 File Offset: 0x00716A48
	public static uint OB9D4235()
	{
		return (uint)new H52867B7().FCA6C832(null, 7538486);
	}

	// Token: 0x0600051D RID: 1309 RVA: 0x0071AFF4 File Offset: 0x007169F4
	public static byte[] A534ED91(string DF39D31F)
	{
		return (byte[])new H52867B7().FCA6C832(new object[] { DF39D31F }, 7546966);
	}

	// Token: 0x0600051E RID: 1310 RVA: 0x0071A9B8 File Offset: 0x007163B8
	public unsafe static bool J0B51CB4(IntPtr M7A7802A, UIntPtr F11C0989, I315F7B4.O504ED39 B0874801, out I315F7B4.O504ED39 C48968BD)
	{
		H52867B7 h52867B = new H52867B7();
		object[] array = new object[4];
		array[0] = M7A7802A;
		array[1] = F11C0989;
		array[2] = B0874801;
		int num = 3;
		TypedReference typedReference = __makeref(C48968BD);
		array[num] = &typedReference;
		return (bool)h52867B.FCA6C832(array, 7525846);
	}

	// Token: 0x0600051F RID: 1311 RVA: 0x0071A7E8 File Offset: 0x007161E8
	public unsafe static uint GB947F95(IntPtr DE921E1F, I315F7B4.HF0497A9 A71DEDB1, out object C0A63F87, uint N39E9316, out uint HC8BC327)
	{
		H52867B7 h52867B = new H52867B7();
		object[] array = new object[5];
		array[0] = DE921E1F;
		array[1] = A71DEDB1;
		int num = 2;
		TypedReference typedReference = __makeref(C0A63F87);
		array[num] = &typedReference;
		array[3] = N39E9316;
		int num2 = 4;
		TypedReference typedReference2 = __makeref(HC8BC327);
		array[num2] = &typedReference2;
		return (uint)h52867B.FCA6C832(array, 7476218);
	}

	// Token: 0x06000520 RID: 1312 RVA: 0x0071A8CC File Offset: 0x007162CC
	public static byte[] B79DB23C(uint AC89F032)
	{
		return (byte[])new H52867B7().FCA6C832(new object[] { AC89F032 }, 7477786);
	}

	// Token: 0x06000521 RID: 1313
	[DllImport("kernel32.dll", CharSet = CharSet.Auto, EntryPoint = "GetModuleHandle", SetLastError = true)]
	public static extern IntPtr B6123297(string P79A48B2);

	// Token: 0x06000522 RID: 1314 RVA: 0x0071A940 File Offset: 0x00716340
	public static bool LB81870A(IntPtr B63B7AB6)
	{
		return (bool)new H52867B7().FCA6C832(new object[] { B63B7AB6 }, 7515803);
	}

	// Token: 0x06000523 RID: 1315 RVA: 0x0071B024 File Offset: 0x00716A24
	public static byte[] BB1A023E()
	{
		return (byte[])new H52867B7().FCA6C832(null, 7538292);
	}

	// Token: 0x06000524 RID: 1316 RVA: 0x0071AB68 File Offset: 0x00716568
	public static bool MF844181(bool C2864D80 = false)
	{
		return (bool)new H52867B7().FCA6C832(new object[] { C2864D80 }, 7518021);
	}

	// Token: 0x06000525 RID: 1317 RVA: 0x0071AF5C File Offset: 0x0071695C
	public static void I4031733(string IEB36C36, string K0381E8C, uint MA354013, uint H3AE681D)
	{
		new H52867B7().FCA6C832(new object[] { IEB36C36, K0381E8C, MA354013, H3AE681D }, 7536227);
	}

	// Token: 0x06000526 RID: 1318 RVA: 0x0071A7B0 File Offset: 0x007161B0
	internal static IntPtr DEBF2CB8(IntPtr GA8ED8AD, object G68E6916)
	{
		return (IntPtr)new H52867B7().FCA6C832(new object[] { GA8ED8AD, G68E6916 }, 7468436);
	}

	// Token: 0x06000527 RID: 1319 RVA: 0x0071AF38 File Offset: 0x00716938
	public static bool J9005D17()
	{
		return (bool)new H52867B7().FCA6C832(null, 7534373);
	}

	// Token: 0x06000528 RID: 1320
	[DllImport("libdl.so.2", EntryPoint = "dlsym")]
	private static extern IntPtr C10656B3(IntPtr BD3A3E05, string J5A3C502);

	// Token: 0x06000529 RID: 1321 RVA: 0x0071A75C File Offset: 0x0071615C
	static I315F7B4()
	{
		new H52867B7().FCA6C832(null, 7462003);
	}

	// Token: 0x040002E0 RID: 736
	private static I315F7B4.BE94F596 CD20F09E;

	// Token: 0x040002E1 RID: 737
	private static I315F7B4.KC8E6A25 JE98EEAA;

	// Token: 0x040002E2 RID: 738
	private static I315F7B4.P7886C9D F403A8B2;

	// Token: 0x040002E3 RID: 739
	private static I315F7B4.DA14B52C G72CADAF;

	// Token: 0x040002E4 RID: 740
	private static I315F7B4.G80D7C09 J8A3613E;

	// Token: 0x040002E5 RID: 741
	private static I315F7B4.BBB9591A G124B793;

	// Token: 0x040002E6 RID: 742
	public static readonly IntPtr OB2B4C02;

	// Token: 0x040002E7 RID: 743
	private static I315F7B4.G8A8D11A IA9CA511;

	// Token: 0x040002E8 RID: 744
	private static I315F7B4.P3825B85 E52119AA;

	// Token: 0x040002E9 RID: 745
	private static I315F7B4.D09D39A3 DC3F7085;

	// Token: 0x040002EA RID: 746
	private static I315F7B4.P303FE01 N43DE787;

	// Token: 0x040002EB RID: 747
	private static I315F7B4.L7A38132 K3B7F8A0;

	// Token: 0x040002EC RID: 748
	private static I315F7B4.DA231920 E532D411;

	// Token: 0x040002ED RID: 749
	private static I315F7B4.MAAF008D FDAA2009;

	// Token: 0x040002EE RID: 750
	private static I315F7B4.PCB3933C CBBB3130;

	// Token: 0x040002EF RID: 751
	private static I315F7B4.C424941E D0A69385;

	// Token: 0x040002F0 RID: 752
	private static I315F7B4.EAB78122 IE3E54A4;

	// Token: 0x040002F1 RID: 753
	private static I315F7B4.I296789F O52B2680;

	// Token: 0x040002F2 RID: 754
	private static I315F7B4.I581080D IFA30A92;

	// Token: 0x040002F3 RID: 755
	private static I315F7B4.H4AB053E E221DA9B;

	// Token: 0x040002F4 RID: 756
	private static I315F7B4.B52B3689 D816A835;

	// Token: 0x040002F5 RID: 757
	private static I315F7B4.DC347601 C5307DBE;

	// Token: 0x040002F6 RID: 758
	private static I315F7B4.EA9973A4 G029BD10;

	// Token: 0x040002F7 RID: 759
	public static readonly IntPtr LC0AED2F;

	// Token: 0x040002F8 RID: 760
	private static I315F7B4.FE3D0FB5 LB04661B;

	// Token: 0x040002F9 RID: 761
	private static I315F7B4.CEAEB92F O119F925;

	// Token: 0x040002FA RID: 762
	private static I315F7B4.F3B64C2B A0143E01;

	// Token: 0x040002FB RID: 763
	private static I315F7B4.OC3C0593 J228E405;

	// Token: 0x040002FC RID: 764
	public static readonly IntPtr D3B5F0A7;

	// Token: 0x020000E0 RID: 224
	// (Invoke) Token: 0x06000543 RID: 1347
	private delegate uint D09D39A3(IntPtr N20B4A27, ref IntPtr A8ADB0B4, ref UIntPtr K53A1C19, I315F7B4.O504ED39 I50E799B, out I315F7B4.O504ED39 B3A03FA3);

	// Token: 0x020000F2 RID: 242
	// (Invoke) Token: 0x06000576 RID: 1398
	private delegate uint C424941E(I315F7B4.F9144AB1 M1333E86, IntPtr PA265C3B, uint H6B549B4, out uint FF88D5BC);

	// Token: 0x0200011D RID: 285
	public struct M0812611
	{
		// Token: 0x060005EE RID: 1518 RVA: 0x0071B55C File Offset: 0x00716F5C
		public unsafe void O9B84186()
		{
			H52867B7 h52867B = new H52867B7();
			object[] array = new object[1];
			int num = 0;
			TypedReference typedReference = __makeref(this);
			array[num] = &typedReference;
			h52867B.FCA6C832(array, 7546443);
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0071B50C File Offset: 0x00716F0C
		public unsafe M0812611(I315F7B4.J3BF7808 I20A94A3, uint K1BBAF3D)
		{
			H52867B7 h52867B = new H52867B7();
			object[] array = new object[3];
			int num = 0;
			TypedReference typedReference = __makeref(this);
			array[num] = &typedReference;
			array[1] = I20A94A3;
			array[2] = K1BBAF3D;
			h52867B.FCA6C832(array, 7546091);
		}

		// Token: 0x04000400 RID: 1024
		public readonly uint AC0F1323;

		// Token: 0x04000401 RID: 1025
		public readonly IntPtr JE97B314;

		// Token: 0x04000402 RID: 1026
		public readonly IntPtr J52770A3;

		// Token: 0x04000403 RID: 1027
		public readonly uint M0BD4FA1;

		// Token: 0x04000404 RID: 1028
		public readonly IntPtr A1AB4627;

		// Token: 0x04000405 RID: 1029
		public readonly IntPtr BB14630E;
	}

	// Token: 0x02000137 RID: 311
	public struct G0309E3D
	{
		// Token: 0x04000476 RID: 1142
		public I315F7B4.OB0F558F IF9096BA;

		// Token: 0x04000477 RID: 1143
		public uint P0075A83;

		// Token: 0x04000478 RID: 1144
		public uint BEBC0526;

		// Token: 0x04000479 RID: 1145
		public byte CABD3FB2;

		// Token: 0x0400047A RID: 1146
		public char I914B5B8;
	}

	// Token: 0x0200016B RID: 363
	public struct P9A3B893
	{
		// Token: 0x04000495 RID: 1173
		public uint IB9CAA8B;

		// Token: 0x04000496 RID: 1174
		public uint BF2ED090;

		// Token: 0x04000497 RID: 1175
		public I315F7B4.J53E4DB6 P09A5322;

		// Token: 0x04000498 RID: 1176
		public I315F7B4.J53E4DB6 DF9EA3A9;

		// Token: 0x04000499 RID: 1177
		public I315F7B4.J53E4DB6 M035453E;
	}

	// Token: 0x02000170 RID: 368
	// (Invoke) Token: 0x060006DD RID: 1757
	private delegate IntPtr DC347601(IntPtr K91E5696);

	// Token: 0x0200017E RID: 382
	public struct J3BF7808
	{
		// Token: 0x060006F5 RID: 1781 RVA: 0x0071B4CC File Offset: 0x00716ECC
		public unsafe void H985770C()
		{
			H52867B7 h52867B = new H52867B7();
			object[] array = new object[1];
			int num = 0;
			TypedReference typedReference = __makeref(this);
			array[num] = &typedReference;
			h52867B.FCA6C832(array, 7545880);
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0071B488 File Offset: 0x00716E88
		public unsafe J3BF7808(string O3A214A1)
		{
			H52867B7 h52867B = new H52867B7();
			object[] array = new object[2];
			int num = 0;
			TypedReference typedReference = __makeref(this);
			array[num] = &typedReference;
			array[1] = O3A214A1;
			h52867B.FCA6C832(array, 7545452);
		}

		// Token: 0x0400049B RID: 1179
		public readonly ushort H222B09D;

		// Token: 0x0400049C RID: 1180
		public readonly ushort G310911A;

		// Token: 0x0400049D RID: 1181
		public readonly IntPtr DB8B5B85;
	}

	// Token: 0x0200017F RID: 383
	public struct G3012187
	{
		// Token: 0x0400049E RID: 1182
		public byte J928F482;

		// Token: 0x0400049F RID: 1183
		public byte N71CB618;
	}

	// Token: 0x020001A8 RID: 424
	// (Invoke) Token: 0x06000734 RID: 1844
	private delegate uint B52B3689(IntPtr G9382F36, ref IntPtr P703E925, IntPtr O396D9BA, ref UIntPtr L9972B83, I315F7B4.K71039A2 F40A1282, I315F7B4.O504ED39 J109A40D);

	// Token: 0x020001A9 RID: 425
	// (Invoke) Token: 0x0600073A RID: 1850
	private delegate int F3B64C2B(IntPtr LE2CBC05, UIntPtr E73C7100);

	// Token: 0x020001BA RID: 442
	public struct JFACEE05
	{
		// Token: 0x040004A1 RID: 1185
		public uint GE8F888B;

		// Token: 0x040004A2 RID: 1186
		public uint C63670B1;
	}

	// Token: 0x020001CE RID: 462
	public enum DB953005 : uint
	{
		// Token: 0x040004A3 RID: 1187
		E5A429B0 = 2U,
		// Token: 0x040004A4 RID: 1188
		L483DBA8 = 2954240U,
		// Token: 0x040004A5 RID: 1189
		B2255330 = 8U,
		// Token: 0x040004A6 RID: 1190
		O327BB8B = 1048576U,
		// Token: 0x040004A7 RID: 1191
		C5A579B0 = 4U,
		// Token: 0x040004A8 RID: 1192
		P42D0B95 = 1U,
		// Token: 0x040004A9 RID: 1193
		L839631D = 2147483648U,
		// Token: 0x040004AA RID: 1194
		H0A98B8D = 1U,
		// Token: 0x040004AB RID: 1195
		L396189F = 5636096U,
		// Token: 0x040004AC RID: 1196
		C6B6639D = 128U,
		// Token: 0x040004AD RID: 1197
		FF385B28 = 134217728U,
		// Token: 0x040004AE RID: 1198
		C0232903 = 2U,
		// Token: 0x040004AF RID: 1199
		C73FB32C = 1U,
		// Token: 0x040004B0 RID: 1200
		LEB97D0D = 64U,
		// Token: 0x040004B1 RID: 1201
		I81F2597 = 32U
	}

	// Token: 0x0200021B RID: 539
	// (Invoke) Token: 0x060007ED RID: 2029
	private delegate uint I296789F(IntPtr A811F5B4, ref IntPtr PC2E612F, ref UIntPtr J502322B, I315F7B4.D091D1BB GF00159E);

	// Token: 0x0200021C RID: 540
	public struct LD04BC28
	{
		// Token: 0x040004C7 RID: 1223
		public uint IF02D933;

		// Token: 0x040004C8 RID: 1224
		public uint I3B56815;

		// Token: 0x040004C9 RID: 1225
		public uint BABF7891;

		// Token: 0x040004CA RID: 1226
		public uint L282AA1B;

		// Token: 0x040004CB RID: 1227
		public uint OB042999;

		// Token: 0x040004CC RID: 1228
		public uint P20DAC0B;

		// Token: 0x040004CD RID: 1229
		public IntPtr N0BABF2E;

		// Token: 0x040004CE RID: 1230
		public IntPtr J9B9278E;

		// Token: 0x040004CF RID: 1231
		public IntPtr H7989804;

		// Token: 0x040004D0 RID: 1232
		public IntPtr M808C093;

		// Token: 0x040004D1 RID: 1233
		public byte I2BC4012;
	}

	// Token: 0x0200025A RID: 602
	// (Invoke) Token: 0x0600084D RID: 2125
	private delegate int DA231920(IntPtr A101729C);

	// Token: 0x0200025D RID: 605
	// (Invoke) Token: 0x06000854 RID: 2132
	private delegate uint P7886C9D(IntPtr G120D617);

	// Token: 0x02000262 RID: 610
	// (Invoke) Token: 0x0600085D RID: 2141
	private delegate IntPtr MAAF008D(string K7131481, string P91B608C);

	// Token: 0x0200028D RID: 653
	// (Invoke) Token: 0x0600089F RID: 2207
	private delegate uint DA14B52C(out IntPtr J11974BC, I315F7B4.DB953005 E9029709, ref I315F7B4.M0812611 D7165E29, out I315F7B4.F5AF7B84 PC099AA7, uint L60F2CAF, uint M0148198);

	// Token: 0x020002A4 RID: 676
	public struct OB0F558F
	{
		// Token: 0x060008C2 RID: 2242 RVA: 0x0071B5E4 File Offset: 0x00716FE4
		public unsafe long H392E896()
		{
			H52867B7 h52867B = new H52867B7();
			object[] array = new object[1];
			int num = 0;
			TypedReference typedReference = __makeref(this);
			array[num] = &typedReference;
			return (long)h52867B.FCA6C832(array, 7546780);
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x0071B59C File Offset: 0x00716F9C
		public unsafe void DB28B028(long G78F99BD)
		{
			H52867B7 h52867B = new H52867B7();
			object[] array = new object[2];
			int num = 0;
			TypedReference typedReference = __makeref(this);
			array[num] = &typedReference;
			array[1] = G78F99BD;
			h52867B.FCA6C832(array, 7546531);
		}

		// Token: 0x040004D3 RID: 1235
		public uint FEBA34A8;

		// Token: 0x040004D4 RID: 1236
		public int HE8D0498;
	}

	// Token: 0x020002AC RID: 684
	public enum O31EAE94
	{
		// Token: 0x040004D5 RID: 1237
		P415443E = 2,
		// Token: 0x040004D6 RID: 1238
		FA891ABF = 4,
		// Token: 0x040004D7 RID: 1239
		K33F7793 = 3,
		// Token: 0x040004D8 RID: 1240
		J5024F1A = 5,
		// Token: 0x040004D9 RID: 1241
		M2136038 = 7,
		// Token: 0x040004DA RID: 1242
		LE3B8A13 = 0,
		// Token: 0x040004DB RID: 1243
		H19D750D,
		// Token: 0x040004DC RID: 1244
		J329CFB4 = 6,
		// Token: 0x040004DD RID: 1245
		G92626A7 = 8
	}

	// Token: 0x020002E3 RID: 739
	// (Invoke) Token: 0x06000924 RID: 2340
	private delegate uint KC8E6A25(uint BE2C7E1A, uint J4280498, uint C93C5284, IntPtr[] H23969A1, uint L9A7B338, out I315F7B4.O31EAE94 PF1DBE3B);

	// Token: 0x0200031A RID: 794
	public struct F5AF7B84
	{
		// Token: 0x040004EA RID: 1258
		public uint FCA8549D;

		// Token: 0x040004EB RID: 1259
		public IntPtr DD91B2BA;
	}

	// Token: 0x0200032C RID: 812
	// (Invoke) Token: 0x06000990 RID: 2448
	private delegate uint CEAEB92F(IntPtr GF1C4636, I315F7B4.M02A8D3B G60912A8, IntPtr E834E79F, uint C822729C, out uint M5272038);

	// Token: 0x02000346 RID: 838
	// (Invoke) Token: 0x060009BA RID: 2490
	private delegate uint BBB9591A(IntPtr N1A0B48E, IntPtr K297D199, out IntPtr A0993619, UIntPtr M2954BA7, UIntPtr P10B4E9C, I315F7B4.OB0F558F C62BED29, ref UIntPtr G4B16ABD, uint CA9D59BB, uint K8AC9E0B, I315F7B4.O504ED39 B8B8118B);

	// Token: 0x02000347 RID: 839
	public enum HF0497A9
	{
		// Token: 0x040004EC RID: 1260
		AB906C1D,
		// Token: 0x040004ED RID: 1261
		J6ADE28A = 30,
		// Token: 0x040004EE RID: 1262
		G68CC70B = 7
	}

	// Token: 0x0200034D RID: 845
	public struct P6B2F008
	{
		// Token: 0x040004F0 RID: 1264
		public uint F2B5AA03;

		// Token: 0x040004F1 RID: 1265
		public uint DB987994;

		// Token: 0x040004F2 RID: 1266
		public uint D82E8A19;

		// Token: 0x040004F3 RID: 1267
		public uint IEA55911;
	}

	// Token: 0x02000374 RID: 884
	// (Invoke) Token: 0x06000A05 RID: 2565
	private delegate uint EAB78122(IntPtr EB30B623, IntPtr HB972C86);

	// Token: 0x020003F7 RID: 1015
	public struct J53E4DB6
	{
		// Token: 0x04000501 RID: 1281
		public uint DA881B3B;

		// Token: 0x04000502 RID: 1282
		public int DDB8DF99;

		// Token: 0x04000503 RID: 1283
		public int D1B1358F;
	}

	// Token: 0x02000408 RID: 1032
	// (Invoke) Token: 0x06000AE5 RID: 2789
	private delegate int L7A38132(IntPtr I096F198);

	// Token: 0x0200042A RID: 1066
	// (Invoke) Token: 0x06000B1B RID: 2843
	private delegate IntPtr P3825B85(string IA8D44BF, I315F7B4.C1809494 IDBFDD1A);

	// Token: 0x0200043B RID: 1083
	// (Invoke) Token: 0x06001636 RID: 5686
	private delegate uint BE94F596(IntPtr B9BC2CA8, I315F7B4.HF0497A9 F3B69B34, IntPtr H33F2CB3, uint J895A499, out uint J9375885);

	// Token: 0x02000447 RID: 1095
	public struct M1931C1F
	{
		// Token: 0x04000515 RID: 1301
		public uint BB273D1D;

		// Token: 0x04000516 RID: 1302
		public uint N232ABB5;

		// Token: 0x04000517 RID: 1303
		public byte KA84CD02;

		// Token: 0x04000518 RID: 1304
		public byte C98C603A;

		// Token: 0x04000519 RID: 1305
		public byte N8318784;

		// Token: 0x0400051A RID: 1306
		public byte D08BE08C;

		// Token: 0x0400051B RID: 1307
		public uint HEA1FC37;

		// Token: 0x0400051C RID: 1308
		public uint D101013A;

		// Token: 0x0400051D RID: 1309
		public uint E491F593;

		// Token: 0x0400051E RID: 1310
		public uint IF1DB8BA;
	}

	// Token: 0x02000473 RID: 1139
	public enum C1809494 : uint
	{
		// Token: 0x04000521 RID: 1313
		N42D2014,
		// Token: 0x04000522 RID: 1314
		HA941A09,
		// Token: 0x04000523 RID: 1315
		P62CB981 = 0U,
		// Token: 0x04000524 RID: 1316
		E30A0BAC = 4U,
		// Token: 0x04000525 RID: 1317
		NB80ABB2 = 1U,
		// Token: 0x04000526 RID: 1318
		H30C4595 = 1U,
		// Token: 0x04000527 RID: 1319
		B3A99FAE = 0U,
		// Token: 0x04000528 RID: 1320
		JD8D8539 = 2U,
		// Token: 0x04000529 RID: 1321
		N2838592 = 2U,
		// Token: 0x0400052A RID: 1322
		CB3714A8 = 2U,
		// Token: 0x0400052B RID: 1323
		ND1E4A16 = 1U,
		// Token: 0x0400052C RID: 1324
		J6970D08 = 32U
	}

	// Token: 0x0200047C RID: 1148
	// (Invoke) Token: 0x060016AD RID: 5805
	private delegate IntPtr G8A8D11A(IntPtr MCA3C604, UIntPtr I42893BE, I315F7B4.C1809494 H6175A81, I315F7B4.C1809494 KEB77E21, IntPtr F2A23B97, uint D9978C8D);

	// Token: 0x02000485 RID: 1157
	// (Invoke) Token: 0x060016BC RID: 5820
	private delegate int FE3D0FB5(IntPtr J232E7A9, UIntPtr B9049509, I315F7B4.C1809494 KEA31F85);

	// Token: 0x0200048D RID: 1165
	public struct BB8DD625
	{
		// Token: 0x04000530 RID: 1328
		public uint B5358DA2;

		// Token: 0x04000531 RID: 1329
		public uint I3829C9D;

		// Token: 0x04000532 RID: 1330
		public I315F7B4.OB0F558F J62ACB8E;

		// Token: 0x04000533 RID: 1331
		public I315F7B4.OB0F558F B9AF87BA;
	}

	// Token: 0x0200049A RID: 1178
	// (Invoke) Token: 0x060016DE RID: 5854
	private delegate uint I581080D(IntPtr BC323A0A, I315F7B4.M02A8D3B A49C8F15, IntPtr B901F1A7, uint DF874F1D);

	// Token: 0x020004CA RID: 1226
	// (Invoke) Token: 0x0600174B RID: 5963
	private delegate uint P303FE01(IntPtr AB900D84, out I315F7B4.F5AF7B84 N791F832, IntPtr A7B74000, uint PA2267A8, uint C100A2BA);

	// Token: 0x020004E6 RID: 1254
	public enum K71039A2 : uint
	{
		// Token: 0x04000550 RID: 1360
		E4215783 = 8192U,
		// Token: 0x04000551 RID: 1361
		NFBBA6BD = 4096U
	}

	// Token: 0x02000508 RID: 1288
	// (Invoke) Token: 0x060017C5 RID: 6085
	private delegate uint PCB3933C(IntPtr CC271D38, IntPtr D0A50819, IntPtr HC9BF727, IntPtr JC14979C, out I315F7B4.F5AF7B84 OE3B88AA, uint BF84F2B8, IntPtr N80D6189, uint G725F7AA, IntPtr EF8AA819, uint LA16D19A);

	// Token: 0x02000535 RID: 1333
	public enum F9144AB1
	{
		// Token: 0x0400055A RID: 1370
		FEAB3F81,
		// Token: 0x0400055B RID: 1371
		E30EB13A = 35,
		// Token: 0x0400055C RID: 1372
		P0952B02 = 76
	}

	// Token: 0x02000579 RID: 1401
	public enum O504ED39 : uint
	{
		// Token: 0x04000577 RID: 1399
		C6892F2B = 64U,
		// Token: 0x04000578 RID: 1400
		ND180915 = 32U,
		// Token: 0x04000579 RID: 1401
		F73865A0 = 256U,
		// Token: 0x0400057A RID: 1402
		H12F17AF = 0U,
		// Token: 0x0400057B RID: 1403
		O68789B6 = 16U,
		// Token: 0x0400057C RID: 1404
		B3989480 = 4U,
		// Token: 0x0400057D RID: 1405
		F1B07425 = 2U,
		// Token: 0x0400057E RID: 1406
		KF24F51A = 8U,
		// Token: 0x0400057F RID: 1407
		O78D8D02 = 1U
	}

	// Token: 0x02000593 RID: 1427
	public struct NF1E78B6
	{
		// Token: 0x04000582 RID: 1410
		public uint CB91B42A;

		// Token: 0x04000583 RID: 1411
		public uint KD2D198C;

		// Token: 0x04000584 RID: 1412
		public I315F7B4.BB8DD625 NE2CD187;
	}

	// Token: 0x0200059A RID: 1434
	// (Invoke) Token: 0x060018CA RID: 6346
	private delegate uint OC3C0593(out IntPtr L63FF636, I315F7B4.DB953005 F831DAAE, ref I315F7B4.M0812611 F5AAD50C, ref I315F7B4.OB0F558F C03C6482, I315F7B4.O504ED39 FC146DA3, uint N1B402BC, IntPtr ECB8D60D);

	// Token: 0x0200059D RID: 1437
	public enum M02A8D3B
	{
		// Token: 0x04000586 RID: 1414
		D71D572F = 17
	}

	// Token: 0x020005A2 RID: 1442
	// (Invoke) Token: 0x060018E3 RID: 6371
	private delegate uint G80D7C09(IntPtr AA921311, IntPtr N3347726, I315F7B4.JE116DBC K1A115A8, IntPtr B40FF99D, uint PD1514B4, out uint FAABFF97);

	// Token: 0x020005A3 RID: 1443
	// (Invoke) Token: 0x060018E9 RID: 6377
	private delegate IntPtr EA9973A4(string E88AA01B, IntPtr F9B14138);

	// Token: 0x020005C3 RID: 1475
	public struct M73F7CBA
	{
		// Token: 0x04000590 RID: 1424
		public IntPtr KC202D84;

		// Token: 0x04000591 RID: 1425
		public IntPtr E73BE1B5;

		// Token: 0x04000592 RID: 1426
		public IntPtr EE24BAB9;

		// Token: 0x04000593 RID: 1427
		public IntPtr OB11432C;

		// Token: 0x04000594 RID: 1428
		public IntPtr A4A196B6;

		// Token: 0x04000595 RID: 1429
		public IntPtr H3256025;
	}

	// Token: 0x020005CF RID: 1487
	public struct C33FACB2
	{
		// Token: 0x04000598 RID: 1432
		public uint NB1AB8A5;

		// Token: 0x04000599 RID: 1433
		public uint PF8A6705;

		// Token: 0x0400059A RID: 1434
		public byte FD063880;
	}

	// Token: 0x020005E5 RID: 1509
	private struct P4B76A11
	{
		// Token: 0x040005AB RID: 1451
		public IntPtr C192B1A4;

		// Token: 0x040005AC RID: 1452
		public IntPtr DA1FBD8D;

		// Token: 0x040005AD RID: 1453
		public IntPtr BCB38BA5;

		// Token: 0x040005AE RID: 1454
		public IntPtr J03051A6;

		// Token: 0x040005AF RID: 1455
		public int D9B575A8;

		// Token: 0x040005B0 RID: 1456
		public int O4B48838;
	}

	// Token: 0x02000635 RID: 1589
	// (Invoke) Token: 0x06001AD9 RID: 6873
	private delegate UIntPtr H4AB053E(IntPtr K7136E1A, UIntPtr HEB39A86, I315F7B4.C1809494 ED17CCBC);

	// Token: 0x02000694 RID: 1684
	public enum D091D1BB : uint
	{
		// Token: 0x04000657 RID: 1623
		IEA7A013 = 32768U
	}

	// Token: 0x020006A1 RID: 1697
	public enum JE116DBC
	{
		// Token: 0x0400065D RID: 1629
		G5845085 = 2
	}
}
