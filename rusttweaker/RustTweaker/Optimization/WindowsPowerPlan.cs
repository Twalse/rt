using System;

namespace RustTweaker.Optimization
{
	// Token: 0x02000031 RID: 49
	public sealed class WindowsPowerPlan : IOptimization
	{
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00009BE8 File Offset: 0x00007FE8
		public OptimizationId Id
		{
			get
			{
				return OptimizationId.WindowsPowerPlan;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00009BEB File Offset: 0x00007FEB
		public bool NeedComputerRestart
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00009BEE File Offset: 0x00007FEE
		public bool NeedSteamRestart
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0014B4A0 File Offset: 0x00148CA0
		public void Apply(OptimizationTargetStatus targetStatus)
		{
			if (targetStatus == OptimizationTargetStatus.Good)
			{
				OptimizationOriginalStateStore.SaveIfMissing<WindowsPowerPlan.PowerPlanState>(this.Id, new WindowsPowerPlan.PowerPlanState
				{
					ActivePowerScheme = WindowsPowerPlan.GetActivePowerScheme()
				});
				PowershellTools.powershellExecute("powercfg -setactive SCHEME_BALANCED");
				return;
			}
			WindowsPowerPlan.PowerPlanState powerPlanState;
			if (OptimizationOriginalStateStore.TryRead<WindowsPowerPlan.PowerPlanState>(this.Id, out powerPlanState) && powerPlanState != null && !P4258EBF.AFA7138A.M6233B19[426](powerPlanState.ActivePowerScheme))
			{
				PowershellTools.powershellExecute(P4258EBF.AFA7138A.M6233B19[478]("powercfg -setactive ", powerPlanState.ActivePowerScheme));
				return;
			}
			PowershellTools.powershellExecute("powercfg -setactive SCHEME_MAX");
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x001493D0 File Offset: 0x00146BD0
		public OptimizationStatus GetStatus()
		{
			string activePowerScheme = WindowsPowerPlan.GetActivePowerScheme();
			Logger.Log(P4258EBF.AFA7138A.M6233B19[478]("ActivePowerScheme before: ", activePowerScheme));
			if (P4258EBF.AFA7138A.M6233B19[593](P4258EBF.AFA7138A.M6233B19[597](activePowerScheme), "381b4222-f694-41f0-9685-ff5bb260df2e"))
			{
				return OptimizationStatus.Bad;
			}
			return OptimizationStatus.Good;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00160548 File Offset: 0x0015DD48
		private static string GetActivePowerScheme()
		{
			return P4258EBF.AFA7138A.M6233B19[597](PowershellTools.powershellExecute("(Get-ItemProperty 'HKLM:\\SYSTEM\\CurrentControlSet\\Control\\Power\\User\\PowerSchemes').ActivePowerScheme"));
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x001609BC File Offset: 0x0015E1BC
		public WindowsPowerPlan()
		{
			P4258EBF.AFA7138A.M6233B19[130](this);
		}

		// Token: 0x0400009E RID: 158
		private const string balancedGUID = "381b4222-f694-41f0-9685-ff5bb260df2e";

		// Token: 0x020000A5 RID: 165
		private sealed class PowerPlanState
		{
			// Token: 0x170000B5 RID: 181
			// (get) Token: 0x0600045C RID: 1116 RVA: 0x0001A6AF File Offset: 0x00018AAF
			// (set) Token: 0x0600045D RID: 1117 RVA: 0x0001A6B7 File Offset: 0x00018AB7
			public string ActivePowerScheme { get; set; } = P4258EBF.AFA7138A.M6233B19[280]();

			// Token: 0x0600045E RID: 1118 RVA: 0x0014F294 File Offset: 0x0014CA94
			public PowerPlanState()
			{
				P4258EBF.AFA7138A.M6233B19[130](this);
			}
		}
	}
}
