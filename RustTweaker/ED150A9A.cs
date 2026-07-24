using System;
using System.Diagnostics;
using System.IO;

// Token: 0x020001F7 RID: 503
[DebuggerDisplay("{2+AFA7138A.ODAA03A6()}")]
public class ED150A9A
{
	// Token: 0x060007AD RID: 1965 RVA: 0x00719B90 File Offset: 0x00715590
	private void K8A01095(uint H00ADB2C)
	{
		if (this.A202C225 != H00ADB2C)
		{
			this.A202C225 = H00ADB2C;
			this.I53A9EB2 = Math.Max(this.A202C225, 1U);
			uint num = Math.Max(this.I53A9EB2, 4096U);
			this.KF9B15BC.D59A8A04(num);
		}
	}

	// Token: 0x060007AE RID: 1966 RVA: 0x00719C58 File Offset: 0x00715658
	private void CC3E9B15(Stream P99B40A8, Stream A512C898)
	{
		this.B925823E.P10C36AF(P99B40A8);
		this.KF9B15BC.PAB0EF91(A512C898, false);
		for (uint num = 0U; num < 12U; num += 1U)
		{
			for (uint num2 = 0U; num2 <= this.FF32DF92; num2 += 1U)
			{
				uint num3 = (num << 4) + num2;
				this.L53F579C[(int)num3].M63722B7();
				this.O38CA63D[(int)num3].M63722B7();
			}
			this.MB3B8B3E[(int)num].M63722B7();
			this.P4835290[(int)num].M63722B7();
			this.L08A6984[(int)num].M63722B7();
			this.KB036F06[(int)num].M63722B7();
		}
		this.MB0F5C81.F69800BF();
		for (uint num = 0U; num < 4U; num += 1U)
		{
			this.E91BCAA1[(int)num].H928582E();
		}
		for (uint num = 0U; num < 114U; num += 1U)
		{
			this.K304D138[(int)num].M63722B7();
		}
		this.M5B48628.MDAD5A9C();
		this.JFA5CC9D.MDAD5A9C();
		this.P781B721.H928582E();
	}

	// Token: 0x060007AF RID: 1967 RVA: 0x0071A110 File Offset: 0x00715B10
	public void O6B1799F(byte[] K62D8995)
	{
		if (K62D8995.Length < 5)
		{
			throw new ArgumentException();
		}
		int num = (int)(K62D8995[0] % 9);
		byte b = K62D8995[0] / 9;
		int num2 = (int)(b % 5);
		int num3 = (int)(b / 5);
		if (num3 > 4)
		{
			throw new ArgumentException();
		}
		uint num4 = 0U;
		for (int i = 0; i < 4; i++)
		{
			num4 += (uint)((uint)K62D8995[1 + i] << i * 8);
		}
		this.K8A01095(num4);
		this.L0809898(num2, num);
		this.D5879584(num3);
	}

	// Token: 0x060007B0 RID: 1968 RVA: 0x00719BE0 File Offset: 0x007155E0
	private void L0809898(int HFBA4B90, int B8102EBE)
	{
		if (HFBA4B90 > 8)
		{
			throw new ArgumentException();
		}
		if (B8102EBE > 8)
		{
			throw new ArgumentException();
		}
		this.MB0F5C81.PB3D3915(HFBA4B90, B8102EBE);
	}

	// Token: 0x060007B1 RID: 1969 RVA: 0x00719C14 File Offset: 0x00715614
	private void D5879584(int E531F030)
	{
		if (E531F030 > 4)
		{
			throw new ArgumentException();
		}
		uint num = 1U << E531F030;
		this.M5B48628.C238C39A(num);
		this.JFA5CC9D.C238C39A(num);
		this.FF32DF92 = num - 1U;
	}

