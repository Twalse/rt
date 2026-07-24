using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using Microsoft.Web.WebView2.Wpf;
using RustTweaker;
using WpfApp1.Model;

namespace WpfApp1
{
	// Token: 0x02000055 RID: 85
	public partial class MainWindow : Window
	{
		// Token: 0x060002D6 RID: 726 RVA: 0x001582C8 File Offset: 0x00155AC8
		public MainWindow()
		{
			P4258EBF.AFA7138A.M6233B19[227](this);
			if (!AuthGuard.IsAuthenticated)
			{
				Logger.Log("MainWindow: not authenticated");
				P4258EBF.AFA7138A.M6233B19[505](1);
				return;
			}
			this.InitializeComponent();
			JA12D4B2 ja12D4B = P4258EBF.AFA7138A.M6233B19[206];
			Rect rect = P4258EBF.AFA7138A.M6233B19[344]();
			ja12D4B(this, P4258EBF.AFA7138A.M6233B19[201](ref rect) + 8.0);
			P4258EBF.AFA7138A.M6233B19[160](this, P4258EBF.AFA7138A.M6233B19[529](this, ldftn(MainWindow_StateChanged)));
			this.InitializeAsync();
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x001480C8 File Offset: 0x001458C8
		public static void NavigateToBenchmarkResult(int? submissionId, string steamId = null)
		{
			MainWindow.<>c__DisplayClass2_0 CS$<>8__locals1 = new MainWindow.<>c__DisplayClass2_0();
			CS$<>8__locals1.submissionId = submissionId;
			CS$<>8__locals1.steamId = steamId;
			try
			{
				Application application = P4258EBF.AFA7138A.M6233B19[82]();
				if (application != null)
				{
					GCBDD11B.MAB2A6A1(J39A4A2E.B0BE4E33(application), P4258EBF.AFA7138A.M6233B19[579](CS$<>8__locals1, ldftn(<NavigateToBenchmarkResult>b__0)), Array.Empty<object>());
				}
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
			}
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00157924 File Offset: 0x00155124
		private void FocusTweakerWindow()
		{
			if (P4258EBF.AFA7138A.M6233B19[149](this) == WindowState.Minimized)
			{
				P4258EBF.AFA7138A.M6233B19[504](this, WindowState.Normal);
			}
			P4258EBF.AFA7138A.M6233B19[469](this, true);
			P4258EBF.AFA7138A.M6233B19[512](this);
			P4258EBF.AFA7138A.M6233B19[38](this);
			P4258EBF.AFA7138A.M6233B19[469](this, false);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0015D9F4 File Offset: 0x0015B1F4
		private void MainWindow_StateChanged(object sender, EventArgs e)
		{
			if (P4258EBF.AFA7138A.M6233B19[149](this) == WindowState.Maximized)
			{
				P4258EBF.AFA7138A.M6233B19[83](this.RootBorder, P4258EBF.AFA7138A.M6233B19[332](0.0));
				return;
			}
			if (P4258EBF.AFA7138A.M6233B19[149](this) == WindowState.Normal)
			{
				P4258EBF.AFA7138A.M6233B19[83](this.RootBorder, P4258EBF.AFA7138A.M6233B19[332](28.0));
			}
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00160BAC File Offset: 0x0015E3AC
		private void InitializeAsync()
		{
			MainWindow.<InitializeAsync>d__5 <InitializeAsync>d__;
			<InitializeAsync>d__.<>t__builder = P4258EBF.AFA7138A.M6233B19[385]();
			<InitializeAsync>d__.<>4__this = this;
			<InitializeAsync>d__.<>1__state = -1;
			<InitializeAsync>d__.<>t__builder.Start<MainWindow.<InitializeAsync>d__5>(ref <InitializeAsync>d__);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00141128 File Offset: 0x0013E928
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

		// Token: 0x060002DC RID: 732 RVA: 0x00140EFC File Offset: 0x0013E6FC
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

		// Token: 0x060002DD RID: 733 RVA: 0x0015B2E8 File Offset: 0x00158AE8
		private void Fullsize_Click(object sender, RoutedEventArgs e)
		{
			if (P4258EBF.AFA7138A.M6233B19[149](this) == WindowState.Normal)
			{
				P4258EBF.AFA7138A.M6233B19[83](this.RootBorder, P4258EBF.AFA7138A.M6233B19[332](0.0));
				P4258EBF.AFA7138A.M6233B19[504](this, WindowState.Maximized);
				return;
			}
			if (P4258EBF.AFA7138A.M6233B19[149](this) == WindowState.Maximized)
			{
				P4258EBF.AFA7138A.M6233B19[83](this.RootBorder, P4258EBF.AFA7138A.M6233B19[332](28.0));
				P4258EBF.AFA7138A.M6233B19[504](this, WindowState.Normal);
			}
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0015BF5C File Offset: 0x0015975C
		private void Close_Click(object sender, RoutedEventArgs e)
		{
			P4258EBF.AFA7138A.M6233B19[238](P4258EBF.AFA7138A.M6233B19[82]());
		}

		// Token: 0x060002DF RID: 735 RVA: 0x001609DC File Offset: 0x0015E1DC
		private void Minimize_Click(object sender, RoutedEventArgs e)
		{
			P4258EBF.AFA7138A.M6233B19[504](this, WindowState.Minimized);
		}

		// Token: 0x040000E4 RID: 228
		private static readonly string logScript = "\r\n(function () {\r\n\r\n    function sendToHost(type, data) {\r\n        try {\r\n            chrome.webview.hostObjects.csAPI.jsLog(type, data?.toString());\r\n        } catch (e) {\r\n            // если host object недоступен — просто игнор\r\n        }\r\n    }\r\n\r\n    // --- console перехват ---\r\n    const levels = ['log', 'warn', 'error', 'info', 'debug'];\r\n\r\n    levels.forEach(level => {\r\n        const original = console[level];\r\n        console[level] = function (...args) {\r\n            try {\r\n                sendToHost('console.' + level, args.map(a => {\r\n                    if (a instanceof Error) return a.stack || a.message;\r\n                    if (typeof a === 'object') return JSON.stringify(a);\r\n                    return a;\r\n                }).join(' '));\r\n            } catch {}\r\n\r\n            original.apply(console, args);\r\n        };\r\n    });\r\n\r\n    // --- Глобальные JS ошибки ---\r\n    window.addEventListener('error', function (event) {\r\n        sendToHost('window.error',\r\n            event.message + ' | ' +\r\n            event.filename + ':' + event.lineno + ':' + event.colno);\r\n    });\r\n\r\n    // --- Необработанные Promise ошибки ---\r\n    window.addEventListener('unhandledrejection', function (event) {\r\n        let reason = event.reason;\r\n        if (reason instanceof Error)\r\n            reason = reason.stack || reason.message;\r\n\r\n        sendToHost('unhandledrejection', reason);\r\n    });\r\n\r\n})();\r\n";
	}
}
