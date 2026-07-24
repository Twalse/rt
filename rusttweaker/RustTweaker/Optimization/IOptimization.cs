using System;

namespace RustTweaker.Optimization
{
	// Token: 0x0200001E RID: 30
	public interface IOptimization
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000F8 RID: 248
		OptimizationId Id { get; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000F9 RID: 249
		bool NeedComputerRestart { get; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000FA RID: 250
		bool NeedSteamRestart { get; }

		// Token: 0x060000FB RID: 251
		OptimizationStatus GetStatus();

		// Token: 0x060000FC RID: 252
		void Apply(OptimizationTargetStatus targetStatus);
	}
}
