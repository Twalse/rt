using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.Win32;
using Newtonsoft.Json;
using RustTweaker;
using RustTweaker_NET.Services;
using WpfApp1.Model;

namespace RustTweakerDemo
{
	// Token: 0x02000011 RID: 17
	public class RustTweakerViewModel : ViewModelBase
	{
		// Token: 0x06000063 RID: 99
		[DllImport("user32.dll")]
		private static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000064 RID: 100 RVA: 0x000030BA File Offset: 0x000014BA
		// (set) Token: 0x06000065 RID: 101 RVA: 0x000030C2 File Offset: 0x000014C2
		public ObservableCollection<SteamAccount> SteamAccounts
		{
			get
			{
				return this._steamAccounts;
			}
			set
			{
				this.SetProperty<ObservableCollection<SteamAccount>>(ref this._steamAccounts, value, "SteamAccounts");
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000066 RID: 102 RVA: 0x000030D7 File Offset: 0x000014D7
		// (set) Token: 0x06000067 RID: 103 RVA: 0x000030DF File Offset: 0x000014DF
		public SteamAccount SelectedAccount
		{
			get
			{
				return this._selectedAccount;
			}
			set
			{
				if (this.SetProperty<SteamAccount>(ref this._selectedAccount, value, "SelectedAccount"))
				{
					this.UpdateConfigPath();
				}
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000030FB File Offset: 0x000014FB
		// (set) Token: 0x06000069 RID: 105 RVA: 0x00003103 File Offset: 0x00001503
		public string ConfigPath
		{
			get
			{
				return this._configPath;
			}
			set
			{
				this.SetProperty<string>(ref this._configPath, value, "ConfigPath");
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00003118 File Offset: 0x00001518
		// (set) Token: 0x0600006B RID: 107 RVA: 0x00003120 File Offset: 0x00001520
		public string CurrentParameters
		{
			get
			{
				return this._currentParameters;
			}
			set
			{
				this.SetProperty<string>(ref this._currentParameters, value, "CurrentParameters");
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003135 File Offset: 0x00001535
		public RustTweakerViewModel()
		{
			this._steamPath = this.GetSteamPath();
			this.SteamAccounts = new ObservableCollection<SteamAccount>();
			this.LoadSteamAccounts();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003165 File Offset: 0x00001565
		public static bool HasPendingBenchmarkBackups()
		{
			return BenchmarkBackupRecovery.HasPendingBackupsAtStartup();
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000316C File Offset: 0x0000156C
		public static void RestorePendingBenchmarkBackups()
		{
			BenchmarkBackupRecovery.TryRestorePendingBackupsAtStartup();
		}

		// Token: 0x0600006F RID: 111 RVA: 0x0014CDF0 File Offset: 0x0014A5F0
		private void LoadSteamAccounts()
		{
			this.SteamAccounts.Clear();
			string text = P4258EBF.AFA7138A.M6233B19[158](this._steamPath, "userdata");
			string text2 = P4258EBF.AFA7138A.M6233B19[278](this._steamPath, "config", "avatarcache");
			Logger.Log(P4258EBF.AFA7138A.M6233B19[478]("userdataPath: ", text));
			Logger.Log(P4258EBF.AFA7138A.M6233B19[478]("avatarCachePath: ", text2));
			if (!P4258EBF.AFA7138A.M6233B19[89](text))
			{
				Logger.Log("userdataPath не найден");
				return;
			}
			string[] array = P4258EBF.AFA7138A.M6233B19[495](text);
			foreach (string text3 in array)
			{
				Logger.Log(P4258EBF.AFA7138A.M6233B19[478]("dir: ", text3));
				string text4 = P4258EBF.AFA7138A.M6233B19[513](text3);
				long num;
				if (P4258EBF.AFA7138A.M6233B19[250](text4, "0") || P4258EBF.AFA7138A.M6233B19[250](text4, "ac") || !P4258EBF.AFA7138A.M6233B19[472](text4, ref num))
				{
					Logger.Log("cnt");
				}
				else
				{
					string text5 = P4258EBF.AFA7138A.M6233B19[278](text3, "config", "localconfig.vdf");
					Logger.Log(text5);
					Logger.Log(P4258EBF.AFA7138A.M6233B19[627](text5));
					if (P4258EBF.AFA7138A.M6233B19[627](text5))
					{
						DateTime dateTime = P4258EBF.AFA7138A.M6233B19[260](text5);
						long num2 = num + 76561197960265728L;
						string text6 = P4258EBF.AFA7138A.M6233B19[277](ref num2);
						string text7 = P4258EBF.AFA7138A.M6233B19[158](text2, P4258EBF.AFA7138A.M6233B19[478](text6, ".png"));
						string text8 = this.ExtractPersonaName(text5);
						SteamAccount steamAccount = new SteamAccount();
						steamAccount.SteamId = text4;
						steamAccount.PersonaName = text8;
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
						P4258EBF.AFA7138A.M6233B19[466](ref defaultInterpolatedStringHandler, 13, 1);
						P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "Использован: ");
						defaultInterpolatedStringHandler.AppendFormatted<DateTime>(dateTime, "dd.MM.yyyy");
						steamAccount.LastUsed = P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler);
						steamAccount.ConfigPath = text5;
						steamAccount.IsSelected = false;
						steamAccount.AvatarPath = (P4258EBF.AFA7138A.M6233B19[627](text7) ? text7 : null);
						SteamAccount steamAccount2 = steamAccount;
						this.SteamAccounts.Add(steamAccount2);
					}
				}
			}
			if (this.SteamAccounts.Any<SteamAccount>())
			{
				this.SelectedAccount = this.SteamAccounts.OrderByDescending<SteamAccount, DateTime>((SteamAccount a) => P4258EBF.AFA7138A.M6233B19[260](a.ConfigPath)).First<SteamAccount>();
				this.SelectedAccount.IsSelected = true;
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0015BF20 File Offset: 0x00159720
		private string ExtractPersonaName(string configPath)
		{
			string text = P4258EBF.AFA7138A.M6233B19[77](configPath, P4258EBF.AFA7138A.M6233B19[204]());
			return this.ExtractVdfValue(text, "PersonaName", 0);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0015B53C File Offset: 0x00158D3C
		private void UpdateConfigPath()
		{
			if (this.SelectedAccount != null)
			{
				this.ConfigPath = this.SelectedAccount.ConfigPath;
				using (IEnumerator<SteamAccount> enumerator = this.SteamAccounts.GetEnumerator())
				{
					while (P4258EBF.AFA7138A.M6233B19[411](enumerator))
					{
						SteamAccount steamAccount = enumerator.Current;
						steamAccount.IsSelected = steamAccount == this.SelectedAccount;
					}
					return;
				}
			}
			this.ConfigPath = "Аккаунт не выбран";
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0015F1C0 File Offset: 0x0015C9C0
		private void SyncCheckboxesWithCurrentParameters(string parameters)
		{
			if (P4258EBF.AFA7138A.M6233B19[88](parameters))
			{
				return;
			}
			this._parameters.Clear();
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0015DE74 File Offset: 0x0015B674
		public string ExtractRustLaunchOptions(string pathToConfig, string pathToGame)
		{
			string text = P4258EBF.AFA7138A.M6233B19[267](pathToConfig);
			string text2 = P4258EBF.AFA7138A.M6233B19[513](pathToGame);
			string text3 = (P4258EBF.AFA7138A.M6233B19[250](text2, "RustStaging") ? "\"700580\"" : "\"252490\"");
			int num = P4258EBF.AFA7138A.M6233B19[622](text, "Software");
			int num2 = P4258EBF.AFA7138A.M6233B19[599](text, text3, num);
			if (num2 == -1)
			{
				return "";
			}
			return this.ExtractVdfValue(text, "LaunchOptions", num2) ?? "";
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00145EE4 File Offset: 0x001436E4
		public string ExtractVdfValue(string content, string key, int searchStart = 0)
		{
			string text2;
			try
			{
				string text = P4258EBF.AFA7138A.M6233B19[64]("\"", key, "\"");
				int num = P4258EBF.AFA7138A.M6233B19[599](content, text, searchStart);
				if (num == -1)
				{
					text2 = null;
				}
				else
				{
					int num2 = num + P4258EBF.AFA7138A.M6233B19[152](text);
					int num3 = this.FindUnescapedQuote(content, num2);
					if (num3 == -1)
					{
						text2 = null;
					}
					else
					{
						int num4 = this.FindUnescapedQuote(content, num3 + 1);
						if (num4 == -1)
						{
							text2 = null;
						}
						else
						{
							text2 = P4258EBF.AFA7138A.M6233B19[487](content, num3 + 1, num4 - num3 - 1);
						}
					}
				}
			}
			catch
			{
				text2 = null;
			}
			return text2;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00158B74 File Offset: 0x00156374
		private int FindUnescapedQuote(string text, int startIndex)
		{
			for (;;)
			{
				int num = P4258EBF.AFA7138A.M6233B19[220](text, '"', startIndex);
				if (num == -1)
				{
					break;
				}
				int num2 = 0;
				int num3 = num - 1;
				while (num3 >= 0 && P4258EBF.AFA7138A.M6233B19[366](text, num3) == '\\')
				{
					num2++;
					num3--;
				}
				if (num2 % 2 == 0)
				{
					return num;
				}
				startIndex = num + 1;
			}
			return -1;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0015CA18 File Offset: 0x0015A218
		public void OnLaunch()
		{
			RustTweakerViewModel.<OnLaunch>d__30 <OnLaunch>d__;
			<OnLaunch>d__.<>t__builder = P4258EBF.AFA7138A.M6233B19[385]();
			<OnLaunch>d__.<>4__this = this;
			<OnLaunch>d__.<>1__state = -1;
			<OnLaunch>d__.<>t__builder.Start<RustTweakerViewModel.<OnLaunch>d__30>(ref <OnLaunch>d__);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00147434 File Offset: 0x00144C34
		public Task CloseSteam(int checkIntervalMs = 1000)
		{
			RustTweakerViewModel.<CloseSteam>d__31 <CloseSteam>d__;
			<CloseSteam>d__.<>t__builder = P4258EBF.AFA7138A.M6233B19[20]();
			<CloseSteam>d__.checkIntervalMs = checkIntervalMs;
			<CloseSteam>d__.<>1__state = -1;
			<CloseSteam>d__.<>t__builder.Start<RustTweakerViewModel.<CloseSteam>d__31>(ref <CloseSteam>d__);
			return P4258EBF.AFA7138A.M6233B19[19](ref <CloseSteam>d__.<>t__builder);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0015A700 File Offset: 0x00157F00
		public void UpdateLocalConfig(string configPath, string launchOptions)
		{
			try
			{
				if (!P4258EBF.AFA7138A.M6233B19[627](configPath))
				{
					throw P4258EBF.AFA7138A.M6233B19[62]("Файл localconfig.vdf не найден");
				}
				string text = P4258EBF.AFA7138A.M6233B19[267](configPath);
				int num = P4258EBF.AFA7138A.M6233B19[622](text, "Software");
				List<FileActions.PathNode> list = JsonConvert.DeserializeObject<List<FileActions.PathNode>>(P4258EBF.AFA7138A.M6233B19[267](P4258EBF.AFA7138A.M6233B19[158](MainLogic.appDataPath, "paths.json")));
				FileActions.PathNode pathNode = list.FirstOrDefault<FileActions.PathNode>((FileActions.PathNode item) => item.is_select);
				string text2 = ((pathNode != null) ? pathNode.folder : null);
				string text3 = (P4258EBF.AFA7138A.M6233B19[250](P4258EBF.AFA7138A.M6233B19[513](text2), "RustStaging") ? "\"700580\"" : "\"252490\"");
				int num2 = P4258EBF.AFA7138A.M6233B19[599](text, text3, num);
				if (num2 == -1)
				{
					text = this.AddRustSection(text, text3);
					num2 = P4258EBF.AFA7138A.M6233B19[599](text, text3, num);
				}
				text = this.UpdateRustLaunchOptions(text, num2, launchOptions);
				Logger.Log("new content:");
				Logger.Log(text);
				P4258EBF.AFA7138A.M6233B19[94](configPath, text);
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
				throw P4258EBF.AFA7138A.M6233B19[62](P4258EBF.AFA7138A.M6233B19[478]("Не удалось обновить конфигурацию: ", P4258EBF.AFA7138A.M6233B19[551](ex)));
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0014FED8 File Offset: 0x0014D6D8
		public void UpdateLocalConfigForAppId(string configPath, int appId, string launchOptions)
		{
			try
			{
				if (!P4258EBF.AFA7138A.M6233B19[627](configPath))
				{
					throw P4258EBF.AFA7138A.M6233B19[62]("Файл localconfig.vdf не найден");
				}
				string text = P4258EBF.AFA7138A.M6233B19[267](configPath);
				int num = P4258EBF.AFA7138A.M6233B19[622](text, "Software");
				if (num == -1)
				{
					throw P4258EBF.AFA7138A.M6233B19[62]("Секция 'Software' не найдена в localconfig.vdf");
				}
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = P4258EBF.AFA7138A.M6233B19[467](2, 1);
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "\"");
				defaultInterpolatedStringHandler.AppendFormatted<int>(appId);
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "\"");
				string text2 = P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler);
				int num2 = P4258EBF.AFA7138A.M6233B19[475](text, text2, num, StringComparison.Ordinal);
				if (num2 == -1)
				{
					text = this.AddRustSection(text, text2);
					num2 = P4258EBF.AFA7138A.M6233B19[475](text, text2, num, StringComparison.Ordinal);
				}
				text = this.UpdateRustLaunchOptions(text, num2, launchOptions ?? P4258EBF.AFA7138A.M6233B19[280]());
				P4258EBF.AFA7138A.M6233B19[94](configPath, text);
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2;
				P4258EBF.AFA7138A.M6233B19[466](ref defaultInterpolatedStringHandler2, 44, 2);
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler2, "Не удалось обновить конфигурацию по AppId=");
				defaultInterpolatedStringHandler2.AppendFormatted<int>(appId);
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler2, ": ");
				P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler2, P4258EBF.AFA7138A.M6233B19[551](ex));
				throw P4258EBF.AFA7138A.M6233B19[62](P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler2));
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0015978C File Offset: 0x00156F8C
		private string UpdateRustLaunchOptions(string content, int appIdIndex, string launchOptions)
		{
			string text = "\"LaunchOptions\"";
			int num = P4258EBF.AFA7138A.M6233B19[599](content, text, appIdIndex);
			int num2 = this.FindClosingBrace(content, appIdIndex);
			if (num != -1 && num < num2)
			{
				int num3 = this.FindUnescapedQuote(content, num + P4258EBF.AFA7138A.M6233B19[152](text));
				int num4 = this.FindUnescapedQuote(content, num3 + 1);
				if (num3 != -1 && num4 != -1)
				{
					content = P4258EBF.AFA7138A.M6233B19[56](content, num3 + 1, num4 - num3 - 1);
					content = P4258EBF.AFA7138A.M6233B19[296](content, num3 + 1, launchOptions);
				}
			}
			else
			{
				int num5 = num2;
				string text2 = P4258EBF.AFA7138A.M6233B19[64]("\t\t\t\t\"LaunchOptions\"\t\t\"", launchOptions, "\"\n\t\t\t");
				content = P4258EBF.AFA7138A.M6233B19[296](content, num5, text2);
			}
			return content;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00141528 File Offset: 0x0013ED28
		public string AddRustSection(string content, string rustId = "\"252490\"")
		{
			int num = P4258EBF.AFA7138A.M6233B19[622](content, "\"apps\"");
			if (num == -1)
			{
				throw P4258EBF.AFA7138A.M6233B19[62]("Секция 'apps' не найдена в localconfig.vdf");
			}
			int num2 = P4258EBF.AFA7138A.M6233B19[599](content, "{", num);
			string text = P4258EBF.AFA7138A.M6233B19[64]("\n\t\t\t", rustId, "\n\t\t\t{\n\t\t\t\t\"LaunchOptions\"\t\t\"\"\n\t\t\t}\n");
			content = P4258EBF.AFA7138A.M6233B19[296](content, num2 + 1, text);
			return content;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0013A284 File Offset: 0x00137A84
		private int FindClosingBrace(string content, int startIndex)
		{
			int num = 0;
			bool flag = false;
			for (int i = startIndex; i < P4258EBF.AFA7138A.M6233B19[152](content); i++)
			{
				if (P4258EBF.AFA7138A.M6233B19[366](content, i) == '{')
				{
					flag = true;
					num++;
				}
				else if (P4258EBF.AFA7138A.M6233B19[366](content, i) == '}')
				{
					num--;
					if (flag && num == 0)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x001545EC File Offset: 0x00151DEC
		private string GetSteamPath()
		{
			try
			{
				using (RegistryKey registryKey = P4258EBF.AFA7138A.M6233B19[415](P4258EBF.AFA7138A.M6233B19[378](), "Software\\Valve\\Steam"))
				{
					string text;
					if (registryKey == null)
					{
						text = null;
					}
					else
					{
						object obj = P4258EBF.AFA7138A.M6233B19[450](registryKey, "SteamPath");
						text = ((obj != null) ? obj.ToString() : null);
					}
					string text2 = text;
					return P4258EBF.AFA7138A.M6233B19[224](text2);
				}
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
			}
			return "C:\\Program Files (x86)\\Steam";
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000039F4 File Offset: 0x00001DF4
		private void ChangeParameters(bool isEnable, string parameter)
		{
			if (isEnable)
			{
				if (!this._parameters.Contains(parameter))
				{
					this._parameters.Add(parameter);
					return;
				}
			}
			else
			{
				this._parameters.Remove(parameter);
			}
		}

		// Token: 0x04000024 RID: 36
		private const uint WM_CLOSE = 16U;

		// Token: 0x04000025 RID: 37
		private List<string> _parameters = new List<string>();

		// Token: 0x04000026 RID: 38
		public string _steamPath;

		// Token: 0x04000027 RID: 39
		private ObservableCollection<SteamAccount> _steamAccounts;

		// Token: 0x04000028 RID: 40
		private SteamAccount _selectedAccount;

		// Token: 0x04000029 RID: 41
		private string _configPath;

		// Token: 0x0400002A RID: 42
		private string _currentParameters;
	}
}
