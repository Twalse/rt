using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using RustTweaker.Model;

namespace RustTweaker.Optimization.Monitoring
{
	// Token: 0x0200003C RID: 60
	public class Disk : IMonitoring
	{
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000236 RID: 566 RVA: 0x0000BB51 File Offset: 0x00009F51
		public string Id
		{
			get
			{
				return "disk-monitoring";
			}
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000BB58 File Offset: 0x00009F58
		public async Task<int[]> GetStatus()
		{
			Disk.DiskStatusCode[] array = await Disk.GetStatusCodes().ConfigureAwait(false);
			return Array.ConvertAll<Disk.DiskStatusCode, int>(array, (Disk.DiskStatusCode code) => (int)code);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000BB94 File Offset: 0x00009F94
		public async Task<string> GetStatusJson()
		{
			Disk.DiskStatusCode[] array = await Disk.GetStatusCodes().ConfigureAwait(false);
			return P4258EBF.AFA7138A.M6233B19[330](array);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000BBCF File Offset: 0x00009FCF
		public static Task<Disk.DiskStatusCode[]> GetStatusCodes()
		{
			Func<Disk.DiskStatusCode[]> func;
			if ((func = Disk.<>O.<0>__GetStatusCore) == null)
			{
				func = (Disk.<>O.<0>__GetStatusCore = new Func<Disk.DiskStatusCode[]>(Disk.GetStatusCore));
			}
			return Task.Run<Disk.DiskStatusCode[]>(func);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00153B8C File Offset: 0x0015138C
		private static Disk.DiskStatusCode[] GetStatusCore()
		{
			Disk.DiskStatusCode[] array;
			try
			{
				string currentSelectedFolder = Configs.getCurrentSelectedFolder();
				if (P4258EBF.AFA7138A.M6233B19[426](currentSelectedFolder) || !P4258EBF.AFA7138A.M6233B19[89](currentSelectedFolder))
				{
					array = new Disk.DiskStatusCode[] { Disk.DiskStatusCode.GameClientNotFound };
				}
				else
				{
					string text = P4258EBF.AFA7138A.M6233B19[350](P4258EBF.AFA7138A.M6233B19[133]());
					string text2 = Disk.NormalizeDriveLetter(text);
					string text3 = Disk.NormalizeDriveLetter(P4258EBF.AFA7138A.M6233B19[350](currentSelectedFolder));
					double? num = Disk.TryGetFreePercent(text);
					bool? flag = Disk.TryIsDriveOnSsd(text2);
					bool? flag2 = Disk.TryIsDriveOnSsd(text3);
					List<Disk.DiskStatusCode> list = new List<Disk.DiskStatusCode>(3);
					if (num != null)
					{
						double valueOrDefault = num.GetValueOrDefault();
						if (valueOrDefault < 10.0)
						{
							list.Add(Disk.DiskStatusCode.LowSystemDiskFreeSpace);
						}
					}
					bool? flag3 = flag2;
					bool flag4 = false;
					if ((flag3.GetValueOrDefault() == flag4) & (flag3 != null))
					{
						list.Add(Disk.DiskStatusCode.GameOnHdd);
					}
					flag3 = flag;
					flag4 = false;
					if ((flag3.GetValueOrDefault() == flag4) & (flag3 != null))
					{
						list.Add(Disk.DiskStatusCode.SystemOnHdd);
					}
					if (list.Count == 0)
					{
						list.Add(Disk.DiskStatusCode.Ok);
					}
					array = list.Distinct<Disk.DiskStatusCode>().ToArray<Disk.DiskStatusCode>();
				}
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
				array = new Disk.DiskStatusCode[] { Disk.DiskStatusCode.Error };
			}
			return array;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0015E758 File Offset: 0x0015BF58
		private static string NormalizeDriveLetter(string pathRoot)
		{
			if (P4258EBF.AFA7138A.M6233B19[426](pathRoot))
			{
				return null;
			}
			string text = P4258EBF.AFA7138A.M6233B19[597](pathRoot);
			if (P4258EBF.AFA7138A.M6233B19[152](text) >= 2 && P4258EBF.AFA7138A.M6233B19[366](text, 1) == ':')
			{
				return P4258EBF.AFA7138A.M6233B19[351](P4258EBF.AFA7138A.M6233B19[487](text, 0, 2));
			}
			return P4258EBF.AFA7138A.M6233B19[351](text);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00157488 File Offset: 0x00154C88
		private static double? TryGetFreePercent(string driveRoot)
		{
			double? num;
			try
			{
				if (P4258EBF.AFA7138A.M6233B19[426](driveRoot))
				{
					num = null;
					num = num;
				}
				else
				{
					DriveInfo driveInfo = P4258EBF.AFA7138A.M6233B19[419](driveRoot);
					if (!P4258EBF.AFA7138A.M6233B19[274](driveInfo))
					{
						num = null;
					}
					else if (P4258EBF.AFA7138A.M6233B19[342](driveInfo) <= 0L)
					{
						num = null;
					}
					else
					{
						num = new double?((double)P4258EBF.AFA7138A.M6233B19[575](driveInfo) * 100.0 / (double)P4258EBF.AFA7138A.M6233B19[342](driveInfo));
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
				num = null;
			}
			return num;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0014A68C File Offset: 0x00147E8C
		private static bool? TryIsDriveOnSsd(string driveLetter)
		{
			bool? flag;
			try
			{
				uint num;
				if (P4258EBF.AFA7138A.M6233B19[426](driveLetter))
				{
					flag = null;
					flag = flag;
				}
				else if (!P4258EBF.AFA7138A.M6233B19[326](driveLetter, ":"))
				{
					flag = null;
				}
				else if (!Disk.TryGetDiskNumberForLogicalDrive(driveLetter, out num))
				{
					flag = null;
				}
				else
				{
					ManagementScope managementScope = P4258EBF.AFA7138A.M6233B19[233]("\\\\.\\root\\Microsoft\\Windows\\Storage");
					P4258EBF.AFA7138A.M6233B19[265](managementScope);
					IDBB349C idbb349C = P4258EBF.AFA7138A.M6233B19[455];
					ManagementScope managementScope2 = managementScope;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = P4258EBF.AFA7138A.M6233B19[467](81, 1);
					P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "SELECT DeviceId, MediaType, SpindleSpeed FROM MSFT_PhysicalDisk WHERE DeviceId = ");
					defaultInterpolatedStringHandler.AppendFormatted<uint>(num);
					using (ManagementObjectSearcher managementObjectSearcher = idbb349C(managementScope2, P4258EBF.AFA7138A.M6233B19[174](P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler))))
					{
						ManagementBaseObject managementBaseObject = P4258EBF.AFA7138A.M6233B19[343](managementObjectSearcher).OfType<ManagementBaseObject>().FirstOrDefault<ManagementBaseObject>();
						if (managementBaseObject == null)
						{
							flag = null;
						}
						else
						{
							ushort? num2 = P4258EBF.AFA7138A.M6233B19[491](managementBaseObject, "MediaType") as ushort?;
							ushort? num3 = num2;
							if (((num3 != null) ? new int?((int)num3.GetValueOrDefault()) : null).GetValueOrDefault() == 4)
							{
								flag = new bool?(true);
							}
							else
							{
								num3 = num2;
								if (((num3 != null) ? new int?((int)num3.GetValueOrDefault()) : null).GetValueOrDefault() == 3)
								{
									flag = new bool?(false);
								}
								else
								{
									uint? num4 = P4258EBF.AFA7138A.M6233B19[491](managementBaseObject, "SpindleSpeed") as uint?;
									if (num4 != null && num4.GetValueOrDefault() > 0U)
									{
										flag = new bool?(false);
									}
									else
									{
										flag = null;
									}
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
				flag = null;
			}
			return flag;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0014F8DC File Offset: 0x0014D0DC
		private static bool TryGetDiskNumberForLogicalDrive(string driveLetter, out uint diskNumber)
		{
			diskNumber = 0U;
			bool flag;
			try
			{
				string text = P4258EBF.AFA7138A.M6233B19[351](driveLetter);
				using (ManagementObjectSearcher managementObjectSearcher = P4258EBF.AFA7138A.M6233B19[454](P4258EBF.AFA7138A.M6233B19[174](P4258EBF.AFA7138A.M6233B19[64]("ASSOCIATORS OF {Win32_LogicalDisk.DeviceID='", text, "'} WHERE AssocClass=Win32_LogicalDiskToPartition"))))
				{
					ManagementObject managementObject = P4258EBF.AFA7138A.M6233B19[343](managementObjectSearcher).OfType<ManagementObject>().FirstOrDefault<ManagementObject>();
					if (managementObject == null)
					{
						flag = false;
					}
					else
					{
						object obj = P4258EBF.AFA7138A.M6233B19[491](managementObject, "DeviceID");
						string text2 = ((obj != null) ? obj.ToString() : null);
						if (P4258EBF.AFA7138A.M6233B19[426](text2))
						{
							flag = false;
						}
						else
						{
							using (ManagementObjectSearcher managementObjectSearcher2 = P4258EBF.AFA7138A.M6233B19[454](P4258EBF.AFA7138A.M6233B19[174](P4258EBF.AFA7138A.M6233B19[64]("ASSOCIATORS OF {Win32_DiskPartition.DeviceID='", Disk.EscapeWmiString(text2), "'} WHERE AssocClass=Win32_DiskDriveToDiskPartition"))))
							{
								ManagementObject managementObject2 = P4258EBF.AFA7138A.M6233B19[343](managementObjectSearcher2).OfType<ManagementObject>().FirstOrDefault<ManagementObject>();
								if (managementObject2 == null)
								{
									flag = false;
								}
								else
								{
									object obj2 = P4258EBF.AFA7138A.M6233B19[491](managementObject2, "Index");
									if (obj2 == null)
									{
										flag = false;
									}
									else
									{
										diskNumber = P4258EBF.AFA7138A.M6233B19[205](obj2);
										flag = true;
									}
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00156D48 File Offset: 0x00154548
		private static string EscapeWmiString(string value)
		{
			return P4258EBF.AFA7138A.M6233B19[114](P4258EBF.AFA7138A.M6233B19[114](value, "\\", "\\\\"), "'", "\\'");
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0015986C File Offset: 0x0015706C
		public Disk()
		{
			P4258EBF.AFA7138A.M6233B19[130](this);
		}

		// Token: 0x020000B6 RID: 182
		public enum DiskStatusCode
		{
			// Token: 0x04000272 RID: 626
			Ok = 1,
			// Token: 0x04000273 RID: 627
			SystemOnHdd,
			// Token: 0x04000274 RID: 628
			GameOnHdd,
			// Token: 0x04000275 RID: 629
			LowSystemDiskFreeSpace,
			// Token: 0x04000276 RID: 630
			GameClientNotFound,
			// Token: 0x04000277 RID: 631
			Error
		}

		// Token: 0x020000B7 RID: 183
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000278 RID: 632
			public static Func<Disk.DiskStatusCode[]> <0>__GetStatusCore;
		}
	}
}
