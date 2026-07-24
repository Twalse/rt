using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.CompilerServices;

namespace RustTweaker.Optimization
{
	// Token: 0x02000030 RID: 48
	public class PagefileOptimization : IOptimization
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00008AEE File Offset: 0x00006EEE
		public OptimizationId Id
		{
			get
			{
				return OptimizationId.AutoPagefile;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00008AF1 File Offset: 0x00006EF1
		public bool NeedComputerRestart
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00008AF4 File Offset: 0x00006EF4
		public bool NeedSteamRestart
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0015E2F8 File Offset: 0x0015BAF8
		public void Apply(OptimizationTargetStatus targetStatus)
		{
			if (targetStatus == OptimizationTargetStatus.Good)
			{
				OptimizationOriginalStateStore.SaveIfMissing<PagefileOptimization.PagefileState>(this.Id, new PagefileOptimization.PagefileState
				{
					AutomaticManagedPagefile = PagefileOptimization.isAutomaticPagefile(),
					Settings = PagefileOptimization.GetPageFileSettings()
				});
				uint num = PagefileOptimization.CalculateDesiredPagefileSizeMb();
				uint configuredOrAllocatedPagefileSizeMb = PagefileOptimization.GetConfiguredOrAllocatedPagefileSizeMb("C:\\pagefile.sys");
				if (configuredOrAllocatedPagefileSizeMb >= num)
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
					P4258EBF.AFA7138A.M6233B19[466](ref defaultInterpolatedStringHandler, 72, 2);
					P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "Pagefile: current (");
					defaultInterpolatedStringHandler.AppendFormatted<uint>(configuredOrAllocatedPagefileSizeMb);
					P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "MB) >= desired (");
					defaultInterpolatedStringHandler.AppendFormatted<uint>(num);
					P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "MB), skip apply (considered applied).");
					Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler));
					return;
				}
				if (!PagefileOptimization.HasEnoughDiskSpaceForIncrease("C:\\pagefile.sys", num, configuredOrAllocatedPagefileSizeMb))
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2;
					P4258EBF.AFA7138A.M6233B19[466](ref defaultInterpolatedStringHandler2, 81, 2);
					P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler2, "Pagefile: not enough disk space to increase from ");
					defaultInterpolatedStringHandler2.AppendFormatted<uint>(configuredOrAllocatedPagefileSizeMb);
					P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler2, "MB to ");
					defaultInterpolatedStringHandler2.AppendFormatted<uint>(num);
					P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler2, "MB (need >10% free after).");
					Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler2));
					return;
				}
				PagefileOptimization.createPagefile((int)num, (int)num);
				return;
			}
			else
			{
				PagefileOptimization.PagefileState pagefileState;
				if (OptimizationOriginalStateStore.TryRead<PagefileOptimization.PagefileState>(this.Id, out pagefileState) && pagefileState != null)
				{
					PagefileOptimization.RestorePagefile(pagefileState);
					return;
				}
				PagefileOptimization.enableAutoPagefile();
				return;
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00008C14 File Offset: 0x00007014
		public OptimizationStatus GetStatus()
		{
			uint num = PagefileOptimization.CalculateDesiredPagefileSizeMb();
			uint configuredOrAllocatedPagefileSizeMb = PagefileOptimization.GetConfiguredOrAllocatedPagefileSizeMb("C:\\pagefile.sys");
			if (configuredOrAllocatedPagefileSizeMb < num)
			{
				return OptimizationStatus.Bad;
			}
			return OptimizationStatus.Good;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0014F0A4 File Offset: 0x0014C8A4
		[NullableContext(2)]
		public PagefileDTO GetPageFileInfo()
		{
			using (ManagementObjectSearcher managementObjectSearcher = P4258EBF.AFA7138A.M6233B19[84]("SELECT * FROM Win32_PageFileUsage"))
			{
				using (ManagementObjectCollection.ManagementObjectEnumerator managementObjectEnumerator = P4258EBF.AFA7138A.M6233B19[122](P4258EBF.AFA7138A.M6233B19[343](managementObjectSearcher)))
				{
					if (managementObjectEnumerator.MoveNext())
					{
						ManagementObject managementObject = (ManagementObject)managementObjectEnumerator.Current;
						PagefileDTO pagefileDTO = new PagefileDTO();
						object obj = P4258EBF.AFA7138A.M6233B19[491](managementObject, "Name");
						pagefileDTO.Path = ((obj != null) ? obj.ToString() : null) ?? P4258EBF.AFA7138A.M6233B19[280]();
						pagefileDTO.AllocatedBaseSize = P4258EBF.AFA7138A.M6233B19[205](P4258EBF.AFA7138A.M6233B19[491](managementObject, "AllocatedBaseSize"));
						pagefileDTO.CurrentUsage = P4258EBF.AFA7138A.M6233B19[205](P4258EBF.AFA7138A.M6233B19[491](managementObject, "CurrentUsage"));
						pagefileDTO.PeakUsage = P4258EBF.AFA7138A.M6233B19[205](P4258EBF.AFA7138A.M6233B19[491](managementObject, "PeakUsage"));
						object obj2 = P4258EBF.AFA7138A.M6233B19[491](managementObject, "InstallDate");
						pagefileDTO.InstallDate = N42EAC14.EB0F9928((obj2 != null) ? obj2.ToString() : null);
						pagefileDTO.TempPageFile = P4258EBF.AFA7138A.M6233B19[502](P4258EBF.AFA7138A.M6233B19[491](managementObject, "TempPageFile"));
						pagefileDTO.IsAutomaticPagefile = PagefileOptimization.isAutomaticPagefile();
						return pagefileDTO;
					}
				}
			}
			return null;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00143040 File Offset: 0x00140840
		private static bool isAutomaticPagefile()
		{
			try
			{
				using (ManagementObjectSearcher managementObjectSearcher = P4258EBF.AFA7138A.M6233B19[84]("SELECT AutomaticManagedPagefile FROM Win32_ComputerSystem"))
				{
					using (ManagementObjectCollection.ManagementObjectEnumerator managementObjectEnumerator = P4258EBF.AFA7138A.M6233B19[122](P4258EBF.AFA7138A.M6233B19[343](managementObjectSearcher)))
					{
						if (managementObjectEnumerator.MoveNext())
						{
							ManagementObject managementObject = (ManagementObject)managementObjectEnumerator.Current;
							return (bool)P4258EBF.AFA7138A.M6233B19[491](managementObject, "AutomaticManagedPagefile");
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Log("Failed to read automatic pagefile status");
				Logger.Log(ex);
			}
			return false;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0016137C File Offset: 0x0015EB7C
		private static uint CalculateDesiredPagefileSizeMb()
		{
			int ramGb = PagefileOptimization.GetRamGb();
			int vramGb = PagefileOptimization.GetVramGb();
			int num = 40 - (ramGb + vramGb);
			if (num < 8)
			{
				num = 8;
			}
			int num2 = num * 1024;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			P4258EBF.AFA7138A.M6233B19[466](ref defaultInterpolatedStringHandler, 54, 4);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "Pagefile desired: 40GB - (RAM:");
			defaultInterpolatedStringHandler.AppendFormatted<int>(ramGb);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "GB + VRAM:");
			defaultInterpolatedStringHandler.AppendFormatted<int>(vramGb);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "GB) => ");
			defaultInterpolatedStringHandler.AppendFormatted<int>(num);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "GB (");
			defaultInterpolatedStringHandler.AppendFormatted<int>(num2);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "MB)");
			Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler));
			return (uint)num2;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0015109C File Offset: 0x0014E89C
		private static int GetRamGb()
		{
			try
			{
				using (ManagementObjectSearcher managementObjectSearcher = P4258EBF.AFA7138A.M6233B19[84]("SELECT TotalPhysicalMemory FROM Win32_ComputerSystem"))
				{
					using (ManagementObjectCollection.ManagementObjectEnumerator managementObjectEnumerator = P4258EBF.AFA7138A.M6233B19[122](P4258EBF.AFA7138A.M6233B19[343](managementObjectSearcher)))
					{
						if (managementObjectEnumerator.MoveNext())
						{
							ManagementObject managementObject = (ManagementObject)managementObjectEnumerator.Current;
							long num = P4258EBF.AFA7138A.M6233B19[104](P4258EBF.AFA7138A.M6233B19[491](managementObject, "TotalPhysicalMemory"));
							return (int)P4258EBF.AFA7138A.M6233B19[177]((double)num / 1073741824.0);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Log("Failed to read RAM size");
				Logger.Log(ex);
			}
			return 0;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00148180 File Offset: 0x00145980
		private static int GetVramGb()
		{
			try
			{
				long num = 0L;
				using (ManagementObjectSearcher managementObjectSearcher = P4258EBF.AFA7138A.M6233B19[84]("SELECT AdapterRAM, Name FROM Win32_VideoController"))
				{
					using (ManagementObjectCollection.ManagementObjectEnumerator managementObjectEnumerator = P4258EBF.AFA7138A.M6233B19[122](P4258EBF.AFA7138A.M6233B19[343](managementObjectSearcher)))
					{
						while (managementObjectEnumerator.MoveNext())
						{
							ManagementBaseObject managementBaseObject = managementObjectEnumerator.Current;
							ManagementObject managementObject = (ManagementObject)managementBaseObject;
							if (P4258EBF.AFA7138A.M6233B19[491](managementObject, "AdapterRAM") != null)
							{
								long num2 = P4258EBF.AFA7138A.M6233B19[104](P4258EBF.AFA7138A.M6233B19[491](managementObject, "AdapterRAM"));
								if (num2 > num)
								{
									num = num2;
								}
							}
						}
					}
					return (num <= 0L) ? 0 : ((int)P4258EBF.AFA7138A.M6233B19[177]((double)num / 1073741824.0));
				}
			}
			catch (Exception ex)
			{
				Logger.Log("Failed to read VRAM size");
				Logger.Log(ex);
			}
			return 0;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0013CDE4 File Offset: 0x0013A5E4
		private static uint GetConfiguredOrAllocatedPagefileSizeMb(string pagefilePath)
		{
			try
			{
				string text = P4258EBF.AFA7138A.M6233B19[114](P4258EBF.AFA7138A.M6233B19[114](pagefilePath, "\\", "\\\\"), "'", "\\'");
				using (ManagementObjectSearcher managementObjectSearcher = P4258EBF.AFA7138A.M6233B19[84](P4258EBF.AFA7138A.M6233B19[64]("SELECT InitialSize, MaximumSize FROM Win32_PageFileSetting WHERE Name = '", text, "'")))
				{
					using (ManagementObjectCollection.ManagementObjectEnumerator managementObjectEnumerator = P4258EBF.AFA7138A.M6233B19[122](P4258EBF.AFA7138A.M6233B19[343](managementObjectSearcher)))
					{
						while (managementObjectEnumerator.MoveNext())
						{
							ManagementBaseObject managementBaseObject = managementObjectEnumerator.Current;
							ManagementObject managementObject = (ManagementObject)managementBaseObject;
							uint num = P4258EBF.AFA7138A.M6233B19[205](P4258EBF.AFA7138A.M6233B19[491](managementObject, "InitialSize"));
							uint num2 = P4258EBF.AFA7138A.M6233B19[205](P4258EBF.AFA7138A.M6233B19[491](managementObject, "MaximumSize"));
							if (num > 0U || num2 > 0U)
							{
								return P4258EBF.AFA7138A.M6233B19[629](num, num2);
							}
						}
					}
				}
				using (ManagementObjectSearcher managementObjectSearcher2 = P4258EBF.AFA7138A.M6233B19[84]("SELECT Name, AllocatedBaseSize FROM Win32_PageFileUsage"))
				{
					using (ManagementObjectCollection.ManagementObjectEnumerator managementObjectEnumerator2 = P4258EBF.AFA7138A.M6233B19[122](P4258EBF.AFA7138A.M6233B19[343](managementObjectSearcher2)))
					{
						while (managementObjectEnumerator2.MoveNext())
						{
							ManagementBaseObject managementBaseObject2 = managementObjectEnumerator2.Current;
							ManagementObject managementObject2 = (ManagementObject)managementBaseObject2;
							object obj = P4258EBF.AFA7138A.M6233B19[491](managementObject2, "Name");
							string text2 = ((obj != null) ? obj.ToString() : null) ?? P4258EBF.AFA7138A.M6233B19[280]();
							if (P4258EBF.AFA7138A.M6233B19[492](text2, pagefilePath, StringComparison.OrdinalIgnoreCase))
							{
								return P4258EBF.AFA7138A.M6233B19[205](P4258EBF.AFA7138A.M6233B19[491](managementObject2, "AllocatedBaseSize"));
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Log("Failed to read pagefile size");
				Logger.Log(ex);
			}
			return 0U;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0015FBB8 File Offset: 0x0015D3B8
		private static bool HasEnoughDiskSpaceForIncrease(string pagefilePath, uint desiredMb, uint currentMb)
		{
			bool flag;
			try
			{
				if (desiredMb <= currentMb)
				{
					flag = true;
				}
				else
				{
					DriveInfo driveInfo = K99147AA.C52E9F1D(P4258EBF.AFA7138A.M6233B19[350](pagefilePath) ?? "C:\\");
					long num = P4258EBF.AFA7138A.M6233B19[342](driveInfo);
					long num2 = P4258EBF.AFA7138A.M6233B19[575](driveInfo);
					long num3 = (long)((ulong)(desiredMb - currentMb) * 1048576UL);
					long num4 = num2 - num3;
					long num5 = (long)P4258EBF.AFA7138A.M6233B19[232]((double)num * 0.1);
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = P4258EBF.AFA7138A.M6233B19[467](64, 6);
					P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "Disk check (");
					P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler, P4258EBF.AFA7138A.M6233B19[553](driveInfo));
					P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "): total=");
					defaultInterpolatedStringHandler.AppendFormatted<long>(num);
					P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, " free=");
					defaultInterpolatedStringHandler.AppendFormatted<long>(num2);
					P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, " delta=");
					defaultInterpolatedStringHandler.AppendFormatted<long>(num3);
					P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, " freeAfter=");
					defaultInterpolatedStringHandler.AppendFormatted<long>(num4);
					P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, " requiredFree(10%)=");
					defaultInterpolatedStringHandler.AppendFormatted<long>(num5);
					Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler));
					flag = num4 > num5;
				}
			}
			catch (Exception ex)
			{
				Logger.Log("Failed to validate disk free space for pagefile change");
				Logger.Log(ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00156470 File Offset: 0x00153C70
		public static void disableAutoPagefile()
		{
			try
			{
				bool flag = PagefileOptimization.isAutomaticPagefile();
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = P4258EBF.AFA7138A.M6233B19[467](33, 1);
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "AutomaticManagedPagefile before: ");
				defaultInterpolatedStringHandler.AppendFormatted<bool>(flag);
				Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler));
				if (flag)
				{
					using (ManagementObjectSearcher managementObjectSearcher = P4258EBF.AFA7138A.M6233B19[84]("SELECT * FROM Win32_ComputerSystem"))
					{
						using (ManagementObjectCollection.ManagementObjectEnumerator managementObjectEnumerator = P4258EBF.AFA7138A.M6233B19[122](P4258EBF.AFA7138A.M6233B19[343](managementObjectSearcher)))
						{
							while (managementObjectEnumerator.MoveNext())
							{
								ManagementBaseObject managementBaseObject = managementObjectEnumerator.Current;
								ManagementObject managementObject = (ManagementObject)managementBaseObject;
								P4258EBF.AFA7138A.M6233B19[3](managementObject, "AutomaticManagedPagefile", false);
								P4258EBF.AFA7138A.M6233B19[243](managementObject);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Log("Failed to disable automatic pagefile");
				Logger.Log(ex);
			}
			finally
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2;
				P4258EBF.AFA7138A.M6233B19[466](ref defaultInterpolatedStringHandler2, 32, 1);
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler2, "AutomaticManagedPagefile after: ");
				defaultInterpolatedStringHandler2.AppendFormatted<bool>(PagefileOptimization.isAutomaticPagefile());
				Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler2));
			}
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00141B6C File Offset: 0x0013F36C
		public static void enableAutoPagefile()
		{
			try
			{
				bool flag = PagefileOptimization.isAutomaticPagefile();
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = P4258EBF.AFA7138A.M6233B19[467](33, 1);
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "AutomaticManagedPagefile before: ");
				defaultInterpolatedStringHandler.AppendFormatted<bool>(flag);
				Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler));
				if (!flag)
				{
					using (ManagementObjectSearcher managementObjectSearcher = P4258EBF.AFA7138A.M6233B19[84]("SELECT * FROM Win32_ComputerSystem"))
					{
						using (ManagementObjectCollection.ManagementObjectEnumerator managementObjectEnumerator = P4258EBF.AFA7138A.M6233B19[122](P4258EBF.AFA7138A.M6233B19[343](managementObjectSearcher)))
						{
							while (managementObjectEnumerator.MoveNext())
							{
								ManagementBaseObject managementBaseObject = managementObjectEnumerator.Current;
								ManagementObject managementObject = (ManagementObject)managementBaseObject;
								P4258EBF.AFA7138A.M6233B19[3](managementObject, "AutomaticManagedPagefile", true);
								P4258EBF.AFA7138A.M6233B19[243](managementObject);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Log("Failed to enable automatic pagefile");
				Logger.Log(ex);
			}
			finally
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2;
				P4258EBF.AFA7138A.M6233B19[466](ref defaultInterpolatedStringHandler2, 32, 1);
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler2, "AutomaticManagedPagefile after: ");
				defaultInterpolatedStringHandler2.AppendFormatted<bool>(PagefileOptimization.isAutomaticPagefile());
				Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler2));
			}
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0014FAD8 File Offset: 0x0014D2D8
		public static void createPagefile(int initSize = 0, int maxSize = 0)
		{
			PagefileOptimization.disableAutoPagefile();
			bool flag = false;
			using (ManagementObjectSearcher managementObjectSearcher = P4258EBF.AFA7138A.M6233B19[84]("SELECT * FROM Win32_PageFileSetting"))
			{
				using (ManagementObjectCollection.ManagementObjectEnumerator managementObjectEnumerator = P4258EBF.AFA7138A.M6233B19[122](P4258EBF.AFA7138A.M6233B19[343](managementObjectSearcher)))
				{
					while (managementObjectEnumerator.MoveNext())
					{
						ManagementBaseObject managementBaseObject = managementObjectEnumerator.Current;
						ManagementObject managementObject = (ManagementObject)managementBaseObject;
						object obj = P4258EBF.AFA7138A.M6233B19[491](managementObject, "Name");
						string text = ((obj != null) ? obj.ToString() : null) ?? P4258EBF.AFA7138A.M6233B19[280]();
						if (!P4258EBF.AFA7138A.M6233B19[492](text, "C:\\pagefile.sys", StringComparison.OrdinalIgnoreCase))
						{
							P4258EBF.AFA7138A.M6233B19[249](managementObject);
						}
						else
						{
							P4258EBF.AFA7138A.M6233B19[3](managementObject, "InitialSize", initSize);
							P4258EBF.AFA7138A.M6233B19[3](managementObject, "MaximumSize", maxSize);
							P4258EBF.AFA7138A.M6233B19[243](managementObject);
							flag = true;
						}
					}
				}
			}
			if (!flag)
			{
				PagefileOptimization.CreateOrUpdatePagefileSetting("C:\\pagefile.sys", (uint)initSize, (uint)maxSize);
			}
			Logger.Log("Pagefile settings were updated");
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00144640 File Offset: 0x00141E40
		private static List<PagefileOptimization.PagefileSettingState> GetPageFileSettings()
		{
			List<PagefileOptimization.PagefileSettingState> list = new List<PagefileOptimization.PagefileSettingState>();
			try
			{
				using (ManagementObjectSearcher managementObjectSearcher = P4258EBF.AFA7138A.M6233B19[84]("SELECT * FROM Win32_PageFileSetting"))
				{
					using (ManagementObjectCollection.ManagementObjectEnumerator managementObjectEnumerator = P4258EBF.AFA7138A.M6233B19[122](P4258EBF.AFA7138A.M6233B19[343](managementObjectSearcher)))
					{
						while (managementObjectEnumerator.MoveNext())
						{
							ManagementBaseObject managementBaseObject = managementObjectEnumerator.Current;
							ManagementObject managementObject = (ManagementObject)managementBaseObject;
							List<PagefileOptimization.PagefileSettingState> list2 = list;
							PagefileOptimization.PagefileSettingState pagefileSettingState = new PagefileOptimization.PagefileSettingState();
							object obj = P4258EBF.AFA7138A.M6233B19[491](managementObject, "Name");
							pagefileSettingState.Name = ((obj != null) ? obj.ToString() : null) ?? P4258EBF.AFA7138A.M6233B19[280]();
							pagefileSettingState.InitialSize = P4258EBF.AFA7138A.M6233B19[205](P4258EBF.AFA7138A.M6233B19[491](managementObject, "InitialSize"));
							pagefileSettingState.MaximumSize = P4258EBF.AFA7138A.M6233B19[205](P4258EBF.AFA7138A.M6233B19[491](managementObject, "MaximumSize"));
							list2.Add(pagefileSettingState);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Log("Failed to read pagefile settings");
				Logger.Log(ex);
			}
			return list;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0013A564 File Offset: 0x00137D64
		private static void RestorePagefile(PagefileOptimization.PagefileState state)
		{
			PagefileOptimization.SetAutomaticManagedPagefile(state.AutomaticManagedPagefile);
			if (state.AutomaticManagedPagefile)
			{
				return;
			}
			HashSet<string> hashSet = (from x in state.Settings
				select x.Name into x
				where !P4258EBF.AFA7138A.M6233B19[426](x)
				select x).ToHashSet<string>(P4258EBF.AFA7138A.M6233B19[28]());
			using (ManagementObjectSearcher managementObjectSearcher = P4258EBF.AFA7138A.M6233B19[84]("SELECT * FROM Win32_PageFileSetting"))
			{
				using (ManagementObjectCollection.ManagementObjectEnumerator managementObjectEnumerator = P4258EBF.AFA7138A.M6233B19[122](P4258EBF.AFA7138A.M6233B19[343](managementObjectSearcher)))
				{
					while (managementObjectEnumerator.MoveNext())
					{
						ManagementBaseObject managementBaseObject = managementObjectEnumerator.Current;
						ManagementObject managementObject = (ManagementObject)managementBaseObject;
						PagefileOptimization.<>c__DisplayClass24_0 CS$<>8__locals1 = new PagefileOptimization.<>c__DisplayClass24_0();
						PagefileOptimization.<>c__DisplayClass24_0 CS$<>8__locals2 = CS$<>8__locals1;
						object obj = P4258EBF.AFA7138A.M6233B19[491](managementObject, "Name");
						CS$<>8__locals2.name = ((obj != null) ? obj.ToString() : null) ?? P4258EBF.AFA7138A.M6233B19[280]();
						if (!hashSet.Contains(CS$<>8__locals1.name))
						{
							P4258EBF.AFA7138A.M6233B19[249](managementObject);
						}
						else
						{
							PagefileOptimization.PagefileSettingState pagefileSettingState = state.Settings.First<PagefileOptimization.PagefileSettingState>((PagefileOptimization.PagefileSettingState x) => P4258EBF.AFA7138A.M6233B19[492](x.Name, CS$<>8__locals1.name, StringComparison.OrdinalIgnoreCase));
							P4258EBF.AFA7138A.M6233B19[3](managementObject, "InitialSize", pagefileSettingState.InitialSize);
							P4258EBF.AFA7138A.M6233B19[3](managementObject, "MaximumSize", pagefileSettingState.MaximumSize);
							P4258EBF.AFA7138A.M6233B19[243](managementObject);
						}
					}
				}
			}
			foreach (PagefileOptimization.PagefileSettingState pagefileSettingState2 in state.Settings)
			{
				if (!P4258EBF.AFA7138A.M6233B19[426](pagefileSettingState2.Name) && !PagefileOptimization.PageFileSettingExists(pagefileSettingState2.Name))
				{
					PagefileOptimization.CreateOrUpdatePagefileSetting(pagefileSettingState2.Name, pagefileSettingState2.InitialSize, pagefileSettingState2.MaximumSize);
				}
			}
			Logger.Log("Pagefile settings were restored");
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0014444C File Offset: 0x00141C4C
		private static bool PageFileSettingExists(string name)
		{
			string text = P4258EBF.AFA7138A.M6233B19[114](P4258EBF.AFA7138A.M6233B19[114](name, "\\", "\\\\"), "'", "\\'");
			bool flag;
			using (ManagementObjectSearcher managementObjectSearcher = P4258EBF.AFA7138A.M6233B19[84](P4258EBF.AFA7138A.M6233B19[64]("SELECT * FROM Win32_PageFileSetting WHERE Name = '", text, "'")))
			{
				using (ManagementObjectCollection.ManagementObjectEnumerator managementObjectEnumerator = P4258EBF.AFA7138A.M6233B19[122](P4258EBF.AFA7138A.M6233B19[343](managementObjectSearcher)))
				{
					if (managementObjectEnumerator.MoveNext())
					{
						ManagementObject managementObject = (ManagementObject)managementObjectEnumerator.Current;
						return true;
					}
				}
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0014EE04 File Offset: 0x0014C604
		private static void CreateOrUpdatePagefileSetting(string name, uint initialSize, uint maximumSize)
		{
			using (ManagementClass managementClass = P4258EBF.AFA7138A.M6233B19[460]("Win32_PageFileSetting"))
			{
				ManagementObject managementObject = P4258EBF.AFA7138A.M6233B19[236](managementClass);
				if (managementObject == null)
				{
					throw P4258EBF.AFA7138A.M6233B19[115]("Failed to create Win32_PageFileSetting instance.");
				}
				using (ManagementObject managementObject2 = managementObject)
				{
					P4258EBF.AFA7138A.M6233B19[3](managementObject2, "Name", name);
					P4258EBF.AFA7138A.M6233B19[3](managementObject2, "InitialSize", initialSize);
					P4258EBF.AFA7138A.M6233B19[3](managementObject2, "MaximumSize", maximumSize);
					P4258EBF.AFA7138A.M6233B19[243](managementObject2);
				}
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00159F74 File Offset: 0x00157774
		private static bool IsSystemManagedPagefileOnSystemDrive(PagefileOptimization.PagefileSettingState setting)
		{
			return P4258EBF.AFA7138A.M6233B19[492](setting.Name, "C:\\pagefile.sys", StringComparison.OrdinalIgnoreCase) && setting.InitialSize == 0U && setting.MaximumSize == 0U;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00140B98 File Offset: 0x0013E398
		private static void SetAutomaticManagedPagefile(bool enabled)
		{
			try
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = P4258EBF.AFA7138A.M6233B19[467](33, 1);
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "AutomaticManagedPagefile before: ");
				defaultInterpolatedStringHandler.AppendFormatted<bool>(PagefileOptimization.isAutomaticPagefile());
				Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler));
				using (ManagementObjectSearcher managementObjectSearcher = P4258EBF.AFA7138A.M6233B19[84]("SELECT * FROM Win32_ComputerSystem"))
				{
					using (ManagementObjectCollection.ManagementObjectEnumerator managementObjectEnumerator = P4258EBF.AFA7138A.M6233B19[122](P4258EBF.AFA7138A.M6233B19[343](managementObjectSearcher)))
					{
						while (managementObjectEnumerator.MoveNext())
						{
							ManagementBaseObject managementBaseObject = managementObjectEnumerator.Current;
							ManagementObject managementObject = (ManagementObject)managementBaseObject;
							P4258EBF.AFA7138A.M6233B19[3](managementObject, "AutomaticManagedPagefile", enabled);
							P4258EBF.AFA7138A.M6233B19[243](managementObject);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Log("Failed to set automatic pagefile status");
				Logger.Log(ex);
			}
			finally
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2;
				P4258EBF.AFA7138A.M6233B19[466](ref defaultInterpolatedStringHandler2, 32, 1);
				P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler2, "AutomaticManagedPagefile after: ");
				defaultInterpolatedStringHandler2.AppendFormatted<bool>(PagefileOptimization.isAutomaticPagefile());
				Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler2));
			}
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0015A6E0 File Offset: 0x00157EE0
		public PagefileOptimization()
		{
			P4258EBF.AFA7138A.M6233B19[130](this);
		}

		// Token: 0x04000099 RID: 153
		private const string SystemManagedPagefileName = "C:\\pagefile.sys";

		// Token: 0x0400009A RID: 154
		private const int IdealTotalMemoryGb = 40;

		// Token: 0x0400009B RID: 155
		private const int MinPagefileGb = 8;

		// Token: 0x0400009C RID: 156
		private const long BytesPerGb = 1073741824L;

		// Token: 0x0400009D RID: 157
		private const long BytesPerMb = 1048576L;

		// Token: 0x020000A1 RID: 161
		private sealed class PagefileState
		{
			// Token: 0x170000B0 RID: 176
			// (get) Token: 0x0600044A RID: 1098 RVA: 0x0001A5F1 File Offset: 0x000189F1
			// (set) Token: 0x0600044B RID: 1099 RVA: 0x0001A5F9 File Offset: 0x000189F9
			public bool AutomaticManagedPagefile { get; set; }

			// Token: 0x170000B1 RID: 177
			// (get) Token: 0x0600044C RID: 1100 RVA: 0x0001A602 File Offset: 0x00018A02
			// (set) Token: 0x0600044D RID: 1101 RVA: 0x0001A60A File Offset: 0x00018A0A
			public List<PagefileOptimization.PagefileSettingState> Settings { get; set; } = new List<PagefileOptimization.PagefileSettingState>();

			// Token: 0x0600044E RID: 1102 RVA: 0x00160168 File Offset: 0x0015D968
			public PagefileState()
			{
				P4258EBF.AFA7138A.M6233B19[130](this);
			}
		}

		// Token: 0x020000A2 RID: 162
		private sealed class PagefileSettingState
		{
			// Token: 0x170000B2 RID: 178
			// (get) Token: 0x0600044F RID: 1103 RVA: 0x0001A626 File Offset: 0x00018A26
			// (set) Token: 0x06000450 RID: 1104 RVA: 0x0001A62E File Offset: 0x00018A2E
			public string Name { get; set; } = P4258EBF.AFA7138A.M6233B19[280]();

			// Token: 0x170000B3 RID: 179
			// (get) Token: 0x06000451 RID: 1105 RVA: 0x0001A637 File Offset: 0x00018A37
			// (set) Token: 0x06000452 RID: 1106 RVA: 0x0001A63F File Offset: 0x00018A3F
			public uint InitialSize { get; set; }

			// Token: 0x170000B4 RID: 180
			// (get) Token: 0x06000453 RID: 1107 RVA: 0x0001A648 File Offset: 0x00018A48
			// (set) Token: 0x06000454 RID: 1108 RVA: 0x0001A650 File Offset: 0x00018A50
			public uint MaximumSize { get; set; }

			// Token: 0x06000455 RID: 1109 RVA: 0x0015D8A8 File Offset: 0x0015B0A8
			public PagefileSettingState()
			{
				P4258EBF.AFA7138A.M6233B19[130](this);
			}
		}
	}
}
