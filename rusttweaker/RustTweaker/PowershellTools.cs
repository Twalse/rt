using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Runtime.CompilerServices;
using Microsoft.PowerShell;

namespace RustTweaker
{
	// Token: 0x02000019 RID: 25
	public static class PowershellTools
	{
		// Token: 0x060000E1 RID: 225 RVA: 0x00130000 File Offset: 0x0012D800
		static PowershellTools()
		{
			try
			{
				InitialSessionState initialSessionState = P4258EBF.AFA7138A.M6233B19[142]();
				P4258EBF.AFA7138A.M6233B19[285](initialSessionState, ExecutionPolicy.Bypass);
				using (Runspace runspace = P4258EBF.AFA7138A.M6233B19[284](initialSessionState))
				{
					P4258EBF.AFA7138A.M6233B19[199](runspace);
					using (PowerShell powerShell = P4258EBF.AFA7138A.M6233B19[425]())
					{
						P4258EBF.AFA7138A.M6233B19[235](powerShell, runspace);
						P4258EBF.AFA7138A.M6233B19[600](powerShell, "Import-Module Defender -ErrorAction Stop");
						P4258EBF.AFA7138A.M6233B19[384](powerShell);
						PowershellTools.HaveDefender = true;
						return;
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
			}
			PowershellTools.HaveDefender = false;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x0014BC60 File Offset: 0x00149460
		public static string powershellExecute(string powershellCommand)
		{
			InitialSessionState initialSessionState = P4258EBF.AFA7138A.M6233B19[142]();
			P4258EBF.AFA7138A.M6233B19[285](initialSessionState, ExecutionPolicy.Bypass);
			string text3;
			using (Runspace runspace = P4258EBF.AFA7138A.M6233B19[284](initialSessionState))
			{
				P4258EBF.AFA7138A.M6233B19[199](runspace);
				using (PowerShell powerShell = P4258EBF.AFA7138A.M6233B19[425]())
				{
					P4258EBF.AFA7138A.M6233B19[235](powerShell, runspace);
					string text = HB9FDCBB.AB31A5B2(PowershellTools.HaveDefender ? "Import-Module Defender" : "", " -ErrorAction Stop \n        \n        ", powershellCommand, "\n        ");
					Logger.Log(P4258EBF.AFA7138A.M6233B19[478]("PowerShell command: ", powershellCommand));
					P4258EBF.AFA7138A.M6233B19[600](powerShell, text);
					Collection<PSObject> collection = P4258EBF.AFA7138A.M6233B19[384](powerShell);
					string text2 = D2B9D912.A91E8BBB(P4258EBF.AFA7138A.M6233B19[181](), collection.Select<PSObject, string>((PSObject r) => r.ToString()));
					if (P4258EBF.AFA7138A.M6233B19[225](P4258EBF.AFA7138A.M6233B19[541](powerShell)).Count > 0)
					{
						using (IEnumerator<ErrorRecord> enumerator = P4258EBF.AFA7138A.M6233B19[225](P4258EBF.AFA7138A.M6233B19[541](powerShell)).GetEnumerator())
						{
							while (P4258EBF.AFA7138A.M6233B19[411](enumerator))
							{
								ErrorRecord errorRecord = enumerator.Current;
								DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = P4258EBF.AFA7138A.M6233B19[467](18, 1);
								P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "PowerShell error: ");
								defaultInterpolatedStringHandler.AppendFormatted<ErrorRecord>(errorRecord);
								Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler));
							}
						}
						if (P4258EBF.AFA7138A.M6233B19[426](text2))
						{
							Logger.Log("PowerShell returned no stdout output.");
						}
					}
					text3 = text2;
				}
			}
			return text3;
		}

		// Token: 0x04000052 RID: 82
		public static readonly bool HaveDefender;
	}
}
