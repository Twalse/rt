using System;
using Newtonsoft.Json;

namespace WpfApp1.Model
{
	// Token: 0x0200005C RID: 92
	public class HwidResponse
	{
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000348 RID: 840 RVA: 0x00011B64 File Offset: 0x0000FF64
		// (set) Token: 0x06000349 RID: 841 RVA: 0x00011B6C File Offset: 0x0000FF6C
		[JsonProperty("success")]
		public bool Success { get; set; }

		// Token: 0x0600034A RID: 842 RVA: 0x0015F100 File Offset: 0x0015C900
		public HwidResponse()
		{
			P4258EBF.AFA7138A.M6233B19[130](this);
		}
	}
}