	// Token: 0x060007B2 RID: 1970 RVA: 0x00719D88 File Offset: 0x00715788
	public void MF05E23F(Stream H38FE708, Stream L0026F0B, long H3040C11)
	{
		this.CC3E9B15(H38FE708, L0026F0B);
		L0BE8811.H6BFD73D h6BFD73D = default(L0BE8811.H6BFD73D);
		h6BFD73D.FE9C919E();
		uint num = 0U;
		uint num2 = 0U;
		uint num3 = 0U;
		uint num4 = 0U;
		ulong num5 = 0UL;
		if (num5 < (ulong)H3040C11)
		{
			if (this.L53F579C[(int)((int)h6BFD73D.J22E253D << 4)].DF1D1809(this.B925823E) != 0U)
			{
				throw new InvalidDataException();
			}
			h6BFD73D.B390A231();
			byte b = this.MB0F5C81.M4926A96(this.B925823E, 0U, 0);
			this.KF9B15BC.I2981D0B(b);
			num5 += 1UL;
		}
		while (num5 < (ulong)H3040C11)
		{
			uint num6 = (uint)num5 & this.FF32DF92;
			if (this.L53F579C[(int)((h6BFD73D.J22E253D << 4) + num6)].DF1D1809(this.B925823E) == 0U)
			{
				byte b2 = this.KF9B15BC.CF3B211D(0U);
				byte b3;
				if (!h6BFD73D.FEA8EA3B())
				{
					b3 = this.MB0F5C81.D795A10A(this.B925823E, (uint)num5, b2, this.KF9B15BC.CF3B211D(num));
				}
				else
				{
					b3 = this.MB0F5C81.M4926A96(this.B925823E, (uint)num5, b2);
				}
				this.KF9B15BC.I2981D0B(b3);
				h6BFD73D.B390A231();
				num5 += 1UL;
			}
			else
			{
				uint num8;
				if (this.MB3B8B3E[(int)h6BFD73D.J22E253D].DF1D1809(this.B925823E) == 1U)
				{
					if (this.P4835290[(int)h6BFD73D.J22E253D].DF1D1809(this.B925823E) == 0U)
					{
						if (this.O38CA63D[(int)((h6BFD73D.J22E253D << 4) + num6)].DF1D1809(this.B925823E) == 0U)
						{
							h6BFD73D.HDB06392();
							this.KF9B15BC.I2981D0B(this.KF9B15BC.CF3B211D(num));
							num5 += 1UL;
							continue;
						}
					}
					else
					{
						uint num7;
						if (this.L08A6984[(int)h6BFD73D.J22E253D].DF1D1809(this.B925823E) == 0U)
						{
							num7 = num2;
						}
						else
						{
							if (this.KB036F06[(int)h6BFD73D.J22E253D].DF1D1809(this.B925823E) == 0U)
							{
								num7 = num3;
							}
							else
							{
								num7 = num4;
								num4 = num3;
							}
							num3 = num2;
						}
						num2 = num;
						num = num7;
					}
					num8 = this.JFA5CC9D.P795C79C(this.B925823E, num6) + 2U;
					h6BFD73D.I1A8533A();
				}
				else
				{
					num4 = num3;
					num3 = num2;
					num2 = num;
					num8 = 2U + this.M5B48628.P795C79C(this.B925823E, num6);
					h6BFD73D.FB2CBDB2();
					uint num9 = this.E91BCAA1[(int)L0BE8811.K013720D(num8)].LB92B522(this.B925823E);
					if (num9 >= 4U)
					{
						int num10 = (int)((num9 >> 1) - 1U);
						num = (2U | (num9 & 1U)) << num10;
						if (num9 < 14U)
						{
							num += PF0C0E9E.AC3AB180(this.K304D138, num - num9 - 1U, this.B925823E, num10);
						}
						else
						{
							num += this.B925823E.D3A188BA(num10 - 4) << 4;
							num += this.P781B721.EB1EB10F(this.B925823E);
						}
					}
					else
					{
						num = num9;
					}
				}
				if ((ulong)num >= (ulong)this.KF9B15BC.DAB65219 + num5 || num >= this.I53A9EB2)
				{
					if (num != 4294967295U)
					{
						throw new InvalidDataException();
					}
					break;
				}
				else
				{
					this.KF9B15BC.G999423F(num, num8);
					num5 += (ulong)num8;
				}
			}
		}
		this.KF9B15BC.D31A8BAA();
		this.KF9B15BC.OD389F35();
		this.B925823E.B928B098();
	}

	// Token: 0x060007B3 RID: 1971 RVA: 0x00719A98 File Offset: 0x00715498
	public ED150A9A()
	{
		this.A202C225 = uint.MaxValue;
		int num = 0;
		while ((long)num < 4L)
		{
			this.E91BCAA1[num] = new PF0C0E9E(6);
			num++;
		}
	}

	// Token: 0x040004B3 RID: 1203
	private readonly FF15D6B0[] P4835290 = new FF15D6B0[12];

	// Token: 0x040004B4 RID: 1204
	private readonly I00BF691 B925823E = new I00BF691();

	// Token: 0x040004B5 RID: 1205
	private readonly ED150A9A.IF10B7AE JFA5CC9D = new ED150A9A.IF10B7AE();

