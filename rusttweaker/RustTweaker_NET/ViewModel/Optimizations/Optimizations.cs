using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RustTweaker;
using RustTweaker.Model;
using RustTweaker.Optimization;
using RustTweaker.Optimization.Monitoring;
using RustTweaker.Optimization.Optimizations;
using RustTweaker.Optimization.Optimizations.AutoCpuAffinity;
using RustTweaker_NET.Model.Http;
using RustTweaker_NET.Services;
using WpfApp1.Model;

namespace RustTweaker_NET.ViewModel.Optimizations
{
	// Token: 0x02000013 RID: 19
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDual)]
	public class Optimizations
	{
		// Token: 0x06000084 RID: 132 RVA: 0x00003AD8 File Offset: 0x00001ED8
		private static Task<HttpClient> CreateHttpClientAsync()
		{
			return Task.Run<HttpClient>(() => new SecureHttp().GetClient());
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00155CD4 File Offset: 0x001534D4
		public static JArray BuildOptimizationsInfo()
		{
			JArray jarray = P4258EBF.AFA7138A.M6233B19[586]();
			foreach (KeyValuePair<OptimizationId, Func<IOptimization>> keyValuePair in Optimizations.OptimizationsDictionary)
			{
				try
				{
					IOptimization optimization = keyValuePair.Value();
					OptimizationStatus status = optimization.GetStatus();
					string text = P4258EBF.AFA7138A.M6233B19[432](status.ToString());
					object obj = jarray;
					JObject jobject = P4258EBF.AFA7138A.M6233B19[345]();
					E4250989.FF0FA0A4(jobject, "id", P4258EBF.AFA7138A.M6233B19[402](Optimizations.ToKey(keyValuePair.Key)));
					E4250989.FF0FA0A4(jobject, "status", P4258EBF.AFA7138A.M6233B19[402](text));
					B0248586.P2129B2D(obj, jobject);
				}
				catch (Exception ex)
				{
					Logger.Log(ex);
				}
			}
			return jarray;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x001594F0 File Offset: 0x00156CF0
		public static void CaptureBenchmarkOptimizationsInfo()
		{
			object benchmarkOptimizationsInfoLock = Optimizations.BenchmarkOptimizationsInfoLock;
			bool flag = false;
			try
			{
				P4258EBF.AFA7138A.M6233B19[520](benchmarkOptimizationsInfoLock, ref flag);
				Optimizations.BenchmarkOptimizationsInfo = Optimizations.BuildOptimizationsInfo();
				Optimizations.BenchmarkOptimizationsInfoCaptured = true;
			}
			finally
			{
				if (flag)
				{
					P4258EBF.AFA7138A.M6233B19[631](benchmarkOptimizationsInfoLock);
				}
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0013D7F4 File Offset: 0x0013AFF4
		public static string AddOptimizationsInfoToBenchmarkResult(string resultJson, bool captureCurrentIfMissing = false, bool consumeCaptured = false)
		{
			string text;
			try
			{
				JObject jobject = P4258EBF.AFA7138A.M6233B19[621](resultJson);
				if (Optimizations.TryNormalizeExistingOptimizationsInfo(jobject))
				{
					Optimizations.EnsureMonitoringOptimizationsInfo(jobject);
					Optimizations.LogOptimizationsInfoState(jobject, "AddOptimizationsInfoToBenchmarkResult(normalized-existing)");
					if (consumeCaptured)
					{
						Optimizations.ClearBenchmarkOptimizationsInfo();
					}
					text = P4258EBF.AFA7138A.M6233B19[612](jobject, Formatting.None);
				}
				else
				{
					JArray jarray = null;
					object benchmarkOptimizationsInfoLock = Optimizations.BenchmarkOptimizationsInfoLock;
					bool flag = false;
					try
					{
						P4258EBF.AFA7138A.M6233B19[520](benchmarkOptimizationsInfoLock, ref flag);
						if (Optimizations.BenchmarkOptimizationsInfoCaptured)
						{
							jarray = (JArray)P4258EBF.AFA7138A.M6233B19[193](Optimizations.BenchmarkOptimizationsInfo);
						}
						if (consumeCaptured)
						{
							Optimizations.BenchmarkOptimizationsInfo = P4258EBF.AFA7138A.M6233B19[586]();
							Optimizations.BenchmarkOptimizationsInfoCaptured = false;
						}
					}
					finally
					{
						if (flag)
						{
							P4258EBF.AFA7138A.M6233B19[631](benchmarkOptimizationsInfoLock);
						}
					}
					if (jarray == null && captureCurrentIfMissing)
					{
						jarray = Optimizations.BuildOptimizationsInfo();
					}
					if (jarray == null)
					{
						jarray = P4258EBF.AFA7138A.M6233B19[586]();
					}
					P4258EBF.AFA7138A.M6233B19[542](jobject, "OptimizationsInfo", jarray);
					Optimizations.EnsureMonitoringOptimizationsInfo(jobject);
					P4258EBF.AFA7138A.M6233B19[187](jobject, "optimizationsInfo");
					Optimizations.LogOptimizationsInfoState(jobject, "AddOptimizationsInfoToBenchmarkResult(appended)");
					text = P4258EBF.AFA7138A.M6233B19[612](jobject, Formatting.None);
				}
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
				text = resultJson;
			}
			return text;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0015EDF4 File Offset: 0x0015C5F4
		private static bool TryNormalizeExistingOptimizationsInfo(JObject root)
		{
			JArray jarray = P4258EBF.AFA7138A.M6233B19[175](root, "OptimizationsInfo") as JArray;
			if (jarray != null && P4258EBF.AFA7138A.M6233B19[234](jarray) > 0)
			{
				return true;
			}
			JArray jarray2 = P4258EBF.AFA7138A.M6233B19[175](root, "optimizationsInfo") as JArray;
			if (jarray2 != null && P4258EBF.AFA7138A.M6233B19[234](jarray2) > 0)
			{
				P4258EBF.AFA7138A.M6233B19[542](root, "OptimizationsInfo", (JArray)P4258EBF.AFA7138A.M6233B19[193](jarray2));
				P4258EBF.AFA7138A.M6233B19[187](root, "optimizationsInfo");
				return true;
			}
			return false;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0014EF54 File Offset: 0x0014C754
		private static void EnsureMonitoringOptimizationsInfo(JObject root)
		{
			JArray jarray = P4258EBF.AFA7138A.M6233B19[175](root, "OptimizationsInfo") as JArray;
			if (jarray == null)
			{
				jarray = P4258EBF.AFA7138A.M6233B19[586]();
				P4258EBF.AFA7138A.M6233B19[542](root, "OptimizationsInfo", jarray);
			}
			foreach (JObject jobject in jarray.OfType<JObject>().ToArray<JObject>())
			{
				JToken jtoken = P4258EBF.AFA7138A.M6233B19[175](jobject, "id");
				string text = ((jtoken != null) ? jtoken.ToString() : null);
				if (Optimizations.MonitoringOptimizationIds.Contains(text))
				{
					P4258EBF.AFA7138A.M6233B19[525](jobject);
				}
			}
			using (IEnumerator<JToken> enumerator = P4258EBF.AFA7138A.M6233B19[523](Optimizations.BuildMonitoringOptimizationsInfo(root)))
			{
				while (P4258EBF.AFA7138A.M6233B19[411](enumerator))
				{
					JToken jtoken2 = enumerator.Current;
					P4258EBF.AFA7138A.M6233B19[401](jarray, jtoken2);
				}
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0015B288 File Offset: 0x00158A88
		private static JArray BuildMonitoringOptimizationsInfo(JObject root)
		{
			JArray jarray = P4258EBF.AFA7138A.M6233B19[586]();
			B0248586.P2129B2D(jarray, Optimizations.CreateOptimizationInfo("temperature-monitoring", Optimizations.GetTemperatureMonitoringStatus(root)));
			B0248586.P2129B2D(jarray, Optimizations.CreateOptimizationInfo("ram-monitoring", Optimizations.GetRamMonitoringStatus(root)));
			B0248586.P2129B2D(jarray, Optimizations.CreateOptimizationInfo("disk-monitoring", Optimizations.GetDiskMonitoringStatus()));
			return jarray;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00141AF8 File Offset: 0x0013F2F8
		private static JObject CreateOptimizationInfo(string id, OptimizationStatus status)
		{
			JObject jobject = P4258EBF.AFA7138A.M6233B19[345]();
			E4250989.FF0FA0A4(jobject, "id", P4258EBF.AFA7138A.M6233B19[402](id));
			E4250989.FF0FA0A4(jobject, "status", P4258EBF.AFA7138A.M6233B19[402](P4258EBF.AFA7138A.M6233B19[432](status.ToString())));
			return jobject;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003EE5 File Offset: 0x000022E5
		private static OptimizationStatus GetTemperatureMonitoringStatus(JObject root)
		{
			return Temperature.GetMonitoringStatus(root);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003EED File Offset: 0x000022ED
		private static OptimizationStatus GetRamMonitoringStatus(JObject root)
		{
			return Ram.GetMonitoringStatus(root);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003EF8 File Offset: 0x000022F8
		private static OptimizationStatus GetDiskMonitoringStatus()
		{
			OptimizationStatus optimizationStatus;
			try
			{
				Disk.DiskStatusCode[] result = Disk.GetStatusCodes().GetAwaiter().GetResult();
				if (result == null || result.Length == 0)
				{
					optimizationStatus = OptimizationStatus.Unsupported;
				}
				else if (result.Contains(Disk.DiskStatusCode.GameClientNotFound, null))
				{
					optimizationStatus = OptimizationStatus.Bad;
				}
				else if (result.Contains(Disk.DiskStatusCode.Ok, null))
				{
					optimizationStatus = OptimizationStatus.Good;
				}
				else
				{
					optimizationStatus = OptimizationStatus.Middle;
				}
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
				optimizationStatus = OptimizationStatus.Unsupported;
			}
			return optimizationStatus;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0015B9D0 File Offset: 0x001591D0
		private static void ClearBenchmarkOptimizationsInfo()
		{
			object benchmarkOptimizationsInfoLock = Optimizations.BenchmarkOptimizationsInfoLock;
			bool flag = false;
			try
			{
				P4258EBF.AFA7138A.M6233B19[520](benchmarkOptimizationsInfoLock, ref flag);
				Optimizations.BenchmarkOptimizationsInfo = P4258EBF.AFA7138A.M6233B19[586]();
				Optimizations.BenchmarkOptimizationsInfoCaptured = false;
			}
			finally
			{
				if (flag)
				{
					P4258EBF.AFA7138A.M6233B19[631](benchmarkOptimizationsInfoLock);
				}
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x001561D0 File Offset: 0x001539D0
		private static void LogOptimizationsInfoState(JObject root, string context)
		{
			JToken jtoken = P4258EBF.AFA7138A.M6233B19[175](root, "OptimizationsInfo");
			string text;
			if (jtoken != null)
			{
				JArray jarray = jtoken as JArray;
				if (jarray == null)
				{
					JValue jvalue = jtoken as JValue;
					if (jvalue != null)
					{
						JTokenType jtokenType = P4258EBF.AFA7138A.M6233B19[630](jvalue);
						if (jtokenType == JTokenType.Null)
						{
							text = "null";
							goto IL_0107;
						}
					}
					text = P4258EBF.AFA7138A.M6233B19[630](jtoken).ToString();
				}
				else
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
					P4258EBF.AFA7138A.M6233B19[466](ref defaultInterpolatedStringHandler, 13, 1);
					P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "array(count=");
					defaultInterpolatedStringHandler.AppendFormatted<int>(P4258EBF.AFA7138A.M6233B19[234](jarray));
					P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, ")");
					text = P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler);
				}
			}
			else
			{
				text = "missing";
			}
			IL_0107:
			string text2 = text;
			Logger.Log(P4258EBF.AFA7138A.M6233B19[64](context, ": OptimizationsInfo=", text2));
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004070 File Offset: 0x00002470
		public async Task<string> GetOptimizationStatus(string id)
		{
			try
			{
				OptimizationId optimizationId = Optimizations.ToOptimizationId(id);
				OptimizationInfoDto optimizationInfoDto;
				if (Optimizations.TryGetCachedStatus(optimizationId, out optimizationInfoDto))
				{
					return P4258EBF.AFA7138A.M6233B19[330](optimizationInfoDto);
				}
				OptimizationInfoDto optimizationInfoDto2 = await Task.Run<OptimizationInfoDto>(delegate
				{
					IOptimization optimization = Optimizations.OptimizationsDictionary[optimizationId]();
					OptimizationStatus status = optimization.GetStatus();
					return new OptimizationInfoDto
					{
						CurrentStatus = status.ToString(),
						IsSupported = (status != OptimizationStatus.Unsupported),
						NeedComputerRestart = optimization.NeedComputerRestart,
						NeedSteamRestart = optimization.NeedSteamRestart
					};
				}).ConfigureAwait(false);
				OptimizationInfoDto optimizationInfoDto3 = optimizationInfoDto2;
				Optimizations.SetCachedStatus(optimizationId, optimizationInfoDto3);
				return P4258EBF.AFA7138A.M6233B19[330](optimizationInfoDto3);
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
			}
			return null;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0015E03C File Offset: 0x0015B83C
		public Task SetOptimizationStatus(string id, OptimizationTargetStatus targetStatus)
		{
			Optimizations.<SetOptimizationStatus>d__31 <SetOptimizationStatus>d__;
			<SetOptimizationStatus>d__.<>t__builder = P4258EBF.AFA7138A.M6233B19[20]();
			<SetOptimizationStatus>d__.id = id;
			<SetOptimizationStatus>d__.targetStatus = targetStatus;
			<SetOptimizationStatus>d__.<>1__state = -1;
			<SetOptimizationStatus>d__.<>t__builder.Start<Optimizations.<SetOptimizationStatus>d__31>(ref <SetOptimizationStatus>d__);
			return P4258EBF.AFA7138A.M6233B19[19](ref <SetOptimizationStatus>d__.<>t__builder);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0014ED10 File Offset: 0x0014C510
		private static bool TryGetCachedStatus(OptimizationId id, out OptimizationInfoDto info)
		{
			object statusCacheLock = Optimizations.StatusCacheLock;
			bool flag = false;
			try
			{
				P4258EBF.AFA7138A.M6233B19[520](statusCacheLock, ref flag);
				Optimizations.CachedOptimizationInfo cachedOptimizationInfo;
				if (Optimizations.StatusCache.TryGetValue(id, out cachedOptimizationInfo) && P4258EBF.AFA7138A.M6233B19[135](P4258EBF.AFA7138A.M6233B19[188](P4258EBF.AFA7138A.M6233B19[241](), cachedOptimizationInfo.CreatedAtUtc), Optimizations.StatusCacheTtl))
				{
					info = cachedOptimizationInfo.Info;
					return true;
				}
				Optimizations.StatusCache.Remove(id);
			}
			finally
			{
				if (flag)
				{
					P4258EBF.AFA7138A.M6233B19[631](statusCacheLock);
				}
			}
			info = null;
			return false;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00158C60 File Offset: 0x00156460
		private static void SetCachedStatus(OptimizationId id, OptimizationInfoDto info)
		{
			object statusCacheLock = Optimizations.StatusCacheLock;
			bool flag = false;
			try
			{
				P4258EBF.AFA7138A.M6233B19[520](statusCacheLock, ref flag);
				Optimizations.StatusCache[id] = new Optimizations.CachedOptimizationInfo
				{
					Info = info,
					CreatedAtUtc = P4258EBF.AFA7138A.M6233B19[241]()
				};
			}
			finally
			{
				if (flag)
				{
					P4258EBF.AFA7138A.M6233B19[631](statusCacheLock);
				}
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0014DA10 File Offset: 0x0014B210
		private static void RemoveCachedStatus(OptimizationId id)
		{
			object statusCacheLock = Optimizations.StatusCacheLock;
			bool flag = false;
			try
			{
				P4258EBF.AFA7138A.M6233B19[520](statusCacheLock, ref flag);
				Optimizations.StatusCache.Remove(id);
			}
			finally
			{
				if (flag)
				{
					P4258EBF.AFA7138A.M6233B19[631](statusCacheLock);
				}
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004228 File Offset: 0x00002628
		public async Task<int> initOptimizationPage()
		{
			try
			{
				string resultsPath = P4258EBF.AFA7138A.M6233B19[278](P4258EBF.AFA7138A.M6233B19[54](Environment.SpecialFolder.ApplicationData), "RustTweakerBenchmark", "Results");
				if (!P4258EBF.AFA7138A.M6233B19[89](resultsPath))
				{
					return 200;
				}
				HttpClient httpClient = await Optimizations.CreateHttpClientAsync().ConfigureAwait(false);
				HttpClient client = httpClient;
				foreach (string jsonPath in P4258EBF.AFA7138A.M6233B19[526](resultsPath, "*.json"))
				{
					try
					{
						string text = Optimizations.AddOptimizationsInfoToBenchmarkResult(await P4258EBF.AFA7138A.M6233B19[102](jsonPath, default(CancellationToken)).ConfigureAwait(false), false, false);
						Logger.Log(P4258EBF.AFA7138A.M6233B19[259]("Benchmark payload before POST (file=", P4258EBF.AFA7138A.M6233B19[513](jsonPath), "): ", text));
						using (StringContent content = P4258EBF.AFA7138A.M6233B19[321](text, P4258EBF.AFA7138A.M6233B19[204](), "application/json"))
						{
							using (HttpResponseMessage response = await P4258EBF.AFA7138A.M6233B19[583](client, "desktop/benchmark", content).ConfigureAwait(false))
							{
								if (P4258EBF.AFA7138A.M6233B19[23](response) != HttpStatusCode.OK)
								{
									Logger.Log(await P4258EBF.AFA7138A.M6233B19[623](P4258EBF.AFA7138A.M6233B19[405](response)).ConfigureAwait(false));
									return -1;
								}
								P4258EBF.AFA7138A.M6233B19[289](jsonPath);
							}
						}
						StringContent content = null;
						HttpResponseMessage response = null;
					}
					catch (Exception ex)
					{
						Logger.Log(ex);
						return -1;
					}
					jsonPath = null;
				}
				string[] array = null;
				resultsPath = null;
				client = null;
			}
			catch (Exception ex2)
			{
				Logger.Log(ex2);
				return -1;
			}
			return 200;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004264 File Offset: 0x00002664
		public async Task<string> getMeHistory(int limit, int offset)
		{
			HttpClient httpClient = await Optimizations.CreateHttpClientAsync().ConfigureAwait(false);
			HttpClient httpClient2 = httpClient;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = P4258EBF.AFA7138A.M6233B19[467](35, 2);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "desktop/benchmark/me?limit=");
			defaultInterpolatedStringHandler.AppendFormatted<int>(limit);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "&offset=");
			defaultInterpolatedStringHandler.AppendFormatted<int>(offset);
			string url = P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler);
			string text;
			string text2;
			if (ResponseCache.TryRead(url, Optimizations.BenchmarkMeHistoryCacheTtl, out text))
			{
				text2 = text;
			}
			else
			{
				using (HttpResponseMessage response = await P4258EBF.AFA7138A.M6233B19[349](httpClient2, url).ConfigureAwait(false))
				{
					if (P4258EBF.AFA7138A.M6233B19[23](response) == HttpStatusCode.OK)
					{
						string msg = await P4258EBF.AFA7138A.M6233B19[623](P4258EBF.AFA7138A.M6233B19[405](response)).ConfigureAwait(false);
						ConfiguredTaskAwaitable configuredTaskAwaitable = P4258EBF.AFA7138A.M6233B19[196](ResponseCache.WriteAsync(url, msg), false);
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter = P4258EBF.AFA7138A.M6233B19[628](ref configuredTaskAwaitable);
						if (!configuredTaskAwaiter.IsCompleted)
						{
							await configuredTaskAwaiter;
							ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
							configuredTaskAwaiter = configuredTaskAwaiter2;
							configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						}
						configuredTaskAwaiter.GetResult();
						text2 = msg;
					}
					else
					{
						text2 = null;
					}
				}
			}
			return text2;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000042B0 File Offset: 0x000026B0
		public async Task<string> getTopFiltered(string cpu, string gpu, string ramType, string ramVolume, int limit, int offset)
		{
			HttpClient httpClient = await Optimizations.CreateHttpClientAsync().ConfigureAwait(false);
			HttpClient httpClient2 = httpClient;
			string text = (P4258EBF.AFA7138A.M6233B19[426](cpu) ? "[]" : cpu);
			string text2 = (P4258EBF.AFA7138A.M6233B19[426](gpu) ? "[]" : gpu);
			string text3 = (P4258EBF.AFA7138A.M6233B19[426](ramType) ? "[]" : ramType);
			string text4 = (P4258EBF.AFA7138A.M6233B19[426](ramVolume) ? "[]" : ramVolume);
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = P4258EBF.AFA7138A.M6233B19[467](76, 6);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "desktop/benchmark/top?sortBy=pc&limit=");
			defaultInterpolatedStringHandler.AppendFormatted<int>(limit);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "&offset=");
			defaultInterpolatedStringHandler.AppendFormatted<int>(offset);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "&cpu=");
			P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler, P4258EBF.AFA7138A.M6233B19[154](text));
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "&gpu=");
			P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler, P4258EBF.AFA7138A.M6233B19[154](text2));
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "&ramType=");
			P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler, P4258EBF.AFA7138A.M6233B19[154](text3));
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "&ramVolume=");
			P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler, P4258EBF.AFA7138A.M6233B19[154](text4));
			string url = P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler);
			string text5;
			string text6;
			if (ResponseCache.TryRead(url, Optimizations.BenchmarkTopCacheTtl, out text5))
			{
				text6 = text5;
			}
			else
			{
				using (HttpResponseMessage response = await P4258EBF.AFA7138A.M6233B19[349](httpClient2, url).ConfigureAwait(false))
				{
					if (P4258EBF.AFA7138A.M6233B19[23](response) == HttpStatusCode.OK)
					{
						string msg = await P4258EBF.AFA7138A.M6233B19[623](P4258EBF.AFA7138A.M6233B19[405](response)).ConfigureAwait(false);
						ConfiguredTaskAwaitable configuredTaskAwaitable = P4258EBF.AFA7138A.M6233B19[196](ResponseCache.WriteAsync(url, msg), false);
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter = P4258EBF.AFA7138A.M6233B19[628](ref configuredTaskAwaitable);
						if (!configuredTaskAwaiter.IsCompleted)
						{
							await configuredTaskAwaiter;
							ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
							configuredTaskAwaiter = configuredTaskAwaiter2;
							configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						}
						configuredTaskAwaiter.GetResult();
						text6 = msg;
					}
					else
					{
						text6 = null;
					}
				}
			}
			return text6;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004320 File Offset: 0x00002720
		public async Task<string> getBenchmarkDevices()
		{
			HttpClient httpClient = await Optimizations.CreateHttpClientAsync().ConfigureAwait(false);
			HttpClient httpClient2 = httpClient;
			string text;
			string text2;
			if (ResponseCache.TryRead("desktop/benchmark/devices", Optimizations.BenchmarkDevicesCacheTtl, out text))
			{
				text2 = text;
			}
			else
			{
				using (HttpResponseMessage response = await P4258EBF.AFA7138A.M6233B19[349](httpClient2, "desktop/benchmark/devices").ConfigureAwait(false))
				{
					if (P4258EBF.AFA7138A.M6233B19[23](response) == HttpStatusCode.OK)
					{
						string msg = await P4258EBF.AFA7138A.M6233B19[623](P4258EBF.AFA7138A.M6233B19[405](response)).ConfigureAwait(false);
						ConfiguredTaskAwaitable configuredTaskAwaitable = P4258EBF.AFA7138A.M6233B19[196](ResponseCache.WriteAsync("desktop/benchmark/devices", msg), false);
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter = P4258EBF.AFA7138A.M6233B19[628](ref configuredTaskAwaitable);
						if (!configuredTaskAwaiter.IsCompleted)
						{
							await configuredTaskAwaiter;
							ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
							configuredTaskAwaiter = configuredTaskAwaiter2;
							configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						}
						configuredTaskAwaiter.GetResult();
						text2 = msg;
					}
					else
					{
						text2 = null;
					}
				}
			}
			return text2;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000435C File Offset: 0x0000275C
		public async Task<string> getMeBenchmarkInfo(int submissionId)
		{
			HttpClient httpClient = await Optimizations.CreateHttpClientAsync().ConfigureAwait(false);
			HttpClient httpClient2 = httpClient;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = P4258EBF.AFA7138A.M6233B19[467](34, 1);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "desktop/benchmark/me?submissionId=");
			defaultInterpolatedStringHandler.AppendFormatted<int>(submissionId);
			string url = P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler);
			string text;
			string text2;
			if (ResponseCache.TryRead(url, Optimizations.BenchmarkMeInfoCacheTtl, out text))
			{
				text2 = text;
			}
			else
			{
				using (HttpResponseMessage response = await P4258EBF.AFA7138A.M6233B19[349](httpClient2, url).ConfigureAwait(false))
				{
					if (P4258EBF.AFA7138A.M6233B19[23](response) == HttpStatusCode.OK)
					{
						string msg = await P4258EBF.AFA7138A.M6233B19[623](P4258EBF.AFA7138A.M6233B19[405](response)).ConfigureAwait(false);
						ConfiguredTaskAwaitable configuredTaskAwaitable = P4258EBF.AFA7138A.M6233B19[196](ResponseCache.WriteAsync(url, msg), false);
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter = P4258EBF.AFA7138A.M6233B19[628](ref configuredTaskAwaitable);
						if (!configuredTaskAwaiter.IsCompleted)
						{
							await configuredTaskAwaiter;
							ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
							configuredTaskAwaiter = configuredTaskAwaiter2;
							configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						}
						configuredTaskAwaiter.GetResult();
						text2 = msg;
					}
					else
					{
						text2 = null;
					}
				}
			}
			return text2;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000043A0 File Offset: 0x000027A0
		public async Task<string> getUserLastBenchmark(string steamId, bool? frameGen = null)
		{
			HttpClient httpClient = await Optimizations.CreateHttpClientAsync().ConfigureAwait(false);
			HttpClient httpClient2 = httpClient;
			string url = P4258EBF.AFA7138A.M6233B19[478]("desktop/benchmark/user?steamid=", P4258EBF.AFA7138A.M6233B19[154](steamId));
			if (frameGen != null)
			{
				A3B2DE09 a3B2DE = P4258EBF.AFA7138A.M6233B19[64];
				string text = url;
				string text2 = "&frameGen=";
				bool value = frameGen.Value;
				url = a3B2DE(text, text2, P4258EBF.AFA7138A.M6233B19[432](P4258EBF.AFA7138A.M6233B19[632](ref value)));
			}
			string text3;
			string text4;
			if (ResponseCache.TryRead(url, Optimizations.BenchmarkUserLastCacheTtl, out text3))
			{
				text4 = text3;
			}
			else
			{
				using (HttpResponseMessage response = await P4258EBF.AFA7138A.M6233B19[349](httpClient2, url).ConfigureAwait(false))
				{
					if (P4258EBF.AFA7138A.M6233B19[23](response) == HttpStatusCode.OK)
					{
						string msg = await P4258EBF.AFA7138A.M6233B19[623](P4258EBF.AFA7138A.M6233B19[405](response)).ConfigureAwait(false);
						ConfiguredTaskAwaitable configuredTaskAwaitable = P4258EBF.AFA7138A.M6233B19[196](ResponseCache.WriteAsync(url, msg), false);
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter = P4258EBF.AFA7138A.M6233B19[628](ref configuredTaskAwaitable);
						if (!configuredTaskAwaiter.IsCompleted)
						{
							await configuredTaskAwaiter;
							ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
							configuredTaskAwaiter = configuredTaskAwaiter2;
							configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						}
						configuredTaskAwaiter.GetResult();
						text4 = msg;
					}
					else
					{
						text4 = null;
					}
				}
			}
			return text4;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000043EC File Offset: 0x000027EC
		public async Task<string> getHistogram()
		{
			HttpClient httpClient = await Optimizations.CreateHttpClientAsync().ConfigureAwait(false);
			HttpClient httpClient2 = httpClient;
			string url = "desktop/benchmark/histogram";
			string text;
			string text2;
			if (ResponseCache.TryRead(url, Optimizations.BenchmarkHistogram, out text))
			{
				text2 = text;
			}
			else
			{
				using (HttpResponseMessage response = await P4258EBF.AFA7138A.M6233B19[349](httpClient2, url).ConfigureAwait(false))
				{
					if (P4258EBF.AFA7138A.M6233B19[23](response) == HttpStatusCode.OK)
					{
						string msg = await P4258EBF.AFA7138A.M6233B19[623](P4258EBF.AFA7138A.M6233B19[405](response)).ConfigureAwait(false);
						ConfiguredTaskAwaitable configuredTaskAwaitable = P4258EBF.AFA7138A.M6233B19[196](ResponseCache.WriteAsync(url, msg), false);
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter = P4258EBF.AFA7138A.M6233B19[628](ref configuredTaskAwaitable);
						if (!configuredTaskAwaiter.IsCompleted)
						{
							await configuredTaskAwaiter;
							ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
							configuredTaskAwaiter = configuredTaskAwaiter2;
							configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						}
						configuredTaskAwaiter.GetResult();
						text2 = msg;
					}
					else
					{
						text2 = null;
					}
				}
			}
			return text2;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004428 File Offset: 0x00002828
		public async Task<string> getUserBenchmarkBySubmission(string steamId, int submissionId)
		{
			HttpClient httpClient = await Optimizations.CreateHttpClientAsync().ConfigureAwait(false);
			HttpClient httpClient2 = httpClient;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = P4258EBF.AFA7138A.M6233B19[467](45, 2);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "desktop/benchmark/user?steamid=");
			P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler, steamId);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "&submissionId=");
			defaultInterpolatedStringHandler.AppendFormatted<int>(submissionId);
			string url = P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler);
			string text;
			string text2;
			if (ResponseCache.TryRead(url, Optimizations.BenchmarkUserCacheTtl, out text))
			{
				text2 = text;
			}
			else
			{
				using (HttpResponseMessage response = await P4258EBF.AFA7138A.M6233B19[349](httpClient2, url).ConfigureAwait(false))
				{
					if (P4258EBF.AFA7138A.M6233B19[23](response) == HttpStatusCode.OK)
					{
						string msg = await P4258EBF.AFA7138A.M6233B19[623](P4258EBF.AFA7138A.M6233B19[405](response)).ConfigureAwait(false);
						ConfiguredTaskAwaitable configuredTaskAwaitable = P4258EBF.AFA7138A.M6233B19[196](ResponseCache.WriteAsync(url, msg), false);
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter = P4258EBF.AFA7138A.M6233B19[628](ref configuredTaskAwaitable);
						if (!configuredTaskAwaiter.IsCompleted)
						{
							await configuredTaskAwaiter;
							ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
							configuredTaskAwaiter = configuredTaskAwaiter2;
							configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						}
						configuredTaskAwaiter.GetResult();
						text2 = msg;
					}
					else
					{
						text2 = null;
					}
				}
			}
			return text2;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004474 File Offset: 0x00002874
		public async Task<string> getUserBenchmarkHistory(string steamId, int limit, int offset)
		{
			HttpClient httpClient = await Optimizations.CreateHttpClientAsync().ConfigureAwait(false);
			HttpClient httpClient2 = httpClient;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = P4258EBF.AFA7138A.M6233B19[467](54, 3);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "desktop/benchmark/user/history?steamid=");
			P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler, steamId);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "&limit=");
			defaultInterpolatedStringHandler.AppendFormatted<int>(limit);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "&offset=");
			defaultInterpolatedStringHandler.AppendFormatted<int>(offset);
			string url = P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler);
			string text;
			string text2;
			if (ResponseCache.TryRead(url, Optimizations.BenchmarkUserHistoryCacheTtl, out text))
			{
				text2 = text;
			}
			else
			{
				using (HttpResponseMessage response = await P4258EBF.AFA7138A.M6233B19[349](httpClient2, url).ConfigureAwait(false))
				{
					if (P4258EBF.AFA7138A.M6233B19[23](response) == HttpStatusCode.OK)
					{
						string msg = await P4258EBF.AFA7138A.M6233B19[623](P4258EBF.AFA7138A.M6233B19[405](response)).ConfigureAwait(false);
						ConfiguredTaskAwaitable configuredTaskAwaitable = P4258EBF.AFA7138A.M6233B19[196](ResponseCache.WriteAsync(url, msg), false);
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter = P4258EBF.AFA7138A.M6233B19[628](ref configuredTaskAwaitable);
						if (!configuredTaskAwaiter.IsCompleted)
						{
							await configuredTaskAwaiter;
							ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
							configuredTaskAwaiter = configuredTaskAwaiter2;
							configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						}
						configuredTaskAwaiter.GetResult();
						text2 = msg;
					}
					else
					{
						text2 = null;
					}
				}
			}
			return text2;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000044C8 File Offset: 0x000028C8
		public async Task<int> startbenchmark(string id)
		{
			try
			{
				Func<string> func;
				if ((func = Optimizations.<>O.<0>__getCurrentSelectedFolder) == null)
				{
					func = (Optimizations.<>O.<0>__getCurrentSelectedFolder = new Func<string>(Configs.getCurrentSelectedFolder));
				}
				string text = await Task.Run<string>(func).ConfigureAwait(false);
				string selectedFolder = text;
				if (P4258EBF.AFA7138A.M6233B19[426](selectedFolder))
				{
					Logger.Log("ERROR: startbenchmark: selected Rust folder not found.");
					return -1;
				}
				Action action;
				if ((action = Optimizations.<>O.<1>__CaptureBenchmarkOptimizationsInfo) == null)
				{
					action = (Optimizations.<>O.<1>__CaptureBenchmarkOptimizationsInfo = P4258EBF.AFA7138A.M6233B19[579](null, ldftn(CaptureBenchmarkOptimizationsInfo)));
				}
				ConfiguredTaskAwaitable configuredTaskAwaitable = KC1E07B9.I4893080(IA8CEE13.DC9E3F16(action), false);
				ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter = P4258EBF.AFA7138A.M6233B19[628](ref configuredTaskAwaitable);
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
				}
				configuredTaskAwaiter.GetResult();
				string language = Optimizations.GetCurrentBenchmarkLanguage();
				string benchmarkExe = P4258EBF.AFA7138A.M6233B19[278](P4258EBF.AFA7138A.M6233B19[587](), "bench", "bench.exe");
				string benchmarkWorkingDirectory = P4258EBF.AFA7138A.M6233B19[158](P4258EBF.AFA7138A.M6233B19[587](), "bench");
				if (!P4258EBF.AFA7138A.M6233B19[627](benchmarkExe))
				{
					benchmarkExe = P4258EBF.AFA7138A.M6233B19[278](P4258EBF.AFA7138A.M6233B19[587](), "ShowFPS", "bench.exe");
					benchmarkWorkingDirectory = P4258EBF.AFA7138A.M6233B19[158](P4258EBF.AFA7138A.M6233B19[587](), "ShowFPS");
				}
				if (!P4258EBF.AFA7138A.M6233B19[627](benchmarkExe))
				{
					benchmarkExe = P4258EBF.AFA7138A.M6233B19[158](P4258EBF.AFA7138A.M6233B19[587](), "bench.exe");
					benchmarkWorkingDirectory = P4258EBF.AFA7138A.M6233B19[587]();
				}
				await Task.Run<Process>(delegate
				{
					ProcessStartInfo processStartInfo = P4258EBF.AFA7138A.M6233B19[394]();
					N62EB38A.CB1145A6(processStartInfo, benchmarkExe);
					O8258311.M5A8918D(processStartInfo, false);
					E33C722B.D28D192D(processStartInfo, benchmarkWorkingDirectory);
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
					P4258EBF.AFA7138A.M6233B19[466](ref defaultInterpolatedStringHandler, 35, 3);
					P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "-benchmark=\"");
					P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler, id);
					P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "\" -gamefolder=\"");
					P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler, selectedFolder);
					P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "\" -lang=");
					P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler, language);
					AA2B3D09.ND86FA10(processStartInfo, P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler));
					return JC11021F.C827CF8C(processStartInfo);
				}).ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
				return -1;
			}
			return 0;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00155714 File Offset: 0x00152F14
		private static string GetCurrentBenchmarkLanguage()
		{
			string text2;
			try
			{
				string text = P4258EBF.AFA7138A.M6233B19[158](MainLogic.appDataPath, "lang.json");
				if (!P4258EBF.AFA7138A.M6233B19[627](text))
				{
					P4258EBF.AFA7138A.M6233B19[94](text, "{\"lang\":\"ru\"}");
					text2 = "ru";
				}
				else
				{
					string text3 = P4258EBF.AFA7138A.M6233B19[267](text);
					JToken jtoken = P4258EBF.AFA7138A.M6233B19[175](P4258EBF.AFA7138A.M6233B19[621](text3), "lang");
					string text4;
					if (jtoken == null)
					{
						text4 = null;
					}
					else
					{
						string text5 = jtoken.ToString();
						text4 = ((text5 != null) ? C3020339.PE003C13(H887B297.F936B29D(text5)) : null);
					}
					string text6 = text4;
					text2 = (P4258EBF.AFA7138A.M6233B19[250](text6, "en") ? "en" : "ru");
				}
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
				text2 = "ru";
			}
			return text2;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000045B8 File Offset: 0x000029B8
		public async Task<string> getDiskStatus()
		{
			return await new Disk().GetStatusJson().ConfigureAwait(false);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000045F4 File Offset: 0x000029F4
		public async Task<string> getRamStatus()
		{
			return await new Ram().GetStatusJson().ConfigureAwait(false);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00004630 File Offset: 0x00002A30
		public async Task<string> getTemperatureStatus()
		{
			return await new Temperature().GetStatusJson().ConfigureAwait(false);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x0000466C File Offset: 0x00002A6C
		public async Task<string> getMyDashboardInfo(bool frameGen = false)
		{
			HttpClient httpClient = await Optimizations.CreateHttpClientAsync().ConfigureAwait(false);
			HttpClient httpClient2 = httpClient;
			string url = P4258EBF.AFA7138A.M6233B19[64]("desktop/benchmark/me?limit=1&benchmarkType=HardwareTest&frameGen=", P4258EBF.AFA7138A.M6233B19[432](P4258EBF.AFA7138A.M6233B19[632](ref frameGen)), "&isStaging=false&hiddenFromLeaderboard=false");
			string text;
			string text2;
			if (ResponseCache.TryRead(url, Optimizations.BenchmarkDashboardInfoCacheTtl, out text))
			{
				text2 = text;
			}
			else
			{
				using (HttpResponseMessage response = await P4258EBF.AFA7138A.M6233B19[349](httpClient2, url).ConfigureAwait(false))
				{
					if (P4258EBF.AFA7138A.M6233B19[23](response) == HttpStatusCode.OK)
					{
						string msg = await P4258EBF.AFA7138A.M6233B19[623](P4258EBF.AFA7138A.M6233B19[405](response)).ConfigureAwait(false);
						ConfiguredTaskAwaitable configuredTaskAwaitable = P4258EBF.AFA7138A.M6233B19[196](ResponseCache.WriteAsync(url, msg), false);
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter = P4258EBF.AFA7138A.M6233B19[628](ref configuredTaskAwaitable);
						if (!configuredTaskAwaiter.IsCompleted)
						{
							await configuredTaskAwaiter;
							ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
							configuredTaskAwaiter = configuredTaskAwaiter2;
							configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						}
						configuredTaskAwaiter.GetResult();
						text2 = msg;
					}
					else
					{
						text2 = null;
					}
				}
			}
			return text2;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000046B0 File Offset: 0x00002AB0
		public async Task<string> getMeHasHardwareTest()
		{
			HttpClient httpClient = await Optimizations.CreateHttpClientAsync().ConfigureAwait(false);
			HttpClient httpClient2 = httpClient;
			string text;
			string text2;
			if (ResponseCache.TryRead("desktop/benchmark/me/has-hardware-test", Optimizations.BenchmarkHasHardwareTestCacheTtl, out text))
			{
				text2 = text;
			}
			else
			{
				using (HttpResponseMessage response = await P4258EBF.AFA7138A.M6233B19[349](httpClient2, "desktop/benchmark/me/has-hardware-test").ConfigureAwait(false))
				{
					if (P4258EBF.AFA7138A.M6233B19[23](response) == HttpStatusCode.OK)
					{
						string msg = await P4258EBF.AFA7138A.M6233B19[623](P4258EBF.AFA7138A.M6233B19[405](response)).ConfigureAwait(false);
						ConfiguredTaskAwaitable configuredTaskAwaitable = P4258EBF.AFA7138A.M6233B19[196](ResponseCache.WriteAsync("desktop/benchmark/me/has-hardware-test", msg), false);
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter = P4258EBF.AFA7138A.M6233B19[628](ref configuredTaskAwaitable);
						if (!configuredTaskAwaiter.IsCompleted)
						{
							await configuredTaskAwaiter;
							ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
							configuredTaskAwaiter = configuredTaskAwaiter2;
							configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						}
						configuredTaskAwaiter.GetResult();
						text2 = msg;
					}
					else
					{
						text2 = null;
					}
				}
			}
			return text2;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000046EB File Offset: 0x00002AEB
		public bool consumeBenchmarkResultRedirectFlag()
		{
			return BenchmarkIpcServer.ConsumePendingBenchmarkResultRedirect();
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000046F2 File Offset: 0x00002AF2
		public Task<bool> HasPendingBenchmarkBackups()
		{
			Func<bool> func;
			if ((func = Optimizations.<>O.<2>__HasPendingBackupsAtStartup) == null)
			{
				func = (Optimizations.<>O.<2>__HasPendingBackupsAtStartup = new Func<bool>(BenchmarkBackupRecovery.HasPendingBackupsAtStartup));
			}
			return Task.Run<bool>(func);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004714 File Offset: 0x00002B14
		public Task<bool> RestorePendingBenchmarkBackups()
		{
			return Task.Run<bool>(delegate
			{
				if (!BenchmarkBackupRecovery.HasPendingBackupsAtStartup())
				{
					return false;
				}
				BenchmarkBackupRecovery.TryRestorePendingBackupsAtStartup();
				return true;
			});
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00143004 File Offset: 0x00140804
		public static string ToKey(OptimizationId id)
		{
			string text;
			if (!Optimizations.Keys.TryGetValue(id, out text))
			{
				throw P4258EBF.AFA7138A.M6233B19[80]("id", id, null);
			}
			return text;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x001519D0 File Offset: 0x0014F1D0
		public static OptimizationId ToOptimizationId(string key)
		{
			OptimizationId optimizationId;
			if (!Optimizations.Ids.TryGetValue(key, out optimizationId))
			{
				throw P4258EBF.AFA7138A.M6233B19[80]("key", key, null);
			}
			return optimizationId;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x001543A4 File Offset: 0x00151BA4
		public Optimizations()
		{
			P4258EBF.AFA7138A.M6233B19[130](this);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x0015CDC0 File Offset: 0x0015A5C0
		// Note: this type is marked as 'beforefieldinit'.
		static Optimizations()
		{
			Dictionary<OptimizationId, string> dictionary = new Dictionary<OptimizationId, string>();
			dictionary[OptimizationId.DisableHags] = "disable-hags";
			dictionary[OptimizationId.EnableGameMode] = "enable-game-mode";
			dictionary[OptimizationId.DisableExclusiveFullscreen] = "disable-exclusive-fullscreen";
			dictionary[OptimizationId.AutoCpuAffinity] = "auto-cpu-affinity";
			dictionary[OptimizationId.AutoPagefile] = "auto-pagefile";
			dictionary[OptimizationId.WindowsPowerPlan] = "windows-power-plan";
			dictionary[OptimizationId.DisablePcieLpm] = "disable-pcie-lpm";
			dictionary[OptimizationId.DisableUnusedServices] = "disable-unused-services";
			dictionary[OptimizationId.DisableHvci] = "disable-hvci";
			dictionary[OptimizationId.DisableDrtp] = "disable-drtp";
			dictionary[OptimizationId.DisableXboxGameBar] = "disable-xbox-game-bar";
			dictionary[OptimizationId.AutoGcBuffer] = "auto-gc-buffer";
			Optimizations.Keys = dictionary;
			Optimizations.Ids = Optimizations.Keys.ToDictionary<KeyValuePair<OptimizationId, string>, string, OptimizationId>((KeyValuePair<OptimizationId, string> pair) => pair.Value, (KeyValuePair<OptimizationId, string> pair) => pair.Key);
			Optimizations.OptimizationsDictionary = new Dictionary<OptimizationId, Func<IOptimization>>
			{
				{
					OptimizationId.DisableHags,
					() => new DisableHAGS()
				},
				{
					OptimizationId.EnableGameMode,
					() => new EnableGameModeOptimization()
				},
				{
					OptimizationId.DisableExclusiveFullscreen,
					() => new DisableExclusiveFullscreen()
				},
				{
					OptimizationId.AutoCpuAffinity,
					() => new AutoCpuAffinity()
				},
				{
					OptimizationId.AutoPagefile,
					() => new PagefileOptimization()
				},
				{
					OptimizationId.WindowsPowerPlan,
					() => new WindowsPowerPlan()
				},
				{
					OptimizationId.DisablePcieLpm,
					() => new DisablePciLpm()
				},
				{
					OptimizationId.DisableUnusedServices,
					() => new DisableUnusedServices()
				},
				{
					OptimizationId.DisableHvci,
					() => new DisableHVCI()
				},
				{
					OptimizationId.DisableDrtp,
					() => new DisableDRTP()
				},
				{
					OptimizationId.DisableXboxGameBar,
					() => new DisableXboxGameBarOptimization()
				},
				{
					OptimizationId.AutoGcBuffer,
					() => new AutoGcBuffer()
				}
			};
		}

		// Token: 0x0400002C RID: 44
		private static readonly TimeSpan StatusCacheTtl = P4258EBF.AFA7138A.M6233B19[395](1L);

		// Token: 0x0400002D RID: 45
		private static readonly object StatusCacheLock = P4258EBF.AFA7138A.M6233B19[131]();

		// Token: 0x0400002E RID: 46
		private static readonly object BenchmarkOptimizationsInfoLock = P4258EBF.AFA7138A.M6233B19[131]();

		// Token: 0x0400002F RID: 47
		private static readonly Dictionary<OptimizationId, Optimizations.CachedOptimizationInfo> StatusCache = new Dictionary<OptimizationId, Optimizations.CachedOptimizationInfo>();

		// Token: 0x04000030 RID: 48
		private static readonly TimeSpan BenchmarkTopCacheTtl = P4258EBF.AFA7138A.M6233B19[461](1);

		// Token: 0x04000031 RID: 49
		private static readonly TimeSpan BenchmarkUserCacheTtl = P4258EBF.AFA7138A.M6233B19[461](1);

		// Token: 0x04000032 RID: 50
		private static readonly TimeSpan BenchmarkHistogram = P4258EBF.AFA7138A.M6233B19[461](1);

		// Token: 0x04000033 RID: 51
		private static readonly TimeSpan BenchmarkMeHistoryCacheTtl = P4258EBF.AFA7138A.M6233B19[395](3L);

		// Token: 0x04000034 RID: 52
		private static readonly TimeSpan BenchmarkDevicesCacheTtl = P4258EBF.AFA7138A.M6233B19[395](3L);

		// Token: 0x04000035 RID: 53
		private static readonly TimeSpan BenchmarkMeInfoCacheTtl = P4258EBF.AFA7138A.M6233B19[395](3L);

		// Token: 0x04000036 RID: 54
		private static readonly TimeSpan BenchmarkUserLastCacheTtl = P4258EBF.AFA7138A.M6233B19[536](1);

		// Token: 0x04000037 RID: 55
		private static readonly TimeSpan BenchmarkUserHistoryCacheTtl = P4258EBF.AFA7138A.M6233B19[395](3L);

		// Token: 0x04000038 RID: 56
		private static readonly TimeSpan BenchmarkHasHardwareTestCacheTtl = P4258EBF.AFA7138A.M6233B19[395](3L);

		// Token: 0x04000039 RID: 57
		private static readonly TimeSpan BenchmarkDashboardInfoCacheTtl = P4258EBF.AFA7138A.M6233B19[395](3L);

		// Token: 0x0400003A RID: 58
		private static readonly string[] MonitoringOptimizationIds = new string[] { "temperature-monitoring", "ram-monitoring", "disk-monitoring" };

		// Token: 0x0400003B RID: 59
		private static JArray BenchmarkOptimizationsInfo = P4258EBF.AFA7138A.M6233B19[586]();

		// Token: 0x0400003C RID: 60
		private static bool BenchmarkOptimizationsInfoCaptured;

		// Token: 0x0400003D RID: 61
		private static readonly IReadOnlyDictionary<OptimizationId, string> Keys;

		// Token: 0x0400003E RID: 62
		private static readonly IReadOnlyDictionary<string, OptimizationId> Ids;

		// Token: 0x0400003F RID: 63
		public static readonly Dictionary<OptimizationId, Func<IOptimization>> OptimizationsDictionary;

		// Token: 0x0200006F RID: 111
		private sealed class CachedOptimizationInfo
		{
			// Token: 0x170000A2 RID: 162
			// (get) Token: 0x060003C1 RID: 961 RVA: 0x0001678A File Offset: 0x00014B8A
			// (set) Token: 0x060003C2 RID: 962 RVA: 0x00016792 File Offset: 0x00014B92
			public OptimizationInfoDto Info { get; set; }

			// Token: 0x170000A3 RID: 163
			// (get) Token: 0x060003C3 RID: 963 RVA: 0x0001679B File Offset: 0x00014B9B
			// (set) Token: 0x060003C4 RID: 964 RVA: 0x000167A3 File Offset: 0x00014BA3
			public DateTime CreatedAtUtc { get; set; }

			// Token: 0x060003C5 RID: 965 RVA: 0x0015E818 File Offset: 0x0015C018
			public CachedOptimizationInfo()
			{
				P4258EBF.AFA7138A.M6233B19[130](this);
			}
		}

		// Token: 0x02000070 RID: 112
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x0400012B RID: 299
			public static Func<string> <0>__getCurrentSelectedFolder;

			// Token: 0x0400012C RID: 300
			public static Action <1>__CaptureBenchmarkOptimizationsInfo;

			// Token: 0x0400012D RID: 301
			public static Func<bool> <2>__HasPendingBackupsAtStartup;
		}
	}
}
