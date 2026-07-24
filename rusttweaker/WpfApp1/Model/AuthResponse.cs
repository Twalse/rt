using System;
using Newtonsoft.Json;

namespace WpfApp1.Model
{
	// Token: 0x0200005B RID: 91
	public class AuthResponse
	{
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000343 RID: 835 RVA: 0x00011B3A File Offset: 0x0000FF3A
		// (set) Token: 0x06000344 RID: 836 RVA: 0x00011B42 File Offset: 0x0000FF42
		[JsonProperty("authenticated")]
		public bool Authenticated { get; set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000345 RID: 837 RVA: 0x00011B4B File Offset: 0x0000FF4B
		// (set) Token: 0x06000346 RID: 838 RVA: 0x00011B53 File Offset: 0x0000FF53
		[JsonProperty("user")]
		public UserDto User { get; set; }

		// Token: 0x06000347 RID: 839 RVA: 0x00138A74 File Offset: 0x00136274
		public AuthResponse()
		{
			P4258EBF.AFA7138A.M6233B19[130](this);
		}
	}
}
