using System;
using System.Runtime.CompilerServices;
using System.Text;
using RustTweaker;

namespace WpfApp1.Model
{
	// Token: 0x02000063 RID: 99
	internal static class HardwareId
	{
		// Token: 0x06000377 RID: 887 RVA: 0x00158F78 File Offset: 0x00156778
		public static string GetHardwareId()
		{
			string text2;
			try
			{
				string componentId = HardwareId.GetComponentId("SELECT UUID FROM Win32_ComputerSystemProduct", "UUID");
				string componentId2 = HardwareId.GetComponentId("SELECT ProcessorId FROM Win32_Processor", "ProcessorId");
				string componentId3 = HardwareId.GetComponentId("SELECT SerialNumber FROM Win32_BaseBoard", "SerialNumber");
				string componentId4 = HardwareId.GetComponentId("SELECT SerialNumber FROM Win32_BIOS", "SerialNumber");
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = P4258EBF.AFA7138A.M6233B19[467](30, 2);
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "HWID component UUID: '");
				P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler, componentId);
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "' (len=");
				defaultInterpolatedStringHandler.AppendFormatted<int>(P4258EBF.AFA7138A.M6233B19[152](componentId));
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, ")");
				Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler));
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = P4258EBF.AFA7138A.M6233B19[467](37, 2);
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler2, "HWID component ProcessorId: '");
				P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler2, componentId2);
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler2, "' (len=");
				defaultInterpolatedStringHandler2.AppendFormatted<int>(P4258EBF.AFA7138A.M6233B19[152](componentId2));
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler2, ")");
				Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler2));
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler3 = P4258EBF.AFA7138A.M6233B19[467](48, 2);
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler3, "HWID component BaseBoard.SerialNumber: '");
				P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler3, componentId3);
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler3, "' (len=");
				defaultInterpolatedStringHandler3.AppendFormatted<int>(P4258EBF.AFA7138A.M6233B19[152](componentId3));
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler3, ")");
				Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler3));
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler4 = P4258EBF.AFA7138A.M6233B19[467](43, 2);
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler4, "HWID component BIOS.SerialNumber: '");
				P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler4, componentId4);
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler4, "' (len=");
				defaultInterpolatedStringHandler4.AppendFormatted<int>(P4258EBF.AFA7138A.M6233B19[152](componentId4));
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler4, ")");
				Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler4));
				StringBuilder stringBuilder = P4258EBF.AFA7138A.M6233B19[353]();
				P4258EBF.AFA7138A.M6233B19[468](stringBuilder, componentId);
				P4258EBF.AFA7138A.M6233B19[468](stringBuilder, componentId2);
				P4258EBF.AFA7138A.M6233B19[468](stringBuilder, componentId3);
				P4258EBF.AFA7138A.M6233B19[468](stringBuilder, componentId4);
				string text = P4258EBF.AFA7138A.M6233B19[351](P4258EBF.AFA7138A.M6233B19[114](P4258EBF.AFA7138A.M6233B19[114](stringBuilder.ToString(), " ", ""), "-", ""));
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler5 = P4258EBF.AFA7138A.M6233B19[467](32, 2);
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler5, "HWID raw concatenated: '");
				defaultInterpolatedStringHandler5.AppendFormatted<StringBuilder>(stringBuilder);
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler5, "' (len=");
				defaultInterpolatedStringHandler5.AppendFormatted<int>(P4258EBF.AFA7138A.M6233B19[116](stringBuilder));
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler5, ")");
				Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler5));
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler6 = P4258EBF.AFA7138A.M6233B19[467](26, 2);
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler6, "HWID normalized: '");
				P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler6, text);
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler6, "' (len=");
				defaultInterpolatedStringHandler6.AppendFormatted<int>(P4258EBF.AFA7138A.M6233B19[152](text));
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler6, ")");
				Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler6));
				if (P4258EBF.AFA7138A.M6233B19[88](text) || P4258EBF.AFA7138A.M6233B19[152](text) < 10)
				{
					Logger.Log("HWID normalized result is empty/too short, fallback to HWID_ERROR");
					text2 = "HWID_ERROR";
				}
				else
				{
					text2 = text;
				}
			}
			catch (Exception ex)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler7;
				P4258EBF.AFA7138A.M6233B19[466](ref defaultInterpolatedStringHandler7, 24, 1);
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler7, "HWID generation failed: ");
				defaultInterpolatedStringHandler7.AppendFormatted<Exception>(ex);
				Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler7));
				text2 = "HWID_ERROR";
			}
			return text2;
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0015D95C File Offset: 0x0015B15C
		private unsafe static string GetComponentId(string query, string property)
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			return (string)new H52867B7().FCA6C832(new object[]
			{
				query,
				property,
				&defaultInterpolatedStringHandler
			}, 30576);
		}
	}
}
