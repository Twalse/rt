using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace RustTweaker.Optimization.Monitoring
{
	// Token: 0x0200003F RID: 63
	public class Ram : IMonitoring
	{
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0000C2E2 File Offset: 0x0000A6E2
		public string Id
		{
			get
			{
				return "ram-monitoring";
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000C2EC File Offset: 0x0000A6EC
		public async Task<int[]> GetStatus()
		{
			int[] array;
			try
			{
				JObject jobject = await MonitoringBenchmarkClient.GetLatestHardwareTestRootAsync().ConfigureAwait(false);
				JObject jobject2 = jobject;
				if (jobject2 == null)
				{
					array = new int[1];
				}
				else
				{
					array = Ram.GetStatusCodes(jobject2);
				}
			}
			catch
			{
				array = new int[1];
			}
			return array;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000C328 File Offset: 0x0000A728
		public async Task<string> GetStatusJson()
		{
			int[] array = await this.GetStatus().ConfigureAwait(false);
			return P4258EBF.AFA7138A.M6233B19[330](array);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x001496FC File Offset: 0x00146EFC
		public static int[] GetStatusCodes(JObject root)
		{
			JObject jobject = P4258EBF.AFA7138A.M6233B19[175](root, "HardwareInfo") as JObject;
			string text;
			if (jobject == null)
			{
				text = null;
			}
			else
			{
				JToken jtoken = P4258EBF.AFA7138A.M6233B19[176](jobject, "RAM_Channel");
				text = ((jtoken != null) ? C3020339.PE003C13(jtoken.ToString()) : null);
			}
			string text2 = text ?? P4258EBF.AFA7138A.M6233B19[280]();
			bool flag = P4258EBF.AFA7138A.M6233B19[433](text2, "single");
			string text3;
			if (jobject == null)
			{
				text3 = null;
			}
			else
			{
				JToken jtoken2 = P4258EBF.AFA7138A.M6233B19[176](jobject, "RAM_Frequency");
				text3 = ((jtoken2 != null) ? jtoken2.ToString() : null);
			}
			double? num = Ram.ParseFrequencyMhz(text3);
			string text4;
			if (jobject == null)
			{
				text4 = null;
			}
			else
			{
				JToken jtoken3 = P4258EBF.AFA7138A.M6233B19[176](jobject, "RAM_XMP_Max_Frequency");
				text4 = ((jtoken3 != null) ? jtoken3.ToString() : null);
			}
			double? num2 = Ram.ParseFrequencyMhz(text4);
			double? maxSceneAverageValue = Ram.GetMaxSceneAverageValue(root, "RamUsage");
			bool flag2 = maxSceneAverageValue > 90.0;
			bool flag3 = maxSceneAverageValue > 99.0;
			bool flag4 = num2 != null && num != null && num.Value + num.Value * 0.01 < num2.Value;
			List<Ram.RamStatusCode> list = new List<Ram.RamStatusCode>(3);
			if (flag3)
			{
				list.Add(Ram.RamStatusCode.CriticalMemoryShortage);
			}
			else if (flag2)
			{
				list.Add(Ram.RamStatusCode.LowAvailableMemory);
			}
			if (flag4)
			{
				list.Add(Ram.RamStatusCode.LowMemorySpeed);
			}
			if (flag)
			{
				list.Add(Ram.RamStatusCode.SingleChannelMode);
			}
			if (list.Count == 0)
			{
				list.Add(Ram.RamStatusCode.Ok);
			}
			return (from code in list.Distinct<Ram.RamStatusCode>()
				select (int)code).ToArray<int>();
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000C50C File Offset: 0x0000A90C
		public static OptimizationStatus GetMonitoringStatus(JObject root)
		{
			int[] statusCodes = Ram.GetStatusCodes(root);
			if (statusCodes.Contains(1))
			{
				return OptimizationStatus.Good;
			}
			if (statusCodes.Contains(4) || statusCodes.Contains(5))
			{
				return OptimizationStatus.Bad;
			}
			if (statusCodes.Any<int>((int code) => code - 2 <= 1))
			{
				return OptimizationStatus.Middle;
			}
			return OptimizationStatus.Unsupported;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0015C82C File Offset: 0x0015A02C
		private static double? GetMaxSceneAverageValue(JObject root, string propertyName)
		{
			JToken jtoken = P4258EBF.AFA7138A.M6233B19[175](root, "BenchmarkInfo");
			if (jtoken == null)
			{
				return null;
			}
			return (from scene in jtoken.OfType<JObject>()
				select P4258EBF.AFA7138A.M6233B19[357](Ram.ReadNumberArray(P4258EBF.AFA7138A.M6233B19[175](scene, propertyName)).DefaultIfEmpty(double.NaN)) into avg
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

		// Token: 0x0600024D RID: 589 RVA: 0x00152B0C File Offset: 0x0015030C
		private static double? ParseFrequencyMhz(string value)
		{
			if (P4258EBF.AFA7138A.M6233B19[426](value))
			{
				return null;
			}
			string text = P4258EBF.AFA7138A.M6233B19[119](value, ',', '.');
			int num = -1;
			int num2 = 0;
			for (int i = 0; i < P4258EBF.AFA7138A.M6233B19[152](text); i++)
			{
				char c = P4258EBF.AFA7138A.M6233B19[366](text, i);
				if (P4258EBF.AFA7138A.M6233B19[452](c) || c == '.')
				{
					if (num < 0)
					{
						num = i;
					}
					num2++;
				}
				else if (num >= 0)
				{
					break;
				}
			}
			if (num < 0)
			{
				return null;
			}
			double num3;
			if (!P4258EBF.AFA7138A.M6233B19[190](P4258EBF.AFA7138A.M6233B19[487](text, num, num2), NumberStyles.Float, P4258EBF.AFA7138A.M6233B19[311](), ref num3))
			{
				return null;
			}
			return new double?(num3);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000C6C1 File Offset: 0x0000AAC1
		private static IEnumerable<double> ReadNumberArray(JToken token)
		{
			Ram.<ReadNumberArray>d__9 <ReadNumberArray>d__ = new Ram.<ReadNumberArray>d__9(-2);
			<ReadNumberArray>d__.<>3__token = token;
			return <ReadNumberArray>d__;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0014EDE4 File Offset: 0x0014C5E4
		public Ram()
		{
			P4258EBF.AFA7138A.M6233B19[130](this);
		}

		// Token: 0x020000BE RID: 190
		public enum RamStatusCode
		{
			// Token: 0x04000292 RID: 658
			Error,
			// Token: 0x04000293 RID: 659
			Ok,
			// Token: 0x04000294 RID: 660
			LowAvailableMemory,
			// Token: 0x04000295 RID: 661
			LowMemorySpeed,
			// Token: 0x04000296 RID: 662
			SingleChannelMode,
			// Token: 0x04000297 RID: 663
			CriticalMemoryShortage
		}
	}
}
