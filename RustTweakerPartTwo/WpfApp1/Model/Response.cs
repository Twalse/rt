using System;
using Newtonsoft.Json;

namespace WpfApp1.Model
{
	// Token: 0x02000067 RID: 103
	public class Response
	{
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600039C RID: 924 RVA: 0x00015C40 File Offset: 0x00014040
		// (set) Token: 0x0600039D RID: 925 RVA: 0x00015C48 File Offset: 0x00014048
		[JsonProperty("success")]
		public bool Success { get; set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600039E RID: 926 RVA: 0x00015C51 File Offset: 0x00014051
		// (set) Token: 0x0600039F RID: 927 RVA: 0x00015C59 File Offset: 0x00014059
		[JsonProperty("message")]
		public string Message { get; set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x00015C62 File Offset: 0x00014062
		// (set) Token: 0x060003A1 RID: 929 RVA: 0x00015C6A File Offset: 0x0001406A
		[JsonProperty("applicationVersion")]
		public string AppVersion { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x00015C73 File Offset: 0x00014073
		// (set) Token: 0x060003A3 RID: 931 RVA: 0x00015C7B File Offset: 0x0001407B
		[JsonProperty("token")]
		public string Token { get; set; }

		// Token: 0x060003A4 RID: 932 RVA: 0x00161740 File Offset: 0x0015EF40
		public Response()
		{
			P4258EBF.AFA7138A.M6233B19[130](this);
		}
	}
}
