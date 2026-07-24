using System;

namespace RustTweaker_NET.ViewModel.Optimizations
{
	// Token: 0x02000014 RID: 20
	public class OptimizationInfoDto
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00004A65 File Offset: 0x00002E65
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00004A6D File Offset: 0x00002E6D
		public string CurrentStatus { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00004A76 File Offset: 0x00002E76
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x00004A7E File Offset: 0x00002E7E
		public bool IsSupported { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00004A87 File Offset: 0x00002E87
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x00004A8F File Offset: 0x00002E8F
		public bool NeedComputerRestart { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00004A98 File Offset: 0x00002E98
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x00004AA0 File Offset: 0x00002EA0
		public bool NeedSteamRestart { get; set; }

		// Token: 0x060000B5 RID: 181 RVA: 0x00158424 File Offset: 0x00155C24
		public OptimizationInfoDto()
		{
			P4258EBF.AFA7138A.M6233B19[130](this);
		}
	}
}
