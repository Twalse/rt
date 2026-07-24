using System;

namespace RustTweaker.Optimization
{
	// Token: 0x0200002F RID: 47
	public class PagefileDTO
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00008A64 File Offset: 0x00006E64
		// (set) Token: 0x0600017E RID: 382 RVA: 0x00008A6C File Offset: 0x00006E6C
		public string Path { get; set; } = P4258EBF.AFA7138A.M6233B19[280]();

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00008A75 File Offset: 0x00006E75
		// (set) Token: 0x06000180 RID: 384 RVA: 0x00008A7D File Offset: 0x00006E7D
		public uint AllocatedBaseSize { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00008A86 File Offset: 0x00006E86
		// (set) Token: 0x06000182 RID: 386 RVA: 0x00008A8E File Offset: 0x00006E8E
		public uint CurrentUsage { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00008A97 File Offset: 0x00006E97
		// (set) Token: 0x06000184 RID: 388 RVA: 0x00008A9F File Offset: 0x00006E9F
		public uint PeakUsage { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00008AA8 File Offset: 0x00006EA8
		// (set) Token: 0x06000186 RID: 390 RVA: 0x00008AB0 File Offset: 0x00006EB0
		public DateTime InstallDate { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00008AB9 File Offset: 0x00006EB9
		// (set) Token: 0x06000188 RID: 392 RVA: 0x00008AC1 File Offset: 0x00006EC1
		public bool TempPageFile { get; set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00008ACA File Offset: 0x00006ECA
		// (set) Token: 0x0600018A RID: 394 RVA: 0x00008AD2 File Offset: 0x00006ED2
		public bool IsAutomaticPagefile { get; set; }

		// Token: 0x0600018B RID: 395 RVA: 0x00160E44 File Offset: 0x0015E644
		public PagefileDTO()
		{
			P4258EBF.AFA7138A.M6233B19[130](this);
		}
	}
}
