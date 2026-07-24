using System;
using System.Threading.Tasks;

namespace RustTweaker.Optimization.Monitoring
{
	// Token: 0x0200003D RID: 61
	public interface IMonitoring
	{
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000241 RID: 577
		string Id { get; }

		// Token: 0x06000242 RID: 578
		Task<int[]> GetStatus();
	}
}
