using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RustTweaker;

namespace WpfApp1.Model
{
	// Token: 0x02000061 RID: 97
	public static class CoreHelper
	{
		// Token: 0x0600036A RID: 874 RVA: 0x001603C4 File Offset: 0x0015DBC4
		static CoreHelper()
		{
			Architecture architecture = P4258EBF.AFA7138A.M6233B19[400]();
			CoreHelper.coreArchitecture = P4258EBF.AFA7138A.M6233B19[432](architecture.ToString());
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			P4258EBF.AFA7138A.M6233B19[466](ref defaultInterpolatedStringHandler, 33, 2);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "init CoreHelper (arch=");
			P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler, CoreHelper.coreArchitecture);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, ", baseDir=");
			P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler, P4258EBF.AFA7138A.M6233B19[587]());
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, ")");
			Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler));
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0014B924 File Offset: 0x00149124
		private static string CreateTempDirectory()
		{
			string text4;
			try
			{
				L083B68C l083B68C = P4258EBF.AFA7138A.M6233B19[158];
				string text = P4258EBF.AFA7138A.M6233B19[312]();
				A3B43D3F a3B43D3F = P4258EBF.AFA7138A.M6233B19[478];
				string text2 = "RustTweaker_Core_";
				Guid guid = P4258EBF.AFA7138A.M6233B19[476]();
				string text3 = l083B68C(text, a3B43D3F(text2, P4258EBF.AFA7138A.M6233B19[569](ref guid, "N")));
				P4258EBF.AFA7138A.M6233B19[111](text3);
				Logger.Log(P4258EBF.AFA7138A.M6233B19[478]("CoreHelper tempPath: ", text3));
				text4 = text3;
			}
			catch (Exception ex)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
				P4258EBF.AFA7138A.M6233B19[466](ref defaultInterpolatedStringHandler, 35, 1);
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "CoreHelper tempPath create failed: ");
				defaultInterpolatedStringHandler.AppendFormatted<Exception>(ex);
				Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler));
				text4 = P4258EBF.AFA7138A.M6233B19[312]();
			}
			return text4;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00141A60 File Offset: 0x0013F260
		private static string GetAppDirectory()
		{
			string text = P4258EBF.AFA7138A.M6233B19[587]();
			if (!P4258EBF.AFA7138A.M6233B19[426](text))
			{
				return text;
			}
			ProcessModule processModule = P4258EBF.AFA7138A.M6233B19[189](Process.GetCurrentProcess());
			string text2 = ((processModule != null) ? A28D4FA1.DCB8B2B2(processModule) : null);
			if (!P4258EBF.AFA7138A.M6233B19[426](text2))
			{
				return P4258EBF.AFA7138A.M6233B19[516](text2);
			}
			return P4258EBF.AFA7138A.M6233B19[185]();
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0015ABD8 File Offset: 0x001583D8
		private static async Task<string> getDownloadLink()
		{
			string text = "https://rusttweaker.com/api/getCoreDownloadLink";
			try
			{
				Stopwatch sw = P4258EBF.AFA7138A.M6233B19[55]();
				using (HttpClient client = P4258EBF.AFA7138A.M6233B19[483]())
				{
					P4258EBF.AFA7138A.M6233B19[273](client, P4258EBF.AFA7138A.M6233B19[9](15L));
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = P4258EBF.AFA7138A.M6233B19[467](54, 3);
					P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "CoreHelper.getDownloadLink: GET ");
					P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler, text);
					P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, " (timeout=");
					TimeSpan timeSpan = P4258EBF.AFA7138A.M6233B19[156](client);
					defaultInterpolatedStringHandler.AppendFormatted<double>(P4258EBF.AFA7138A.M6233B19[625](ref timeSpan));
					P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "s, archKey=");
					P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler, CoreHelper.coreArchitecture);
					P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, ")");
					Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler));
					HttpResponseMessage httpResponseMessage = await P4258EBF.AFA7138A.M6233B19[349](client, text).ConfigureAwait(false);
					using (HttpResponseMessage response = httpResponseMessage)
					{
						string text2 = await P4258EBF.AFA7138A.M6233B19[623](P4258EBF.AFA7138A.M6233B19[405](response)).ConfigureAwait(false);
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = P4258EBF.AFA7138A.M6233B19[467](58, 4);
						P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler2, "CoreHelper.getDownloadLink: status=");
						defaultInterpolatedStringHandler2.AppendFormatted<int>((int)P4258EBF.AFA7138A.M6233B19[23](response));
						P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler2, " ");
						P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler2, P4258EBF.AFA7138A.M6233B19[484](response));
						P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler2, ", elapsedMs=");
						defaultInterpolatedStringHandler2.AppendFormatted<long>(P4258EBF.AFA7138A.M6233B19[358](sw));
						P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler2, ", bodyLen=");
						defaultInterpolatedStringHandler2.AppendFormatted<int>((text2 != null) ? P4258EBF.AFA7138A.M6233B19[153](text2) : 0);
						Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler2));
						P4258EBF.AFA7138A.M6233B19[538](response);
						JObject jobject = null;
						try
						{
							jobject = JsonConvert.DeserializeObject<JObject>(text2);
						}
						catch (Exception ex)
						{
							DefaultInterpolatedStringHandler defaultInterpolatedStringHandler3 = P4258EBF.AFA7138A.M6233B19[467](47, 1);
							P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler3, "CoreHelper.getDownloadLink: JSON parse failed: ");
							defaultInterpolatedStringHandler3.AppendFormatted<Exception>(ex);
							Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler3));
							Logger.Log(A3320FA7.N6B66084("CoreHelper.getDownloadLink: bodyHead=", (text2 == null) ? "<null>" : P4258EBF.AFA7138A.M6233B19[487](text2, 0, P4258EBF.AFA7138A.M6233B19[207](300, P4258EBF.AFA7138A.M6233B19[152](text2)))));
							return null;
						}
						if (jobject == null || !P4258EBF.AFA7138A.M6233B19[48](jobject, CoreHelper.coreArchitecture))
						{
							string text3 = "CoreHelper.getDownloadLink: key '";
							string text4 = CoreHelper.coreArchitecture;
							string text5 = "' not found. Keys=";
							string text6;
							if (jobject != null)
							{
								text6 = D2B9D912.A91E8BBB(",", from p in P4258EBF.AFA7138A.M6233B19[604](jobject)
									select P4258EBF.AFA7138A.M6233B19[186](p));
							}
							else
							{
								text6 = "<null>";
							}
							Logger.Log(HB9FDCBB.AB31A5B2(text3, text4, text5, text6));
							return null;
						}
						JToken jtoken = P4258EBF.AFA7138A.M6233B19[175](jobject, CoreHelper.coreArchitecture);
						string text7 = ((jtoken != null) ? jtoken.ToString() : null);
						Logger.Log(A3320FA7.N6B66084("CoreHelper.getDownloadLink: link=", P4258EBF.AFA7138A.M6233B19[426](text7) ? "<empty>" : text7));
						return text7;
					}
				}
			}
			catch (TaskCanceledException ex2)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler4 = P4258EBF.AFA7138A.M6233B19[467](56, 1);
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler4, "CoreHelper.getDownloadLink: TIMEOUT/CANCELED after 15s? ");
				defaultInterpolatedStringHandler4.AppendFormatted<TaskCanceledException>(ex2);
				Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler4));
			}
			catch (Exception ex3)
			{
				Logger.Log(ex3);
			}
			return null;
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00012534 File Offset: 0x00010934
		private static async Task<string> downloadCore(string link)
		{
			try
			{
				Stopwatch sw = P4258EBF.AFA7138A.M6233B19[55]();
				using (HttpClient client = P4258EBF.AFA7138A.M6233B19[483]())
				{
					P4258EBF.AFA7138A.M6233B19[273](client, P4258EBF.AFA7138A.M6233B19[395](5L));
					string text = P4258EBF.AFA7138A.M6233B19[513](P4258EBF.AFA7138A.M6233B19[365](P4258EBF.AFA7138A.M6233B19[105](link)));
					string destPath = P4258EBF.AFA7138A.M6233B19[158](CoreHelper.tempPath, text);
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = P4258EBF.AFA7138A.M6233B19[467](45, 3);
					P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "CoreHelper.downloadCore: GET ");
					P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler, link);
					P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, " -> ");
					P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler, destPath);
					P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, " (timeout=");
					TimeSpan timeSpan = P4258EBF.AFA7138A.M6233B19[156](client);
					defaultInterpolatedStringHandler.AppendFormatted<double>(P4258EBF.AFA7138A.M6233B19[281](ref timeSpan));
					P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "m)");
					Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler));
					HttpResponseMessage httpResponseMessage = await P4258EBF.AFA7138A.M6233B19[7](client, link, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
					using (HttpResponseMessage response = httpResponseMessage)
					{
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = P4258EBF.AFA7138A.M6233B19[467](46, 3);
						P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler2, "CoreHelper.downloadCore: status=");
						defaultInterpolatedStringHandler2.AppendFormatted<int>((int)P4258EBF.AFA7138A.M6233B19[23](response));
						P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler2, " ");
						P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler2, P4258EBF.AFA7138A.M6233B19[484](response));
						P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler2, ", contentLen=");
						string text2;
						if (P4258EBF.AFA7138A.M6233B19[510](P4258EBF.AFA7138A.M6233B19[322](P4258EBF.AFA7138A.M6233B19[405](response))) == null)
						{
							text2 = null;
						}
						else
						{
							long? num;
							long valueOrDefault = num.GetValueOrDefault();
							text2 = P4258EBF.AFA7138A.M6233B19[277](ref valueOrDefault);
						}
						H40C0030.B93BA69A(ref defaultInterpolatedStringHandler2, text2 ?? "<null>");
						Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler2));
						P4258EBF.AFA7138A.M6233B19[538](response);
						long totalBytes = 0L;
						using (Stream input = await P4258EBF.AFA7138A.M6233B19[565](P4258EBF.AFA7138A.M6233B19[405](response)).ConfigureAwait(false))
						{
							using (FileStream fs = P4258EBF.AFA7138A.M6233B19[544](destPath, FileMode.Create, FileAccess.Write, FileShare.Read))
							{
								byte[] buffer = new byte[131072];
								int read;
								while ((read = await P4258EBF.AFA7138A.M6233B19[379](input, buffer, 0, buffer.Length).ConfigureAwait(false)) > 0)
								{
									ConfiguredTaskAwaitable configuredTaskAwaitable = P4258EBF.AFA7138A.M6233B19[196](P4258EBF.AFA7138A.M6233B19[325](fs, buffer, 0, read), false);
									ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter = P4258EBF.AFA7138A.M6233B19[628](ref configuredTaskAwaitable);
									if (!configuredTaskAwaiter.IsCompleted)
									{
										await configuredTaskAwaiter;
										ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
										configuredTaskAwaiter = configuredTaskAwaiter2;
										configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
									}
									configuredTaskAwaiter.GetResult();
									totalBytes += (long)read;
								}
								buffer = null;
							}
							FileStream fs = null;
						}
						Stream input = null;
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler3 = P4258EBF.AFA7138A.M6233B19[467](57, 3);
						P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler3, "CoreHelper.downloadCore: done bytes=");
						defaultInterpolatedStringHandler3.AppendFormatted<long>(totalBytes);
						P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler3, ", elapsedMs=");
						defaultInterpolatedStringHandler3.AppendFormatted<long>(P4258EBF.AFA7138A.M6233B19[358](sw));
						P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler3, ", exists=");
						defaultInterpolatedStringHandler3.AppendFormatted<bool>(P4258EBF.AFA7138A.M6233B19[627](destPath));
						Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler3));
						return destPath;
					}
				}
			}
			catch (TaskCanceledException ex)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler4 = P4258EBF.AFA7138A.M6233B19[467](52, 1);
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler4, "CoreHelper.downloadCore: TIMEOUT/CANCELED after 5m? ");
				defaultInterpolatedStringHandler4.AppendFormatted<TaskCanceledException>(ex);
				Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler4));
			}
			catch (Exception ex2)
			{
				Logger.Log(ex2);
			}
			return null;
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00154FB0 File Offset: 0x001527B0
		private static bool extractCabFile(string pathToCabFile, string pathToOutputDictionary)
		{
			try
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = P4258EBF.AFA7138A.M6233B19[467](46, 2);
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "CoreHelper.extractCabFile: expand.exe \"");
				P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler, pathToCabFile);
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "\" -> \"");
				P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler, pathToOutputDictionary);
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "\"");
				Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler));
				ProcessStartInfo processStartInfo = P4258EBF.AFA7138A.M6233B19[394]();
				N62EB38A.CB1145A6(processStartInfo, "expand.exe");
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = P4258EBF.AFA7138A.M6233B19[467](10, 2);
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler2, "\"");
				P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler2, pathToCabFile);
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler2, "\" -F:* \"");
				P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler2, pathToOutputDictionary);
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler2, "\"");
				AA2B3D09.ND86FA10(processStartInfo, P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler2));
				O8258311.M5A8918D(processStartInfo, false);
				OD07B821.I71E06A0(processStartInfo, true);
				DAB6CE3D.G1236E97(processStartInfo, true);
				JD06799C.I832069D(processStartInfo, true);
				E33C722B.D28D192D(processStartInfo, pathToOutputDictionary);
				ProcessStartInfo processStartInfo2 = processStartInfo;
				using (Process process = P4258EBF.AFA7138A.M6233B19[388](processStartInfo2))
				{
					if (process == null)
					{
						Logger.Log("CoreHelper.extractCabFile: Process.Start returned null");
						return false;
					}
					J3A12CB2 j3A12CB = P4258EBF.AFA7138A.M6233B19[356];
					object obj = process;
					TimeSpan timeSpan = P4258EBF.AFA7138A.M6233B19[395](10L);
					if (!j3A12CB(obj, (int)P4258EBF.AFA7138A.M6233B19[464](ref timeSpan)))
					{
						try
						{
							P4258EBF.AFA7138A.M6233B19[229](process, true);
						}
						catch
						{
						}
						Logger.Log("CoreHelper.extractCabFile: TIMEOUT (10m) while waiting for expand.exe");
						return false;
					}
					string text = P4258EBF.AFA7138A.M6233B19[355](P4258EBF.AFA7138A.M6233B19[399](process));
					string text2 = P4258EBF.AFA7138A.M6233B19[355](P4258EBF.AFA7138A.M6233B19[403](process));
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler3 = P4258EBF.AFA7138A.M6233B19[467](60, 3);
					P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler3, "CoreHelper.extractCabFile: exitCode=");
					defaultInterpolatedStringHandler3.AppendFormatted<int>(P4258EBF.AFA7138A.M6233B19[373](process));
					P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler3, ", stdoutLen=");
					defaultInterpolatedStringHandler3.AppendFormatted<int>((text != null) ? P4258EBF.AFA7138A.M6233B19[153](text) : 0);
					P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler3, ", stderrLen=");
					defaultInterpolatedStringHandler3.AppendFormatted<int>((text2 != null) ? P4258EBF.AFA7138A.M6233B19[153](text2) : 0);
					Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler3));
					if (!P4258EBF.AFA7138A.M6233B19[426](text2))
					{
						Logger.Log(P4258EBF.AFA7138A.M6233B19[478]("CoreHelper.extractCabFile: stderrHead=", P4258EBF.AFA7138A.M6233B19[487](text2, 0, P4258EBF.AFA7138A.M6233B19[207](800, P4258EBF.AFA7138A.M6233B19[152](text2)))));
					}
					if (!P4258EBF.AFA7138A.M6233B19[426](text))
					{
						Logger.Log(P4258EBF.AFA7138A.M6233B19[478]("CoreHelper.extractCabFile: stdoutHead=", P4258EBF.AFA7138A.M6233B19[487](text, 0, P4258EBF.AFA7138A.M6233B19[207](800, P4258EBF.AFA7138A.M6233B19[152](text)))));
					}
					return P4258EBF.AFA7138A.M6233B19[373](process) == 0;
				}
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
			}
			return false;
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00012800 File Offset: 0x00010C00
		public static async Task<bool> containsWebviewFolder()
		{
			string text = await CoreHelper.getDownloadLink().ConfigureAwait(false);
			string text2 = text;
			bool flag;
			if (P4258EBF.AFA7138A.M6233B19[88](text2))
			{
				P4258EBF.AFA7138A.M6233B19[129]("ERROR: Error when try get link for download!");
				flag = false;
			}
			else
			{
				string text3 = P4258EBF.AFA7138A.M6233B19[158](CoreHelper.GetAppDirectory(), P4258EBF.AFA7138A.M6233B19[391](text2));
				Logger.Log(P4258EBF.AFA7138A.M6233B19[478]("WebView2 custom runtime check path: ", text3));
				bool flag2 = P4258EBF.AFA7138A.M6233B19[89](text3);
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = P4258EBF.AFA7138A.M6233B19[467](31, 1);
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "WebView2 custom runtime exists=");
				defaultInterpolatedStringHandler.AppendFormatted<bool>(flag2);
				Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler));
				flag = flag2;
			}
			return flag;
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0001283C File Offset: 0x00010C3C
		public static async Task<string> getPathToWebview()
		{
			bool flag = await CoreHelper.containsWebviewFolder().ConfigureAwait(false);
			string text2;
			if (flag)
			{
				string text = await CoreHelper.getDownloadLink().ConfigureAwait(false);
				if (P4258EBF.AFA7138A.M6233B19[88](text))
				{
					P4258EBF.AFA7138A.M6233B19[129]("ERROR: Error when try get link for download!");
					text2 = null;
				}
				else
				{
					string text3 = P4258EBF.AFA7138A.M6233B19[158](CoreHelper.GetAppDirectory(), P4258EBF.AFA7138A.M6233B19[391](text));
					Logger.Log(P4258EBF.AFA7138A.M6233B19[478]("CoreHelper.getPathToWebview: ", text3));
					text2 = text3;
				}
			}
			else
			{
				text2 = null;
			}
			return text2;
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00012878 File Offset: 0x00010C78
		public static async Task<bool> installCore()
		{
			bool flag3;
			try
			{
				string appDir = CoreHelper.GetAppDirectory();
				Logger.Log(P4258EBF.AFA7138A.M6233B19[64]("CoreHelper.installCore: start (appDir=", appDir, ")"));
				bool flag = await CoreHelper.containsWebviewFolder().ConfigureAwait(false);
				bool flag2 = flag;
				if (flag2)
				{
					Logger.Log("CoreHelper.installCore: already installed");
					flag3 = true;
				}
				else
				{
					string text = await CoreHelper.getDownloadLink().ConfigureAwait(false);
					if (P4258EBF.AFA7138A.M6233B19[88](text))
					{
						P4258EBF.AFA7138A.M6233B19[129]("ERROR: Error when try get link for download!");
						flag3 = false;
					}
					else
					{
						string text2 = await CoreHelper.downloadCore(text).ConfigureAwait(false);
						if (P4258EBF.AFA7138A.M6233B19[88](text2))
						{
							P4258EBF.AFA7138A.M6233B19[129]("ERROR: Error when download webview core!");
							flag3 = false;
						}
						else
						{
							bool flag4 = CoreHelper.extractCabFile(text2, appDir);
							DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = P4258EBF.AFA7138A.M6233B19[467](39, 1);
							P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "CoreHelper.installCore: extract result=");
							defaultInterpolatedStringHandler.AppendFormatted<bool>(flag4);
							Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler));
							flag3 = flag4;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
				flag3 = false;
			}
			return flag3;
		}

		// Token: 0x040000F9 RID: 249
		public static string PATH_TO_WEBVIEW = null;

		// Token: 0x040000FA RID: 250
		public static bool NeedFix = false;

		// Token: 0x040000FB RID: 251
		private static readonly string tempPath = CoreHelper.CreateTempDirectory();

		// Token: 0x040000FC RID: 252
		private static readonly string coreArchitecture;
	}
}