	// Token: 0x040004B6 RID: 1206
	private readonly FF15D6B0[] L53F579C = new FF15D6B0[192];

	// Token: 0x040004B7 RID: 1207
	private uint A202C225;

	// Token: 0x040004B8 RID: 1208
	private readonly ED150A9A.IF10B7AE M5B48628 = new ED150A9A.IF10B7AE();

	// Token: 0x040004B9 RID: 1209
	private uint FF32DF92;

	// Token: 0x040004BA RID: 1210
	private readonly FF15D6B0[] KB036F06 = new FF15D6B0[12];

	// Token: 0x040004BB RID: 1211
	private readonly FF15D6B0[] L08A6984 = new FF15D6B0[12];

	// Token: 0x040004BC RID: 1212
	private readonly PF0C0E9E[] E91BCAA1 = new PF0C0E9E[4];

	// Token: 0x040004BD RID: 1213
	private uint I53A9EB2;

	// Token: 0x040004BE RID: 1214
	private readonly ND1CA726 KF9B15BC = new ND1CA726();

	// Token: 0x040004BF RID: 1215
	private PF0C0E9E P781B721 = new PF0C0E9E(4);

	// Token: 0x040004C0 RID: 1216
	private readonly FF15D6B0[] O38CA63D = new FF15D6B0[192];

	// Token: 0x040004C1 RID: 1217
	private readonly FF15D6B0[] MB3B8B3E = new FF15D6B0[12];

	// Token: 0x040004C2 RID: 1218
	private readonly ED150A9A.AC9CFE37 MB0F5C81 = new ED150A9A.AC9CFE37();

	// Token: 0x040004C3 RID: 1219
	private readonly FF15D6B0[] K304D138 = new FF15D6B0[114];

	// Token: 0x040004C4 RID: 1220
	private uint D5A2F73D = 1U;

