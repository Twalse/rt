using System;

namespace SteamGameSearcher
{
	// Token: 0x0200004B RID: 75
	public class InstalledGame
	{
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0000D72A File Offset: 0x0000BB2A
		// (set) Token: 0x06000299 RID: 665 RVA: 0x0000D732 File Offset: 0x0000BB32
		public int AppId { get; set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0000D73B File Offset: 0x0000BB3B
		// (set) Token: 0x0600029B RID: 667 RVA: 0x0000D743 File Offset: 0x0000BB43
		public string Name { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0000D74C File Offset: 0x0000BB4C
		// (set) Token: 0x0600029D RID: 669 RVA: 0x0000D754 File Offset: 0x0000BB54
		public string InstallPath { get; set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600029E RID: 670 RVA: 0x0000D75D File Offset: 0x0000BB5D
		// (set) Token: 0x0600029F RID: 671 RVA: 0x0000D765 File Offset: 0x0000BB65
		public string InstallDir { get; set; }

		// Token: 0x060002A0 RID: 672 RVA: 0x0015A4C0 File Offset: 0x00157CC0
		public InstalledGame()
		{
			P4258EBF.AFA7138A.M6233B19[130](this);
		}
	}
}
