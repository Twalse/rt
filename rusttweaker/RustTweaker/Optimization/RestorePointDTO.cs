using System;

namespace RustTweaker.Optimization
{
	// Token: 0x02000033 RID: 51
	public class RestorePointDTO
	{
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00009CBD File Offset: 0x000080BD
		// (set) Token: 0x060001AA RID: 426 RVA: 0x00009CC5 File Offset: 0x000080C5
		public uint SequenceNumber { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00009CCE File Offset: 0x000080CE
		// (set) Token: 0x060001AC RID: 428 RVA: 0x00009CD6 File Offset: 0x000080D6
		public string Description { get; set; } = P4258EBF.AFA7138A.M6233B19[280]();

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00009CDF File Offset: 0x000080DF
		// (set) Token: 0x060001AE RID: 430 RVA: 0x00009CE7 File Offset: 0x000080E7
		public DateTime CreationTime { get; set; }

		// Token: 0x060001AF RID: 431 RVA: 0x00161C38 File Offset: 0x0015F438
		public RestorePointDTO()
		{
			P4258EBF.AFA7138A.M6233B19[130](this);
		}
	}
}
