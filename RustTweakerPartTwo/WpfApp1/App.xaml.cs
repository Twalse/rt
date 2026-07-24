using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using Microsoft.Web.WebView2.Core;
using RustTweaker;
using RustTweaker_NET.Services;

namespace WpfApp1
{
	// Token: 0x02000052 RID: 82
	public partial class App : Application
	{
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000E06F File Offset: 0x0000C46F
		// (set) Token: 0x060002B5 RID: 693 RVA: 0x0000E076 File Offset: 0x0000C476
		public static List<string> StartupArgs { get; private set; }

		// Token: 0x060002B6 RID: 694 RVA: 0x001559E8 File Offset: 0x001531E8
		protected override void OnStartup(StartupEventArgs e)
		{
			App.<OnStartup>d__11 <OnStartup>d__;
			<OnStartup>d__.<>t__builder = P4258EBF.AFA7138A.M6233B19[385]();
			<OnStartup>d__.<>4__this = this;
			<OnStartup>d__.e = e;
			<OnStartup>d__.<>1__state = -1;
			<OnStartup>d__.<>t__builder.Start<App.<OnStartup>d__11>(ref <OnStartup>d__);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0015D928 File Offset: 0x0015B128
		protected override void OnExit(ExitEventArgs e)
		{
			Logger.Log("Stop Program");
			BenchmarkIpcServer.Stop();
			Logger.Stop();
			P4258EBF.AFA7138A.M6233B19[573](this, e);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0015EA38 File Offset: 0x0015C238
		private static Task CheckForUpdatesOnStartup()
		{
			App.<CheckForUpdatesOnStartup>d__13 <CheckForUpdatesOnStartup>d__;
			<CheckForUpdatesOnStartup>d__.<>t__builder = P4258EBF.AFA7138A.M6233B19[20]();
			<CheckForUpdatesOnStartup>d__.<>1__state = -1;
			<CheckForUpdatesOnStartup>d__.<>t__builder.Start<App.<CheckForUpdatesOnStartup>d__13>(ref <CheckForUpdatesOnStartup>d__);
			return P4258EBF.AFA7138A.M6233B19[19](ref <CheckForUpdatesOnStartup>d__.<>t__builder);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x001571FC File Offset: 0x001549FC
		public static bool SafeNativeUpdate(string exeName, string exePath)
		{
			bool flag;
			try
			{
				string text = P4258EBF.AFA7138A.M6233B19[516](exePath);
				string text2 = P4258EBF.AFA7138A.M6233B19[158](text, "update.bat");
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = P4258EBF.AFA7138A.M6233B19[467](517, 7);
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "\r\n@echo off\r\nsetlocal\r\n\r\n:loop\r\ntasklist /fi \"imagename eq ");
				P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler, exeName);
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "\" 2>NUL | find /I /N \"");
				P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler, exeName);
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "\" >NUL\r\nif \"%ERRORLEVEL%\"==\"0\" (\r\n    echo Ждем завершения программы...\r\n    timeout /t 1 /nobreak >nul\r\n    goto loop\r\n)\r\n\r\necho Программа завершена. Начинаем обновление...\r\ncd /d \"");
				P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler, text);
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "\"\r\n\r\nif exist \"");
				P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler, exePath);
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "\" (\r\n    echo Удаляем старый файл...\r\n    del \"");
				P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler, exePath);
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "\"\r\n)\r\n\r\nif exist \"new.exe\" (\r\n    echo Копируем новый файл...\r\n    move \"new.exe\" \"");
				P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler, exePath);
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "\"\r\n)\r\n\r\necho Запускаем обновленную программу...\r\nstart \"\" \"");
				P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler, exePath);
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "\"\r\n\r\necho Удаляем временные файлы...\r\ndel \"%~f0\"\r\n");
				string text3 = P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler);
				P4258EBF.AFA7138A.M6233B19[94](text2, text3);
				ProcessStartInfo processStartInfo = P4258EBF.AFA7138A.M6233B19[394]();
				N62EB38A.CB1145A6(processStartInfo, "cmd.exe");
				AA2B3D09.ND86FA10(processStartInfo, P4258EBF.AFA7138A.M6233B19[64]("/c \"", text2, "\""));
				O8258311.M5A8918D(processStartInfo, true);
				DA0042B3.L1037C85(processStartInfo, "runas");
				F5B3B684.E7069F2E(processStartInfo, ProcessWindowStyle.Hidden);
				JD06799C.I832069D(processStartInfo, true);
				JC11021F.C827CF8C(processStartInfo);
				P4258EBF.AFA7138A.M6233B19[505](0);
				flag = true;
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
				P4258EBF.AFA7138A.M6233B19[129](P4258EBF.AFA7138A.M6233B19[478]("Ошибка обновления: ", P4258EBF.AFA7138A.M6233B19[551](ex)));
				flag = false;
			}
			return flag;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00146284 File Offset: 0x00143A84
		public static bool TryExtractToken(string[] args, string scheme, out string token)
		{
			token = null;
			if (args == null || args.Length == 0)
			{
				return false;
			}
			string text = P4258EBF.AFA7138A.M6233B19[478](P4258EBF.AFA7138A.M6233B19[577](scheme), "://");
			int i = 0;
			while (i < args.Length)
			{
				string text2 = args[i];
				if (!P4258EBF.AFA7138A.M6233B19[426](text2) && P4258EBF.AFA7138A.M6233B19[269](P4258EBF.AFA7138A.M6233B19[577](text2), text))
				{
					string text3 = P4258EBF.AFA7138A.M6233B19[398](text2, P4258EBF.AFA7138A.M6233B19[152](text));
					text3 = P4258EBF.AFA7138A.M6233B19[172](text3, '/');
					string[] array = P506988F.O783D00F(text3, new char[] { '=' }, 2);
					if (array.Length != 2)
					{
						return false;
					}
					if (!P4258EBF.AFA7138A.M6233B19[85](array[0], "token", StringComparison.OrdinalIgnoreCase))
					{
						return false;
					}
					token = array[1];
					return !P4258EBF.AFA7138A.M6233B19[426](token);
				}
				else
				{
					i++;
				}
			}
			return false;
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00147E84 File Offset: 0x00145684
		private static void LogWinwsProcessInfo()
		{
			try
			{
				Process process = P4258EBF.AFA7138A.M6233B19[98]("winws").FirstOrDefault<Process>();
				if (process == null)
				{
					Logger.Log("winws.exe process not found");
				}
				else
				{
					string text = "Unknown";
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = P4258EBF.AFA7138A.M6233B19[467](59, 1);
					P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "SELECT ExecutablePath FROM Win32_Process WHERE ProcessId = ");
					defaultInterpolatedStringHandler.AppendFormatted<int>(P4258EBF.AFA7138A.M6233B19[581](process));
					using (ManagementObjectSearcher managementObjectSearcher = P4258EBF.AFA7138A.M6233B19[84](P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler)))
					{
						using (ManagementObjectCollection.ManagementObjectEnumerator managementObjectEnumerator = P4258EBF.AFA7138A.M6233B19[122](P4258EBF.AFA7138A.M6233B19[343](managementObjectSearcher)))
						{
							while (managementObjectEnumerator.MoveNext())
							{
								ManagementBaseObject managementBaseObject = managementObjectEnumerator.Current;
								ManagementObject managementObject = (ManagementObject)managementBaseObject;
								object obj = P4258EBF.AFA7138A.M6233B19[491](managementObject, "ExecutablePath");
								text = ((obj != null) ? obj.ToString() : null) ?? "Unknown";
							}
						}
					}
					Logger.Log("winws.exe process found");
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = P4258EBF.AFA7138A.M6233B19[467](15, 1);
					P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler2, "winws.exe PID: ");
					defaultInterpolatedStringHandler2.AppendFormatted<int>(P4258EBF.AFA7138A.M6233B19[581](process));
					Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler2));
					Logger.Log(P4258EBF.AFA7138A.M6233B19[478]("winws.exe Path: ", text));
				}
			}
			catch (Exception ex)
			{
				Logger.Log(P4258EBF.AFA7138A.M6233B19[478]("winws.exe process check error: ", P4258EBF.AFA7138A.M6233B19[551](ex)));
			}
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0014E060 File Offset: 0x0014B860
		private static void CloseOtherRustTweakers()
		{
			Process process = P4258EBF.AFA7138A.M6233B19[242]();
			Process[] array = P4258EBF.AFA7138A.M6233B19[98]("RustTweaker");
			foreach (Process process2 in array)
			{
				if (P4258EBF.AFA7138A.M6233B19[581](process2) != P4258EBF.AFA7138A.M6233B19[581](process))
				{
					try
					{
						if (P4258EBF.AFA7138A.M6233B19[43](process2) != P4258EBF.AFA7138A.M6233B19[500]())
						{
							P4258EBF.AFA7138A.M6233B19[420](process2);
							if (!P4258EBF.AFA7138A.M6233B19[356](process2, 5000))
							{
								P4258EBF.AFA7138A.M6233B19[431](process2);
							}
						}
						else
						{
							P4258EBF.AFA7138A.M6233B19[431](process2);
						}
					}
					catch (Exception ex)
					{
						Logger.Log(ex);
					}
				}
			}
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00155834 File Offset: 0x00153034
		private static Task CloseSplashAsync()
		{
			App.<>c__DisplayClass18_0 CS$<>8__locals1 = new App.<>c__DisplayClass18_0();
			if (App.splash == null)
			{
				return P4258EBF.AFA7138A.M6233B19[375]();
			}
			CS$<>8__locals1.tcs = new TaskCompletionSource<bool>();
			DoubleAnimation doubleAnimation = P4258EBF.AFA7138A.M6233B19[488](0.0, P4258EBF.AFA7138A.M6233B19[47](P4258EBF.AFA7138A.M6233B19[537](250L)));
			K4B98A3E.ND08B497(doubleAnimation, FillBehavior.Stop);
			DoubleAnimation doubleAnimation2 = doubleAnimation;
			P4258EBF.AFA7138A.M6233B19[549](doubleAnimation2, P4258EBF.AFA7138A.M6233B19[529](CS$<>8__locals1, ldftn(<CloseSplashAsync>b__0)));
			P4258EBF.AFA7138A.M6233B19[527](App.splash, P4258EBF.AFA7138A.M6233B19[291](), doubleAnimation2);
			return CS$<>8__locals1.tcs.Task;
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0015F56C File Offset: 0x0015CD6C
		public App()
		{
			P4258EBF.AFA7138A.M6233B19[396](this);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00154ECC File Offset: 0x001526CC
		// Note: this type is marked as 'beforefieldinit'.
		static App()
		{
			ProcessModule processModule = P4258EBF.AFA7138A.M6233B19[189](P4258EBF.AFA7138A.M6233B19[242]());
			string text;
			if ((text = ((processModule != null) ? A28D4FA1.DCB8B2B2(processModule) : null)) == null)
			{
				text = P4258EBF.AFA7138A.M6233B19[471]() ?? P4258EBF.AFA7138A.M6233B19[587]();
			}
			App.exePath = text;
			App.STEAMTID = null;
			App.EMAIL = null;
		}

		// Token: 0x040000D7 RID: 215
		public static string curVersion = P4258EBF.AFA7138A.M6233B19[334](P4258EBF.AFA7138A.M6233B19[574](P4258EBF.AFA7138A.M6233B19[302](P4258EBF.AFA7138A.M6233B19[161]())), 3);

		// Token: 0x040000D8 RID: 216
		private static string exeName = P4258EBF.AFA7138A.M6233B19[118](P4258EBF.AFA7138A.M6233B19[63]());

		// Token: 0x040000D9 RID: 217
		private static string exePath;

		// Token: 0x040000DA RID: 218
		private static StartupSplashWindow splash;

		// Token: 0x040000DC RID: 220
		public static CoreWebView2Environment SharedEnvironment;

		// Token: 0x040000DD RID: 221
		public static string STEAMTID;

		// Token: 0x040000DE RID: 222
		public static string EMAIL;
	}
}
