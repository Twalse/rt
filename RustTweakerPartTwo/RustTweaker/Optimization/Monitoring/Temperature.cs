using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace RustTweaker.Optimization.Monitoring
{
	// Token: 0x02000040 RID: 64
	public class Temperature : IMonitoring
	{
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000250 RID: 592 RVA: 0x0000C6D9 File Offset: 0x0000AAD9
		public string Id
		{
			get
			{
				return "temperature-monitoring";
			}
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000C6E0 File Offset: 0x0000AAE0
		public Task<int[]> GetStatus()
		{
			return Temperature.GetStatusFromLatestHardwareTestAsync();
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000C6E8 File Offset: 0x0000AAE8
		public async Task<string> GetStatusJson()
		{
			int[] array = await this.GetStatus().ConfigureAwait(false);
			return P4258EBF.AFA7138A.M6233B19[330](array);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000C72C File Offset: 0x0000AB2C
		private static async Task<int[]> GetStatusFromLatestHardwareTestAsync()
		{
			JObject jobject = await MonitoringBenchmarkClient.GetLatestHardwareTestRootAsync().ConfigureAwait(false);
			JObject jobject2 = jobject;
			int[] array;
			if (jobject2 == null)
			{
				array = new int[1];
			}
			else
			{
				array = Temperature.GetStatusCodes(jobject2);
			}
			return array;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000C768 File Offset: 0x0000AB68
		public static OptimizationStatus GetMonitoringStatus(JObject root)
		{
			OptimizationStatus optimizationStatus;
			try
			{
				int[] statusCodes = Temperature.GetStatusCodes(root);
				if (statusCodes.Contains(1))
				{
					optimizationStatus = OptimizationStatus.Good;
				}
				else if (statusCodes.Any<int>((int code) => code - 5 <= 2))
				{
					optimizationStatus = OptimizationStatus.Bad;
				}
				else if (statusCodes.Any<int>((int code) => code - 2 <= 2))
				{
					optimizationStatus = OptimizationStatus.Middle;
				}
				else
				{
					optimizationStatus = OptimizationStatus.Unsupported;
				}
			}
			catch
			{
				optimizationStatus = OptimizationStatus.Unsupported;
			}
			return optimizationStatus;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00154178 File Offset: 0x00151978
		public static int[] GetStatusCodes(JObject root)
		{
			int[] array2;
			try
			{
				List<Temperature.TemperatureStatusCode> list = new List<Temperature.TemperatureStatusCode>(6);
				Temperature.AddStatusByThresholds(list, Temperature.GetMaxSceneAverageValue(root, "CpuTemp"), 80.0, 90.0, Temperature.TemperatureStatusCode.HighCpuTemperature, Temperature.TemperatureStatusCode.CpuOverheat);
				Temperature.AddStatusByThresholds(list, Temperature.GetMaxSceneAverageValue(root, "GpuTemp"), 75.0, 85.0, Temperature.TemperatureStatusCode.HighGpuTemperature, Temperature.TemperatureStatusCode.GpuOverheat);
				Temperature.AddStatusByThresholds(list, Temperature.GetMaxSceneAverageRamTemperature(root), 55.0, 75.0, Temperature.TemperatureStatusCode.HighRamTemperature, Temperature.TemperatureStatusCode.RamOverheat);
				if (list.Count == 0)
				{
					list.Add(Temperature.TemperatureStatusCode.Ok);
				}
				int[] array = (from code in list.Distinct<Temperature.TemperatureStatusCode>()
					select (int)code).ToArray<int>();
				object statusLock = Temperature.StatusLock;
				bool flag = false;
				try
				{
					P4258EBF.AFA7138A.M6233B19[520](statusLock, ref flag);
					Temperature._lastStatusCodes = array;
				}
				finally
				{
					if (flag)
					{
						P4258EBF.AFA7138A.M6233B19[631](statusLock);
					}
				}
				array2 = array;
			}
			catch
			{
				int[] array3 = new int[1];
				object statusLock2 = Temperature.StatusLock;
				bool flag2 = false;
				try
				{
					P4258EBF.AFA7138A.M6233B19[520](statusLock2, ref flag2);
					Temperature._lastStatusCodes = array3;
				}
				finally
				{
					if (flag2)
					{
						P4258EBF.AFA7138A.M6233B19[631](statusLock2);
					}
				}
				array2 = array3;
			}
			return array2;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000C948 File Offset: 0x0000AD48
		private static void AddStatusByThresholds(ICollection<Temperature.TemperatureStatusCode> codes, double? value, double goodMax, double middleMax, Temperature.TemperatureStatusCode middleStatus, Temperature.TemperatureStatusCode badStatus)
		{
			if (value == null)
			{
				return;
			}
			if (value.Value <= goodMax)
			{
				return;
			}
			if (value.Value <= middleMax)
			{
				codes.Add(middleStatus);
				return;
			}
			codes.Add(badStatus);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000C97C File Offset: 0x0000AD7C
		private static double? GetMaxSceneAverageValue(JObject root, string propertyName)
		{
			return (from scene in Temperature.GetBenchmarkScenes(root)
				select P4258EBF.AFA7138A.M6233B19[357](Temperature.ReadNumberArray(P4258EBF.AFA7138A.M6233B19[175](scene, propertyName)).DefaultIfEmpty(double.NaN)) into avg
				where !P4258EBF.AFA7138A.M6233B19[95](avg)
				select avg).DefaultIfEmpty(double.NaN).Max<double>(delegate(double avg)
			{
				if (!P4258EBF.AFA7138A.M6233B19[95](avg))
				{
					return new double?(avg);
				}
				return null;
			});
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000CA04 File Offset: 0x0000AE04
		private static double? GetMaxSceneAverageRamTemperature(JObject root)
		{
			return (from avg in Temperature.GetBenchmarkScenes(root).Select<JObject, double>(delegate(JObject scene)
				{
					JToken jtoken = P4258EBF.AFA7138A.M6233B19[175](scene, "RamTemp");
					return BF1EDB12.C21F2F90((((jtoken != null) ? jtoken.OfType<JObject>() : null) ?? Enumerable.Empty<JObject>()).SelectMany<JObject, double>((JObject module) => Temperature.ReadNumberArray(P4258EBF.AFA7138A.M6233B19[175](module, "values") ?? P4258EBF.AFA7138A.M6233B19[175](module, "Values"))).DefaultIfEmpty(double.NaN));
				})
				where !P4258EBF.AFA7138A.M6233B19[95](avg)
				select avg).DefaultIfEmpty(double.NaN).Max<double>(delegate(double avg)
			{
				if (!P4258EBF.AFA7138A.M6233B19[95](avg))
				{
					return new double?(avg);
				}
				return null;
			});
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0014E498 File Offset: 0x0014BC98
		private static IEnumerable<JObject> GetBenchmarkScenes(JObject root)
		{
			JToken jtoken = P4258EBF.AFA7138A.M6233B19[175](root, "BenchmarkInfo");
			return ((jtoken != null) ? jtoken.OfType<JObject>() : null) ?? Enumerable.Empty<JObject>();
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000CAB3 File Offset: 0x0000AEB3
		private static IEnumerable<double> ReadNumberArray(JToken token)
		{
			Temperature.<ReadNumberArray>d__14 <ReadNumberArray>d__ = new Temperature.<ReadNumberArray>d__14(-2);
			<ReadNumberArray>d__.<>3__token = token;
			return <ReadNumberArray>d__;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x001594D0 File Offset: 0x00156CD0
		public Temperature()
		{
			P4258EBF.AFA7138A.M6233B19[130](this);
		}

		// Token: 0x040000C3 RID: 195
		private static readonly object StatusLock = P4258EBF.AFA7138A.M6233B19[131]();

		// Token: 0x040000C4 RID: 196
		private static int[] _lastStatusCodes = new int[1];

		// Token: 0x020000C4 RID: 196
		public enum TemperatureStatusCode
		{
			// Token: 0x040002AC RID: 684
			Error,
			// Token: 0x040002AD RID: 685
			Ok,
			// Token: 0x040002AE RID: 686
			HighCpuTemperature,
			// Token: 0x040002AF RID: 687
			HighGpuTemperature,
			// Token: 0x040002B0 RID: 688
			HighRamTemperature,
			// Token: 0x040002B1 RID: 689
			CpuOverheat,
			// Token: 0x040002B2 RID: 690
			GpuOverheat,
			// Token: 0x040002B3 RID: 691
			RamOverheat
		}
	}
}
