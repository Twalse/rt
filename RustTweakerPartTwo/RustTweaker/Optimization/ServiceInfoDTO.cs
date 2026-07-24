using System;

namespace RustTweaker.Optimization
{
	// Token: 0x0200002B RID: 43
	public class ServiceInfoDTO
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00008210 File Offset: 0x00006610
		// (set) Token: 0x06000164 RID: 356 RVA: 0x00008218 File Offset: 0x00006618
		public string Name { get; set; } = P4258EBF.AFA7138A.M6233B19[280]();

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00008221 File Offset: 0x00006621
		// (set) Token: 0x06000166 RID: 358 RVA: 0x00008229 File Offset: 0x00006629
		public string DisplayName { get; set; } = P4258EBF.AFA7138A.M6233B19[280]();

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00008232 File Offset: 0x00006632
		// (set) Token: 0x06000168 RID: 360 RVA: 0x0000823A File Offset: 0x0000663A
		public string State { get; set; } = P4258EBF.AFA7138A.M6233B19[280]();

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00008243 File Offset: 0x00006643
		// (set) Token: 0x0600016A RID: 362 RVA: 0x0000824B File Offset: 0x0000664B
		public string Status { get; set; } = P4258EBF.AFA7138A.M6233B19[280]();

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00008254 File Offset: 0x00006654
		// (set) Token: 0x0600016C RID: 364 RVA: 0x0000825C File Offset: 0x0000665C
		public ServiceStartupType StartMode { get; set; }

		// Token: 0x0600016D RID: 365 RVA: 0x001555E8 File Offset: 0x00152DE8
		public ServiceInfoDTO()
		{
			P4258EBF.AFA7138A.M6233B19[130](this);
		}
	}
}
