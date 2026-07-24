using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using RustTweaker;
using WpfApp1.Model;

namespace RustTweaker_NET.Services
{
	// Token: 0x02000016 RID: 22
	internal static class BenchmarkIpcServer
	{
		// Token: 0x060000C7 RID: 199 RVA: 0x0013D790 File Offset: 0x0013AF90
		public static void Start()
		{
			if (BenchmarkIpcServer._cts != null)
			{
				return;
			}
			BenchmarkIpcServer._cts = P4258EBF.AFA7138A.M6233B19[97]();
			BenchmarkIpcServer._acceptTask = K41AFEAE.P5B38F3E(() => BenchmarkIpcServer.AcceptLoopAsync(P4258EBF.AFA7138A.M6233B19[157](BenchmarkIpcServer._cts)));
			Logger.Log("Benchmark IPC server started: \\\\.\\pipe\\RustTweakerBenchmark");
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00148908 File Offset: 0x00146108
		public static void Stop()
		{
			try
			{
				CancellationTokenSource cts = BenchmarkIpcServer._cts;
				if (cts != null)
				{
					DC3431A2.GCB804BD(cts);
				}
			}
			catch
			{
			}
			finally
			{
				CancellationTokenSource cts2 = BenchmarkIpcServer._cts;
				if (cts2 != null)
				{
					GD0D780D.C08B3598(cts2);
				}
				BenchmarkIpcServer._cts = null;
				BenchmarkIpcServer._acceptTask = null;
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0015E0B8 File Offset: 0x0015B8B8
		private static Task AcceptLoopAsync(CancellationToken cancellationToken)
		{
			BenchmarkIpcServer.<AcceptLoopAsync>d__6 <AcceptLoopAsync>d__;
			<AcceptLoopAsync>d__.<>t__builder = P4258EBF.AFA7138A.M6233B19[20]();
			<AcceptLoopAsync>d__.cancellationToken = cancellationToken;
			<AcceptLoopAsync>d__.<>1__state = -1;
			<AcceptLoopAsync>d__.<>t__builder.Start<BenchmarkIpcServer.<AcceptLoopAsync>d__6>(ref <AcceptLoopAsync>d__);
			return P4258EBF.AFA7138A.M6233B19[19](ref <AcceptLoopAsync>d__.<>t__builder);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0015FFB0 File Offset: 0x0015D7B0
		private static Task HandleClientAsync(NamedPipeServerStream pipe, CancellationToken cancellationToken)
		{
			BenchmarkIpcServer.<HandleClientAsync>d__7 <HandleClientAsync>d__;
			<HandleClientAsync>d__.<>t__builder = P4258EBF.AFA7138A.M6233B19[20]();
			<HandleClientAsync>d__.pipe = pipe;
			<HandleClientAsync>d__.cancellationToken = cancellationToken;
			<HandleClientAsync>d__.<>1__state = -1;
			<HandleClientAsync>d__.<>t__builder.Start<BenchmarkIpcServer.<HandleClientAsync>d__7>(ref <HandleClientAsync>d__);
			return P4258EBF.AFA7138A.M6233B19[19](ref <HandleClientAsync>d__.<>t__builder);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00151A08 File Offset: 0x0014F208
		[NullableContext(2)]
		private static void LogShowFpsLogFile(string logText)
		{
			if (P4258EBF.AFA7138A.M6233B19[426](logText))
			{
				Logger.Log("ShowFPS log file received via IPC, but the payload was empty.");
				return;
			}
			Logger.Log(P4258EBF.AFA7138A.M6233B19[64]("ShowFPS log file received via IPC:", P4258EBF.AFA7138A.M6233B19[181](), logText));
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0015DF8C File Offset: 0x0015B78C
		private static bool TryParseIpcMessage(string ipcLine, out BenchmarkIpcServer.BenchmarkIpcMessage message)
		{
			message = null;
			bool flag;
			try
			{
				BenchmarkIpcServer.BenchmarkIpcMessage benchmarkIpcMessage = JsonSerializer.Deserialize<BenchmarkIpcServer.BenchmarkIpcMessage>(ipcLine, null);
				if (benchmarkIpcMessage == null || P4258EBF.AFA7138A.M6233B19[426](benchmarkIpcMessage.Type))
				{
					flag = false;
				}
				else
				{
					message = benchmarkIpcMessage;
					flag = true;
				}
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0015B010 File Offset: 0x00158810
		private static Task SendBenchmarkResultAsync(string resultJson)
		{
			BenchmarkIpcServer.<SendBenchmarkResultAsync>d__10 <SendBenchmarkResultAsync>d__;
			<SendBenchmarkResultAsync>d__.<>t__builder = P4258EBF.AFA7138A.M6233B19[20]();
			<SendBenchmarkResultAsync>d__.resultJson = resultJson;
			<SendBenchmarkResultAsync>d__.<>1__state = -1;
			<SendBenchmarkResultAsync>d__.<>t__builder.Start<BenchmarkIpcServer.<SendBenchmarkResultAsync>d__10>(ref <SendBenchmarkResultAsync>d__);
			return P4258EBF.AFA7138A.M6233B19[19](ref <SendBenchmarkResultAsync>d__.<>t__builder);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000054BC File Offset: 0x000038BC
		private static async Task<int?> GetLatestSubmissionIdAsync(SecureHttp secureHttp, string resultJson)
		{
			int? num;
			try
			{
				string text = BenchmarkIpcServer.ExtractBenchmarkType(resultJson);
				string text2 = "desktop/benchmark/me?limit=1";
				if (!P4258EBF.AFA7138A.M6233B19[426](text))
				{
					text2 = P4258EBF.AFA7138A.M6233B19[64](text2, "&benchmarkType=", P4258EBF.AFA7138A.M6233B19[154](text));
				}
				HttpResponseMessage httpResponseMessage = await P4258EBF.AFA7138A.M6233B19[349](secureHttp.GetClient(), text2).ConfigureAwait(false);
				using (HttpResponseMessage response = httpResponseMessage)
				{
					if (P4258EBF.AFA7138A.M6233B19[23](response) != HttpStatusCode.OK)
					{
						num = null;
					}
					else
					{
						string text3 = await P4258EBF.AFA7138A.M6233B19[623](P4258EBF.AFA7138A.M6233B19[405](response)).ConfigureAwait(false);
						num = BenchmarkIpcServer.ExtractSubmissionId(text3);
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
				num = null;
			}
			return num;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0014DE34 File Offset: 0x0014B634
		private static string ExtractBenchmarkType(string json)
		{
			string text2;
			try
			{
				JToken jtoken = P4258EBF.AFA7138A.M6233B19[337](json);
				JToken jtoken2 = P4258EBF.AFA7138A.M6233B19[327](jtoken, "BenchmarkType");
				string text;
				if ((text = ((jtoken2 != null) ? jtoken2.ToString() : null)) == null)
				{
					JToken jtoken3 = P4258EBF.AFA7138A.M6233B19[327](jtoken, "benchmarkType");
					if ((text = ((jtoken3 != null) ? jtoken3.ToString() : null)) == null)
					{
						JToken jtoken4 = P4258EBF.AFA7138A.M6233B19[327](jtoken, "rawBenchmark");
						if (jtoken4 == null)
						{
							text = null;
						}
						else
						{
							JToken jtoken5 = I1918324.PC1B69A9(jtoken4, "BenchmarkType");
							text = ((jtoken5 != null) ? jtoken5.ToString() : null);
						}
					}
				}
				text2 = text;
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
				text2 = null;
			}
			return text2;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x0013A394 File Offset: 0x00137B94
		private static int? ExtractSubmissionId(string json)
		{
			int? num;
			if (P4258EBF.AFA7138A.M6233B19[426](json))
			{
				num = null;
				return num;
			}
			try
			{
				num = BenchmarkIpcServer.FindSubmissionId(P4258EBF.AFA7138A.M6233B19[337](json));
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
				num = null;
			}
			return num;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00140718 File Offset: 0x0013DF18
		private static int? FindSubmissionId(JToken token)
		{
			JObject jobject = token as JObject;
			if (jobject != null)
			{
				foreach (string text in new string[] { "submissionId", "SubmissionId", "id", "Id" })
				{
					JToken jtoken;
					if (P4258EBF.AFA7138A.M6233B19[210](jobject, text, StringComparison.OrdinalIgnoreCase, ref jtoken) && P4258EBF.AFA7138A.M6233B19[630](jtoken) == JTokenType.Integer)
					{
						return new int?(jtoken.Value<int>());
					}
				}
			}
			using (IEnumerator<JToken> enumerator = P4258EBF.AFA7138A.M6233B19[474](token).GetEnumerator())
			{
				while (P4258EBF.AFA7138A.M6233B19[411](enumerator))
				{
					JToken jtoken2 = enumerator.Current;
					int? num = BenchmarkIpcServer.FindSubmissionId(jtoken2);
					if (num != null)
					{
						return num;
					}
				}
			}
			return null;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00160DFC File Offset: 0x0015E5FC
		public static bool ConsumePendingBenchmarkResultRedirect()
		{
			return P4258EBF.AFA7138A.M6233B19[308](ref BenchmarkIpcServer._hasPendingBenchmarkResultRedirect, 0) == 1;
		}

		// Token: 0x04000047 RID: 71
		private const string PipeName = "RustTweakerBenchmark";

		// Token: 0x04000048 RID: 72
		private static CancellationTokenSource _cts;

		// Token: 0x04000049 RID: 73
		private static Task _acceptTask;

		// Token: 0x0400004A RID: 74
		private static int _hasPendingBenchmarkResultRedirect;

		// Token: 0x0200008A RID: 138
		private sealed class BenchmarkIpcMessage
		{
			// Token: 0x170000A7 RID: 167
			// (get) Token: 0x0600040E RID: 1038 RVA: 0x00019733 File Offset: 0x00017B33
			// (set) Token: 0x0600040F RID: 1039 RVA: 0x0001973B File Offset: 0x00017B3B
			public string Type { get; set; }

			// Token: 0x170000A8 RID: 168
			// (get) Token: 0x06000410 RID: 1040 RVA: 0x00019744 File Offset: 0x00017B44
			// (set) Token: 0x06000411 RID: 1041 RVA: 0x0001974C File Offset: 0x00017B4C
			public string Text { get; set; }

			// Token: 0x06000412 RID: 1042 RVA: 0x0015A6C0 File Offset: 0x00157EC0
			public BenchmarkIpcMessage()
			{
				P4258EBF.AFA7138A.M6233B19[130](this);
			}
		}
	}
}
