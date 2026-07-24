using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RustTweaker;
using RustTweaker.Model;
using RustTweakerDemo;
using SteamGameSearcher;
using WpfApp1.Model;

namespace WpfApp1
{
	// Token: 0x02000056 RID: 86
	[ComVisible(true)]
	public class JsBridge
	{
		// Token: 0x060002E5 RID: 741 RVA: 0x00154B70 File Offset: 0x00152370
		public void RestartComputer()
		{
			JsBridge.<RestartComputer>d__0 <RestartComputer>d__;
			<RestartComputer>d__.<>t__builder = P4258EBF.AFA7138A.M6233B19[385]();
			<RestartComputer>d__.<>1__state = -1;
			<RestartComputer>d__.<>t__builder.Start<JsBridge.<RestartComputer>d__0>(ref <RestartComputer>d__);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000EFDB File Offset: 0x0000D3DB
		public bool CreateBackupToConfig(string path)
		{
			return Configs.CreateBackupToConfig(path);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000EFE3 File Offset: 0x0000D3E3
		public bool CreateBackupToBind(string path)
		{
			return Configs.CreateBackupToBind(path);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000EFEB File Offset: 0x0000D3EB
		public string GetEmail()
		{
			return App.EMAIL;
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000EFF2 File Offset: 0x0000D3F2
		public string GetSteamId()
		{
			return App.STEAMTID;
		}

		// Token: 0x060002EA RID: 746 RVA: 0x001580AC File Offset: 0x001558AC
		public bool UpdateCfgToCurrentSettings(string path)
		{
			try
			{
				string currentSelectedFolder = Configs.getCurrentSelectedFolder();
				string text = P4258EBF.AFA7138A.M6233B19[158](WpfApp1.Model.MainLogic.appDataPath, "configs.json");
				string text2 = P4258EBF.AFA7138A.M6233B19[278](currentSelectedFolder, "cfg", "client.cfg");
				List<Configs.ConfigNode> list = JsonConvert.DeserializeObject<List<Configs.ConfigNode>>(P4258EBF.AFA7138A.M6233B19[267](text));
				if (!P4258EBF.AFA7138A.M6233B19[88](currentSelectedFolder) && list.Any<Configs.ConfigNode>((Configs.ConfigNode x) => P4258EBF.AFA7138A.M6233B19[250](x.content, path)) && P4258EBF.AFA7138A.M6233B19[627](text2))
				{
					Configs.ConfigNode configNode = list.Find((Configs.ConfigNode x) => x.is_select);
					P4258EBF.AFA7138A.M6233B19[473](text2, configNode.content, true);
					configNode.launch_params = this.getCurrentParamsLaunch();
					P4258EBF.AFA7138A.M6233B19[94](text, P4258EBF.AFA7138A.M6233B19[330](list));
					return true;
				}
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
			}
			return false;
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000F0E8 File Offset: 0x0000D4E8
		public async Task<float> getCurrentSensivity()
		{
			List<FileActions.PathNode> list = JsonConvert.DeserializeObject<List<FileActions.PathNode>>(P4258EBF.AFA7138A.M6233B19[267](P4258EBF.AFA7138A.M6233B19[478](WpfApp1.Model.MainLogic.appDataPath, "\\paths.json")));
			FileActions.PathNode pathNode = list.FirstOrDefault<FileActions.PathNode>((FileActions.PathNode item) => item.is_select);
			string text = ((pathNode != null) ? pathNode.folder : null);
			float num = 1f;
			if (!P4258EBF.AFA7138A.M6233B19[88](text) && P4258EBF.AFA7138A.M6233B19[89](text))
			{
				string[] array = P4258EBF.AFA7138A.M6233B19[65](P4258EBF.AFA7138A.M6233B19[278](text, "cfg", "client.cfg"));
				foreach (string text2 in array)
				{
					if (P4258EBF.AFA7138A.M6233B19[433](text2, "ads_sensitivity"))
					{
						try
						{
							string[] array3 = P4258EBF.AFA7138A.M6233B19[141](text2, ' ', StringSplitOptions.None);
							num = HFB89500.EEAFA1A1((array3 != null) ? PE36C7B1.M2A4663E(array3[1], '"') : null, P4258EBF.AFA7138A.M6233B19[311]());
						}
						catch (Exception ex)
						{
							Logger.Log(ex);
						}
					}
				}
			}
			return num;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00155660 File Offset: 0x00152E60
		public string getCurrentRustId()
		{
			List<FileActions.PathNode> list = JsonConvert.DeserializeObject<List<FileActions.PathNode>>(P4258EBF.AFA7138A.M6233B19[267](P4258EBF.AFA7138A.M6233B19[158](WpfApp1.Model.MainLogic.appDataPath, "paths.json")));
			FileActions.PathNode pathNode = list.FirstOrDefault<FileActions.PathNode>((FileActions.PathNode item) => item.is_select);
			string text = ((pathNode != null) ? pathNode.folder : null);
			return P4258EBF.AFA7138A.M6233B19[250](P4258EBF.AFA7138A.M6233B19[513](text), "RustStaging") ? "\"700580\"" : "\"252490\"";
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0014CCA0 File Offset: 0x0014A4A0
		public bool UpdateBindToCurrentSettings(string path)
		{
			try
			{
				string currentSelectedFolder = Configs.getCurrentSelectedFolder();
				string text = P4258EBF.AFA7138A.M6233B19[158](WpfApp1.Model.MainLogic.appDataPath, "binds.json");
				string text2 = P4258EBF.AFA7138A.M6233B19[278](currentSelectedFolder, "cfg", "keys.cfg");
				List<Configs.BindNode> list = JsonConvert.DeserializeObject<List<Configs.BindNode>>(P4258EBF.AFA7138A.M6233B19[267](text));
				if (!P4258EBF.AFA7138A.M6233B19[88](currentSelectedFolder) && list.Any<Configs.BindNode>((Configs.BindNode x) => P4258EBF.AFA7138A.M6233B19[250](x.content, path)) && P4258EBF.AFA7138A.M6233B19[627](text2))
				{
					Configs.BindNode bindNode = list.Find((Configs.BindNode x) => x.is_select);
					P4258EBF.AFA7138A.M6233B19[473](text2, bindNode.content, true);
					P4258EBF.AFA7138A.M6233B19[94](text, P4258EBF.AFA7138A.M6233B19[330](list));
					return true;
				}
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
			}
			return false;
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0014369C File Offset: 0x00140E9C
		public string getConfigsBackup()
		{
			string text = P4258EBF.AFA7138A.M6233B19[158](WpfApp1.Model.MainLogic.appDataPath, "configs_backup.json");
			if (!P4258EBF.AFA7138A.M6233B19[627](text))
			{
				P4258EBF.AFA7138A.M6233B19[94](text, P4258EBF.AFA7138A.M6233B19[412](new List<Configs.ConfigGameBackupNode>(), Formatting.Indented));
			}
			return P4258EBF.AFA7138A.M6233B19[267](text);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0014EBD8 File Offset: 0x0014C3D8
		public string getKeysBackup()
		{
			string text = P4258EBF.AFA7138A.M6233B19[158](WpfApp1.Model.MainLogic.appDataPath, "keys_backup.json");
			if (!P4258EBF.AFA7138A.M6233B19[627](text))
			{
				P4258EBF.AFA7138A.M6233B19[94](text, P4258EBF.AFA7138A.M6233B19[412](new List<Configs.ConfigKeysBackupNode>(), Formatting.Indented));
			}
			return P4258EBF.AFA7138A.M6233B19[267](text);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000F2F4 File Offset: 0x0000D6F4
		public string[] findRusts()
		{
			List<string> list = new List<string>();
			global::SteamGameSearcher.MainLogic mainLogic = new global::SteamGameSearcher.MainLogic();
			string text = mainLogic.FindGame(252490);
			if (text != null)
			{
				list.Add(text);
			}
			string text2 = mainLogic.FindGame(700580);
			if (text2 != null)
			{
				list.Add(text2);
			}
			return list.ToArray();
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0015EEDC File Offset: 0x0015C6DC
		public bool fileComparison(string path1, string path2)
		{
			if (!P4258EBF.AFA7138A.M6233B19[627](path1) || !P4258EBF.AFA7138A.M6233B19[627](path2))
			{
				return false;
			}
			string text = P4258EBF.AFA7138A.M6233B19[267](path1);
			string text2 = P4258EBF.AFA7138A.M6233B19[267](path2);
			return !P4258EBF.AFA7138A.M6233B19[593](P4258EBF.AFA7138A.M6233B19[597](text), P4258EBF.AFA7138A.M6233B19[597](text2));
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00151224 File Offset: 0x0014EA24
		public bool replaceFileContent(string pathFrom, string pathTo)
		{
			if (!P4258EBF.AFA7138A.M6233B19[627](pathFrom) || !P4258EBF.AFA7138A.M6233B19[627](pathTo))
			{
				return false;
			}
			try
			{
				string text = P4258EBF.AFA7138A.M6233B19[267](pathFrom);
				P4258EBF.AFA7138A.M6233B19[94](pathTo, text);
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
				return false;
			}
			return true;
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00140850 File Offset: 0x0013E050
		public bool updateLaunchParamsInConfig(string pathToCfg, string newLaunchParams)
		{
			string text = P4258EBF.AFA7138A.M6233B19[158](WpfApp1.Model.MainLogic.appDataPath, "configs.json");
			if (!P4258EBF.AFA7138A.M6233B19[627](text))
			{
				return false;
			}
			string text2 = P4258EBF.AFA7138A.M6233B19[267](text);
			JArray jarray = P4258EBF.AFA7138A.M6233B19[147](text2);
			if (jarray == null)
			{
				return false;
			}
			bool flag = false;
			using (IEnumerator<JToken> enumerator = P4258EBF.AFA7138A.M6233B19[523](jarray))
			{
				while (P4258EBF.AFA7138A.M6233B19[411](enumerator))
				{
					JToken jtoken = enumerator.Current;
					JToken jtoken2 = P4258EBF.AFA7138A.M6233B19[327](jtoken, "content");
					if (jtoken2 != null && D4894B18.CEA99C94(jtoken2.ToString(), pathToCfg, StringComparison.OrdinalIgnoreCase))
					{
						P4258EBF.AFA7138A.M6233B19[136](jtoken, "launch_params", P4258EBF.AFA7138A.M6233B19[402](newLaunchParams));
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				return false;
			}
			P4258EBF.AFA7138A.M6233B19[94](text, P4258EBF.AFA7138A.M6233B19[612](jarray, Formatting.Indented));
			return true;
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000F494 File Offset: 0x0000D894
		public string getVersion()
		{
			return App.curVersion;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0015EC20 File Offset: 0x0015C420
		public string getBindNameByElement(string element, string path)
		{
			string text = P4258EBF.AFA7138A.M6233B19[2](path, ".json");
			JObject jobject = P4258EBF.AFA7138A.M6233B19[621](element);
			P4258EBF.AFA7138A.M6233B19[627](text);
			return null;
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0015DCD8 File Offset: 0x0015B4D8
		public string getCurrentLang()
		{
			string text = P4258EBF.AFA7138A.M6233B19[158](WpfApp1.Model.MainLogic.appDataPath, "lang.json");
			if (!P4258EBF.AFA7138A.M6233B19[627](text))
			{
				P4258EBF.AFA7138A.M6233B19[94](text, "{\"lang\":\"ru\"}");
			}
			return P4258EBF.AFA7138A.M6233B19[267](text);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0013E6B8 File Offset: 0x0013BEB8
		public void updateLang(string newLangJson)
		{
			string text = P4258EBF.AFA7138A.M6233B19[158](WpfApp1.Model.MainLogic.appDataPath, "lang.json");
			if (!P4258EBF.AFA7138A.M6233B19[627](text))
			{
				P4258EBF.AFA7138A.M6233B19[94](text, "{\"lang\":\"ru\"}");
			}
			P4258EBF.AFA7138A.M6233B19[94](text, newLangJson);
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00156F3C File Offset: 0x0015473C
		public void saveBindObject(string bindList, string path)
		{
			string text = P4258EBF.AFA7138A.M6233B19[158](WpfApp1.Model.MainLogic.appDataPath, "binds_cache.json");
			Dictionary<string, string> dictionary;
			if (P4258EBF.AFA7138A.M6233B19[627](text))
			{
				string text2 = P4258EBF.AFA7138A.M6233B19[267](text);
				dictionary = (P4258EBF.AFA7138A.M6233B19[426](text2) ? new Dictionary<string, string>() : (JsonConvert.DeserializeObject<Dictionary<string, string>>(text2) ?? new Dictionary<string, string>()));
			}
			else
			{
				dictionary = new Dictionary<string, string>();
			}
			dictionary[path] = bindList;
			P4258EBF.AFA7138A.M6233B19[94](text, P4258EBF.AFA7138A.M6233B19[412](dictionary, Formatting.Indented));
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00161848 File Offset: 0x0015F048
		public void deleteBindFromCfg(string element)
		{
			JsBridge.<deleteBindFromCfg>d__20 <deleteBindFromCfg>d__;
			<deleteBindFromCfg>d__.<>t__builder = P4258EBF.AFA7138A.M6233B19[385]();
			<deleteBindFromCfg>d__.<>4__this = this;
			<deleteBindFromCfg>d__.element = element;
			<deleteBindFromCfg>d__.<>1__state = -1;
			<deleteBindFromCfg>d__.<>t__builder.Start<JsBridge.<deleteBindFromCfg>d__20>(ref <deleteBindFromCfg>d__);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00150968 File Offset: 0x0014E168
		public bool containsBindObject(string path)
		{
			string text = P4258EBF.AFA7138A.M6233B19[158](WpfApp1.Model.MainLogic.appDataPath, "binds_cache.json");
			if (P4258EBF.AFA7138A.M6233B19[627](text))
			{
				string text2 = P4258EBF.AFA7138A.M6233B19[267](text);
				Dictionary<string, string> dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(text2);
				return dictionary.ContainsKey(path);
			}
			return false;
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0015EF78 File Offset: 0x0015C778
		public string getBindObject(string path)
		{
			string text = P4258EBF.AFA7138A.M6233B19[158](WpfApp1.Model.MainLogic.appDataPath, "binds_cache.json");
			string text2 = P4258EBF.AFA7138A.M6233B19[267](text);
			Dictionary<string, string> dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(text2);
			return dictionary[path];
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000F64C File Offset: 0x0000DA4C
		public void updateFavouriteCommands(string json)
		{
			List<string> list = JsonConvert.DeserializeObject<List<string>>(json);
			WebAppStorage.UpdateFavouritesCommands(list);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000F666 File Offset: 0x0000DA66
		public string[] getFavouriteCommands()
		{
			return WebAppStorage.GetFavouritesCommands().ToArray();
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00159E0C File Offset: 0x0015760C
		public void jsLog(object type, object data)
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			P4258EBF.AFA7138A.M6233B19[466](ref defaultInterpolatedStringHandler, 12, 2);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "WEBVIEW2| ");
			defaultInterpolatedStringHandler.AppendFormatted<object>(type);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, ": ");
			defaultInterpolatedStringHandler.AppendFormatted<object>(data);
			Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler));
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000F6C0 File Offset: 0x0000DAC0
		public async Task<string> getBindsList()
		{
			Logger.Log("getBindsList");
			string text = P4258EBF.AFA7138A.M6233B19[267](P4258EBF.AFA7138A.M6233B19[478](WpfApp1.Model.MainLogic.appDataPath, "\\paths.json"));
			List<JsBridge.PathItem> list = JsonConvert.DeserializeObject<List<JsBridge.PathItem>>(text);
			JsBridge.PathItem pathItem = list.FirstOrDefault<JsBridge.PathItem>((JsBridge.PathItem x) => x.is_select);
			string text2 = I7A29812.F086D53B(H00E4429.O3A30837((pathItem != null) ? pathItem.folder : null, "cfg", "keys.cfg"));
			SecureHttp secureHttp = new SecureHttp();
			var anon = new
			{
				keys = text2
			};
			string text3 = P4258EBF.AFA7138A.M6233B19[330](anon);
			StringContent stringContent = P4258EBF.AFA7138A.M6233B19[321](text3, P4258EBF.AFA7138A.M6233B19[204](), "application/json");
			HttpResponseMessage httpResponseMessage = await P4258EBF.AFA7138A.M6233B19[583](secureHttp.GetClient(), "binds/get", stringContent);
			HttpResponseMessage httpResponseMessage2 = httpResponseMessage;
			if (P4258EBF.AFA7138A.M6233B19[23](httpResponseMessage2) == HttpStatusCode.Unauthorized)
			{
				this.logout();
			}
			string text4 = await P4258EBF.AFA7138A.M6233B19[623](P4258EBF.AFA7138A.M6233B19[405](httpResponseMessage2));
			Logger.Log(text4);
			return text4;
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000F704 File Offset: 0x0000DB04
		public async Task<string> createBindCommand(string jsonObject)
		{
			SecureHttp secureHttp = new SecureHttp();
			StringContent stringContent = P4258EBF.AFA7138A.M6233B19[321](jsonObject, P4258EBF.AFA7138A.M6233B19[204](), "application/json");
			HttpResponseMessage httpResponseMessage = await P4258EBF.AFA7138A.M6233B19[583](secureHttp.GetClient(), "binds/create", stringContent);
			HttpResponseMessage httpResponseMessage2 = httpResponseMessage;
			if (P4258EBF.AFA7138A.M6233B19[23](httpResponseMessage2) == HttpStatusCode.Unauthorized)
			{
				this.logout();
			}
			string text = await P4258EBF.AFA7138A.M6233B19[623](P4258EBF.AFA7138A.M6233B19[405](httpResponseMessage2));
			Logger.Log(text);
			JToken jtoken = P4258EBF.AFA7138A.M6233B19[175](P4258EBF.AFA7138A.M6233B19[621](text), "command");
			return (jtoken != null) ? jtoken.ToString() : null;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0015C7D0 File Offset: 0x00159FD0
		public Task applyBindInFile(string bindLine)
		{
			JsBridge.<applyBindInFile>d__28 <applyBindInFile>d__;
			<applyBindInFile>d__.<>t__builder = P4258EBF.AFA7138A.M6233B19[20]();
			<applyBindInFile>d__.<>4__this = this;
			<applyBindInFile>d__.bindLine = bindLine;
			<applyBindInFile>d__.<>1__state = -1;
			<applyBindInFile>d__.<>t__builder.Start<JsBridge.<applyBindInFile>d__28>(ref <applyBindInFile>d__);
			return P4258EBF.AFA7138A.M6233B19[19](ref <applyBindInFile>d__.<>t__builder);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0015E514 File Offset: 0x0015BD14
		public string getCurrentParamsLaunch()
		{
			RustTweakerViewModel rustTweakerViewModel = new RustTweakerViewModel();
			string configPathToLastUser = WpfApp1.Model.MainLogic.SteamParser.GetConfigPathToLastUser();
			List<FileActions.PathNode> list = JsonConvert.DeserializeObject<List<FileActions.PathNode>>(P4258EBF.AFA7138A.M6233B19[267](P4258EBF.AFA7138A.M6233B19[158](WpfApp1.Model.MainLogic.appDataPath, "paths.json")));
			FileActions.PathNode pathNode = list.FirstOrDefault<FileActions.PathNode>((FileActions.PathNode item) => item.is_select);
			string text = ((pathNode != null) ? pathNode.folder : null);
			return rustTweakerViewModel.ExtractRustLaunchOptions(configPathToLastUser, text);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000F80C File Offset: 0x0000DC0C
		public string checkFolder(string path)
		{
			return FileActions.checkFolder(path).ToString();
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0015C534 File Offset: 0x00159D34
		public bool pathsCheck()
		{
			return JsonConvert.DeserializeObject<List<FileActions.PathNode>>(P4258EBF.AFA7138A.M6233B19[267](P4258EBF.AFA7138A.M6233B19[478](WpfApp1.Model.MainLogic.appDataPath, "\\paths.json"))).Any<FileActions.PathNode>((FileActions.PathNode item) => item.is_select);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000F86C File Offset: 0x0000DC6C
		public bool checkPathsHealth()
		{
			return WpfApp1.Model.MainLogic.checkPathsHealth();
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0015FB50 File Offset: 0x0015D350
		public string[] getPaths()
		{
			return (from item in JsonConvert.DeserializeObject<List<FileActions.PathNode>>(P4258EBF.AFA7138A.M6233B19[267](P4258EBF.AFA7138A.M6233B19[478](WpfApp1.Model.MainLogic.appDataPath, "\\paths.json")))
				select item.folder).ToArray<string>();
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0015D47C File Offset: 0x0015AC7C
		public string getFullPaths()
		{
			return P4258EBF.AFA7138A.M6233B19[267](P4258EBF.AFA7138A.M6233B19[478](WpfApp1.Model.MainLogic.appDataPath, "\\paths.json"));
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0014942C File Offset: 0x00146C2C
		public bool addNewPath(string newPath)
		{
			bool flag;
			try
			{
				List<FileActions.PathNode> list = JsonConvert.DeserializeObject<List<FileActions.PathNode>>(P4258EBF.AFA7138A.M6233B19[267](P4258EBF.AFA7138A.M6233B19[478](WpfApp1.Model.MainLogic.appDataPath, "\\paths.json")));
				if (!list.Any<FileActions.PathNode>((FileActions.PathNode n) => P4258EBF.AFA7138A.M6233B19[250](n.folder, newPath)))
				{
					list.Add(new FileActions.PathNode
					{
						folder = newPath
					});
					P4258EBF.AFA7138A.M6233B19[94](P4258EBF.AFA7138A.M6233B19[478](WpfApp1.Model.MainLogic.appDataPath, "\\paths.json"), P4258EBF.AFA7138A.M6233B19[412](list, Formatting.Indented));
					flag = true;
				}
				else
				{
					flag = false;
				}
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000309 RID: 777 RVA: 0x001588B4 File Offset: 0x001560B4
		public void selectPath(string path)
		{
			try
			{
				List<FileActions.PathNode> list = JsonConvert.DeserializeObject<List<FileActions.PathNode>>(P4258EBF.AFA7138A.M6233B19[267](P4258EBF.AFA7138A.M6233B19[478](WpfApp1.Model.MainLogic.appDataPath, "\\paths.json")));
				FileActions.PathNode pathNode = list.FirstOrDefault<FileActions.PathNode>((FileActions.PathNode item) => P4258EBF.AFA7138A.M6233B19[250](item.folder, path));
				Logger.Log(pathNode.folder);
				foreach (FileActions.PathNode pathNode2 in list)
				{
					pathNode2.is_select = false;
				}
				if (pathNode != null)
				{
					pathNode.is_select = true;
				}
				P4258EBF.AFA7138A.M6233B19[94](P4258EBF.AFA7138A.M6233B19[478](WpfApp1.Model.MainLogic.appDataPath, "\\paths.json"), P4258EBF.AFA7138A.M6233B19[412](list, Formatting.Indented));
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
			}
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000FA5C File Offset: 0x0000DE5C
		public async Task<string> getStartupTweaks()
		{
			return await WpfApp1.Model.MainLogic.getStartupTweaks();
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000FA98 File Offset: 0x0000DE98
		public async Task<bool> applyTweaks(string data)
		{
			return await WpfApp1.Model.MainLogic.applyTweaks(data);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000FADC File Offset: 0x0000DEDC
		public async Task<string> getStartupGraphics()
		{
			return await WpfApp1.Model.MainLogic.getStartupGraphics();
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000FB18 File Offset: 0x0000DF18
		public async Task<bool> applyGraphics(string data)
		{
			return await WpfApp1.Model.MainLogic.applyGraphics(data);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00161F6C File Offset: 0x0015F76C
		public Task<string> selectFolder()
		{
			JsBridge.<>c__DisplayClass41_0 CS$<>8__locals1 = new JsBridge.<>c__DisplayClass41_0();
			CS$<>8__locals1.tcs = new TaskCompletionSource<string>();
			P4258EBF.AFA7138A.M6233B19[532](P4258EBF.AFA7138A.M6233B19[370](P4258EBF.AFA7138A.M6233B19[82]()), P4258EBF.AFA7138A.M6233B19[579](CS$<>8__locals1, ldftn(<selectFolder>b__0)), Array.Empty<object>());
			return CS$<>8__locals1.tcs.Task;
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0015E6A4 File Offset: 0x0015BEA4
		public Task<string> selectFile()
		{
			JsBridge.<>c__DisplayClass42_0 CS$<>8__locals1 = new JsBridge.<>c__DisplayClass42_0();
			CS$<>8__locals1.tcs = new TaskCompletionSource<string>();
			P4258EBF.AFA7138A.M6233B19[532](P4258EBF.AFA7138A.M6233B19[370](P4258EBF.AFA7138A.M6233B19[82]()), P4258EBF.AFA7138A.M6233B19[579](CS$<>8__locals1, ldftn(<selectFile>b__0)), Array.Empty<object>());
			return CS$<>8__locals1.tcs.Task;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000FBF2 File Offset: 0x0000DFF2
		public bool SteamIsRunning()
		{
			return WpfApp1.Model.MainLogic.SteamIsRunning();
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0015DB88 File Offset: 0x0015B388
		public Task CloseSteam()
		{
			JsBridge.<CloseSteam>d__44 <CloseSteam>d__;
			<CloseSteam>d__.<>t__builder = P4258EBF.AFA7138A.M6233B19[20]();
			<CloseSteam>d__.<>4__this = this;
			<CloseSteam>d__.<>1__state = -1;
			<CloseSteam>d__.<>t__builder.Start<JsBridge.<CloseSteam>d__44>(ref <CloseSteam>d__);
			return P4258EBF.AFA7138A.M6233B19[19](ref <CloseSteam>d__.<>t__builder);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000FC3F File Offset: 0x0000E03F
		public void StartSteam()
		{
			WpfApp1.Model.MainLogic.StartSteam();
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000FC46 File Offset: 0x0000E046
		public void StartRust()
		{
			WpfApp1.Model.MainLogic.StartRust();
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0015AC34 File Offset: 0x00158434
		public string getMail()
		{
			string text = P4258EBF.AFA7138A.M6233B19[158](WpfApp1.Model.MainLogic.appDataPath, "appdata.json");
			if (!P4258EBF.AFA7138A.M6233B19[627](text))
			{
				return "";
			}
			string text2;
			try
			{
				JToken jtoken = P4258EBF.AFA7138A.M6233B19[337](P4258EBF.AFA7138A.M6233B19[267](text));
				JToken jtoken2 = P4258EBF.AFA7138A.M6233B19[327](jtoken, "mail");
				text2 = ((jtoken2 != null) ? jtoken2.Value<string>() : null) ?? "";
			}
			catch
			{
				text2 = "";
			}
			return text2;
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0013CC6C File Offset: 0x0013A46C
		public string chengeCurrentFolder(string newPath)
		{
			string text = this.checkFolder(newPath);
			if (P4258EBF.AFA7138A.M6233B19[250](text, "ALLGOOD"))
			{
				string text2 = P4258EBF.AFA7138A.M6233B19[267](P4258EBF.AFA7138A.M6233B19[158](WpfApp1.Model.MainLogic.appDataPath, "paths.json"));
				JArray jarray = P4258EBF.AFA7138A.M6233B19[147](text2);
				using (IEnumerator<JToken> enumerator = P4258EBF.AFA7138A.M6233B19[523](jarray))
				{
					while (P4258EBF.AFA7138A.M6233B19[411](enumerator))
					{
						JObject jobject = (JObject)enumerator.Current;
						JToken jtoken = P4258EBF.AFA7138A.M6233B19[175](jobject, "is_select");
						bool flag = jtoken != null && jtoken.Value<bool>();
						if (flag)
						{
							P4258EBF.AFA7138A.M6233B19[542](jobject, "folder", P4258EBF.AFA7138A.M6233B19[402](newPath));
							break;
						}
					}
				}
				P4258EBF.AFA7138A.M6233B19[94](P4258EBF.AFA7138A.M6233B19[158](WpfApp1.Model.MainLogic.appDataPath, "paths.json"), P4258EBF.AFA7138A.M6233B19[612](jarray, Formatting.Indented));
				return "ALLGOOD";
			}
			return text;
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00156110 File Offset: 0x00153910
		public string GetSelectedFolder()
		{
			string text = P4258EBF.AFA7138A.M6233B19[158](WpfApp1.Model.MainLogic.appDataPath, "paths.json");
			if (!P4258EBF.AFA7138A.M6233B19[627](text))
			{
				return null;
			}
			string text2 = P4258EBF.AFA7138A.M6233B19[267](text);
			JArray jarray = P4258EBF.AFA7138A.M6233B19[147](text2);
			JObject jobject = jarray.OfType<JObject>().FirstOrDefault<JObject>((JObject o) => P4258EBF.AFA7138A.M6233B19[578](P4258EBF.AFA7138A.M6233B19[175](o, "is_select")).GetValueOrDefault());
			if (jobject == null)
			{
				return null;
			}
			JToken jtoken = P4258EBF.AFA7138A.M6233B19[176](jobject, "folder");
			if (jtoken == null)
			{
				return null;
			}
			return jtoken.ToString();
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0014B674 File Offset: 0x00148E74
		public string setPathToFolder(string targetPath, string newPath)
		{
			string text = this.checkFolder(newPath);
			if (P4258EBF.AFA7138A.M6233B19[593](text, "ALLGOOD"))
			{
				return text;
			}
			string text2 = P4258EBF.AFA7138A.M6233B19[158](WpfApp1.Model.MainLogic.appDataPath, "paths.json");
			string text3 = P4258EBF.AFA7138A.M6233B19[267](text2);
			JArray jarray = P4258EBF.AFA7138A.M6233B19[147](text3);
			bool flag = false;
			using (IEnumerator<JToken> enumerator = P4258EBF.AFA7138A.M6233B19[523](jarray))
			{
				while (P4258EBF.AFA7138A.M6233B19[411](enumerator))
				{
					JObject jobject = (JObject)enumerator.Current;
					JToken jtoken = P4258EBF.AFA7138A.M6233B19[175](jobject, "folder");
					string text4 = ((jtoken != null) ? jtoken.Value<string>() : null);
					if (text4 != null && P4258EBF.AFA7138A.M6233B19[85](text4, targetPath, StringComparison.OrdinalIgnoreCase))
					{
						P4258EBF.AFA7138A.M6233B19[542](jobject, "folder", P4258EBF.AFA7138A.M6233B19[402](newPath));
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				return "FOLDER_NOT_FOUND";
			}
			P4258EBF.AFA7138A.M6233B19[94](text2, P4258EBF.AFA7138A.M6233B19[612](jarray, Formatting.Indented));
			return "ALLGOOD";
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0014D670 File Offset: 0x0014AE70
		public bool removePath(string targetPath)
		{
			string text = P4258EBF.AFA7138A.M6233B19[158](WpfApp1.Model.MainLogic.appDataPath, "paths.json");
			if (!P4258EBF.AFA7138A.M6233B19[627](text))
			{
				return false;
			}
			string text2 = P4258EBF.AFA7138A.M6233B19[267](text);
			JArray jarray;
			try
			{
				jarray = P4258EBF.AFA7138A.M6233B19[147](text2);
			}
			catch
			{
				return false;
			}
			bool flag = false;
			foreach (JToken jtoken in jarray.ToList<JToken>())
			{
				JObject jobject = jtoken as JObject;
				if (jobject != null)
				{
					JToken jtoken2 = P4258EBF.AFA7138A.M6233B19[175](jobject, "folder");
					string text3 = ((jtoken2 != null) ? jtoken2.Value<string>() : null);
					if (text3 != null && P4258EBF.AFA7138A.M6233B19[85](text3, targetPath, StringComparison.OrdinalIgnoreCase))
					{
						P4258EBF.AFA7138A.M6233B19[217](jarray, jobject);
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				return false;
			}
			P4258EBF.AFA7138A.M6233B19[94](text, P4258EBF.AFA7138A.M6233B19[612](jarray, Formatting.Indented));
			return true;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00153FB0 File Offset: 0x001517B0
		public bool addConfig(string path, string name)
		{
			JArray jarray = P4258EBF.AFA7138A.M6233B19[147](P4258EBF.AFA7138A.M6233B19[267](P4258EBF.AFA7138A.M6233B19[478](WpfApp1.Model.MainLogic.appDataPath, "\\configs.json")));
			if (jarray == null)
			{
				return false;
			}
			using (IEnumerator<JToken> enumerator = P4258EBF.AFA7138A.M6233B19[523](jarray))
			{
				while (P4258EBF.AFA7138A.M6233B19[411](enumerator))
				{
					JToken jtoken = enumerator.Current;
					JToken jtoken2 = P4258EBF.AFA7138A.M6233B19[327](jtoken, "content");
					if (jtoken2 != null && D4894B18.CEA99C94(jtoken2.ToString(), path, StringComparison.OrdinalIgnoreCase))
					{
						return false;
					}
				}
			}
			JObject jobject = P4258EBF.AFA7138A.M6233B19[345]();
			E4250989.FF0FA0A4(jobject, "is_select", P4258EBF.AFA7138A.M6233B19[448](false));
			E4250989.FF0FA0A4(jobject, "name", P4258EBF.AFA7138A.M6233B19[402](name));
			E4250989.FF0FA0A4(jobject, "content", P4258EBF.AFA7138A.M6233B19[402](path));
			E4250989.FF0FA0A4(jobject, "launch_params", P4258EBF.AFA7138A.M6233B19[402](this.getCurrentParamsLaunch()));
			JObject jobject2 = jobject;
			P4258EBF.AFA7138A.M6233B19[401](jarray, jobject2);
			P4258EBF.AFA7138A.M6233B19[94](P4258EBF.AFA7138A.M6233B19[478](WpfApp1.Model.MainLogic.appDataPath, "\\configs.json"), P4258EBF.AFA7138A.M6233B19[612](jarray, Formatting.Indented));
			return true;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00142740 File Offset: 0x0013FF40
		public bool addConfigOnlyPath(string path)
		{
			string text = P4258EBF.AFA7138A.M6233B19[158](WpfApp1.Model.MainLogic.appDataPath, "configs.json");
			if (!P4258EBF.AFA7138A.M6233B19[627](text))
			{
				return false;
			}
			JArray jarray = P4258EBF.AFA7138A.M6233B19[147](P4258EBF.AFA7138A.M6233B19[267](text));
			if (jarray == null)
			{
				return false;
			}
			using (IEnumerator<JToken> enumerator = P4258EBF.AFA7138A.M6233B19[523](jarray))
			{
				while (P4258EBF.AFA7138A.M6233B19[411](enumerator))
				{
					JToken jtoken = enumerator.Current;
					JToken jtoken2 = P4258EBF.AFA7138A.M6233B19[327](jtoken, "content");
					if (jtoken2 != null && D4894B18.CEA99C94(jtoken2.ToString(), path, StringComparison.OrdinalIgnoreCase))
					{
						return false;
					}
				}
			}
			string text2 = P4258EBF.AFA7138A.M6233B19[478](P4258EBF.AFA7138A.M6233B19[391](path), "Copy");
			JObject jobject = P4258EBF.AFA7138A.M6233B19[345]();
			E4250989.FF0FA0A4(jobject, "is_select", P4258EBF.AFA7138A.M6233B19[448](false));
			E4250989.FF0FA0A4(jobject, "name", P4258EBF.AFA7138A.M6233B19[402](text2));
			E4250989.FF0FA0A4(jobject, "content", P4258EBF.AFA7138A.M6233B19[402](path));
			E4250989.FF0FA0A4(jobject, "launch_params", P4258EBF.AFA7138A.M6233B19[402](""));
			JObject jobject2 = jobject;
			P4258EBF.AFA7138A.M6233B19[401](jarray, jobject2);
			P4258EBF.AFA7138A.M6233B19[94](text, P4258EBF.AFA7138A.M6233B19[612](jarray, Formatting.Indented));
			return true;
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0013C1E0 File Offset: 0x001399E0
		public string createCopy(string sourcePath)
		{
			string text = P4258EBF.AFA7138A.M6233B19[158](WpfApp1.Model.MainLogic.appDataPath, "configs.json");
			string text2 = P4258EBF.AFA7138A.M6233B19[158](WpfApp1.Model.MainLogic.appDataPath, "configs");
			if (!P4258EBF.AFA7138A.M6233B19[627](text))
			{
				return null;
			}
			P4258EBF.AFA7138A.M6233B19[111](text2);
			string text3 = P4258EBF.AFA7138A.M6233B19[476]().ToString();
			string text4 = P4258EBF.AFA7138A.M6233B19[124](sourcePath);
			string text5 = P4258EBF.AFA7138A.M6233B19[158](text2, P4258EBF.AFA7138A.M6233B19[478](text3, text4));
			P4258EBF.AFA7138A.M6233B19[534](sourcePath, text5);
			JArray jarray = P4258EBF.AFA7138A.M6233B19[147](P4258EBF.AFA7138A.M6233B19[267](text));
			if (jarray == null)
			{
				return null;
			}
			JObject jobject = jarray.FirstOrDefault<JToken>(delegate(JToken i)
			{
				JToken jtoken3 = P4258EBF.AFA7138A.M6233B19[327](i, "content");
				return GF2C9AAB.MF064291((jtoken3 != null) ? jtoken3.ToString() : null, sourcePath, StringComparison.OrdinalIgnoreCase);
			}) as JObject;
			string text6;
			if (jobject == null)
			{
				text6 = null;
			}
			else
			{
				JToken jtoken = P4258EBF.AFA7138A.M6233B19[176](jobject, "name");
				text6 = ((jtoken != null) ? jtoken.ToString() : null);
			}
			string text7 = text6 ?? "Copy";
			JObject jobject2 = P4258EBF.AFA7138A.M6233B19[345]();
			E4250989.FF0FA0A4(jobject2, "is_select", P4258EBF.AFA7138A.M6233B19[448](false));
			E4250989.FF0FA0A4(jobject2, "name", P4258EBF.AFA7138A.M6233B19[402](text7));
			E4250989.FF0FA0A4(jobject2, "content", P4258EBF.AFA7138A.M6233B19[402](text5));
			string text8 = "launch_params";
			string text9;
			if (jobject == null)
			{
				text9 = null;
			}
			else
			{
				JToken jtoken2 = P4258EBF.AFA7138A.M6233B19[176](jobject, "launch_params");
				text9 = ((jtoken2 != null) ? jtoken2.ToString() : null);
			}
			E4250989.FF0FA0A4(jobject2, text8, M90DAD20.EC866133(text9 ?? ""));
			JObject jobject3 = jobject2;
			P4258EBF.AFA7138A.M6233B19[401](jarray, jobject3);
			P4258EBF.AFA7138A.M6233B19[94](text, P4258EBF.AFA7138A.M6233B19[612](jarray, Formatting.Indented));
			JObject jobject4 = P4258EBF.AFA7138A.M6233B19[345]();
			E4250989.FF0FA0A4(jobject4, "name", P4258EBF.AFA7138A.M6233B19[402](text7));
			E4250989.FF0FA0A4(jobject4, "path", P4258EBF.AFA7138A.M6233B19[402](text5));
			JObject jobject5 = jobject4;
			return jobject5.ToString();
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00158CF0 File Offset: 0x001564F0
		public string createCopyBind(string sourcePath)
		{
			string text = P4258EBF.AFA7138A.M6233B19[158](WpfApp1.Model.MainLogic.appDataPath, "binds.json");
			string text2 = P4258EBF.AFA7138A.M6233B19[158](WpfApp1.Model.MainLogic.appDataPath, "binds");
			if (!P4258EBF.AFA7138A.M6233B19[627](text))
			{
				return null;
			}
			P4258EBF.AFA7138A.M6233B19[111](text2);
			string text3 = P4258EBF.AFA7138A.M6233B19[476]().ToString();
			string text4 = P4258EBF.AFA7138A.M6233B19[124](sourcePath);
			string text5 = P4258EBF.AFA7138A.M6233B19[158](text2, P4258EBF.AFA7138A.M6233B19[478](text3, text4));
			P4258EBF.AFA7138A.M6233B19[534](sourcePath, text5);
			JArray jarray = P4258EBF.AFA7138A.M6233B19[147](P4258EBF.AFA7138A.M6233B19[267](text));
			if (jarray == null)
			{
				return null;
			}
			JObject jobject = jarray.FirstOrDefault<JToken>(delegate(JToken i)
			{
				JToken jtoken2 = P4258EBF.AFA7138A.M6233B19[327](i, "content");
				return GF2C9AAB.MF064291((jtoken2 != null) ? jtoken2.ToString() : null, sourcePath, StringComparison.OrdinalIgnoreCase);
			}) as JObject;
			string text6;
			if (jobject == null)
			{
				text6 = null;
			}
			else
			{
				JToken jtoken = P4258EBF.AFA7138A.M6233B19[176](jobject, "name");
				text6 = ((jtoken != null) ? jtoken.ToString() : null);
			}
			string text7 = text6 ?? "Copy";
			JObject jobject2 = P4258EBF.AFA7138A.M6233B19[345]();
			E4250989.FF0FA0A4(jobject2, "is_select", P4258EBF.AFA7138A.M6233B19[448](false));
			E4250989.FF0FA0A4(jobject2, "name", P4258EBF.AFA7138A.M6233B19[402](text7));
			E4250989.FF0FA0A4(jobject2, "content", P4258EBF.AFA7138A.M6233B19[402](text5));
			JObject jobject3 = jobject2;
			P4258EBF.AFA7138A.M6233B19[401](jarray, jobject3);
			P4258EBF.AFA7138A.M6233B19[94](text, P4258EBF.AFA7138A.M6233B19[612](jarray, Formatting.Indented));
			JObject jobject4 = P4258EBF.AFA7138A.M6233B19[345]();
			E4250989.FF0FA0A4(jobject4, "name", P4258EBF.AFA7138A.M6233B19[402](text7));
			E4250989.FF0FA0A4(jobject4, "path", P4258EBF.AFA7138A.M6233B19[402](text5));
			JObject jobject5 = jobject4;
			return jobject5.ToString();
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00138898 File Offset: 0x00136098
		public bool addConfigPathAndLaunchParams(string path, string launchParams)
		{
			string text = P4258EBF.AFA7138A.M6233B19[158](WpfApp1.Model.MainLogic.appDataPath, "configs.json");
			if (!P4258EBF.AFA7138A.M6233B19[627](text))
			{
				return false;
			}
			JArray jarray = P4258EBF.AFA7138A.M6233B19[147](P4258EBF.AFA7138A.M6233B19[267](text));
			if (jarray == null)
			{
				return false;
			}
			using (IEnumerator<JToken> enumerator = P4258EBF.AFA7138A.M6233B19[523](jarray))
			{
				while (P4258EBF.AFA7138A.M6233B19[411](enumerator))
				{
					JToken jtoken = enumerator.Current;
					JToken jtoken2 = P4258EBF.AFA7138A.M6233B19[327](jtoken, "content");
					if (jtoken2 != null && D4894B18.CEA99C94(jtoken2.ToString(), path, StringComparison.OrdinalIgnoreCase))
					{
						return false;
					}
				}
			}
			string text2 = P4258EBF.AFA7138A.M6233B19[391](path);
			JObject jobject = P4258EBF.AFA7138A.M6233B19[345]();
			E4250989.FF0FA0A4(jobject, "is_select", P4258EBF.AFA7138A.M6233B19[448](false));
			E4250989.FF0FA0A4(jobject, "name", P4258EBF.AFA7138A.M6233B19[402](text2));
			E4250989.FF0FA0A4(jobject, "content", P4258EBF.AFA7138A.M6233B19[402](path));
			E4250989.FF0FA0A4(jobject, "launch_params", P4258EBF.AFA7138A.M6233B19[402](launchParams));
			JObject jobject2 = jobject;
			P4258EBF.AFA7138A.M6233B19[401](jarray, jobject2);
			P4258EBF.AFA7138A.M6233B19[94](text, P4258EBF.AFA7138A.M6233B19[612](jarray, Formatting.Indented));
			return true;
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00159564 File Offset: 0x00156D64
		public bool addConfigFull(string path, string launchParams, string fileName)
		{
			string text = P4258EBF.AFA7138A.M6233B19[158](WpfApp1.Model.MainLogic.appDataPath, "configs.json");
			if (!P4258EBF.AFA7138A.M6233B19[627](text))
			{
				return false;
			}
			JArray jarray = P4258EBF.AFA7138A.M6233B19[147](P4258EBF.AFA7138A.M6233B19[267](text));
			if (jarray == null)
			{
				return false;
			}
			using (IEnumerator<JToken> enumerator = P4258EBF.AFA7138A.M6233B19[523](jarray))
			{
				while (P4258EBF.AFA7138A.M6233B19[411](enumerator))
				{
					JToken jtoken = enumerator.Current;
					JToken jtoken2 = P4258EBF.AFA7138A.M6233B19[327](jtoken, "content");
					if (jtoken2 != null && D4894B18.CEA99C94(jtoken2.ToString(), path, StringComparison.OrdinalIgnoreCase))
					{
						return false;
					}
				}
			}
			string text2 = P4258EBF.AFA7138A.M6233B19[391](path);
			JObject jobject = P4258EBF.AFA7138A.M6233B19[345]();
			E4250989.FF0FA0A4(jobject, "is_select", P4258EBF.AFA7138A.M6233B19[448](false));
			E4250989.FF0FA0A4(jobject, "name", P4258EBF.AFA7138A.M6233B19[402](fileName));
			E4250989.FF0FA0A4(jobject, "content", P4258EBF.AFA7138A.M6233B19[402](path));
			E4250989.FF0FA0A4(jobject, "launch_params", P4258EBF.AFA7138A.M6233B19[402](launchParams));
			JObject jobject2 = jobject;
			P4258EBF.AFA7138A.M6233B19[401](jarray, jobject2);
			P4258EBF.AFA7138A.M6233B19[94](text, P4258EBF.AFA7138A.M6233B19[612](jarray, Formatting.Indented));
			return true;
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0015F1EC File Offset: 0x0015C9EC
		public Task exportConfig(string _path, string _name = null)
		{
			JsBridge.<exportConfig>d__58 <exportConfig>d__;
			<exportConfig>d__.<>t__builder = P4258EBF.AFA7138A.M6233B19[20]();
			<exportConfig>d__._path = _path;
			<exportConfig>d__._name = _name;
			<exportConfig>d__.<>1__state = -1;
			<exportConfig>d__.<>t__builder.Start<JsBridge.<exportConfig>d__58>(ref <exportConfig>d__);
			return P4258EBF.AFA7138A.M6233B19[19](ref <exportConfig>d__.<>t__builder);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0015BA70 File Offset: 0x00159270
		public string removeConfig(string path)
		{
			JArray jarray = P4258EBF.AFA7138A.M6233B19[147](P4258EBF.AFA7138A.M6233B19[267](P4258EBF.AFA7138A.M6233B19[478](WpfApp1.Model.MainLogic.appDataPath, "\\configs.json")));
			if (jarray == null)
			{
				return "JSON_ERROR";
			}
			bool flag = false;
			foreach (JToken jtoken in jarray.ToList<JToken>())
			{
				JObject jobject = (JObject)jtoken;
				JToken jtoken2 = P4258EBF.AFA7138A.M6233B19[175](jobject, "content");
				if (jtoken2 != null && D4894B18.CEA99C94(jtoken2.ToString(), path, StringComparison.OrdinalIgnoreCase))
				{
					P4258EBF.AFA7138A.M6233B19[217](jarray, jobject);
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return "NOT_FOUND";
			}
			P4258EBF.AFA7138A.M6233B19[289](path);
			P4258EBF.AFA7138A.M6233B19[94](P4258EBF.AFA7138A.M6233B19[478](WpfApp1.Model.MainLogic.appDataPath, "\\configs.json"), P4258EBF.AFA7138A.M6233B19[612](jarray, Formatting.Indented));
			return "ALLGOOD";
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0015B0D8 File Offset: 0x001588D8
		public string copyConfig(string sourcePath)
		{
			if (!P4258EBF.AFA7138A.M6233B19[627](sourcePath))
			{
				throw P4258EBF.AFA7138A.M6233B19[99]("Исходный файл не найден", sourcePath);
			}
			string text = P4258EBF.AFA7138A.M6233B19[158](WpfApp1.Model.MainLogic.appDataPath, "configs");
			P4258EBF.AFA7138A.M6233B19[111](text);
			string text2 = P4258EBF.AFA7138A.M6233B19[391](sourcePath);
			string text3 = P4258EBF.AFA7138A.M6233B19[124](sourcePath);
			string text4 = P4258EBF.AFA7138A.M6233B19[64](text2, "_copy", text3);
			string text5 = P4258EBF.AFA7138A.M6233B19[158](text, text4);
			int num = 1;
			while (P4258EBF.AFA7138A.M6233B19[627](text5))
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
				P4258EBF.AFA7138A.M6233B19[466](ref defaultInterpolatedStringHandler, 6, 3);
				P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler, text2);
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "_copy_");
				defaultInterpolatedStringHandler.AppendFormatted<int>(num);
				P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler, text3);
				text4 = P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler);
				text5 = P4258EBF.AFA7138A.M6233B19[158](text, text4);
				num++;
			}
			P4258EBF.AFA7138A.M6233B19[534](sourcePath, text5);
			return text5;
		}

		// Token: 0x06000322 RID: 802 RVA: 0x001579A0 File Offset: 0x001551A0
		public string selectConfig(string path)
		{
			WpfApp1.Model.MainLogic.ReadOnlyCheck();
			JArray jarray = P4258EBF.AFA7138A.M6233B19[147](P4258EBF.AFA7138A.M6233B19[267](P4258EBF.AFA7138A.M6233B19[478](WpfApp1.Model.MainLogic.appDataPath, "\\configs.json")));
			if (jarray == null)
			{
				return "JSON_ERROR";
			}
			bool flag = false;
			using (IEnumerator<JToken> enumerator = P4258EBF.AFA7138A.M6233B19[523](jarray))
			{
				while (P4258EBF.AFA7138A.M6233B19[411](enumerator))
				{
					JObject jobject = (JObject)enumerator.Current;
					JToken jtoken = P4258EBF.AFA7138A.M6233B19[175](jobject, "content");
					string text = ((jtoken != null) ? jtoken.ToString() : null);
					if (text != null && P4258EBF.AFA7138A.M6233B19[85](text, path, StringComparison.OrdinalIgnoreCase))
					{
						P4258EBF.AFA7138A.M6233B19[542](jobject, "is_select", P4258EBF.AFA7138A.M6233B19[448](true));
						flag = true;
					}
					else
					{
						P4258EBF.AFA7138A.M6233B19[542](jobject, "is_select", P4258EBF.AFA7138A.M6233B19[448](false));
					}
				}
			}
			if (!flag)
			{
				return "NOT_FOUND";
			}
			P4258EBF.AFA7138A.M6233B19[94](P4258EBF.AFA7138A.M6233B19[478](WpfApp1.Model.MainLogic.appDataPath, "\\configs.json"), P4258EBF.AFA7138A.M6233B19[612](jarray, Formatting.Indented));
			return "ALLGOOD";
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0015D69C File Offset: 0x0015AE9C
		public string createConfig()
		{
			WpfApp1.Model.MainLogic.ReadOnlyCheck();
			string text = P4258EBF.AFA7138A.M6233B19[158](WpfApp1.Model.MainLogic.appDataPath, "configs");
			P4258EBF.AFA7138A.M6233B19[111](text);
			string text2 = P4258EBF.AFA7138A.M6233B19[278](this.GetSelectedFolder(), "cfg", "client.cfg");
			if (!P4258EBF.AFA7138A.M6233B19[627](text2))
			{
				throw P4258EBF.AFA7138A.M6233B19[99]("Исходный файл не найден", text2);
			}
			string text3 = P4258EBF.AFA7138A.M6233B19[124](text2);
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			P4258EBF.AFA7138A.M6233B19[466](ref defaultInterpolatedStringHandler, 0, 2);
			defaultInterpolatedStringHandler.AppendFormatted<Guid>(P4258EBF.AFA7138A.M6233B19[476]());
			P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler, text3);
			string text4 = P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler);
			string text5 = P4258EBF.AFA7138A.M6233B19[158](text, text4);
			while (P4258EBF.AFA7138A.M6233B19[627](text5))
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2;
				P4258EBF.AFA7138A.M6233B19[466](ref defaultInterpolatedStringHandler2, 0, 2);
				defaultInterpolatedStringHandler2.AppendFormatted<Guid>(P4258EBF.AFA7138A.M6233B19[476]());
				P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler2, text3);
				text4 = P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler2);
				text5 = P4258EBF.AFA7138A.M6233B19[158](text, text4);
			}
			P4258EBF.AFA7138A.M6233B19[534](text2, text5);
			return text5;
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00145444 File Offset: 0x00142C44
		public bool createOrUpdateConfigsElement(string clientPath, string launchParams)
		{
			WpfApp1.Model.MainLogic.ReadOnlyCheck();
			string text = P4258EBF.AFA7138A.M6233B19[158](WpfApp1.Model.MainLogic.appDataPath, "configs.json");
			if (!P4258EBF.AFA7138A.M6233B19[627](text))
			{
				return false;
			}
			JArray jarray = P4258EBF.AFA7138A.M6233B19[147](P4258EBF.AFA7138A.M6233B19[267](text));
			if (jarray == null)
			{
				return false;
			}
			using (IEnumerator<JToken> enumerator = P4258EBF.AFA7138A.M6233B19[523](jarray))
			{
				while (P4258EBF.AFA7138A.M6233B19[411](enumerator))
				{
					JToken jtoken = enumerator.Current;
					JToken jtoken2 = P4258EBF.AFA7138A.M6233B19[327](jtoken, "content");
					if (jtoken2 != null && D4894B18.CEA99C94(jtoken2.ToString(), clientPath, StringComparison.OrdinalIgnoreCase))
					{
						P4258EBF.AFA7138A.M6233B19[136](jtoken, "launch_params", P4258EBF.AFA7138A.M6233B19[402](launchParams));
						P4258EBF.AFA7138A.M6233B19[94](text, P4258EBF.AFA7138A.M6233B19[612](jarray, Formatting.Indented));
						return true;
					}
				}
			}
			string text2 = P4258EBF.AFA7138A.M6233B19[391](clientPath);
			JObject jobject = P4258EBF.AFA7138A.M6233B19[345]();
			E4250989.FF0FA0A4(jobject, "is_select", P4258EBF.AFA7138A.M6233B19[448](false));
			E4250989.FF0FA0A4(jobject, "name", P4258EBF.AFA7138A.M6233B19[402](text2));
			E4250989.FF0FA0A4(jobject, "content", P4258EBF.AFA7138A.M6233B19[402](clientPath));
			E4250989.FF0FA0A4(jobject, "launch_params", P4258EBF.AFA7138A.M6233B19[402](launchParams));
			JObject jobject2 = jobject;
			P4258EBF.AFA7138A.M6233B19[401](jarray, jobject2);
			P4258EBF.AFA7138A.M6233B19[94](text, P4258EBF.AFA7138A.M6233B19[612](jarray, Formatting.Indented));
			return true;
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0014A19C File Offset: 0x0014799C
		public string createBackupFile()
		{
			WpfApp1.Model.MainLogic.ReadOnlyCheck();
			string text = P4258EBF.AFA7138A.M6233B19[158](WpfApp1.Model.MainLogic.appDataPath, "configs");
			P4258EBF.AFA7138A.M6233B19[111](text);
			string text2 = P4258EBF.AFA7138A.M6233B19[278](this.GetSelectedFolder(), "cfg", "client.cfg");
			if (!P4258EBF.AFA7138A.M6233B19[627](text2))
			{
				throw P4258EBF.AFA7138A.M6233B19[99]("Исходный файл не найден", text2);
			}
			string text3 = P4258EBF.AFA7138A.M6233B19[124](text2);
			DateTime dateTime = P4258EBF.AFA7138A.M6233B19[300]();
			string text4 = P4258EBF.AFA7138A.M6233B19[362](ref dateTime, "dd.MM.yyyy");
			string text5 = P4258EBF.AFA7138A.M6233B19[64]("backup_", text4, text3);
			string text6 = P4258EBF.AFA7138A.M6233B19[158](text, text5);
			P4258EBF.AFA7138A.M6233B19[473](text2, text6, true);
			List<FileInfo> list = P4258EBF.AFA7138A.M6233B19[417](P4258EBF.AFA7138A.M6233B19[216](text), "backup_*.cfg").OrderBy<FileInfo, DateTime>(delegate(FileInfo f)
			{
				string text7 = P4258EBF.AFA7138A.M6233B19[391](P4258EBF.AFA7138A.M6233B19[31](f));
				string text8 = P4258EBF.AFA7138A.M6233B19[398](text7, P4258EBF.AFA7138A.M6233B19[153]("backup_"));
				DateTime dateTime2;
				if (P4258EBF.AFA7138A.M6233B19[293](text8, "dd.MM.yyyy", null, DateTimeStyles.None, ref dateTime2))
				{
					return dateTime2;
				}
				return P4258EBF.AFA7138A.M6233B19[572]();
			}).ToList<FileInfo>();
			while (list.Count > 5)
			{
				P4258EBF.AFA7138A.M6233B19[289](P4258EBF.AFA7138A.M6233B19[329](list[0]));
				list.RemoveAt(0);
			}
			return text6;
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00144D2C File Offset: 0x0014252C
		public string getFullConfigs()
		{
			return P4258EBF.AFA7138A.M6233B19[267](P4258EBF.AFA7138A.M6233B19[478](WpfApp1.Model.MainLogic.appDataPath, "\\configs.json"));
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0015AD44 File Offset: 0x00158544
		public void applyConfig(string sourcePath)
		{
			WpfApp1.Model.MainLogic.ReadOnlyCheck();
			if (!P4258EBF.AFA7138A.M6233B19[627](sourcePath))
			{
				throw P4258EBF.AFA7138A.M6233B19[99]("Исходный файл не найден", sourcePath);
			}
			string text = P4258EBF.AFA7138A.M6233B19[158](WpfApp1.Model.MainLogic.appDataPath, "configs.json");
			if (!P4258EBF.AFA7138A.M6233B19[627](text))
			{
				throw P4258EBF.AFA7138A.M6233B19[99]("configs.json не найден", text);
			}
			JArray jarray = P4258EBF.AFA7138A.M6233B19[147](P4258EBF.AFA7138A.M6233B19[267](text));
			JObject jobject = jarray.FirstOrDefault<JToken>(delegate(JToken i)
			{
				JToken jtoken2 = P4258EBF.AFA7138A.M6233B19[327](i, "content");
				return GF2C9AAB.MF064291((jtoken2 != null) ? jtoken2.ToString() : null, sourcePath, StringComparison.OrdinalIgnoreCase);
			}) as JObject;
			if (jobject == null)
			{
				throw P4258EBF.AFA7138A.M6233B19[62]("Конфиг не найден в configs.json");
			}
			JToken jtoken = P4258EBF.AFA7138A.M6233B19[175](jobject, "launch_params");
			string text2 = ((jtoken != null) ? jtoken.ToString() : null) ?? "";
			string text3 = P4258EBF.AFA7138A.M6233B19[278](this.GetSelectedFolder(), "cfg", "client.cfg");
			string text4 = P4258EBF.AFA7138A.M6233B19[516](text3);
			if (!P4258EBF.AFA7138A.M6233B19[88](text4))
			{
				P4258EBF.AFA7138A.M6233B19[111](text4);
			}
			string text5 = P4258EBF.AFA7138A.M6233B19[267](sourcePath);
			P4258EBF.AFA7138A.M6233B19[94](text3, text5);
			RustTweakerViewModel rustTweakerViewModel = new RustTweakerViewModel();
			string text6 = P4258EBF.AFA7138A.M6233B19[267](WpfApp1.Model.MainLogic.SteamParser.GetConfigPathToLastUser());
			List<ValueTuple<string, string>> list = WpfApp1.Model.MainLogic.ParseLaunchParams(text2);
			rustTweakerViewModel.UpdateLocalConfig(WpfApp1.Model.MainLogic.SteamParser.GetConfigPathToLastUser(), WpfApp1.Model.MainLogic.BuildLaunchParams(list));
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0014EC4C File Offset: 0x0014C44C
		public void logout()
		{
			Auth.removeToken();
			P4258EBF.AFA7138A.M6233B19[361](P4258EBF.AFA7138A.M6233B19[610](P4258EBF.AFA7138A.M6233B19[371]()));
			P4258EBF.AFA7138A.M6233B19[238](P4258EBF.AFA7138A.M6233B19[82]());
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0015E968 File Offset: 0x0015C168
		public string getFullBinds()
		{
			return P4258EBF.AFA7138A.M6233B19[267](P4258EBF.AFA7138A.M6233B19[478](WpfApp1.Model.MainLogic.appDataPath, "\\binds.json"));
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0015543C File Offset: 0x00152C3C
		public bool addBind(string path, string name)
		{
			WpfApp1.Model.MainLogic.ReadOnlyCheck();
			JArray jarray = P4258EBF.AFA7138A.M6233B19[147](P4258EBF.AFA7138A.M6233B19[267](P4258EBF.AFA7138A.M6233B19[478](WpfApp1.Model.MainLogic.appDataPath, "\\binds.json")));
			if (jarray == null)
			{
				return false;
			}
			using (IEnumerator<JToken> enumerator = P4258EBF.AFA7138A.M6233B19[523](jarray))
			{
				while (P4258EBF.AFA7138A.M6233B19[411](enumerator))
				{
					JToken jtoken = enumerator.Current;
					JToken jtoken2 = P4258EBF.AFA7138A.M6233B19[327](jtoken, "content");
					if (jtoken2 != null && D4894B18.CEA99C94(jtoken2.ToString(), path, StringComparison.OrdinalIgnoreCase))
					{
						return false;
					}
				}
			}
			JObject jobject = P4258EBF.AFA7138A.M6233B19[345]();
			E4250989.FF0FA0A4(jobject, "is_select", P4258EBF.AFA7138A.M6233B19[448](false));
			E4250989.FF0FA0A4(jobject, "name", P4258EBF.AFA7138A.M6233B19[402](name));
			E4250989.FF0FA0A4(jobject, "content", P4258EBF.AFA7138A.M6233B19[402](path));
			JObject jobject2 = jobject;
			P4258EBF.AFA7138A.M6233B19[401](jarray, jobject2);
			P4258EBF.AFA7138A.M6233B19[94](P4258EBF.AFA7138A.M6233B19[478](WpfApp1.Model.MainLogic.appDataPath, "\\binds.json"), P4258EBF.AFA7138A.M6233B19[612](jarray, Formatting.Indented));
			return true;
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0014871C File Offset: 0x00145F1C
		public bool addBindOnlyPath(string path)
		{
			WpfApp1.Model.MainLogic.ReadOnlyCheck();
			string text = P4258EBF.AFA7138A.M6233B19[158](WpfApp1.Model.MainLogic.appDataPath, "binds.json");
			if (!P4258EBF.AFA7138A.M6233B19[627](text))
			{
				return false;
			}
			JArray jarray = P4258EBF.AFA7138A.M6233B19[147](P4258EBF.AFA7138A.M6233B19[267](text));
			if (jarray == null)
			{
				return false;
			}
			using (IEnumerator<JToken> enumerator = P4258EBF.AFA7138A.M6233B19[523](jarray))
			{
				while (P4258EBF.AFA7138A.M6233B19[411](enumerator))
				{
					JToken jtoken = enumerator.Current;
					JToken jtoken2 = P4258EBF.AFA7138A.M6233B19[327](jtoken, "content");
					if (jtoken2 != null && D4894B18.CEA99C94(jtoken2.ToString(), path, StringComparison.OrdinalIgnoreCase))
					{
						return false;
					}
				}
			}
			string text2 = P4258EBF.AFA7138A.M6233B19[391](path);
			JObject jobject = P4258EBF.AFA7138A.M6233B19[345]();
			E4250989.FF0FA0A4(jobject, "is_select", P4258EBF.AFA7138A.M6233B19[448](false));
			E4250989.FF0FA0A4(jobject, "name", P4258EBF.AFA7138A.M6233B19[402](text2));
			E4250989.FF0FA0A4(jobject, "content", P4258EBF.AFA7138A.M6233B19[402](path));
			JObject jobject2 = jobject;
			P4258EBF.AFA7138A.M6233B19[401](jarray, jobject2);
			P4258EBF.AFA7138A.M6233B19[94](text, P4258EBF.AFA7138A.M6233B19[612](jarray, Formatting.Indented));
			return true;
		}

		// Token: 0x0600032C RID: 812 RVA: 0x001472D0 File Offset: 0x00144AD0
		public string removeBind(string path)
		{
			WpfApp1.Model.MainLogic.ReadOnlyCheck();
			JArray jarray = P4258EBF.AFA7138A.M6233B19[147](P4258EBF.AFA7138A.M6233B19[267](P4258EBF.AFA7138A.M6233B19[478](WpfApp1.Model.MainLogic.appDataPath, "\\binds.json")));
			if (jarray == null)
			{
				return "JSON_ERROR";
			}
			bool flag = false;
			foreach (JToken jtoken in jarray.ToList<JToken>())
			{
				JObject jobject = (JObject)jtoken;
				JToken jtoken2 = P4258EBF.AFA7138A.M6233B19[175](jobject, "content");
				if (jtoken2 != null && D4894B18.CEA99C94(jtoken2.ToString(), path, StringComparison.OrdinalIgnoreCase))
				{
					P4258EBF.AFA7138A.M6233B19[217](jarray, jobject);
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return "NOT_FOUND";
			}
			P4258EBF.AFA7138A.M6233B19[289](path);
			P4258EBF.AFA7138A.M6233B19[94](P4258EBF.AFA7138A.M6233B19[478](WpfApp1.Model.MainLogic.appDataPath, "\\binds.json"), P4258EBF.AFA7138A.M6233B19[612](jarray, Formatting.Indented));
			return "ALLGOOD";
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0014F76C File Offset: 0x0014CF6C
		public string copyBind(string sourcePath)
		{
			WpfApp1.Model.MainLogic.ReadOnlyCheck();
			if (!P4258EBF.AFA7138A.M6233B19[627](sourcePath))
			{
				throw P4258EBF.AFA7138A.M6233B19[99]("Исходный файл не найден", sourcePath);
			}
			string text = P4258EBF.AFA7138A.M6233B19[158](WpfApp1.Model.MainLogic.appDataPath, "binds");
			P4258EBF.AFA7138A.M6233B19[111](text);
			string text2 = P4258EBF.AFA7138A.M6233B19[391](sourcePath);
			string text3 = P4258EBF.AFA7138A.M6233B19[124](sourcePath);
			string text4 = P4258EBF.AFA7138A.M6233B19[64](text2, "_copy", text3);
			string text5 = P4258EBF.AFA7138A.M6233B19[158](text, text4);
			int num = 1;
			while (P4258EBF.AFA7138A.M6233B19[627](text5))
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
				P4258EBF.AFA7138A.M6233B19[466](ref defaultInterpolatedStringHandler, 6, 3);
				P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler, text2);
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "_copy_");
				defaultInterpolatedStringHandler.AppendFormatted<int>(num);
				P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler, text3);
				text4 = P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler);
				text5 = P4258EBF.AFA7138A.M6233B19[158](text, text4);
				num++;
			}
			P4258EBF.AFA7138A.M6233B19[534](sourcePath, text5);
			return text5;
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00154CF0 File Offset: 0x001524F0
		public string copyFileInConfigs(string sourcePath)
		{
			try
			{
				WpfApp1.Model.MainLogic.ReadOnlyCheck();
				if (!P4258EBF.AFA7138A.M6233B19[627](sourcePath))
				{
					return null;
				}
				string text = P4258EBF.AFA7138A.M6233B19[158](WpfApp1.Model.MainLogic.appDataPath, "configs");
				L083B68C l083B68C = P4258EBF.AFA7138A.M6233B19[158];
				string text2 = text;
				Guid guid = P4258EBF.AFA7138A.M6233B19[476]();
				string text3 = l083B68C(text2, P4258EBF.AFA7138A.M6233B19[478](guid.ToString(), P4258EBF.AFA7138A.M6233B19[124](sourcePath)));
				P4258EBF.AFA7138A.M6233B19[534](sourcePath, text3);
				return text3;
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
			}
			return null;
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00153118 File Offset: 0x00150918
		public string selectBind(string path)
		{
			WpfApp1.Model.MainLogic.ReadOnlyCheck();
			JArray jarray = P4258EBF.AFA7138A.M6233B19[147](P4258EBF.AFA7138A.M6233B19[267](P4258EBF.AFA7138A.M6233B19[478](WpfApp1.Model.MainLogic.appDataPath, "\\binds.json")));
			if (jarray == null)
			{
				return "JSON_ERROR";
			}
			bool flag = false;
			using (IEnumerator<JToken> enumerator = P4258EBF.AFA7138A.M6233B19[523](jarray))
			{
				while (P4258EBF.AFA7138A.M6233B19[411](enumerator))
				{
					JObject jobject = (JObject)enumerator.Current;
					JToken jtoken = P4258EBF.AFA7138A.M6233B19[175](jobject, "content");
					string text = ((jtoken != null) ? jtoken.ToString() : null);
					if (text != null && P4258EBF.AFA7138A.M6233B19[85](text, path, StringComparison.OrdinalIgnoreCase))
					{
						P4258EBF.AFA7138A.M6233B19[542](jobject, "is_select", P4258EBF.AFA7138A.M6233B19[448](true));
						flag = true;
					}
					else
					{
						P4258EBF.AFA7138A.M6233B19[542](jobject, "is_select", P4258EBF.AFA7138A.M6233B19[448](false));
					}
				}
			}
			if (!flag)
			{
				return "NOT_FOUND";
			}
			P4258EBF.AFA7138A.M6233B19[94](P4258EBF.AFA7138A.M6233B19[478](WpfApp1.Model.MainLogic.appDataPath, "\\binds.json"), P4258EBF.AFA7138A.M6233B19[612](jarray, Formatting.Indented));
			return "ALLGOOD";
		}

		// Token: 0x06000330 RID: 816 RVA: 0x001589E8 File Offset: 0x001561E8
		public string renameConfig(string path, string newName)
		{
			WpfApp1.Model.MainLogic.ReadOnlyCheck();
			string text = P4258EBF.AFA7138A.M6233B19[158](WpfApp1.Model.MainLogic.appDataPath, "configs.json");
			if (!P4258EBF.AFA7138A.M6233B19[627](text))
			{
				return "JSON_NOT_FOUND";
			}
			JArray jarray = P4258EBF.AFA7138A.M6233B19[147](P4258EBF.AFA7138A.M6233B19[267](text));
			if (jarray == null)
			{
				return "JSON_ERROR";
			}
			bool flag = false;
			using (IEnumerator<JToken> enumerator = P4258EBF.AFA7138A.M6233B19[523](jarray))
			{
				while (P4258EBF.AFA7138A.M6233B19[411](enumerator))
				{
					JObject jobject = (JObject)enumerator.Current;
					JToken jtoken = P4258EBF.AFA7138A.M6233B19[175](jobject, "content");
					string text2 = ((jtoken != null) ? jtoken.ToString() : null);
					if (text2 != null && P4258EBF.AFA7138A.M6233B19[85](text2, path, StringComparison.OrdinalIgnoreCase))
					{
						P4258EBF.AFA7138A.M6233B19[542](jobject, "name", P4258EBF.AFA7138A.M6233B19[402](newName));
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				return "NOT_FOUND";
			}
			P4258EBF.AFA7138A.M6233B19[94](text, P4258EBF.AFA7138A.M6233B19[612](jarray, Formatting.Indented));
			return "ALLGOOD";
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0014A500 File Offset: 0x00147D00
		public string renameBind(string path, string newName)
		{
			WpfApp1.Model.MainLogic.ReadOnlyCheck();
			string text = P4258EBF.AFA7138A.M6233B19[158](WpfApp1.Model.MainLogic.appDataPath, "binds.json");
			if (!P4258EBF.AFA7138A.M6233B19[627](text))
			{
				return "JSON_NOT_FOUND";
			}
			JArray jarray = P4258EBF.AFA7138A.M6233B19[147](P4258EBF.AFA7138A.M6233B19[267](text));
			if (jarray == null)
			{
				return "JSON_ERROR";
			}
			bool flag = false;
			using (IEnumerator<JToken> enumerator = P4258EBF.AFA7138A.M6233B19[523](jarray))
			{
				while (P4258EBF.AFA7138A.M6233B19[411](enumerator))
				{
					JObject jobject = (JObject)enumerator.Current;
					JToken jtoken = P4258EBF.AFA7138A.M6233B19[175](jobject, "content");
					string text2 = ((jtoken != null) ? jtoken.ToString() : null);
					if (text2 != null && P4258EBF.AFA7138A.M6233B19[85](text2, path, StringComparison.OrdinalIgnoreCase))
					{
						P4258EBF.AFA7138A.M6233B19[542](jobject, "name", P4258EBF.AFA7138A.M6233B19[402](newName));
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				return "NOT_FOUND";
			}
			P4258EBF.AFA7138A.M6233B19[94](text, P4258EBF.AFA7138A.M6233B19[612](jarray, Formatting.Indented));
			return "ALLGOOD";
		}

		// Token: 0x06000332 RID: 818 RVA: 0x001512B4 File Offset: 0x0014EAB4
		public string createBind()
		{
			WpfApp1.Model.MainLogic.ReadOnlyCheck();
			string text = P4258EBF.AFA7138A.M6233B19[158](WpfApp1.Model.MainLogic.appDataPath, "binds");
			P4258EBF.AFA7138A.M6233B19[111](text);
			string text2 = P4258EBF.AFA7138A.M6233B19[278](this.GetSelectedFolder(), "cfg", "keys.cfg");
			if (!P4258EBF.AFA7138A.M6233B19[627](text2))
			{
				throw P4258EBF.AFA7138A.M6233B19[99]("Исходный файл не найден", text2);
			}
			string text3 = P4258EBF.AFA7138A.M6233B19[391](text2);
			string text4 = P4258EBF.AFA7138A.M6233B19[124](text2);
			DateTime dateTime = P4258EBF.AFA7138A.M6233B19[300]();
			string text5 = P4258EBF.AFA7138A.M6233B19[362](ref dateTime, "dd.MM.yyyy");
			string text6 = P4258EBF.AFA7138A.M6233B19[259](text3, "_", text5, text4);
			string text7 = P4258EBF.AFA7138A.M6233B19[158](text, text6);
			int num = 1;
			while (P4258EBF.AFA7138A.M6233B19[627](text7))
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
				P4258EBF.AFA7138A.M6233B19[466](ref defaultInterpolatedStringHandler, 2, 4);
				P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler, text3);
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "_");
				P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler, text5);
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "_");
				defaultInterpolatedStringHandler.AppendFormatted<int>(num);
				P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler, text4);
				text6 = P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler);
				text7 = P4258EBF.AFA7138A.M6233B19[158](text, text6);
				num++;
			}
			P4258EBF.AFA7138A.M6233B19[534](text2, text7);
			return text7;
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0014F3F8 File Offset: 0x0014CBF8
		public void applyBind(string sourcePath)
		{
			WpfApp1.Model.MainLogic.ReadOnlyCheck();
			string text = P4258EBF.AFA7138A.M6233B19[278](this.GetSelectedFolder(), "cfg", "keys.cfg");
			if (!P4258EBF.AFA7138A.M6233B19[627](sourcePath))
			{
				Logger.Log(P4258EBF.AFA7138A.M6233B19[478]("Исходный файл не найден: ", sourcePath));
				return;
			}
			string text2 = P4258EBF.AFA7138A.M6233B19[516](text);
			if (!P4258EBF.AFA7138A.M6233B19[88](text2))
			{
				P4258EBF.AFA7138A.M6233B19[111](text2);
			}
			string text3 = P4258EBF.AFA7138A.M6233B19[267](sourcePath);
			P4258EBF.AFA7138A.M6233B19[94](text, text3);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00011639 File Offset: 0x0000FA39
		public bool resetGraphics()
		{
			return WpfApp1.Model.MainLogic.resetGraphics();
		}

		// Token: 0x06000335 RID: 821 RVA: 0x001439FC File Offset: 0x001411FC
		public void checkCurrntConfigs()
		{
			string text = P4258EBF.AFA7138A.M6233B19[158](WpfApp1.Model.MainLogic.appDataPath, "configs.json");
			if (!P4258EBF.AFA7138A.M6233B19[627](text))
			{
				return;
			}
			JArray jarray = P4258EBF.AFA7138A.M6233B19[147](P4258EBF.AFA7138A.M6233B19[267](text));
			if (jarray == null)
			{
				return;
			}
			JArray jarray2 = P4258EBF.AFA7138A.M6233B19[147](P4258EBF.AFA7138A.M6233B19[267](P4258EBF.AFA7138A.M6233B19[158](WpfApp1.Model.MainLogic.appDataPath, "paths.json")));
			if (jarray == null)
			{
				return;
			}
			JToken jtoken = jarray.FirstOrDefault<JToken>((JToken x) => P4258EBF.AFA7138A.M6233B19[578](P4258EBF.AFA7138A.M6233B19[327](x, "is_select")).GetValueOrDefault());
			JToken jtoken2 = jarray2.FirstOrDefault<JToken>((JToken x) => P4258EBF.AFA7138A.M6233B19[578](P4258EBF.AFA7138A.M6233B19[327](x, "is_select")).GetValueOrDefault());
			if (jtoken != null && jtoken2 != null)
			{
				JToken jtoken3 = P4258EBF.AFA7138A.M6233B19[327](jtoken, "content");
				string text2 = I7A29812.F086D53B((jtoken3 != null) ? jtoken3.ToString() : null);
				JToken jtoken4 = P4258EBF.AFA7138A.M6233B19[327](jtoken, "launch_params");
				string text3 = ((jtoken4 != null) ? jtoken4.ToString() : null);
				JToken jtoken5 = P4258EBF.AFA7138A.M6233B19[327](jtoken2, "folder");
				string text4 = I7A29812.F086D53B(A3320FA7.N6B66084((jtoken5 != null) ? jtoken5.ToString() : null, "\\cfg\\client.cfg"));
				RustTweakerViewModel rustTweakerViewModel = new RustTweakerViewModel();
				List<FileActions.PathNode> list = JsonConvert.DeserializeObject<List<FileActions.PathNode>>(P4258EBF.AFA7138A.M6233B19[267](P4258EBF.AFA7138A.M6233B19[158](WpfApp1.Model.MainLogic.appDataPath, "paths.json")));
				FileActions.PathNode pathNode = list.FirstOrDefault<FileActions.PathNode>((FileActions.PathNode item) => item.is_select);
				string text5 = ((pathNode != null) ? pathNode.folder : null);
				string text6 = rustTweakerViewModel.ExtractRustLaunchOptions(WpfApp1.Model.MainLogic.SteamParser.GetConfigPathToLastUser(), text5);
				if (P4258EBF.AFA7138A.M6233B19[593](text2, text4) || P4258EBF.AFA7138A.M6233B19[593](text3, text6))
				{
					P4258EBF.AFA7138A.M6233B19[136](jtoken, "is_select", P4258EBF.AFA7138A.M6233B19[448](false));
					P4258EBF.AFA7138A.M6233B19[94](text, P4258EBF.AFA7138A.M6233B19[612](jarray, Formatting.Indented));
					return;
				}
			}
			else
			{
				Logger.Log("Havent current launch");
			}
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0014D7F4 File Offset: 0x0014AFF4
		public void checkCurrntBinds()
		{
			string text = P4258EBF.AFA7138A.M6233B19[158](WpfApp1.Model.MainLogic.appDataPath, "binds.json");
			if (!P4258EBF.AFA7138A.M6233B19[627](text))
			{
				return;
			}
			JArray jarray = P4258EBF.AFA7138A.M6233B19[147](P4258EBF.AFA7138A.M6233B19[267](text));
			if (jarray == null)
			{
				return;
			}
			JArray jarray2 = P4258EBF.AFA7138A.M6233B19[147](P4258EBF.AFA7138A.M6233B19[267](P4258EBF.AFA7138A.M6233B19[158](WpfApp1.Model.MainLogic.appDataPath, "paths.json")));
			if (jarray == null)
			{
				return;
			}
			JToken jtoken = jarray.FirstOrDefault<JToken>((JToken x) => P4258EBF.AFA7138A.M6233B19[578](P4258EBF.AFA7138A.M6233B19[327](x, "is_select")).GetValueOrDefault());
			JToken jtoken2 = jarray2.FirstOrDefault<JToken>((JToken x) => P4258EBF.AFA7138A.M6233B19[578](P4258EBF.AFA7138A.M6233B19[327](x, "is_select")).GetValueOrDefault());
			if (jtoken != null && jtoken2 != null)
			{
				JToken jtoken3 = P4258EBF.AFA7138A.M6233B19[327](jtoken, "content");
				string text2 = I7A29812.F086D53B((jtoken3 != null) ? jtoken3.ToString() : null);
				JToken jtoken4 = P4258EBF.AFA7138A.M6233B19[327](jtoken2, "folder");
				string text3 = I7A29812.F086D53B(H00E4429.O3A30837((jtoken4 != null) ? jtoken4.ToString() : null, "cfg", "keys.cfg"));
				if (P4258EBF.AFA7138A.M6233B19[593](text2, text3))
				{
					P4258EBF.AFA7138A.M6233B19[136](jtoken, "is_select", P4258EBF.AFA7138A.M6233B19[448](false));
					P4258EBF.AFA7138A.M6233B19[94](text, P4258EBF.AFA7138A.M6233B19[612](jarray, Formatting.Indented));
					return;
				}
			}
			else
			{
				Logger.Log("Havent current launch");
			}
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00011920 File Offset: 0x0000FD20
		public bool HaveIsSelectConfigs()
		{
			Configs.ConfigNode[] array = Configs.ParseConfigsConfig(null);
			return array.Any<Configs.ConfigNode>((Configs.ConfigNode x) => x.is_select);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00156C04 File Offset: 0x00154404
		public void updateCurrentConfig()
		{
			string currentParamsLaunch = this.getCurrentParamsLaunch();
			string text = P4258EBF.AFA7138A.M6233B19[267](P4258EBF.AFA7138A.M6233B19[278](Configs.getCurrentSelectedFolder(), "cfg", "client.cfg"));
			Configs.ConfigNode[] array = Configs.ParseConfigsConfig(null);
			Configs.ConfigNode configNode = array.FirstOrDefault<Configs.ConfigNode>((Configs.ConfigNode x) => x.is_select);
			if (configNode == null)
			{
				return;
			}
			configNode.launch_params = currentParamsLaunch;
			P4258EBF.AFA7138A.M6233B19[94](configNode.content, text);
			P4258EBF.AFA7138A.M6233B19[94](P4258EBF.AFA7138A.M6233B19[158](WpfApp1.Model.MainLogic.appDataPath, "configs.json"), P4258EBF.AFA7138A.M6233B19[412](array, Formatting.Indented));
		}

		// Token: 0x06000339 RID: 825 RVA: 0x000119EC File Offset: 0x0000FDEC
		public async Task<string> getStatistics(string id)
		{
			string text2;
			try
			{
				string text = await Statistic.GetStatistics(id);
				text2 = text;
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
				Logger.Log(P4258EBF.AFA7138A.M6233B19[478]("Error when get statistic: ", id));
				text2 = null;
			}
			return text2;
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00011A30 File Offset: 0x0000FE30
		public async Task<string> getFriendsPage(string id, int offset)
		{
			string text2;
			try
			{
				string text = await Statistic.GetFreindList(id, offset);
				text2 = text;
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
				Logger.Log(P4258EBF.AFA7138A.M6233B19[478]("Error when get statistic: ", id));
				text2 = null;
			}
			return text2;
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00011A7C File Offset: 0x0000FE7C
		public async Task<string> getSimpleUserInfo(string id)
		{
			string text2;
			try
			{
				string text = await Statistic.GetSimpleUserInfo(id);
				text2 = text;
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
				Logger.Log(P4258EBF.AFA7138A.M6233B19[478]("Error when get statistic: ", id));
				text2 = null;
			}
			return text2;
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00011AC0 File Offset: 0x0000FEC0
		public async Task<string> getLeaderboard(string statType, int limit)
		{
			string text3;
			try
			{
				string text = await Statistic.GetLeaderboard(statType, limit);
				string text2 = text;
				text3 = text2;
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
				Logger.Log(P4258EBF.AFA7138A.M6233B19[478]("Error when get leaderboard ", statType));
				text3 = null;
			}
			return text3;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00011B0B File Offset: 0x0000FF0B
		public void GuardCheck()
		{
			AuthGuard.OnAction();
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0015F6A0 File Offset: 0x0015CEA0
		public JsBridge()
		{
			P4258EBF.AFA7138A.M6233B19[130](this);
		}

		// Token: 0x020000EF RID: 239
		public class PathItem
		{
			// Token: 0x170000CE RID: 206
			// (get) Token: 0x06000563 RID: 1379 RVA: 0x0001DD4E File Offset: 0x0001C14E
			// (set) Token: 0x06000564 RID: 1380 RVA: 0x0001DD56 File Offset: 0x0001C156
			public bool is_select { get; set; }

			// Token: 0x170000CF RID: 207
			// (get) Token: 0x06000565 RID: 1381 RVA: 0x0001DD5F File Offset: 0x0001C15F
			// (set) Token: 0x06000566 RID: 1382 RVA: 0x0001DD67 File Offset: 0x0001C167
			public bool have_warn { get; set; }

			// Token: 0x170000D0 RID: 208
			// (get) Token: 0x06000567 RID: 1383 RVA: 0x0001DD70 File Offset: 0x0001C170
			// (set) Token: 0x06000568 RID: 1384 RVA: 0x0001DD78 File Offset: 0x0001C178
			public string folder { get; set; }

			// Token: 0x06000569 RID: 1385 RVA: 0x0015F778 File Offset: 0x0015CF78
			public PathItem()
			{
				P4258EBF.AFA7138A.M6233B19[130](this);
			}
		}

		// Token: 0x020000F0 RID: 240
		private class BindFileElement
		{
			// Token: 0x170000D1 RID: 209
			// (get) Token: 0x0600056A RID: 1386 RVA: 0x0001DD89 File Offset: 0x0001C189
			// (set) Token: 0x0600056B RID: 1387 RVA: 0x0001DD91 File Offset: 0x0001C191
			public bool is_select { get; set; }

			// Token: 0x170000D2 RID: 210
			// (get) Token: 0x0600056C RID: 1388 RVA: 0x0001DD9A File Offset: 0x0001C19A
			// (set) Token: 0x0600056D RID: 1389 RVA: 0x0001DDA2 File Offset: 0x0001C1A2
			public string name { get; set; }

			// Token: 0x170000D3 RID: 211
			// (get) Token: 0x0600056E RID: 1390 RVA: 0x0001DDAB File Offset: 0x0001C1AB
			// (set) Token: 0x0600056F RID: 1391 RVA: 0x0001DDB3 File Offset: 0x0001C1B3
			public string content { get; set; }

			// Token: 0x06000570 RID: 1392 RVA: 0x00151BFC File Offset: 0x0014F3FC
			public BindFileElement()
			{
				P4258EBF.AFA7138A.M6233B19[130](this);
			}
		}
	}
}
