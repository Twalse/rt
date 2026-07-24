using System;

namespace RustTweaker.Optimization
{
	// Token: 0x0200002A RID: 42
	public class ServiceConfig
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600015E RID: 350 RVA: 0x000081DB File Offset: 0x000065DB
		// (set) Token: 0x0600015F RID: 351 RVA: 0x000081E3 File Offset: 0x000065E3
		public string Name { get; set; } = P4258EBF.AFA7138A.M6233B19[280]();

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000160 RID: 352 RVA: 0x000081EC File Offset: 0x000065EC
		// (set) Token: 0x06000161 RID: 353 RVA: 0x000081F4 File Offset: 0x000065F4
		public ServiceStartupType StartupType { get; set; }

		// Token: 0x06000162 RID: 354 RVA: 0x00143814 File Offset: 0x00141014
		public ServiceConfig()
		{
			P4258EBF.AFA7138A.M6233B19[130](this);
		}
	}
}