	// Token: 0x020003A6 RID: 934
	[DebuggerDisplay("{2+AFA7138A.ODAA03A6()}")]
	private class IF10B7AE
	{
		// Token: 0x06000A50 RID: 2640 RVA: 0x0071B1DC File Offset: 0x00716BDC
		public void MDAD5A9C()
		{
			this.C0002BA5.M63722B7();
			for (uint num = 0U; num < this.KB29D0AE; num += 1U)
			{
				this.J2A1651B[(int)num].H928582E();
				this.M0A1EEAC[(int)num].H928582E();
			}
			this.H80BD81A.M63722B7();
			this.B53CBD06.H928582E();
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x0071B244 File Offset: 0x00716C44
		public uint P795C79C(I00BF691 N8A0592D, uint J0BB601D)
		{
			if (this.C0002BA5.DF1D1809(N8A0592D) == 0U)
			{
				return this.J2A1651B[(int)J0BB601D].LB92B522(N8A0592D);
			}
			uint num = 8U;
			if (this.H80BD81A.DF1D1809(N8A0592D) == 0U)
			{
				num += this.M0A1EEAC[(int)J0BB601D].LB92B522(N8A0592D);
			}
			else
			{
				num += 8U;
				num += this.B53CBD06.LB92B522(N8A0592D);
			}
			return num;
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x0071B18C File Offset: 0x00716B8C
		public void C238C39A(uint KA301FA0)
		{
			for (uint num = this.KB29D0AE; num < KA301FA0; num += 1U)
			{
				this.J2A1651B[(int)num] = new PF0C0E9E(3);
				this.M0A1EEAC[(int)num] = new PF0C0E9E(3);
			}
			this.KB29D0AE = KA301FA0;
		}

		// Token: 0x040004FB RID: 1275
		private readonly PF0C0E9E[] J2A1651B = new PF0C0E9E[16];

		// Token: 0x040004FC RID: 1276
		private FF15D6B0 H80BD81A;

		// Token: 0x040004FD RID: 1277
		private PF0C0E9E B53CBD06 = new PF0C0E9E(8);

		// Token: 0x040004FE RID: 1278
		private uint KB29D0AE;

		// Token: 0x040004FF RID: 1279
		private readonly PF0C0E9E[] M0A1EEAC = new PF0C0E9E[16];

		// Token: 0x04000500 RID: 1280
		private FF15D6B0 C0002BA5;
	}

	// Token: 0x02000503 RID: 1283
	[DebuggerDisplay("{2+AFA7138A.ODAA03A6()}")]
	private class AC9CFE37
	{
		// Token: 0x060017B9 RID: 6073 RVA: 0x0071B3F8 File Offset: 0x00716DF8
		public byte M4926A96(I00BF691 LA181612, uint KD888122, byte IA3BDC0A)
		{
			return this.A09F1B19[(int)this.NCA67B80(KD888122, IA3BDC0A)].I827B9AE(LA181612);
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x0071B3C8 File Offset: 0x00716DC8
		private uint NCA67B80(uint EDBBC8AC, byte G399B811)
		{
			return ((EDBBC8AC & this.EBA4E684) << this.M11D9BBD) + (uint)(G399B811 >> 8 - this.M11D9BBD);
		}

		// Token: 0x060017BC RID: 6076 RVA: 0x0071B420 File Offset: 0x00716E20
		public byte D795A10A(I00BF691 HB9415B2, uint G4990D20, byte FAB834AA, byte EC3DDE21)
		{
			return this.A09F1B19[(int)this.NCA67B80(G4990D20, FAB834AA)].NC34EEBD(HB9415B2, EC3DDE21);
		}

		// Token: 0x060017BD RID: 6077 RVA: 0x0071B384 File Offset: 0x00716D84
		public void F69800BF()
		{
			uint num = 1U << this.M11D9BBD + this.H184D32F;
			for (uint num2 = 0U; num2 < num; num2 += 1U)
			{
				this.A09F1B19[(int)num2].JF28E827();
			}
		}

		// Token: 0x060017BE RID: 6078 RVA: 0x0071B2F4 File Offset: 0x00716CF4
		public void PB3D3915(int L5298F33, int N59CAF1A)
		{
			if (this.A09F1B19 != null && this.M11D9BBD == N59CAF1A && this.H184D32F == L5298F33)
			{
				return;
			}
			this.H184D32F = L5298F33;
			this.EBA4E684 = (1U << L5298F33) - 1U;
			this.M11D9BBD = N59CAF1A;
			uint num = 1U << this.M11D9BBD + this.H184D32F;
			this.A09F1B19 = new ED150A9A.AC9CFE37.P92CC490[num];
			for (uint num2 = 0U; num2 < num; num2 += 1U)
			{
				this.A09F1B19[(int)num2].PA17E19D();
			}
		}

		// Token: 0x04000555 RID: 1365
		private ED150A9A.AC9CFE37.P92CC490[] A09F1B19;

		// Token: 0x04000556 RID: 1366
		private int M11D9BBD;

		// Token: 0x04000557 RID: 1367
		private uint EBA4E684;

		// Token: 0x04000558 RID: 1368
		private int H184D32F;

		// Token: 0x04000559 RID: 1369
		private uint I931EE2E = 1U;

		// Token: 0x0200069D RID: 1693
		private struct P92CC490
		{
			// Token: 0x06001BDC RID: 7132 RVA: 0x0071B67C File Offset: 0x0071707C
			public byte I827B9AE(I00BF691 J30373BB)
			{
				uint num = 1U;
				do
				{
					num = (num << 1) | this.C795829A[(int)num].DF1D1809(J30373BB);
				}
				while (num < 256U);
				return (byte)num;
			}

			// Token: 0x06001BDD RID: 7133 RVA: 0x0071B6B0 File Offset: 0x007170B0
			public byte NC34EEBD(I00BF691 JEAB5087, byte J8B4983A)
			{
				uint num = 1U;
				for (;;)
				{
					uint num2 = (uint)((J8B4983A >> 7) & 1);
					J8B4983A = (byte)(J8B4983A << 1);
					uint num3 = this.C795829A[(int)((1U + num2 << 8) + num)].DF1D1809(JEAB5087);
					num = (num << 1) | num3;
					if (num2 != num3)
					{
						break;
					}
					if (num >= 256U)
					{
						goto IL_006B;
					}
				}
				while (num < 256U)
				{
					num = (num << 1) | this.C795829A[(int)num].DF1D1809(JEAB5087);
				}
				IL_006B:
				return (byte)num;
			}

			// Token: 0x06001BDE RID: 7134 RVA: 0x0071B648 File Offset: 0x00717048
			public void JF28E827()
			{
				for (int i = 0; i < 768; i++)
				{
					this.C795829A[i].M63722B7();
				}
			}

			// Token: 0x06001BDF RID: 7135 RVA: 0x0071B628 File Offset: 0x00717028
			public void PA17E19D()
			{
				this.C795829A = new FF15D6B0[768];
			}

			// Token: 0x0400065C RID: 1628
			private FF15D6B0[] C795829A;
		}
	}
}
