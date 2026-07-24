using System;
using System.Threading.Tasks;
using Microsoft.Web.WebView2.Wpf;
using Newtonsoft.Json.Linq;

namespace WpfApp1.Model
{
	// Token: 0x02000060 RID: 96
	public static class Controller
	{
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000363 RID: 867 RVA: 0x000122DA File Offset: 0x000106DA
		private static LoginPage loginPage
		{
			get
			{
				if (Controller._loginPage == null)
				{
					Controller._loginPage = new LoginPage();
				}
				return Controller._loginPage;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000364 RID: 868 RVA: 0x000122F2 File Offset: 0x000106F2
		private static MainWindow mainWindow
		{
			get
			{
				if (Controller._mainWindow == null)
				{
					Controller._mainWindow = new MainWindow();
				}
				return Controller._mainWindow;
			}
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00159CCC File Offset: 0x001574CC
		public static void openLoginPage()
		{
			P4258EBF.AFA7138A.M6233B19[288](Controller.loginPage);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0015DD6C File Offset: 0x0015B56C
		public static void closeLoginPage()
		{
			P4258EBF.AFA7138A.M6233B19[51](Controller.loginPage);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0015DE50 File Offset: 0x0015B650
		public static void openMainWindow()
		{
			P4258EBF.AFA7138A.M6233B19[288](Controller.mainWindow);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00160CA4 File Offset: 0x0015E4A4
		public static void closeMainWindow()
		{
			P4258EBF.AFA7138A.M6233B19[51](Controller.mainWindow);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00148394 File Offset: 0x00145B94
		public static Task Action(string _json, WebView2 webView)
		{
			Controller.<Action>d__12 <Action>d__;
			<Action>d__.<>t__builder = P4258EBF.AFA7138A.M6233B19[20]();
			<Action>d__._json = _json;
			<Action>d__.webView = webView;
			<Action>d__.<>1__state = -1;
			<Action>d__.<>t__builder.Start<Controller.<Action>d__12>(ref <Action>d__);
			return P4258EBF.AFA7138A.M6233B19[19](ref <Action>d__.<>t__builder);
		}

		// Token: 0x040000F7 RID: 247
		private static LoginPage _loginPage;

		// Token: 0x040000F8 RID: 248
		private static MainWindow _mainWindow;

		// Token: 0x02000113 RID: 275
		public class _GlobalActionType
		{
			// Token: 0x170000D6 RID: 214
			// (get) Token: 0x060005D6 RID: 1494 RVA: 0x0001F9A5 File Offset: 0x0001DDA5
			// (set) Token: 0x060005D7 RID: 1495 RVA: 0x0001F9AD File Offset: 0x0001DDAD
			public string Action { get; set; }

			// Token: 0x170000D7 RID: 215
			// (get) Token: 0x060005D8 RID: 1496 RVA: 0x0001F9B6 File Offset: 0x0001DDB6
			// (set) Token: 0x060005D9 RID: 1497 RVA: 0x0001F9BE File Offset: 0x0001DDBE
			public JObject Payload { get; set; }

			// Token: 0x060005DA RID: 1498 RVA: 0x0015AC14 File Offset: 0x00158414
			public _GlobalActionType()
			{
				P4258EBF.AFA7138A.M6233B19[130](this);
			}
		}

		// Token: 0x02000114 RID: 276
		private class InstrumentType
		{
			// Token: 0x170000D8 RID: 216
			// (get) Token: 0x060005DB RID: 1499 RVA: 0x0001F9CF File Offset: 0x0001DDCF
			// (set) Token: 0x060005DC RID: 1500 RVA: 0x0001F9D7 File Offset: 0x0001DDD7
			public string Link { get; set; }

			// Token: 0x060005DD RID: 1501 RVA: 0x0015F89C File Offset: 0x0015D09C
			public InstrumentType()
			{
				P4258EBF.AFA7138A.M6233B19[130](this);
			}
		}
	}
}
