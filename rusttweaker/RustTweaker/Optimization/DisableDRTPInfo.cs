using System;

namespace RustTweaker.Optimization
{
	// Token: 0x02000024 RID: 36
	public class DisableDRTPInfo
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00006AB4 File Offset: 0x00004EB4
		// (set) Token: 0x06000116 RID: 278 RVA: 0x00006ABC File Offset: 0x00004EBC
		public bool IsTamperProtected { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00006AC5 File Offset: 0x00004EC5
		// (set) Token: 0x06000118 RID: 280 RVA: 0x00006ACD File Offset: 0x00004ECD
		public bool RealTimeProtectionEnabled { get; set; }

		// Token: 0x06000119 RID: 281 RVA: 0x00161E9C File Offset: 0x0015F69C
		public DisableDRTPInfo()
		{
			P4258EBF.AFA7138A.M6233B19[130](this);
		}
	}
}
