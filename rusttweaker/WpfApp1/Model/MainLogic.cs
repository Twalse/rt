using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RustTweaker;
using RustTweaker.Model;
using RustTweakerDemo;
using SteamGameSearcher;

namespace WpfApp1.Model
{
	// Token: 0x02000065 RID: 101
	internal static class MainLogic
	{
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600037A RID: 890 RVA: 0x00012FF0 File Offset: 0x000113F0
		public static string currentPathsToRust
		{
			get
			{
				return MainLogic.pathsToRust.First<string>();
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600037B RID: 891 RVA: 0x00012FFC File Offset: 0x000113FC
		// (set) Token: 0x0600037C RID: 892 RVA: 0x00013003 File Offset: 0x00011403
		public static List<string> pathsToRust { get; private set; } = new List<string>();

		// Token: 0x0600037D RID: 893 RVA: 0x0015C744 File Offset: 0x00159F44
		private static void ClearStartupStateCache()
		{
			object startupStateCacheLock = MainLogic.StartupStateCacheLock;
			bool flag = false;
			try
			{
				P4258EBF.AFA7138A.M6233B19[520](startupStateCacheLock, ref flag);
				MainLogic.StartupStateCache.Clear();
			}
			finally
			{
				if (flag)
				{
					P4258EBF.AFA7138A.M6233B19[631](startupStateCacheLock);
				}
			}
		}

		// Token: 0x0600037E RID: 894 RVA: 0x00151F54 File Offset: 0x0014F754
		private static bool TryGetStartupStateCache(string signature, out string response)
		{
			object startupStateCacheLock = MainLogic.StartupStateCacheLock;
			bool flag = false;
			try
			{
				P4258EBF.AFA7138A.M6233B19[520](startupStateCacheLock, ref flag);
				MainLogic.StartupStateCacheEntry startupStateCacheEntry;
				if (MainLogic.StartupStateCache.TryGetValue(signature, out startupStateCacheEntry) && P4258EBF.AFA7138A.M6233B19[135](P4258EBF.AFA7138A.M6233B19[188](P4258EBF.AFA7138A.M6233B19[241](), startupStateCacheEntry.CreatedAtUtc), MainLogic.StartupStateCacheTtl))
				{
					response = startupStateCacheEntry.Response;
					return true;
				}
				MainLogic.StartupStateCache.Remove(signature);
			}
			finally
			{
				if (flag)
				{
					P4258EBF.AFA7138A.M6233B19[631](startupStateCacheLock);
				}
			}
			response = null;
			return false;
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00151890 File Offset: 0x0014F090
		private static void SetStartupStateCache(string signature, string response)
		{
			object startupStateCacheLock = MainLogic.StartupStateCacheLock;
			bool flag = false;
			try
			{
				P4258EBF.AFA7138A.M6233B19[520](startupStateCacheLock, ref flag);
				MainLogic.StartupStateCache[signature] = new MainLogic.StartupStateCacheEntry
				{
					Response = response,
					CreatedAtUtc = P4258EBF.AFA7138A.M6233B19[241]()
				};
			}
			finally
			{
				if (flag)
				{
					P4258EBF.AFA7138A.M6233B19[631](startupStateCacheLock);
				}
			}
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0015C614 File Offset: 0x00159E14
		private static MainLogic.StartupStateRequest BuildStartupStateRequest(string endpoint)
		{
			List<FileActions.PathNode> list = JsonConvert.DeserializeObject<List<FileActions.PathNode>>(P4258EBF.AFA7138A.M6233B19[267](P4258EBF.AFA7138A.M6233B19[158](MainLogic.appDataPath, "paths.json")));
			FileActions.PathNode pathNode = list.FirstOrDefault<FileActions.PathNode>((FileActions.PathNode item) => item.is_select);
			string text = ((pathNode != null) ? pathNode.folder : null);
			if (P4258EBF.AFA7138A.M6233B19[88](text))
			{
				return null;
			}
			string text2 = P4258EBF.AFA7138A.M6233B19[267](P4258EBF.AFA7138A.M6233B19[278](text, "cfg", "client.cfg"));
			RustTweakerViewModel rustTweakerViewModel = new RustTweakerViewModel();
			string configPathToLastUser = MainLogic.SteamParser.GetConfigPathToLastUser();
			string text3 = rustTweakerViewModel.ExtractRustLaunchOptions(configPathToLastUser, text);
			return new MainLogic.StartupStateRequest
			{
				Endpoint = endpoint,
				SelectedFolder = text,
				ClientContent = text2,
				LaunchOptions = MainLogic.LaunchParamsVdfToApi(text3),
				SecureHttp = new SecureHttp()
			};
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0015A544 File Offset: 0x00157D44
		private static void RemoveReadOnlyRecursively(string path)
		{
			if (!P4258EBF.AFA7138A.M6233B19[89](path))
			{
				Logger.Log(P4258EBF.AFA7138A.M6233B19[478]("Папка не найдена: ", path));
				return;
			}
			foreach (string text in P4258EBF.AFA7138A.M6233B19[540](path))
			{
				FileInfo fileInfo = P4258EBF.AFA7138A.M6233B19[543](text);
				if (P4258EBF.AFA7138A.M6233B19[598](fileInfo))
				{
					P4258EBF.AFA7138A.M6233B19[90](fileInfo, false);
					Logger.Log(P4258EBF.AFA7138A.M6233B19[478]("Снял ReadOnly: ", text));
				}
			}
			foreach (string text2 in P4258EBF.AFA7138A.M6233B19[495](path))
			{
				MainLogic.RemoveReadOnlyRecursively(text2);
			}
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00150E10 File Offset: 0x0014E610
		public static void ReadOnlyCheck()
		{
			try
			{
				string currentSelectedFolder = Configs.getCurrentSelectedFolder();
				if (!P4258EBF.AFA7138A.M6233B19[88](currentSelectedFolder))
				{
					P4258EBF.AFA7138A.M6233B19[278](currentSelectedFolder, "cfg", "");
					string text = P4258EBF.AFA7138A.M6233B19[278](currentSelectedFolder, "cfg", "client.cfg");
					FileInfo fileInfo = P4258EBF.AFA7138A.M6233B19[543](text);
					if (P4258EBF.AFA7138A.M6233B19[598](fileInfo))
					{
						P4258EBF.AFA7138A.M6233B19[90](fileInfo, false);
					}
					string text2 = P4258EBF.AFA7138A.M6233B19[278](currentSelectedFolder, "cfg", "keys.cfg");
					FileInfo fileInfo2 = P4258EBF.AFA7138A.M6233B19[543](text2);
					if (P4258EBF.AFA7138A.M6233B19[598](fileInfo2))
					{
						P4258EBF.AFA7138A.M6233B19[90](fileInfo2, false);
					}
					string configPathToLastUser = MainLogic.SteamParser.GetConfigPathToLastUser();
					FileInfo fileInfo3 = P4258EBF.AFA7138A.M6233B19[543](configPathToLastUser);
					if (P4258EBF.AFA7138A.M6233B19[598](fileInfo3))
					{
						P4258EBF.AFA7138A.M6233B19[90](fileInfo3, false);
					}
				}
				MainLogic.RemoveReadOnlyRecursively(MainLogic.appDataPath);
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
			}
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00143188 File Offset: 0x00140988
		public static void checkPaths()
		{
			MainLogic.ReadOnlyCheck();
			if (!P4258EBF.AFA7138A.M6233B19[89](MainLogic.appDataPath))
			{
				P4258EBF.AFA7138A.M6233B19[111](MainLogic.appDataPath);
			}
			if (!P4258EBF.AFA7138A.M6233B19[627](P4258EBF.AFA7138A.M6233B19[478](MainLogic.appDataPath, "\\appdata.json")))
			{
				P4258EBF.AFA7138A.M6233B19[94](P4258EBF.AFA7138A.M6233B19[478](MainLogic.appDataPath, "\\appdata.json"), "{}");
			}
			else
			{
				try
				{
					P4258EBF.AFA7138A.M6233B19[337](P4258EBF.AFA7138A.M6233B19[267](P4258EBF.AFA7138A.M6233B19[478](MainLogic.appDataPath, "\\appdata.json")));
				}
				catch
				{
					P4258EBF.AFA7138A.M6233B19[94](P4258EBF.AFA7138A.M6233B19[478](MainLogic.appDataPath, "\\appdata.json"), "{}");
				}
			}
			if (!P4258EBF.AFA7138A.M6233B19[627](P4258EBF.AFA7138A.M6233B19[478](MainLogic.appDataPath, "\\paths.json")))
			{
				List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
				MainLogic mainLogic = new MainLogic();
				string text = mainLogic.FindGame(252490);
				string text2 = mainLogic.FindGame(700580);
				if (!P4258EBF.AFA7138A.M6233B19[88](text))
				{
					list.Add(new Dictionary<string, object>
					{
						{ "is_select", false },
						{ "have_warn", false },
						{ "folder", text }
					});
				}
				if (!P4258EBF.AFA7138A.M6233B19[88](text2))
				{
					list.Add(new Dictionary<string, object>
					{
						{ "is_select", false },
						{ "have_warn", false },
						{ "folder", text2 }
					});
				}
				P4258EBF.AFA7138A.M6233B19[94](P4258EBF.AFA7138A.M6233B19[478](MainLogic.appDataPath, "\\paths.json"), P4258EBF.AFA7138A.M6233B19[412](list, Formatting.Indented));
				MainLogic.checkPaths();
			}
			else
			{
				try
				{
					JToken jtoken = P4258EBF.AFA7138A.M6233B19[337](P4258EBF.AFA7138A.M6233B19[267](P4258EBF.AFA7138A.M6233B19[478](MainLogic.appDataPath, "\\paths.json")));
					JArray jarray = jtoken as JArray;
					if (jarray != null)
					{
						using (IEnumerator<JToken> enumerator = P4258EBF.AFA7138A.M6233B19[523](jarray))
						{
							while (P4258EBF.AFA7138A.M6233B19[411](enumerator))
							{
								JToken jtoken2 = enumerator.Current;
								JToken jtoken3 = P4258EBF.AFA7138A.M6233B19[327](jtoken2, "folder");
								if (!D609753E.J80D2B8D((jtoken3 != null) ? jtoken3.ToString() : null))
								{
									JToken jtoken4 = P4258EBF.AFA7138A.M6233B19[327](jtoken2, "folder");
									if (!DEA5F889.N1AB4812((jtoken4 != null) ? jtoken4.ToString() : null))
									{
										P4258EBF.AFA7138A.M6233B19[136](jtoken2, "have_warn", P4258EBF.AFA7138A.M6233B19[448](true));
									}
									else
									{
										P4258EBF.AFA7138A.M6233B19[136](jtoken2, "have_warn", P4258EBF.AFA7138A.M6233B19[448](false));
									}
								}
							}
						}
					}
					P4258EBF.AFA7138A.M6233B19[94](P4258EBF.AFA7138A.M6233B19[478](MainLogic.appDataPath, "\\paths.json"), P4258EBF.AFA7138A.M6233B19[412](jtoken, Formatting.Indented));
				}
				catch
				{
					P4258EBF.AFA7138A.M6233B19[289](P4258EBF.AFA7138A.M6233B19[478](MainLogic.appDataPath, "\\paths.json"));
					MainLogic.checkPaths();
				}
			}
			if (!P4258EBF.AFA7138A.M6233B19[627](P4258EBF.AFA7138A.M6233B19[478](MainLogic.appDataPath, "\\configs.json")))
			{
				P4258EBF.AFA7138A.M6233B19[94](P4258EBF.AFA7138A.M6233B19[478](MainLogic.appDataPath, "\\configs.json"), "[]");
			}
			if (!P4258EBF.AFA7138A.M6233B19[627](P4258EBF.AFA7138A.M6233B19[478](MainLogic.appDataPath, "\\binds.json")))
			{
				P4258EBF.AFA7138A.M6233B19[94](P4258EBF.AFA7138A.M6233B19[478](MainLogic.appDataPath, "\\binds.json"), "[]");
			}
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0014A964 File Offset: 0x00148164
		public static bool checkPathsHealth()
		{
			bool flag;
			try
			{
				if (!P4258EBF.AFA7138A.M6233B19[627](P4258EBF.AFA7138A.M6233B19[478](MainLogic.appDataPath, "\\paths.json")))
				{
					MainLogic.checkPaths();
					flag = true;
				}
				else
				{
					bool flag2 = false;
					List<FileActions.PathNode> list = JsonConvert.DeserializeObject<List<FileActions.PathNode>>(P4258EBF.AFA7138A.M6233B19[267](P4258EBF.AFA7138A.M6233B19[478](MainLogic.appDataPath, "\\paths.json")));
					foreach (FileActions.PathNode pathNode in list)
					{
						if (!P4258EBF.AFA7138A.M6233B19[627](P4258EBF.AFA7138A.M6233B19[478](pathNode.folder, "\\cfg\\client.cfg")))
						{
							pathNode.have_warn = true;
							flag2 = true;
						}
						else
						{
							FileInfo fileInfo = P4258EBF.AFA7138A.M6233B19[543](P4258EBF.AFA7138A.M6233B19[478](pathNode.folder, "\\cfg\\client.cfg"));
							if (P4258EBF.AFA7138A.M6233B19[598](fileInfo))
							{
								P4258EBF.AFA7138A.M6233B19[90](fileInfo, false);
							}
						}
						if (!P4258EBF.AFA7138A.M6233B19[627](P4258EBF.AFA7138A.M6233B19[478](pathNode.folder, "\\cfg\\keys.cfg")))
						{
							pathNode.have_warn = true;
							P4258EBF.AFA7138A.M6233B19[94](P4258EBF.AFA7138A.M6233B19[478](MainLogic.appDataPath, "\\paths.json"), P4258EBF.AFA7138A.M6233B19[412](list, Formatting.Indented));
							flag2 = true;
						}
						else
						{
							FileInfo fileInfo2 = P4258EBF.AFA7138A.M6233B19[543](P4258EBF.AFA7138A.M6233B19[478](pathNode.folder, "\\cfg\\keys.cfg"));
							if (P4258EBF.AFA7138A.M6233B19[598](fileInfo2))
							{
								P4258EBF.AFA7138A.M6233B19[90](fileInfo2, false);
							}
						}
					}
					P4258EBF.AFA7138A.M6233B19[94](P4258EBF.AFA7138A.M6233B19[478](MainLogic.appDataPath, "\\paths.json"), P4258EBF.AFA7138A.M6233B19[412](list, Formatting.Indented));
					flag = flag2;
				}
			}
			catch (Exception ex)
			{
				Logger.Log("Error checkPathsHealth()");
				Logger.Log(ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0015C718 File Offset: 0x00159F18
		public static void openLink(string _url)
		{
			ProcessStartInfo processStartInfo = P4258EBF.AFA7138A.M6233B19[620](_url);
			O8258311.M5A8918D(processStartInfo, true);
			JC11021F.C827CF8C(processStartInfo);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00154700 File Offset: 0x00151F00
		public static void accessTweaks(JObject payload)
		{
			new H52867B7().GAA69983(new object[] { payload }, 48624);
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00160B30 File Offset: 0x0015E330
		public static void accessGraphicsSettings(JObject payload)
		{
			new H52867B7().O0290C29(new object[] { payload }, 38736);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0001441C File Offset: 0x0001281C
		public static async Task<bool> applyTweaks(string data)
		{
			try
			{
				MainLogic.<>c__DisplayClass27_0 CS$<>8__locals1 = new MainLogic.<>c__DisplayClass27_0();
				MainLogic.ClearStartupStateCache();
				MainLogic.StartupStateRequest startupStateRequest = await Task.Run<MainLogic.StartupStateRequest>(() => MainLogic.BuildStartupStateRequest("tweaks/set")).ConfigureAwait(false);
				CS$<>8__locals1.request = startupStateRequest;
				if (CS$<>8__locals1.request == null)
				{
					Logger.Log("Не выбрана папка для конфига.");
					return false;
				}
				var anon = new
				{
					client_cfg = CS$<>8__locals1.request.ClientContent,
					launch_params = CS$<>8__locals1.request.LaunchOptions,
					tweaks = JsonConvert.DeserializeObject<MainLogic.TweaksSettingsDTO>(data)
				};
				string text = P4258EBF.AFA7138A.M6233B19[412](anon, Formatting.Indented);
				StringContent stringContent = P4258EBF.AFA7138A.M6233B19[321](text, P4258EBF.AFA7138A.M6233B19[204](), "application/json");
				HttpResponseMessage httpResponseMessage = await P4258EBF.AFA7138A.M6233B19[583](CS$<>8__locals1.request.SecureHttp.GetClient(), "tweaks/set", stringContent).ConfigureAwait(false);
				if (P4258EBF.AFA7138A.M6233B19[23](httpResponseMessage) == HttpStatusCode.OK)
				{
					CS$<>8__locals1.res = JsonConvert.DeserializeObject<MainLogic.Tweaks_SetResponseDto>(await P4258EBF.AFA7138A.M6233B19[623](P4258EBF.AFA7138A.M6233B19[405](httpResponseMessage)).ConfigureAwait(false));
					ConfiguredTaskAwaitable configuredTaskAwaitable = P4258EBF.AFA7138A.M6233B19[196](P4258EBF.AFA7138A.M6233B19[511](P4258EBF.AFA7138A.M6233B19[579](CS$<>8__locals1, ldftn(<applyTweaks>b__1))), false);
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter = P4258EBF.AFA7138A.M6233B19[628](ref configuredTaskAwaitable);
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
					if (!configuredTaskAwaiter.IsCompleted)
					{
						await configuredTaskAwaiter;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
					}
					configuredTaskAwaiter.GetResult();
					if (P4258EBF.AFA7138A.M6233B19[593](CS$<>8__locals1.request.LaunchOptions, CS$<>8__locals1.res.LaunchParams))
					{
						MainLogic.<>c__DisplayClass27_1 CS$<>8__locals2 = new MainLogic.<>c__DisplayClass27_1();
						CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
						CS$<>8__locals2.rustTweakerViewModel = new RustTweakerViewModel();
						TaskAwaiter taskAwaiter = P4258EBF.AFA7138A.M6233B19[515](CS$<>8__locals2.rustTweakerViewModel.CloseSteam(1000));
						if (!P4258EBF.AFA7138A.M6233B19[211](ref taskAwaiter))
						{
							await taskAwaiter;
							TaskAwaiter taskAwaiter2;
							taskAwaiter = taskAwaiter2;
							taskAwaiter2 = default(TaskAwaiter);
						}
						P4258EBF.AFA7138A.M6233B19[192](ref taskAwaiter);
						configuredTaskAwaitable = P4258EBF.AFA7138A.M6233B19[196](P4258EBF.AFA7138A.M6233B19[511](P4258EBF.AFA7138A.M6233B19[579](CS$<>8__locals2, ldftn(<applyTweaks>b__2))), false);
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter3 = P4258EBF.AFA7138A.M6233B19[628](ref configuredTaskAwaitable);
						if (!configuredTaskAwaiter3.IsCompleted)
						{
							await configuredTaskAwaiter3;
							configuredTaskAwaiter3 = configuredTaskAwaiter2;
							configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						}
						configuredTaskAwaiter3.GetResult();
						CS$<>8__locals2 = null;
					}
					MainLogic.ClearStartupStateCache();
					return true;
				}
				CS$<>8__locals1 = null;
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
			}
			return false;
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0014C728 File Offset: 0x00149F28
		private static string LaunchParamsVdfToApi(string s)
		{
			if (P4258EBF.AFA7138A.M6233B19[88](s))
			{
				return s;
			}
			while (P4258EBF.AFA7138A.M6233B19[433](s, "\\\\\""))
			{
				s = P4258EBF.AFA7138A.M6233B19[114](s, "\\\\\"", "\\\"");
			}
			return P4258EBF.AFA7138A.M6233B19[114](s, "\\\"", "\"");
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0015A040 File Offset: 0x00157840
		private static string LaunchParamsApiToVdf(string s)
		{
			if (P4258EBF.AFA7138A.M6233B19[88](s))
			{
				return s;
			}
			while (P4258EBF.AFA7138A.M6233B19[433](s, "\\\\\""))
			{
				s = P4258EBF.AFA7138A.M6233B19[114](s, "\\\\\"", "\\\"");
			}
			s = P4258EBF.AFA7138A.M6233B19[114](s, "\\\"", "\"");
			return P4258EBF.AFA7138A.M6233B19[114](s, "\"", "\\\"");
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0014314C File Offset: 0x0014094C
		public static string NormalizeLaunchParamsForApi(string s)
		{
			if (P4258EBF.AFA7138A.M6233B19[88](s))
			{
				return s;
			}
			return P4258EBF.AFA7138A.M6233B19[114](s, "\\\\\"", "\\\"");
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00014510 File Offset: 0x00012910
		public static async Task<bool> applyGraphics(string data)
		{
			try
			{
				MainLogic.<>c__DisplayClass31_0 CS$<>8__locals1 = new MainLogic.<>c__DisplayClass31_0();
				MainLogic.ClearStartupStateCache();
				MainLogic.StartupStateRequest startupStateRequest = await Task.Run<MainLogic.StartupStateRequest>(() => MainLogic.BuildStartupStateRequest("graphics/set")).ConfigureAwait(false);
				CS$<>8__locals1.request = startupStateRequest;
				if (CS$<>8__locals1.request == null)
				{
					Logger.Log("Не выбрана папка для конфига.");
					return false;
				}
				var anon = new
				{
					client_cfg = CS$<>8__locals1.request.ClientContent,
					launch_params = CS$<>8__locals1.request.LaunchOptions,
					graphics = JsonConvert.DeserializeObject<MainLogic.GraphicsSettingsDTO>(data)
				};
				string text = P4258EBF.AFA7138A.M6233B19[412](anon, Formatting.Indented);
				StringContent stringContent = P4258EBF.AFA7138A.M6233B19[321](text, P4258EBF.AFA7138A.M6233B19[204](), "application/json");
				HttpResponseMessage httpResponseMessage = await P4258EBF.AFA7138A.M6233B19[583](CS$<>8__locals1.request.SecureHttp.GetClient(), "graphics/set", stringContent).ConfigureAwait(false);
				if (P4258EBF.AFA7138A.M6233B19[23](httpResponseMessage) == HttpStatusCode.OK)
				{
					CS$<>8__locals1.res = JsonConvert.DeserializeObject<MainLogic.Tweaks_SetResponseDto>(await P4258EBF.AFA7138A.M6233B19[623](P4258EBF.AFA7138A.M6233B19[405](httpResponseMessage)).ConfigureAwait(false));
					ConfiguredTaskAwaitable configuredTaskAwaitable = P4258EBF.AFA7138A.M6233B19[196](P4258EBF.AFA7138A.M6233B19[511](P4258EBF.AFA7138A.M6233B19[579](CS$<>8__locals1, ldftn(<applyGraphics>b__1))), false);
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter = P4258EBF.AFA7138A.M6233B19[628](ref configuredTaskAwaitable);
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
					if (!configuredTaskAwaiter.IsCompleted)
					{
						await configuredTaskAwaiter;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
					}
					configuredTaskAwaiter.GetResult();
					if (P4258EBF.AFA7138A.M6233B19[593](CS$<>8__locals1.request.LaunchOptions, CS$<>8__locals1.res.LaunchParams))
					{
						MainLogic.<>c__DisplayClass31_1 CS$<>8__locals2 = new MainLogic.<>c__DisplayClass31_1();
						CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
						CS$<>8__locals2.rustTweakerViewModel = new RustTweakerViewModel();
						TaskAwaiter taskAwaiter = P4258EBF.AFA7138A.M6233B19[515](CS$<>8__locals2.rustTweakerViewModel.CloseSteam(1000));
						if (!P4258EBF.AFA7138A.M6233B19[211](ref taskAwaiter))
						{
							await taskAwaiter;
							TaskAwaiter taskAwaiter2;
							taskAwaiter = taskAwaiter2;
							taskAwaiter2 = default(TaskAwaiter);
						}
						P4258EBF.AFA7138A.M6233B19[192](ref taskAwaiter);
						configuredTaskAwaitable = P4258EBF.AFA7138A.M6233B19[196](P4258EBF.AFA7138A.M6233B19[511](P4258EBF.AFA7138A.M6233B19[579](CS$<>8__locals2, ldftn(<applyGraphics>b__2))), false);
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter3 = P4258EBF.AFA7138A.M6233B19[628](ref configuredTaskAwaitable);
						if (!configuredTaskAwaiter3.IsCompleted)
						{
							await configuredTaskAwaiter3;
							configuredTaskAwaiter3 = configuredTaskAwaiter2;
							configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						}
						configuredTaskAwaiter3.GetResult();
						CS$<>8__locals2 = null;
					}
					MainLogic.ClearStartupStateCache();
					return true;
				}
				CS$<>8__locals1 = null;
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
			}
			return false;
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0015D830 File Offset: 0x0015B030
		private static string EscapeLaunchParamsForVdf(string s)
		{
			if (P4258EBF.AFA7138A.M6233B19[88](s))
			{
				return s;
			}
			s = P4258EBF.AFA7138A.M6233B19[114](s, "\\\"", "\"");
			return P4258EBF.AFA7138A.M6233B19[114](s, "\"", "\\\"");
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00014584 File Offset: 0x00012984
		public static async Task<string> getStartupTweaks()
		{
			string text;
			try
			{
				MainLogic.StartupStateRequest startupStateRequest = await Task.Run<MainLogic.StartupStateRequest>(() => MainLogic.BuildStartupStateRequest("tweaks/get")).ConfigureAwait(false);
				MainLogic.StartupStateRequest request = startupStateRequest;
				string text2;
				if (request == null)
				{
					Logger.Log("Не выбрана папка для конфига.");
					text = "{}";
				}
				else if (MainLogic.TryGetStartupStateCache(request.Signature, out text2))
				{
					text = text2;
				}
				else
				{
					var anon = new
					{
						client_cfg = request.ClientContent,
						launch_params = request.LaunchOptions
					};
					string text3 = P4258EBF.AFA7138A.M6233B19[412](anon, Formatting.Indented);
					StringContent stringContent = P4258EBF.AFA7138A.M6233B19[321](text3, P4258EBF.AFA7138A.M6233B19[204](), "application/json");
					HttpResponseMessage httpResponseMessage = await P4258EBF.AFA7138A.M6233B19[583](request.SecureHttp.GetClient(), "tweaks/get", stringContent).ConfigureAwait(false);
					string text4 = await P4258EBF.AFA7138A.M6233B19[623](P4258EBF.AFA7138A.M6233B19[405](httpResponseMessage)).ConfigureAwait(false);
					MainLogic.SetStartupStateCache(request.Signature, text4);
					text = text4;
				}
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
				text = null;
			}
			return text;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0015D538 File Offset: 0x0015AD38
		public static string Norm(string s)
		{
			if (s == null)
			{
				return null;
			}
			return P4258EBF.AFA7138A.M6233B19[172](P4258EBF.AFA7138A.M6233B19[114](P4258EBF.AFA7138A.M6233B19[597](s), "\\\"", "\""), '"');
		}

		// Token: 0x06000390 RID: 912 RVA: 0x000145E4 File Offset: 0x000129E4
		public static async Task<string> getStartupGraphics()
		{
			string text;
			try
			{
				MainLogic.StartupStateRequest startupStateRequest = await Task.Run<MainLogic.StartupStateRequest>(() => MainLogic.BuildStartupStateRequest("graphics/get")).ConfigureAwait(false);
				MainLogic.StartupStateRequest request = startupStateRequest;
				string text2;
				if (request == null)
				{
					Logger.Log("Не выбрана папка для конфига.");
					text = "{}";
				}
				else if (MainLogic.TryGetStartupStateCache(request.Signature, out text2))
				{
					text = text2;
				}
				else
				{
					var anon = new
					{
						client_cfg = request.ClientContent,
						launch_params = request.LaunchOptions
					};
					string text3 = P4258EBF.AFA7138A.M6233B19[412](anon, Formatting.Indented);
					StringContent stringContent = P4258EBF.AFA7138A.M6233B19[321](text3, P4258EBF.AFA7138A.M6233B19[204](), "application/json");
					HttpResponseMessage httpResponseMessage = await P4258EBF.AFA7138A.M6233B19[583](request.SecureHttp.GetClient(), "graphics/get", stringContent).ConfigureAwait(false);
					string text4 = await P4258EBF.AFA7138A.M6233B19[623](P4258EBF.AFA7138A.M6233B19[405](httpResponseMessage)).ConfigureAwait(false);
					MainLogic.SetStartupStateCache(request.Signature, text4);
					text = text4;
				}
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
				text = null;
			}
			return text;
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00152D6C File Offset: 0x0015056C
		public static bool resetGraphics()
		{
			Logger.Log("RESET GRAPHICS");
			try
			{
				MainLogic.ClearStartupStateCache();
				MainLogic.ReadOnlyCheck();
				List<FileActions.PathNode> list = JsonConvert.DeserializeObject<List<FileActions.PathNode>>(P4258EBF.AFA7138A.M6233B19[267](P4258EBF.AFA7138A.M6233B19[158](MainLogic.appDataPath, "paths.json")));
				FileActions.PathNode pathNode = list.FirstOrDefault<FileActions.PathNode>((FileActions.PathNode item) => item.is_select);
				string text = ((pathNode != null) ? pathNode.folder : null);
				if (P4258EBF.AFA7138A.M6233B19[88](text))
				{
					Logger.Log("Не выбрана папка для конфига.");
					return false;
				}
				List<Params.Node> list2 = (from line in P4258EBF.AFA7138A.M6233B19[65](P4258EBF.AFA7138A.M6233B19[278](text, "cfg", "client.cfg"))
					select P4258EBF.AFA7138A.M6233B19[597](line) into line
					where !P4258EBF.AFA7138A.M6233B19[88](line)
					select line).Select<string, Params.Node>(delegate(string line)
				{
					string[] array3 = ID887B86.N59E432C(line, new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
					string text2 = MainLogic.Norm(array3[0]);
					string text3 = ((array3.Length > 1) ? MainLogic.Norm(array3[1]) : "");
					return new Params.Node(text2, text3);
				}).ToList<Params.Node>();
				RustTweakerViewModel rustTweakerViewModel = new RustTweakerViewModel();
				List<ValueTuple<string, string>> list3 = MainLogic.ParseLaunchParams(rustTweakerViewModel.ExtractVdfValue(P4258EBF.AFA7138A.M6233B19[267](MainLogic.SteamParser.GetConfigPathToLastUser()), "LaunchOptions", 0));
				foreach (KeyValuePair<string, Dictionary<int, Params.TweakNode>> keyValuePair in Params.Graphics)
				{
					Dictionary<int, Params.TweakNode> value = keyValuePair.Value;
					foreach (KeyValuePair<int, Params.TweakNode> keyValuePair2 in value)
					{
						Params.Node[] config_params = keyValuePair2.Value.config_params;
						if (config_params != null)
						{
							Params.Node[] array = config_params;
							for (int i = 0; i < array.Length; i++)
							{
								Params.Node oneParam2 = array[i];
								list2.RemoveAll((Params.Node n) => P4258EBF.AFA7138A.M6233B19[250](n.key, oneParam2.key));
							}
						}
						Params.Node[] launch_params = keyValuePair2.Value.launch_params;
						if (launch_params != null)
						{
							Params.Node[] array2 = launch_params;
							for (int j = 0; j < array2.Length; j++)
							{
								Params.Node oneParam = array2[j];
								if (list3.Any<ValueTuple<string, string>>(([TupleElementNames(new string[] { "Key", "Value" })] ValueTuple<string, string> x) => P4258EBF.AFA7138A.M6233B19[250](x.Item1, oneParam.key)))
								{
									list3.RemoveAll(([TupleElementNames(new string[] { "Key", "Value" })] ValueTuple<string, string> x) => P4258EBF.AFA7138A.M6233B19[250](x.Item1, oneParam.key));
								}
							}
						}
					}
				}
				N4AA191F.L58EEE80(P4258EBF.AFA7138A.M6233B19[478](text, "\\cfg\\client.cfg"), list2.Select<Params.Node, string>((Params.Node node) => P4258EBF.AFA7138A.M6233B19[64](node.key, " ", node.value)));
				rustTweakerViewModel.UpdateLocalConfig(MainLogic.SteamParser.GetConfigPathToLastUser(), MainLogic.BuildLaunchParams(list3));
			}
			catch (Exception ex)
			{
				Logger.Log(P4258EBF.AFA7138A.M6233B19[478]("Error then resetGraphics: ", P4258EBF.AFA7138A.M6233B19[551](ex)));
				Logger.Log(ex);
			}
			return false;
		}

		// Token: 0x06000392 RID: 914 RVA: 0x001471B8 File Offset: 0x001449B8
		[return: TupleElementNames(new string[] { "Key", "Value" })]
		public static List<ValueTuple<string, string>> ParseLaunchParams(string input)
		{
			List<ValueTuple<string, string>> list = new List<ValueTuple<string, string>>();
			if (!P4258EBF.AFA7138A.M6233B19[88](input))
			{
				string[] array = P4258EBF.AFA7138A.M6233B19[141](input, '-', StringSplitOptions.None);
				if (array.Length != 0)
				{
					foreach (string text in array)
					{
						string text2 = "";
						int num = P4258EBF.AFA7138A.M6233B19[317](text, ' ');
						string text3;
						if (num != -1)
						{
							text3 = P4258EBF.AFA7138A.M6233B19[487](text, 0, num);
							text2 = P4258EBF.AFA7138A.M6233B19[398](text, num);
						}
						else
						{
							text3 = text;
						}
						if (!P4258EBF.AFA7138A.M6233B19[426](P4258EBF.AFA7138A.M6233B19[597](text3)))
						{
							list.Add(new ValueTuple<string, string>(P4258EBF.AFA7138A.M6233B19[597](text3), P4258EBF.AFA7138A.M6233B19[597](text2)));
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0015FF00 File Offset: 0x0015D700
		public static string BuildLaunchParams([TupleElementNames(new string[] { "Key", "Value" })] List<ValueTuple<string, string>> list)
		{
			return D2B9D912.A91E8BBB(" ", list.Select<ValueTuple<string, string>, string>(([TupleElementNames(new string[] { "Key", "Value" })] ValueTuple<string, string> x) => P4258EBF.AFA7138A.M6233B19[259]("-", x.Item1, " ", P4258EBF.AFA7138A.M6233B19[114](x.Item2, "\\\"", "\""))));
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0015A3C8 File Offset: 0x00157BC8
		public static bool SteamIsRunning()
		{
			Process[] array = P4258EBF.AFA7138A.M6233B19[98]("steam");
			Process[] array2 = P4258EBF.AFA7138A.M6233B19[98]("Rust");
			Process[] array3 = P4258EBF.AFA7138A.M6233B19[98]("RustClient");
			return array.Length != 0 || array3.Length != 0 || array2.Length != 0;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00151DC0 File Offset: 0x0014F5C0
		public static bool HaveNewLaunchParamsForTweaks(string _json)
		{
			JObject payload = JsonConvert.DeserializeObject<Controller._GlobalActionType>(_json).Payload;
			string result = MainLogic.getStartupTweaks().GetAwaiter().GetResult();
			JObject jobject = P4258EBF.AFA7138A.M6233B19[621](result);
			using (IEnumerator<KeyValuePair<string, JToken>> enumerator = P4258EBF.AFA7138A.M6233B19[445](jobject))
			{
				while (P4258EBF.AFA7138A.M6233B19[411](enumerator))
				{
					KeyValuePair<string, JToken> keyValuePair = enumerator.Current;
					string key = keyValuePair.Key;
					string text = keyValuePair.Value.Value<string>();
					string text2 = P4258EBF.AFA7138A.M6233B19[327](P4258EBF.AFA7138A.M6233B19[126](payload, key), "value").Value<string>();
					if (P4258EBF.AFA7138A.M6233B19[593](text2, text) && Params.Tweaks.First<KeyValuePair<string, Params.TweakNode>>((KeyValuePair<string, Params.TweakNode> x) => P4258EBF.AFA7138A.M6233B19[250](x.Key, key)).Value.launch_params != null)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0014E184 File Offset: 0x0014B984
		public static void WriteAllText(string path, string content, Encoding encoding = null)
		{
			if (encoding == null)
			{
				encoding = P4258EBF.AFA7138A.M6233B19[204]();
			}
			using (FileStream fileStream = P4258EBF.AFA7138A.M6233B19[71](path, FileMode.Create, FileAccess.Write, FileShare.None, 4096, false))
			{
				byte[] array = P4258EBF.AFA7138A.M6233B19[240](encoding, content);
				P4258EBF.AFA7138A.M6233B19[0](fileStream, array, 0, array.Length);
				P4258EBF.AFA7138A.M6233B19[215](fileStream, true);
			}
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0015F248 File Offset: 0x0015CA48
		public static void StartSteam()
		{
			RustTweakerViewModel rustTweakerViewModel = new RustTweakerViewModel();
			Process process = P4258EBF.AFA7138A.M6233B19[603]();
			ProcessStartInfo processStartInfo = P4258EBF.AFA7138A.M6233B19[394]();
			N62EB38A.CB1145A6(processStartInfo, P4258EBF.AFA7138A.M6233B19[158](rustTweakerViewModel._steamPath, "steam.exe"));
			O8258311.M5A8918D(processStartInfo, true);
			P2B8E68F.OC24431D(process, processStartInfo);
			Process process2 = process;
			Logger.Log(P4258EBF.AFA7138A.M6233B19[316](P4258EBF.AFA7138A.M6233B19[40](process2)));
			P4258EBF.AFA7138A.M6233B19[524](process2);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00151710 File Offset: 0x0014EF10
		public static void StartRust()
		{
			try
			{
				Configs.PathsNode pathsNode = Configs.ParsePathsConfig(null).FirstOrDefault<Configs.PathsNode>((Configs.PathsNode x) => x.is_select);
				Logger.Log(P4258EBF.AFA7138A.M6233B19[478]("INFO: StartRust: currentPath = ", pathsNode.folder));
				if (pathsNode == null)
				{
					Logger.Log("ERROR: Current folder cant find!");
				}
				else
				{
					string text = P4258EBF.AFA7138A.M6233B19[513](pathsNode.folder);
					if (!P4258EBF.AFA7138A.M6233B19[250](text, "Rust"))
					{
						if (!P4258EBF.AFA7138A.M6233B19[250](text, "RustStaging"))
						{
							P4258EBF.AFA7138A.M6233B19[361](P4258EBF.AFA7138A.M6233B19[158](pathsNode.folder, "Rust.exe"));
						}
						else
						{
							P4258EBF.AFA7138A.M6233B19[361]("steam://rungameid/700580");
						}
					}
					else
					{
						P4258EBF.AFA7138A.M6233B19[361]("steam://rungameid/252490");
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Log("ERROR: StartRust");
				Logger.Log(ex);
			}
		}

		// Token: 0x040000FD RID: 253
		public static string graphicsShurtcut = "\\cfg\\client.cfg";

		// Token: 0x040000FF RID: 255
		public static string appDataPath = P4258EBF.AFA7138A.M6233B19[158](P4258EBF.AFA7138A.M6233B19[54](Environment.SpecialFolder.ApplicationData), "RustTweaker");

		// Token: 0x04000100 RID: 256
		private static readonly TimeSpan StartupStateCacheTtl = P4258EBF.AFA7138A.M6233B19[395](3L);

		// Token: 0x04000101 RID: 257
		private static readonly object StartupStateCacheLock = P4258EBF.AFA7138A.M6233B19[131]();

		// Token: 0x04000102 RID: 258
		private static readonly Dictionary<string, MainLogic.StartupStateCacheEntry> StartupStateCache = new Dictionary<string, MainLogic.StartupStateCacheEntry>();

		// Token: 0x02000120 RID: 288
		private sealed class StartupStateCacheEntry
		{
			// Token: 0x170000D9 RID: 217
			// (get) Token: 0x060005F4 RID: 1524 RVA: 0x00020E02 File Offset: 0x0001F202
			// (set) Token: 0x060005F5 RID: 1525 RVA: 0x00020E0A File Offset: 0x0001F20A
			public string Response { get; set; }

			// Token: 0x170000DA RID: 218
			// (get) Token: 0x060005F6 RID: 1526 RVA: 0x00020E13 File Offset: 0x0001F213
			// (set) Token: 0x060005F7 RID: 1527 RVA: 0x00020E1B File Offset: 0x0001F21B
			public DateTime CreatedAtUtc { get; set; }

			// Token: 0x060005F8 RID: 1528 RVA: 0x001619D0 File Offset: 0x0015F1D0
			public StartupStateCacheEntry()
			{
				P4258EBF.AFA7138A.M6233B19[130](this);
			}
		}

		// Token: 0x02000121 RID: 289
		private sealed class StartupStateRequest
		{
			// Token: 0x170000DB RID: 219
			// (get) Token: 0x060005F9 RID: 1529 RVA: 0x00020E2C File Offset: 0x0001F22C
			// (set) Token: 0x060005FA RID: 1530 RVA: 0x00020E34 File Offset: 0x0001F234
			public string Endpoint { get; set; }

			// Token: 0x170000DC RID: 220
			// (get) Token: 0x060005FB RID: 1531 RVA: 0x00020E3D File Offset: 0x0001F23D
			// (set) Token: 0x060005FC RID: 1532 RVA: 0x00020E45 File Offset: 0x0001F245
			public string SelectedFolder { get; set; }

			// Token: 0x170000DD RID: 221
			// (get) Token: 0x060005FD RID: 1533 RVA: 0x00020E4E File Offset: 0x0001F24E
			// (set) Token: 0x060005FE RID: 1534 RVA: 0x00020E56 File Offset: 0x0001F256
			public string ClientContent { get; set; }

			// Token: 0x170000DE RID: 222
			// (get) Token: 0x060005FF RID: 1535 RVA: 0x00020E5F File Offset: 0x0001F25F
			// (set) Token: 0x06000600 RID: 1536 RVA: 0x00020E67 File Offset: 0x0001F267
			public string LaunchOptions { get; set; }

			// Token: 0x170000DF RID: 223
			// (get) Token: 0x06000601 RID: 1537 RVA: 0x00020E70 File Offset: 0x0001F270
			// (set) Token: 0x06000602 RID: 1538 RVA: 0x00020E78 File Offset: 0x0001F278
			public SecureHttp SecureHttp { get; set; }

			// Token: 0x170000E0 RID: 224
			// (get) Token: 0x06000603 RID: 1539 RVA: 0x001600B8 File Offset: 0x0015D8B8
			public unsafe string Signature
			{
				get
				{
					MABE489E mabe489E = P4258EBF.AFA7138A.M6233B19[315];
					string text = "\n";
					InlineArray4<string> inlineArray = default(InlineArray4<string>);
					*<PrivateImplementationDetails>.InlineArrayElementRef<InlineArray4<string>, string>(ref inlineArray, 0) = this.Endpoint;
					*<PrivateImplementationDetails>.InlineArrayElementRef<InlineArray4<string>, string>(ref inlineArray, 1) = this.SelectedFolder;
					*<PrivateImplementationDetails>.InlineArrayElementRef<InlineArray4<string>, string>(ref inlineArray, 2) = this.ClientContent;
					*<PrivateImplementationDetails>.InlineArrayElementRef<InlineArray4<string>, string>(ref inlineArray, 3) = this.LaunchOptions;
					return mabe489E(text, <PrivateImplementationDetails>.InlineArrayAsReadOnlySpan<InlineArray4<string>, string>(in inlineArray, 4));
				}
			}

			// Token: 0x06000604 RID: 1540 RVA: 0x00160B8C File Offset: 0x0015E38C
			public StartupStateRequest()
			{
				P4258EBF.AFA7138A.M6233B19[130](this);
			}
		}

		// Token: 0x02000122 RID: 290
		public class GraphicsSettingsDTO
		{
			// Token: 0x170000E1 RID: 225
			// (get) Token: 0x06000605 RID: 1541 RVA: 0x00020EEF File Offset: 0x0001F2EF
			// (set) Token: 0x06000606 RID: 1542 RVA: 0x00020EF7 File Offset: 0x0001F2F7
			[JsonProperty("shadows")]
			public int Shadows { get; set; }

			// Token: 0x170000E2 RID: 226
			// (get) Token: 0x06000607 RID: 1543 RVA: 0x00020F00 File Offset: 0x0001F300
			// (set) Token: 0x06000608 RID: 1544 RVA: 0x00020F08 File Offset: 0x0001F308
			[JsonProperty("textures")]
			public int Textures { get; set; }

			// Token: 0x170000E3 RID: 227
			// (get) Token: 0x06000609 RID: 1545 RVA: 0x00020F11 File Offset: 0x0001F311
			// (set) Token: 0x0600060A RID: 1546 RVA: 0x00020F19 File Offset: 0x0001F319
			[JsonProperty("lighting")]
			public int Lighting { get; set; }

			// Token: 0x170000E4 RID: 228
			// (get) Token: 0x0600060B RID: 1547 RVA: 0x00020F22 File Offset: 0x0001F322
			// (set) Token: 0x0600060C RID: 1548 RVA: 0x00020F2A File Offset: 0x0001F32A
			[JsonProperty("trees")]
			public int Trees { get; set; }

			// Token: 0x170000E5 RID: 229
			// (get) Token: 0x0600060D RID: 1549 RVA: 0x00020F33 File Offset: 0x0001F333
			// (set) Token: 0x0600060E RID: 1550 RVA: 0x00020F3B File Offset: 0x0001F33B
			[JsonProperty("reflections_on_the_water")]
			public int ReflectionsOnTheWater { get; set; }

			// Token: 0x170000E6 RID: 230
			// (get) Token: 0x0600060F RID: 1551 RVA: 0x00020F44 File Offset: 0x0001F344
			// (set) Token: 0x06000610 RID: 1552 RVA: 0x00020F4C File Offset: 0x0001F34C
			[JsonProperty("grass")]
			public int Grass { get; set; }

			// Token: 0x170000E7 RID: 231
			// (get) Token: 0x06000611 RID: 1553 RVA: 0x00020F55 File Offset: 0x0001F355
			// (set) Token: 0x06000612 RID: 1554 RVA: 0x00020F5D File Offset: 0x0001F35D
			[JsonProperty("clouds")]
			public int Clouds { get; set; }

			// Token: 0x170000E8 RID: 232
			// (get) Token: 0x06000613 RID: 1555 RVA: 0x00020F66 File Offset: 0x0001F366
			// (set) Token: 0x06000614 RID: 1556 RVA: 0x00020F6E File Offset: 0x0001F36E
			[JsonProperty("smoothing")]
			public int Smoothing { get; set; }

			// Token: 0x06000615 RID: 1557 RVA: 0x00157588 File Offset: 0x00154D88
			public GraphicsSettingsDTO()
			{
				P4258EBF.AFA7138A.M6233B19[130](this);
			}
		}

		// Token: 0x02000123 RID: 291
		public class TweaksSettingsDTO
		{
			// Token: 0x170000E9 RID: 233
			// (get) Token: 0x06000616 RID: 1558 RVA: 0x00020F7F File Offset: 0x0001F37F
			// (set) Token: 0x06000617 RID: 1559 RVA: 0x00020F87 File Offset: 0x0001F387
			[JsonProperty("trash_settings")]
			public string TrashSettings { get; set; }

			// Token: 0x170000EA RID: 234
			// (get) Token: 0x06000618 RID: 1560 RVA: 0x00020F90 File Offset: 0x0001F390
			// (set) Token: 0x06000619 RID: 1561 RVA: 0x00020F98 File Offset: 0x0001F398
			[JsonProperty("legs_vision")]
			public string LegsVision { get; set; }

			// Token: 0x170000EB RID: 235
			// (get) Token: 0x0600061A RID: 1562 RVA: 0x00020FA1 File Offset: 0x0001F3A1
			// (set) Token: 0x0600061B RID: 1563 RVA: 0x00020FA9 File Offset: 0x0001F3A9
			[JsonProperty("shake_cam")]
			public string ShakeCam { get; set; }

			// Token: 0x170000EC RID: 236
			// (get) Token: 0x0600061C RID: 1564 RVA: 0x00020FB2 File Offset: 0x0001F3B2
			// (set) Token: 0x0600061D RID: 1565 RVA: 0x00020FBA File Offset: 0x0001F3BA
			[JsonProperty("cross_on_the_threes")]
			public string CrossOnTheThrees { get; set; }

			// Token: 0x170000ED RID: 237
			// (get) Token: 0x0600061E RID: 1566 RVA: 0x00020FC3 File Offset: 0x0001F3C3
			// (set) Token: 0x0600061F RID: 1567 RVA: 0x00020FCB File Offset: 0x0001F3CB
			[JsonProperty("disable_secure_occlusion")]
			public string DisableSecureOcclusion { get; set; }

			// Token: 0x170000EE RID: 238
			// (get) Token: 0x06000620 RID: 1568 RVA: 0x00020FD4 File Offset: 0x0001F3D4
			// (set) Token: 0x06000621 RID: 1569 RVA: 0x00020FDC File Offset: 0x0001F3DC
			[JsonProperty("disable_the_wreckage")]
			public string DisableTheWreckage { get; set; }

			// Token: 0x170000EF RID: 239
			// (get) Token: 0x06000622 RID: 1570 RVA: 0x00020FE5 File Offset: 0x0001F3E5
			// (set) Token: 0x06000623 RID: 1571 RVA: 0x00020FED File Offset: 0x0001F3ED
			[JsonProperty("disable_eyes_animation")]
			public string DisableEyesAnimation { get; set; }

			// Token: 0x170000F0 RID: 240
			// (get) Token: 0x06000624 RID: 1572 RVA: 0x00020FF6 File Offset: 0x0001F3F6
			// (set) Token: 0x06000625 RID: 1573 RVA: 0x00020FFE File Offset: 0x0001F3FE
			[JsonProperty("disable_leg_deformity")]
			public string DisableLegDeformity { get; set; }

			// Token: 0x170000F1 RID: 241
			// (get) Token: 0x06000626 RID: 1574 RVA: 0x00021007 File Offset: 0x0001F407
			// (set) Token: 0x06000627 RID: 1575 RVA: 0x0002100F File Offset: 0x0001F40F
			[JsonProperty("disable_stroboscope")]
			public string DisableStroboscope { get; set; }

			// Token: 0x170000F2 RID: 242
			// (get) Token: 0x06000628 RID: 1576 RVA: 0x00021018 File Offset: 0x0001F418
			// (set) Token: 0x06000629 RID: 1577 RVA: 0x00021020 File Offset: 0x0001F420
			[JsonProperty("fast_head_rotate")]
			public string FastHeadRotate { get; set; }

			// Token: 0x170000F3 RID: 243
			// (get) Token: 0x0600062A RID: 1578 RVA: 0x00021029 File Offset: 0x0001F429
			// (set) Token: 0x0600062B RID: 1579 RVA: 0x00021031 File Offset: 0x0001F431
			[JsonProperty("return_events_textannounce")]
			public string ReturnEventsTextannounce { get; set; }

			// Token: 0x170000F4 RID: 244
			// (get) Token: 0x0600062C RID: 1580 RVA: 0x0002103A File Offset: 0x0001F43A
			// (set) Token: 0x0600062D RID: 1581 RVA: 0x00021042 File Offset: 0x0001F442
			[JsonProperty("disable_craft_delay")]
			public string DisableCraftDelay { get; set; }

			// Token: 0x170000F5 RID: 245
			// (get) Token: 0x0600062E RID: 1582 RVA: 0x0002104B File Offset: 0x0001F44B
			// (set) Token: 0x0600062F RID: 1583 RVA: 0x00021053 File Offset: 0x0001F453
			[JsonProperty("smalltime_bag_unclaim")]
			public string SmalltimeBagUnclaim { get; set; }

			// Token: 0x170000F6 RID: 246
			// (get) Token: 0x06000630 RID: 1584 RVA: 0x0002105C File Offset: 0x0001F45C
			// (set) Token: 0x06000631 RID: 1585 RVA: 0x00021064 File Offset: 0x0001F464
			[JsonProperty("add_map_info")]
			public string AddMapInfo { get; set; }

			// Token: 0x170000F7 RID: 247
			// (get) Token: 0x06000632 RID: 1586 RVA: 0x0002106D File Offset: 0x0001F46D
			// (set) Token: 0x06000633 RID: 1587 RVA: 0x00021075 File Offset: 0x0001F475
			[JsonProperty("disable_show_errors")]
			public string DisableShowErrors { get; set; }

			// Token: 0x170000F8 RID: 248
			// (get) Token: 0x06000634 RID: 1588 RVA: 0x0002107E File Offset: 0x0001F47E
			// (set) Token: 0x06000635 RID: 1589 RVA: 0x00021086 File Offset: 0x0001F486
			[JsonProperty("add_admin_gestures")]
			public string AddAdminGestures { get; set; }

			// Token: 0x170000F9 RID: 249
			// (get) Token: 0x06000636 RID: 1590 RVA: 0x0002108F File Offset: 0x0001F48F
			// (set) Token: 0x06000637 RID: 1591 RVA: 0x00021097 File Offset: 0x0001F497
			[JsonProperty("server_hitmarks")]
			public string ServerHitmarks { get; set; }

			// Token: 0x170000FA RID: 250
			// (get) Token: 0x06000638 RID: 1592 RVA: 0x000210A0 File Offset: 0x0001F4A0
			// (set) Token: 0x06000639 RID: 1593 RVA: 0x000210A8 File Offset: 0x0001F4A8
			[JsonProperty("old_announce_for_take_item")]
			public string OldAnnounceForTakeItem { get; set; }

			// Token: 0x170000FB RID: 251
			// (get) Token: 0x0600063A RID: 1594 RVA: 0x000210B1 File Offset: 0x0001F4B1
			// (set) Token: 0x0600063B RID: 1595 RVA: 0x000210B9 File Offset: 0x0001F4B9
			[JsonProperty("small_time_use_menu")]
			public string SmallTimeUseMenu { get; set; }

			// Token: 0x170000FC RID: 252
			// (get) Token: 0x0600063C RID: 1596 RVA: 0x000210C2 File Offset: 0x0001F4C2
			// (set) Token: 0x0600063D RID: 1597 RVA: 0x000210CA File Offset: 0x0001F4CA
			[JsonProperty("admin_tp")]
			public string AdminTp { get; set; }

			// Token: 0x170000FD RID: 253
			// (get) Token: 0x0600063E RID: 1598 RVA: 0x000210D3 File Offset: 0x0001F4D3
			// (set) Token: 0x0600063F RID: 1599 RVA: 0x000210DB File Offset: 0x0001F4DB
			[JsonProperty("small_item_in_hand")]
			public string SmallItemInHand { get; set; }

			// Token: 0x170000FE RID: 254
			// (get) Token: 0x06000640 RID: 1600 RVA: 0x000210E4 File Offset: 0x0001F4E4
			// (set) Token: 0x06000641 RID: 1601 RVA: 0x000210EC File Offset: 0x0001F4EC
			[JsonProperty("convenient_skin_sorting")]
			public string ConvenientSkinSorting { get; set; }

			// Token: 0x170000FF RID: 255
			// (get) Token: 0x06000642 RID: 1602 RVA: 0x000210F5 File Offset: 0x0001F4F5
			// (set) Token: 0x06000643 RID: 1603 RVA: 0x000210FD File Offset: 0x0001F4FD
			[JsonProperty("enlarged_console")]
			public string EnlargedConsole { get; set; }

			// Token: 0x17000100 RID: 256
			// (get) Token: 0x06000644 RID: 1604 RVA: 0x00021106 File Offset: 0x0001F506
			// (set) Token: 0x06000645 RID: 1605 RVA: 0x0002110E File Offset: 0x0001F50E
			[JsonProperty("left_handed")]
			public string LeftHanded { get; set; }

			// Token: 0x17000101 RID: 257
			// (get) Token: 0x06000646 RID: 1606 RVA: 0x00021117 File Offset: 0x0001F517
			// (set) Token: 0x06000647 RID: 1607 RVA: 0x0002111F File Offset: 0x0001F51F
			[JsonProperty("glass_reflection")]
			public string GlassReflection { get; set; }

			// Token: 0x06000648 RID: 1608 RVA: 0x0015CB14 File Offset: 0x0015A314
			public TweaksSettingsDTO()
			{
				P4258EBF.AFA7138A.M6233B19[130](this);
			}
		}

		// Token: 0x02000124 RID: 292
		public class Tweaks_SetResponseDto
		{
			// Token: 0x17000102 RID: 258
			// (get) Token: 0x06000649 RID: 1609 RVA: 0x00021130 File Offset: 0x0001F530
			// (set) Token: 0x0600064A RID: 1610 RVA: 0x00021138 File Offset: 0x0001F538
			[JsonProperty("client_cfg")]
			public string ClientCfg { get; set; }

			// Token: 0x17000103 RID: 259
			// (get) Token: 0x0600064B RID: 1611 RVA: 0x00021141 File Offset: 0x0001F541
			// (set) Token: 0x0600064C RID: 1612 RVA: 0x00021149 File Offset: 0x0001F549
			[JsonProperty("launch_params")]
			public string LaunchParams { get; set; }

			// Token: 0x0600064D RID: 1613 RVA: 0x00151C1C File Offset: 0x0014F41C
			public Tweaks_SetResponseDto()
			{
				P4258EBF.AFA7138A.M6233B19[130](this);
			}
		}

		// Token: 0x02000125 RID: 293
		public static class SteamParser
		{
			// Token: 0x0600064E RID: 1614 RVA: 0x00154960 File Offset: 0x00152160
			public static string GetConfigPathToLastUser()
			{
				RustTweakerViewModel rustTweakerViewModel = new RustTweakerViewModel();
				string lastUsedSteamId = MainLogic.SteamParser.GetLastUsedSteamId(P4258EBF.AFA7138A.M6233B19[267](P4258EBF.AFA7138A.M6233B19[478](rustTweakerViewModel._steamPath, "\\config\\loginusers.vdf")));
				long num = MainLogic.SteamParser.SteamId64ToAccountId(P4258EBF.AFA7138A.M6233B19[257](lastUsedSteamId));
				string text = P4258EBF.AFA7138A.M6233B19[277](ref num);
				return P4258EBF.AFA7138A.M6233B19[259](rustTweakerViewModel._steamPath, "\\userdata\\", text, "\\config\\localconfig.vdf");
			}

			// Token: 0x0600064F RID: 1615 RVA: 0x000211B4 File Offset: 0x0001F5B4
			public static long SteamId64ToAccountId(long steamId64)
			{
				return steamId64 - 76561197960265728L;
			}

			// Token: 0x06000650 RID: 1616 RVA: 0x00156664 File Offset: 0x00153E64
			public static string GetLastUsedSteamId(string vdfText)
			{
				Regex regex = P4258EBF.AFA7138A.M6233B19[456]("\"(\\d+)\"\\s*\\{([^}]*)\\}", RegexOptions.Singleline);
				string text = null;
				long num = long.MinValue;
				using (IEnumerator enumerator = P4258EBF.AFA7138A.M6233B19[624](P4258EBF.AFA7138A.M6233B19[408](regex, vdfText)))
				{
					while (P4258EBF.AFA7138A.M6233B19[411](enumerator))
					{
						Match match = (Match)P4258EBF.AFA7138A.M6233B19[606](enumerator);
						string text2 = P4258EBF.AFA7138A.M6233B19[372](P4258EBF.AFA7138A.M6233B19[212](P4258EBF.AFA7138A.M6233B19[614](match), 1));
						string text3 = P4258EBF.AFA7138A.M6233B19[372](P4258EBF.AFA7138A.M6233B19[212](P4258EBF.AFA7138A.M6233B19[614](match), 2));
						if (P4258EBF.AFA7138A.M6233B19[611](P4258EBF.AFA7138A.M6233B19[577](text3), "\"mostrecent\"\\s*\"1\""))
						{
							return text2;
						}
						Match match2 = P4258EBF.AFA7138A.M6233B19[458](text3, "\"Timestamp\"\\s*\"(\\d+)\"", RegexOptions.IgnoreCase);
						long num2;
						if (P4258EBF.AFA7138A.M6233B19[494](match2) && P4258EBF.AFA7138A.M6233B19[472](P4258EBF.AFA7138A.M6233B19[372](P4258EBF.AFA7138A.M6233B19[212](P4258EBF.AFA7138A.M6233B19[614](match2), 1)), ref num2) && num2 > num)
						{
							num = num2;
							text = text2;
						}
					}
				}
				return text;
			}
		}
	}
}
