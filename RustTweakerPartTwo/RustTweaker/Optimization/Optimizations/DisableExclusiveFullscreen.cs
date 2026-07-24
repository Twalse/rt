using System;
using RustTweakerDemo;
using WpfApp1;
using WpfApp1.Model;

namespace RustTweaker.Optimization.Optimizations
{
	// Token: 0x02000034 RID: 52
	public sealed class DisableExclusiveFullscreen : IOptimization
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00009D03 File Offset: 0x00008103
		public OptimizationId Id
		{
			get
			{
				return OptimizationId.DisableExclusiveFullscreen;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00009D06 File Offset: 0x00008106
		public bool NeedComputerRestart
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00009D09 File Offset: 0x00008109
		public bool NeedSteamRestart
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x001511D0 File Offset: 0x0014E9D0
		public OptimizationStatus GetStatus()
		{
			string currentParamsLaunch = new JsBridge().getCurrentParamsLaunch();
			if (P4258EBF.AFA7138A.M6233B19[433](currentParamsLaunch, "-window-mode exclusive") || P4258EBF.AFA7138A.M6233B19[433](currentParamsLaunch, "-window-mode \\\"exclusive\\\""))
			{
				return OptimizationStatus.Bad;
			}
			return OptimizationStatus.Good;
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x001602A8 File Offset: 0x0015DAA8
		public void Apply(OptimizationTargetStatus targetStatus)
		{
			string configPathToLastUser = MainLogic.SteamParser.GetConfigPathToLastUser();
			string text = new JsBridge().getCurrentParamsLaunch();
			RustTweakerViewModel rustTweakerViewModel = new RustTweakerViewModel();
			text = P4258EBF.AFA7138A.M6233B19[114](text, "-window-mode exclusive", "");
			text = P4258EBF.AFA7138A.M6233B19[114](text, "-window-mode \\\"exclusive\\\"", "");
			rustTweakerViewModel.UpdateLocalConfig(configPathToLastUser, text);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0015BD50 File Offset: 0x00159550
		public DisableExclusiveFullscreen()
		{
			P4258EBF.AFA7138A.M6233B19[130](this);
		}
	}
}
