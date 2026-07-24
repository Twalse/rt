using System;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace RustTweaker.Optimization
{
	// Token: 0x02000020 RID: 32
	internal static class OptimizationOriginalStateStore
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000FD RID: 253 RVA: 0x0016129C File Offset: 0x0015EA9C
		private static string StateDirectory
		{
			get
			{
				return P4258EBF.AFA7138A.M6233B19[278](P4258EBF.AFA7138A.M6233B19[54](Environment.SpecialFolder.ApplicationData), "RustTweaker", "OptimizationOriginalStates");
			}
		}

		// Token: 0x060000FE RID: 254 RVA: 0x0014670C File Offset: 0x00143F0C
		public static void SaveIfMissing<T>(OptimizationId optimizationId, T state)
		{
			string path = OptimizationOriginalStateStore.GetPath(optimizationId);
			if (P4258EBF.AFA7138A.M6233B19[627](path))
			{
				return;
			}
			P4258EBF.AFA7138A.M6233B19[111](OptimizationOriginalStateStore.StateDirectory);
			P4258EBF.AFA7138A.M6233B19[94](path, JsonSerializer.Serialize<T>(state, OptimizationOriginalStateStore.JsonOptions));
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0015BE94 File Offset: 0x00159694
		public static void SaveOrReplace<T>(OptimizationId optimizationId, T state)
		{
			string path = OptimizationOriginalStateStore.GetPath(optimizationId);
			P4258EBF.AFA7138A.M6233B19[111](OptimizationOriginalStateStore.StateDirectory);
			P4258EBF.AFA7138A.M6233B19[94](path, JsonSerializer.Serialize<T>(state, OptimizationOriginalStateStore.JsonOptions));
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00142EFC File Offset: 0x001406FC
		public static bool TryRead<T>(OptimizationId optimizationId, out T state)
		{
			state = default(T);
			string path = OptimizationOriginalStateStore.GetPath(optimizationId);
			if (!P4258EBF.AFA7138A.M6233B19[627](path))
			{
				return false;
			}
			bool flag;
			try
			{
				state = JsonSerializer.Deserialize<T>(P4258EBF.AFA7138A.M6233B19[267](path), OptimizationOriginalStateStore.JsonOptions);
				flag = state != null;
			}
			catch (Exception ex)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
				P4258EBF.AFA7138A.M6233B19[466](ref defaultInterpolatedStringHandler, 34, 1);
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "Failed to read original state for ");
				defaultInterpolatedStringHandler.AppendFormatted<OptimizationId>(optimizationId);
				Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler));
				Logger.Log(ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0015ED8C File Offset: 0x0015C58C
		private static string GetPath(OptimizationId optimizationId)
		{
			L083B68C l083B68C = P4258EBF.AFA7138A.M6233B19[158];
			string stateDirectory = OptimizationOriginalStateStore.StateDirectory;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			P4258EBF.AFA7138A.M6233B19[466](ref defaultInterpolatedStringHandler, 5, 1);
			defaultInterpolatedStringHandler.AppendFormatted<OptimizationId>(optimizationId);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, ".json");
			return l083B68C(stateDirectory, P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler));
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00160278 File Offset: 0x0015DA78
		// Note: this type is marked as 'beforefieldinit'.
		static OptimizationOriginalStateStore()
		{
			JsonSerializerOptions jsonSerializerOptions = P4258EBF.AFA7138A.M6233B19[14]();
			EC20BB1D.G79C000C(jsonSerializerOptions, true);
			N2389A30.H1B6258D(jsonSerializerOptions, true);
			OptimizationOriginalStateStore.JsonOptions = jsonSerializerOptions;
		}

		// Token: 0x04000064 RID: 100
		private static readonly JsonSerializerOptions JsonOptions;
	}
}
