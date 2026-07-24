using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace RustTweaker.Model
{
	// Token: 0x02000043 RID: 67
	public static class RuntimeChecks
	{
		// Token: 0x06000272 RID: 626
		[DllImport("kernel32.dll")]
		private static extern bool IsDebuggerPresent();

		// Token: 0x06000273 RID: 627
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool CheckRemoteDebuggerPresent(IntPtr hProcess, ref bool isDebuggerPresent);

		// Token: 0x06000274 RID: 628
		[DllImport("ntdll.dll")]
		private static extern int NtQueryInformationProcess(IntPtr processHandle, int processInformationClass, ref IntPtr processInformation, int processInformationLength, out int returnLength);

		// Token: 0x06000275 RID: 629 RVA: 0x0015C9E0 File Offset: 0x0015A1E0
		public static bool IsEnvironmentSuspicious(out string reason)
		{
			StringBuilder stringBuilder = P4258EBF.AFA7138A.M6233B19[353]();
			reason = stringBuilder.ToString();
			return P4258EBF.AFA7138A.M6233B19[116](stringBuilder) > 0;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00158444 File Offset: 0x00155C44
		private static bool IsDebuggerAttached()
		{
			try
			{
				if (P4258EBF.AFA7138A.M6233B19[352]())
				{
					return true;
				}
				if (RuntimeChecks.IsDebuggerPresent())
				{
					return true;
				}
			}
			catch
			{
			}
			return false;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00154A10 File Offset: 0x00152210
		private static bool IsRemoteDebuggerPresent()
		{
			try
			{
				bool flag = false;
				IntPtr intPtr = P4258EBF.AFA7138A.M6233B19[348](P4258EBF.AFA7138A.M6233B19[242]());
				RuntimeChecks.CheckRemoteDebuggerPresent(intPtr, ref flag);
				return flag;
			}
			catch
			{
			}
			return false;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00146040 File Offset: 0x00143840
		private static bool IsNtQueryDebugPortPresent()
		{
			try
			{
				IntPtr intPtr = P4258EBF.AFA7138A.M6233B19[500]();
				int num2;
				int num = RuntimeChecks.NtQueryInformationProcess(P4258EBF.AFA7138A.M6233B19[348](P4258EBF.AFA7138A.M6233B19[242]()), 7, ref intPtr, P4258EBF.AFA7138A.M6233B19[45](), out num2);
				return intPtr != P4258EBF.AFA7138A.M6233B19[500]();
			}
			catch
			{
			}
			return false;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00155EB4 File Offset: 0x001536B4
		private static string DetectKnownTools()
		{
			string[] array = new string[]
			{
				"dnspy", "dnspy-x86", "ilspy", "ida64", "ida", "x64dbg", "x32dbg", "windbg", "processhacker", "procmon",
				"procdump", "ollydbg", "frida-server", "frida"
			};
			try
			{
				Process[] array2 = P4258EBF.AFA7138A.M6233B19[78]();
				Process[] array3 = array2;
				for (int i = 0; i < array3.Length; i++)
				{
					Process process = array3[i];
					string name = P4258EBF.AFA7138A.M6233B19[432](P4258EBF.AFA7138A.M6233B19[246](process));
					if (array.Any<string>((string s) => P4258EBF.AFA7138A.M6233B19[433](name, s)))
					{
						return P4258EBF.AFA7138A.M6233B19[246](process);
					}
					try
					{
						RuntimeChecks.<>c__DisplayClass7_1 CS$<>8__locals2 = new RuntimeChecks.<>c__DisplayClass7_1();
						RuntimeChecks.<>c__DisplayClass7_1 CS$<>8__locals3 = CS$<>8__locals2;
						ProcessModule processModule = P4258EBF.AFA7138A.M6233B19[189](process);
						CS$<>8__locals3.path = ((processModule != null) ? A28D4FA1.DCB8B2B2(processModule) : null);
						if (!P4258EBF.AFA7138A.M6233B19[88](CS$<>8__locals2.path) && array.Any<string>((string s) => P4258EBF.AFA7138A.M6233B19[433](P4258EBF.AFA7138A.M6233B19[432](CS$<>8__locals2.path), s)))
						{
							return P4258EBF.AFA7138A.M6233B19[513](CS$<>8__locals2.path);
						}
					}
					catch
					{
					}
				}
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0014BAFC File Offset: 0x001492FC
		private static string DetectSuspiciousModules()
		{
			string[] array = new string[] { "dbghelp.dll", "frida-agent", "frida.dll", "scylla", "cheatengine" };
			try
			{
				Process process = P4258EBF.AFA7138A.M6233B19[242]();
				using (IEnumerator enumerator = P4258EBF.AFA7138A.M6233B19[531](P4258EBF.AFA7138A.M6233B19[252](process)))
				{
					while (P4258EBF.AFA7138A.M6233B19[411](enumerator))
					{
						ProcessModule processModule = (ProcessModule)P4258EBF.AFA7138A.M6233B19[606](enumerator);
						string mn = P4258EBF.AFA7138A.M6233B19[432](P4258EBF.AFA7138A.M6233B19[519](processModule));
						if (array.Any<string>((string s) => P4258EBF.AFA7138A.M6233B19[433](mn, s)))
						{
							return P4258EBF.AFA7138A.M6233B19[519](processModule);
						}
					}
				}
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00147AC0 File Offset: 0x001452C0
		private static bool TimingCheck()
		{
			try
			{
				Stopwatch stopwatch = P4258EBF.AFA7138A.M6233B19[55]();
				P4258EBF.AFA7138A.M6233B19[381](10);
				P4258EBF.AFA7138A.M6233B19[59](stopwatch);
				if (P4258EBF.AFA7138A.M6233B19[358](stopwatch) > 100L)
				{
					return true;
				}
			}
			catch
			{
			}
			return false;
		}
	}
}
