using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using Microsoft.Web.WebView2.Wpf;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WpfApp1.Model;

namespace WpfApp1
{
	// Token: 0x02000054 RID: 84
	public partial class LoginPage : Window
	{
		// Token: 0x060002CB RID: 715 RVA: 0x00156BB4 File Offset: 0x001543B4
		public LoginPage()
		{
			P4258EBF.AFA7138A.M6233B19[227](this);
			this.InitializeComponent();
			this.InitializeAsync();
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0015F6F0 File Offset: 0x0015CEF0
		private void InitializeAsync()
		{
			LoginPage.<InitializeAsync>d__2 <InitializeAsync>d__;
			<InitializeAsync>d__.<>t__builder = P4258EBF.AFA7138A.M6233B19[385]();
			<InitializeAsync>d__.<>4__this = this;
			<InitializeAsync>d__.<>1__state = -1;
			<InitializeAsync>d__.<>t__builder.Start<LoginPage.<InitializeAsync>d__2>(ref <InitializeAsync>d__);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00143CA8 File Offset: 0x001414A8
		private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (P4258EBF.AFA7138A.M6233B19[169](e) is Button)
			{
				return;
			}
			try
			{
				P4258EBF.AFA7138A.M6233B19[15](this);
			}
			catch (InvalidOperationException)
			{
			}
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00161470 File Offset: 0x0015EC70
		private void Close_Click(object sender, RoutedEventArgs e)
		{
			P4258EBF.AFA7138A.M6233B19[238](P4258EBF.AFA7138A.M6233B19[82]());
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00156CD4 File Offset: 0x001544D4
		private void openLink(object sender, RoutedEventArgs e)
		{
			Button button = sender as Button;
			string text;
			if (button == null)
			{
				text = null;
			}
			else
			{
				object obj = P4258EBF.AFA7138A.M6233B19[6](button);
				text = ((obj != null) ? obj.ToString() : null);
			}
			ProcessStartInfo processStartInfo = LD1D3D23.O33F1980(text);
			O8258311.M5A8918D(processStartInfo, true);
			JC11021F.C827CF8C(processStartInfo);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0015EC68 File Offset: 0x0015C468
		private void Minimize_Click(object sender, RoutedEventArgs e)
		{
			P4258EBF.AFA7138A.M6233B19[504](this, WindowState.Minimized);
		}

		// Token: 0x040000E1 RID: 225
		private static readonly string logScript = "\r\n(function () {\r\n\r\n    function sendToHost(type, data) {\r\n        try {\r\n            chrome.webview.hostObjects.csAPI.jsLog(type, data?.toString());\r\n        } catch (e) {\r\n            // если host object недоступен — просто игнор\r\n        }\r\n    }\r\n\r\n    // --- console перехват ---\r\n    const levels = ['log', 'warn', 'error', 'info', 'debug'];\r\n\r\n    levels.forEach(level => {\r\n        const original = console[level];\r\n        console[level] = function (...args) {\r\n            try {\r\n                sendToHost('console.' + level, args.map(a => {\r\n                    if (a instanceof Error) return a.stack || a.message;\r\n                    if (typeof a === 'object') return JSON.stringify(a);\r\n                    return a;\r\n                }).join(' '));\r\n            } catch {}\r\n\r\n            original.apply(console, args);\r\n        };\r\n    });\r\n\r\n    // --- Глобальные JS ошибки ---\r\n    window.addEventListener('error', function (event) {\r\n        sendToHost('window.error',\r\n            event.message + ' | ' +\r\n            event.filename + ':' + event.lineno + ':' + event.colno);\r\n    });\r\n\r\n    // --- Необработанные Promise ошибки ---\r\n    window.addEventListener('unhandledrejection', function (event) {\r\n        let reason = event.reason;\r\n        if (reason instanceof Error)\r\n            reason = reason.stack || reason.message;\r\n\r\n        sendToHost('unhandledrejection', reason);\r\n    });\r\n\r\n})();\r\n";

		// Token: 0x020000E8 RID: 232
		[ComVisible(true)]
		public class JsBridge
		{
			// Token: 0x06000553 RID: 1363 RVA: 0x0015A4E0 File Offset: 0x00157CE0
			public string getCurrentLang()
			{
				string text = P4258EBF.AFA7138A.M6233B19[158](MainLogic.appDataPath, "lang.json");
				if (!P4258EBF.AFA7138A.M6233B19[627](text))
				{
					P4258EBF.AFA7138A.M6233B19[94](text, "{\"lang\":\"ru\"}");
				}
				return P4258EBF.AFA7138A.M6233B19[267](text);
			}

			// Token: 0x06000554 RID: 1364 RVA: 0x0013AB18 File Offset: 0x00138318
			public void updateLang(string newLangJson)
			{
				string text = P4258EBF.AFA7138A.M6233B19[158](MainLogic.appDataPath, "lang.json");
				if (!P4258EBF.AFA7138A.M6233B19[627](text))
				{
					P4258EBF.AFA7138A.M6233B19[94](text, "{\"lang\":\"ru\"}");
				}
				P4258EBF.AFA7138A.M6233B19[94](text, newLangJson);
			}

			// Token: 0x06000555 RID: 1365 RVA: 0x0001D498 File Offset: 0x0001B898
			public async Task<int> auth(string _json)
			{
				JObject payload = JsonConvert.DeserializeObject<Controller._GlobalActionType>(_json).Payload;
				ValueTuple<bool, int> valueTuple = await Auth.userAuth(payload);
				ValueTuple<bool, int> valueTuple2 = valueTuple;
				int num;
				if (valueTuple2.Item1 && Auth.checkAuth())
				{
					AuthGuard.SetAuthenticated(true);
					Controller.openMainWindow();
					Controller.closeLoginPage();
					num = 0;
				}
				else
				{
					num = valueTuple2.Item2;
				}
				return num;
			}

			// Token: 0x06000556 RID: 1366 RVA: 0x0015D8DC File Offset: 0x0015B0DC
			public JsBridge()
			{
				P4258EBF.AFA7138A.M6233B19[130](this);
			}
		}
	}
}
