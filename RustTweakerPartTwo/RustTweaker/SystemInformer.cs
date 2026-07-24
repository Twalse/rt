using System;
using System.Diagnostics;
using System.Management;

namespace RustTweaker
{
	// Token: 0x0200001A RID: 26
	internal class SystemInformer
	{
		// Token: 0x060000E3 RID: 227 RVA: 0x0013D5EC File Offset: 0x0013ADEC
		public static int GetTotalRAM()
		{
			int num4;
			try
			{
				double num = 0.0;
				ManagementObjectSearcher managementObjectSearcher = P4258EBF.AFA7138A.M6233B19[84]("SELECT Capacity FROM Win32_PhysicalMemory");
				using (ManagementObjectCollection.ManagementObjectEnumerator managementObjectEnumerator = P4258EBF.AFA7138A.M6233B19[122](P4258EBF.AFA7138A.M6233B19[343](managementObjectSearcher)))
				{
					while (managementObjectEnumerator.MoveNext())
					{
						ManagementBaseObject managementBaseObject = managementObjectEnumerator.Current;
						ManagementObject managementObject = (ManagementObject)managementBaseObject;
						ulong num2 = (ulong)P4258EBF.AFA7138A.M6233B19[491](managementObject, "Capacity");
						num += num2;
					}
				}
				double num3 = num / 1024.0 / 1024.0 / 1024.0;
				num4 = (int)P4258EBF.AFA7138A.M6233B19[177](num);
			}
			catch (Exception ex)
			{
				Logger.Log("Failed to read total RAM");
				Logger.Log(ex);
				num4 = 0;
			}
			return num4;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x001563B0 File Offset: 0x00153BB0
		public static string ResolveConfigPath(string path)
		{
			if (P4258EBF.AFA7138A.M6233B19[67](path))
			{
				return path;
			}
			string text = P4258EBF.AFA7138A.M6233B19[158](AppContext.BaseDirectory, path);
			if (P4258EBF.AFA7138A.M6233B19[627](text))
			{
				return text;
			}
			return P4258EBF.AFA7138A.M6233B19[224](path);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0015B988 File Offset: 0x00159188
		public static void RestartComputer()
		{
			ProcessStartInfo processStartInfo = P4258EBF.AFA7138A.M6233B19[394]();
			N62EB38A.CB1145A6(processStartInfo, "shutdown");
			AA2B3D09.ND86FA10(processStartInfo, "/r /t 0");
			JD06799C.I832069D(processStartInfo, true);
			O8258311.M5A8918D(processStartInfo, false);
			JC11021F.C827CF8C(processStartInfo);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00161A78 File Offset: 0x0015F278
		public SystemInformer()
		{
			P4258EBF.AFA7138A.M6233B19[130](this);
		}
	}
}
