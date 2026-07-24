using System;
using System.Diagnostics;

// Token: 0x020005D5 RID: 1493
[DebuggerDisplay("{2+AFA7138A.ODAA03A6()}")]
[Serializable]
public class E7854517
{
	// Token: 0x0600198B RID: 6539 RVA: 0x00102B8C File Offset: 0x0010038C
	public E7854517()
	{
		this.State = AE2AE992.Invalid;
		this.Expires = DateTime.MaxValue;
		this.MaxBuild = DateTime.MaxValue;
		this.RunningTime = 0;
		this.UserData = new byte[0];
		this.UserName = string.Empty;
		this.EMail = string.Empty;
	}

	// Token: 0x0400059C RID: 1436
	public DateTime MaxBuild;

	// Token: 0x0400059D RID: 1437
	public byte[] UserData;

	// Token: 0x0400059E RID: 1438
	public int RunningTime;

	// Token: 0x0400059F RID: 1439
	public DateTime Expires;

	// Token: 0x040005A0 RID: 1440
	public string UserName;

	// Token: 0x040005A1 RID: 1441
	public string EMail;

	// Token: 0x040005A2 RID: 1442
	public AE2AE992 State;
}
