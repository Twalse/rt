using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WpfApp1;
using WpfApp1.Model;

namespace RustTweaker.Optimization.Monitoring
{
	// Token: 0x0200003E RID: 62
	internal static class MonitoringBenchmarkClient
	{
		// Token: 0x06000243 RID: 579 RVA: 0x0000C144 File Offset: 0x0000A544
		public static async Task<JObject> GetLatestHardwareTestRootAsync()
		{
			JObject jobject;
			try
			{
				using (HttpClient client = new SecureHttp().GetClient())
				{
					int? num = await MonitoringBenchmarkClient.GetLatestSubmissionIdAsync(client).ConfigureAwait(false);
					int? num2 = num;
					if (num2 == null)
					{
						jobject = null;
					}
					else
					{
						string steamtid = App.STEAMTID;
						if (P4258EBF.AFA7138A.M6233B19[426](steamtid))
						{
							jobject = null;
						}
						else
						{
							string text = P4258EBF.AFA7138A.M6233B19[4]("desktop/benchmark/user?steamid={0}&submissionId={1}", P4258EBF.AFA7138A.M6233B19[154](steamtid), num2.Value);
							using (HttpResponseMessage response = await P4258EBF.AFA7138A.M6233B19[349](client, text).ConfigureAwait(false))
							{
								if (P4258EBF.AFA7138A.M6233B19[23](response) != HttpStatusCode.OK)
								{
									jobject = null;
								}
								else
								{
									string text2 = await P4258EBF.AFA7138A.M6233B19[623](P4258EBF.AFA7138A.M6233B19[405](response)).ConfigureAwait(false);
									if (P4258EBF.AFA7138A.M6233B19[426](text2))
									{
										jobject = null;
									}
									else
									{
										JToken jtoken = JsonConvert.DeserializeObject<JToken>(text2);
										jobject = MonitoringBenchmarkClient.ExtractRawBenchmarkRoot(jtoken);
									}
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
				jobject = null;
			}
			return jobject;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000C180 File Offset: 0x0000A580
		private static async Task<int?> GetLatestSubmissionIdAsync(HttpClient client)
		{
			HttpResponseMessage httpResponseMessage = await P4258EBF.AFA7138A.M6233B19[349](client, "desktop/benchmark/me?limit=1&benchmarkType=HardwareTest&frameGen=false").ConfigureAwait(false);
			int? num;
			using (HttpResponseMessage response = httpResponseMessage)
			{
				if (P4258EBF.AFA7138A.M6233B19[23](response) != HttpStatusCode.OK)
				{
					num = null;
				}
				else
				{
					string text = await P4258EBF.AFA7138A.M6233B19[623](P4258EBF.AFA7138A.M6233B19[405](response)).ConfigureAwait(false);
					if (P4258EBF.AFA7138A.M6233B19[426](text))
					{
						num = null;
					}
					else
					{
						JToken jtoken = JsonConvert.DeserializeObject<JToken>(text);
						num = MonitoringBenchmarkClient.ExtractSubmissionId(jtoken);
					}
				}
			}
			return num;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00147488 File Offset: 0x00144C88
		private static int? ExtractSubmissionId(JToken token)
		{
			JObject jobject = token as JObject;
			if (jobject == null)
			{
				return null;
			}
			JToken jtoken = P4258EBF.AFA7138A.M6233B19[175](jobject, "items");
			JObject jobject2 = ((jtoken != null) ? jtoken.OfType<JObject>().FirstOrDefault<JObject>() : null);
			if (jobject2 == null)
			{
				return null;
			}
			JToken jtoken2 = P4258EBF.AFA7138A.M6233B19[175](jobject2, "submissionId");
			if (jtoken2 == null)
			{
				return null;
			}
			if (P4258EBF.AFA7138A.M6233B19[630](jtoken2) == JTokenType.Integer)
			{
				return new int?(jtoken2.Value<int>());
			}
			int num;
			if (!P4258EBF.AFA7138A.M6233B19[270](jtoken2.ToString(), ref num))
			{
				return null;
			}
			return new int?(num);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00155A34 File Offset: 0x00153234
		private static JObject ExtractRawBenchmarkRoot(JToken token)
		{
			JObject jobject = token as JObject;
			if (jobject != null)
			{
				return P4258EBF.AFA7138A.M6233B19[175](jobject, "rawBenchmark") as JObject;
			}
			JArray jarray = token as JArray;
			if (jarray != null)
			{
				return (from item in jarray.OfType<JObject>()
					select P4258EBF.AFA7138A.M6233B19[175](item, "rawBenchmark") as JObject).FirstOrDefault<JObject>((JObject root) => root != null);
			}
			return null;
		}

		// Token: 0x040000C1 RID: 193
		private const string LatestHardwareTestEndpoint = "desktop/benchmark/me?limit=1&benchmarkType=HardwareTest&frameGen=false";

		// Token: 0x040000C2 RID: 194
		private const string UserBenchmarkEndpoint = "desktop/benchmark/user?steamid={0}&submissionId={1}";
	}
}
