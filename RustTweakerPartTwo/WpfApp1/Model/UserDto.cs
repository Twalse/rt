using System;
using Newtonsoft.Json;

namespace WpfApp1.Model
{
	// Token: 0x0200005D RID: 93
	public class UserDto
	{
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600034B RID: 843 RVA: 0x00011B7D File Offset: 0x0000FF7D
		// (set) Token: 0x0600034C RID: 844 RVA: 0x00011B85 File Offset: 0x0000FF85
		[JsonProperty("email")]
		public string Email { get; set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600034D RID: 845 RVA: 0x00011B8E File Offset: 0x0000FF8E
		// (set) Token: 0x0600034E RID: 846 RVA: 0x00011B96 File Offset: 0x0000FF96
		[JsonProperty("plan")]
		public string Plan { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600034F RID: 847 RVA: 0x00011B9F File Offset: 0x0000FF9F
		// (set) Token: 0x06000350 RID: 848 RVA: 0x00011BA7 File Offset: 0x0000FFA7
		[JsonProperty("steamid")]
		public string SteamId { get; set; }

		// Token: 0x06000351 RID: 849 RVA: 0x001604E4 File Offset: 0x0015DCE4
		public UserDto()
		{
			P4258EBF.AFA7138A.M6233B19[130](this);
		}
	}
}
