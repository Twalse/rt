using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Microsoft.Win32;

namespace RustTweaker.Optimization
{
	// Token: 0x02000029 RID: 41
	public class DisableUnusedServices : IOptimization
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600013B RID: 315 RVA: 0x0015B950 File Offset: 0x00159150
		private static string UnsupportedServicesStateDirectory
		{
			get
			{
				return P4258EBF.AFA7138A.M6233B19[278](P4258EBF.AFA7138A.M6233B19[54](Environment.SpecialFolder.ApplicationData), "RustTweaker", "OptimizationOriginalStates");
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600013C RID: 316 RVA: 0x001493A8 File Offset: 0x00146BA8
		private static string UnsupportedServicesPath
		{
			get
			{
				return P4258EBF.AFA7138A.M6233B19[158](DisableUnusedServices.UnsupportedServicesStateDirectory, "DisableUnusedServices.UnsupportedServices.json");
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00007065 File Offset: 0x00005465
		public OptimizationId Id
		{
			get
			{
				return OptimizationId.DisableUnusedServices;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00007068 File Offset: 0x00005468
		public bool NeedComputerRestart
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600013F RID: 319 RVA: 0x0000706B File Offset: 0x0000546B
		public bool NeedSteamRestart
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0013A49C File Offset: 0x00137C9C
		public void Apply(OptimizationTargetStatus targetStatus)
		{
			List<ServiceInfoDTO> allServices = DisableUnusedServices.GetAllServices();
			IReadOnlyList<ServiceConfig> goodServicesConfig = this.GetGoodServicesConfig();
			IReadOnlyList<ServiceConfig> badServicesConfig = this.GetBadServicesConfig();
			IReadOnlyList<string> unsupportedServiceNames = DisableUnusedServices.GetUnsupportedServiceNames(goodServicesConfig, badServicesConfig, allServices);
			IReadOnlyList<ServiceConfig> readOnlyList = DisableUnusedServices.FilterUnsupportedServices(goodServicesConfig, unsupportedServiceNames);
			if (targetStatus == OptimizationTargetStatus.Good)
			{
				string unsupportedServicesJson = DisableUnusedServices.GetUnsupportedServicesJson(unsupportedServiceNames);
				DisableUnusedServices.SaveUnsupportedServices(unsupportedServicesJson);
				DisableUnusedServices.LogUnsupportedServices(unsupportedServicesJson);
				IReadOnlyList<ServiceConfig> affectedServiceStates = this.GetAffectedServiceStates(readOnlyList, allServices);
				if (affectedServiceStates.Count > 0)
				{
					OptimizationOriginalStateStore.SaveIfMissing<IReadOnlyList<ServiceConfig>>(this.Id, affectedServiceStates);
				}
			}
			IReadOnlyList<ServiceConfig> readOnlyList2;
			if (targetStatus != OptimizationTargetStatus.Good)
			{
				if (targetStatus != OptimizationTargetStatus.Bad)
				{
					throw P4258EBF.AFA7138A.M6233B19[80]("targetStatus", targetStatus, null);
				}
				readOnlyList2 = this.GetRestoreServicesConfig(badServicesConfig, unsupportedServiceNames);
			}
			else
			{
				readOnlyList2 = readOnlyList;
			}
			IReadOnlyList<ServiceConfig> readOnlyList3 = readOnlyList2;
			DisableUnusedServices.ApplyServiceConfig(readOnlyList3, allServices);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00007118 File Offset: 0x00005518
		private IReadOnlyList<ServiceConfig> GetRestoreServicesConfig(IReadOnlyList<ServiceConfig> badConfig, IReadOnlyCollection<string> unsupportedServiceNames)
		{
			List<ServiceConfig> list;
			if (!OptimizationOriginalStateStore.TryRead<List<ServiceConfig>>(this.Id, out list) || list == null)
			{
				return DisableUnusedServices.FilterUnsupportedServices(badConfig, unsupportedServiceNames);
			}
			return DisableUnusedServices.FilterUnsupportedServices(list, unsupportedServiceNames);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0014FC64 File Offset: 0x0014D464
		private IReadOnlyList<ServiceConfig> GetAffectedServiceStates(IReadOnlyList<ServiceConfig> filteredGoodConfig, IReadOnlyList<ServiceInfoDTO> allServices)
		{
			List<ServiceConfig> list = new List<ServiceConfig>();
			using (IEnumerator<ServiceConfig> enumerator = filteredGoodConfig.GetEnumerator())
			{
				while (P4258EBF.AFA7138A.M6233B19[411](enumerator))
				{
					ServiceConfig serviceConfig = enumerator.Current;
					using (IEnumerator<ServiceInfoDTO> enumerator2 = DisableUnusedServices.FindServices(allServices, serviceConfig.Name).GetEnumerator())
					{
						while (P4258EBF.AFA7138A.M6233B19[411](enumerator2))
						{
							ServiceInfoDTO service = enumerator2.Current;
							if (!list.Exists((ServiceConfig x) => P4258EBF.AFA7138A.M6233B19[492](x.Name, service.Name, StringComparison.OrdinalIgnoreCase)))
							{
								list.Add(new ServiceConfig
								{
									Name = service.Name,
									StartupType = service.StartMode
								});
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00007224 File Offset: 0x00005624
		public OptimizationStatus GetStatus()
		{
			IReadOnlyList<ServiceConfig> goodServicesConfig = this.GetGoodServicesConfig();
			IReadOnlyList<ServiceConfig> badServicesConfig = this.GetBadServicesConfig();
			List<ServiceInfoDTO> allServices = DisableUnusedServices.GetAllServices();
			IReadOnlyList<ServiceInfoDTO> currentServices = this.GetCurrentServices();
			IReadOnlyList<string> unsupportedServiceNames = DisableUnusedServices.GetUnsupportedServiceNames(goodServicesConfig, badServicesConfig, allServices);
			IReadOnlyList<ServiceConfig> readOnlyList = DisableUnusedServices.FilterUnsupportedServices(goodServicesConfig, unsupportedServiceNames);
			return DisableUnusedServices.GetStatusForConfig(readOnlyList, currentServices);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00007268 File Offset: 0x00005668
		private IReadOnlyList<ServiceConfig> GetBadServicesConfig()
		{
			return DisableUnusedServices.ReadServicesConfig("Assets/ServicesConfigBad.json");
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00007274 File Offset: 0x00005674
		private IReadOnlyList<ServiceConfig> GetGoodServicesConfig()
		{
			return DisableUnusedServices.ReadServicesConfig("Assets/ServicesConfigGood.json");
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0015AB68 File Offset: 0x00158368
		private static IReadOnlyList<ServiceConfig> ReadServicesConfig(string path)
		{
			if (P4258EBF.AFA7138A.M6233B19[426](path))
			{
				throw P4258EBF.AFA7138A.M6233B19[110]("Config path cannot be empty.", "path");
			}
			string text = SystemInformer.ResolveConfigPath(path);
			string text2 = P4258EBF.AFA7138A.M6233B19[267](text);
			List<ServiceConfig> list = JsonSerializer.Deserialize<List<ServiceConfig>>(text2, DisableUnusedServices.JsonOptions);
			return list ?? new List<ServiceConfig>();
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00140D64 File Offset: 0x0013E564
		private static IReadOnlyList<string> GetUnsupportedServiceNames(IReadOnlyList<ServiceConfig> goodConfig, IReadOnlyList<ServiceConfig> badConfig, IReadOnlyList<ServiceInfoDTO> allServices)
		{
			HashSet<string> hashSet = new HashSet<string>(P4258EBF.AFA7138A.M6233B19[28]());
			List<ServiceConfig> list = goodConfig.Concat<ServiceConfig>(badConfig).ToList<ServiceConfig>();
			using (IEnumerator<string> enumerator = DisableUnusedServices.ReadUnsupportedServicesNameList().GetEnumerator())
			{
				while (P4258EBF.AFA7138A.M6233B19[411](enumerator))
				{
					string text = enumerator.Current;
					hashSet.Add(text);
				}
			}
			using (IEnumerator<string> enumerator2 = DisableUnusedServices.DetectUnsupportedServiceNames(list, allServices).GetEnumerator())
			{
				while (P4258EBF.AFA7138A.M6233B19[411](enumerator2))
				{
					string text2 = enumerator2.Current;
					hashSet.Add(text2);
				}
			}
			using (IEnumerator<string> enumerator3 = DisableUnusedServices.DetectUnknownStartupServiceNames(list, allServices).GetEnumerator())
			{
				while (P4258EBF.AFA7138A.M6233B19[411](enumerator3))
				{
					string text3 = enumerator3.Current;
					hashSet.Add(text3);
				}
			}
			return hashSet.ToList<string>();
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0013F4F8 File Offset: 0x0013CCF8
		[NullableContext(2)]
		public static ServiceStartupType ParseServiceStartupType(string startMode)
		{
			if (startMode != null)
			{
				int num = P4258EBF.AFA7138A.M6233B19[153](startMode);
				switch (num)
				{
				case 4:
				{
					char c = P4258EBF.AFA7138A.M6233B19[367](startMode, 0);
					if (c != 'A')
					{
						if (c == 'B')
						{
							if (P4258EBF.AFA7138A.M6233B19[250](startMode, "Boot"))
							{
								return ServiceStartupType.Boot;
							}
						}
					}
					else if (P4258EBF.AFA7138A.M6233B19[250](startMode, "Auto"))
					{
						return ServiceStartupType.Automatic;
					}
					break;
				}
				case 5:
				case 10:
					break;
				case 6:
				{
					char c = P4258EBF.AFA7138A.M6233B19[367](startMode, 0);
					if (c != 'M')
					{
						if (c == 'S')
						{
							if (P4258EBF.AFA7138A.M6233B19[250](startMode, "System"))
							{
								return ServiceStartupType.System;
							}
						}
					}
					else if (P4258EBF.AFA7138A.M6233B19[250](startMode, "Manual"))
					{
						return ServiceStartupType.Manual;
					}
					break;
				}
				case 7:
					if (P4258EBF.AFA7138A.M6233B19[250](startMode, "Unknown"))
					{
						return ServiceStartupType.Unknown;
					}
					break;
				case 8:
					if (P4258EBF.AFA7138A.M6233B19[250](startMode, "Disabled"))
					{
						return ServiceStartupType.Disabled;
					}
					break;
				case 9:
					if (P4258EBF.AFA7138A.M6233B19[250](startMode, "Automatic"))
					{
						return ServiceStartupType.Automatic;
					}
					break;
				case 11:
					if (P4258EBF.AFA7138A.M6233B19[250](startMode, "DelayedAuto"))
					{
						return ServiceStartupType.AutomaticDelayedStart;
					}
					break;
				default:
					if (num == 21)
					{
						if (P4258EBF.AFA7138A.M6233B19[250](startMode, "AutomaticDelayedStart"))
						{
							return ServiceStartupType.AutomaticDelayedStart;
						}
					}
					break;
				}
			}
			return ServiceStartupType.Unknown;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00143D08 File Offset: 0x00141508
		[return: Nullable(2)]
		public static ServiceInfoDTO GetServiceInfo(string serviceId)
		{
			if (!P4258EBF.AFA7138A.M6233B19[208]())
			{
				throw P4258EBF.AFA7138A.M6233B19[258]("Service info is available only on Windows.");
			}
			try
			{
				string text = P4258EBF.AFA7138A.M6233B19[64]("SELECT * FROM Win32_Service WHERE Name = '", DisableUnusedServices.EscapeWqlString(serviceId), "'");
				using (ManagementObjectSearcher managementObjectSearcher = P4258EBF.AFA7138A.M6233B19[84](text))
				{
					using (ManagementObjectCollection managementObjectCollection = P4258EBF.AFA7138A.M6233B19[343](managementObjectSearcher))
					{
						using (ManagementObjectCollection.ManagementObjectEnumerator managementObjectEnumerator = P4258EBF.AFA7138A.M6233B19[122](managementObjectCollection))
						{
							if (managementObjectEnumerator.MoveNext())
							{
								ManagementObject managementObject = (ManagementObject)managementObjectEnumerator.Current;
								ServiceInfoDTO serviceInfoDTO = new ServiceInfoDTO();
								object obj = P4258EBF.AFA7138A.M6233B19[491](managementObject, "Name");
								serviceInfoDTO.Name = ((obj != null) ? obj.ToString() : null) ?? "null";
								object obj2 = P4258EBF.AFA7138A.M6233B19[491](managementObject, "DisplayName");
								serviceInfoDTO.DisplayName = ((obj2 != null) ? obj2.ToString() : null) ?? "null";
								object obj3 = P4258EBF.AFA7138A.M6233B19[491](managementObject, "State");
								serviceInfoDTO.State = ((obj3 != null) ? obj3.ToString() : null) ?? "null";
								object obj4 = P4258EBF.AFA7138A.M6233B19[491](managementObject, "Status");
								serviceInfoDTO.Status = ((obj4 != null) ? obj4.ToString() : null) ?? "null";
								serviceInfoDTO.StartMode = DisableUnusedServices.GetServiceStartupType(managementObject);
								return serviceInfoDTO;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Log(P4258EBF.AFA7138A.M6233B19[478]("Failed to get service info for ", serviceId));
				Logger.Log(ex);
			}
			return null;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0015AF48 File Offset: 0x00158748
		private static string EscapeWqlString(string value)
		{
			return P4258EBF.AFA7138A.M6233B19[114](P4258EBF.AFA7138A.M6233B19[114](value, "\\", "\\\\"), "'", "\\'");
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00150BDC File Offset: 0x0014E3DC
		public IReadOnlyList<ServiceInfoDTO> GetCurrentServices()
		{
			List<ServiceInfoDTO> list = new List<ServiceInfoDTO>();
			List<ServiceInfoDTO> allServices = DisableUnusedServices.GetAllServices();
			using (IEnumerator<ServiceConfig> enumerator = this.GetGoodServicesConfig().GetEnumerator())
			{
				while (P4258EBF.AFA7138A.M6233B19[411](enumerator))
				{
					ServiceConfig serviceConfig = enumerator.Current;
					DisableUnusedServices.AddServiceIfExists(list, allServices, serviceConfig.Name);
				}
			}
			using (IEnumerator<ServiceConfig> enumerator2 = this.GetBadServicesConfig().GetEnumerator())
			{
				while (P4258EBF.AFA7138A.M6233B19[411](enumerator2))
				{
					ServiceConfig serviceConfig2 = enumerator2.Current;
					DisableUnusedServices.AddServiceIfExists(list, allServices, serviceConfig2.Name);
				}
			}
			return list;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00149930 File Offset: 0x00147130
		private static List<ServiceInfoDTO> GetAllServices()
		{
			List<ServiceInfoDTO> list = new List<ServiceInfoDTO>();
			try
			{
				using (ManagementObjectSearcher managementObjectSearcher = P4258EBF.AFA7138A.M6233B19[84]("SELECT * FROM Win32_Service"))
				{
					using (ManagementObjectCollection managementObjectCollection = P4258EBF.AFA7138A.M6233B19[343](managementObjectSearcher))
					{
						using (ManagementObjectCollection.ManagementObjectEnumerator managementObjectEnumerator = P4258EBF.AFA7138A.M6233B19[122](managementObjectCollection))
						{
							while (managementObjectEnumerator.MoveNext())
							{
								ManagementBaseObject managementBaseObject = managementObjectEnumerator.Current;
								ManagementObject managementObject = (ManagementObject)managementBaseObject;
								List<ServiceInfoDTO> list2 = list;
								ServiceInfoDTO serviceInfoDTO = new ServiceInfoDTO();
								object obj = P4258EBF.AFA7138A.M6233B19[491](managementObject, "Name");
								serviceInfoDTO.Name = ((obj != null) ? obj.ToString() : null) ?? "null";
								object obj2 = P4258EBF.AFA7138A.M6233B19[491](managementObject, "DisplayName");
								serviceInfoDTO.DisplayName = ((obj2 != null) ? obj2.ToString() : null) ?? "null";
								object obj3 = P4258EBF.AFA7138A.M6233B19[491](managementObject, "State");
								serviceInfoDTO.State = ((obj3 != null) ? obj3.ToString() : null) ?? "null";
								object obj4 = P4258EBF.AFA7138A.M6233B19[491](managementObject, "Status");
								serviceInfoDTO.Status = ((obj4 != null) ? obj4.ToString() : null) ?? "null";
								serviceInfoDTO.StartMode = DisableUnusedServices.GetServiceStartupType(managementObject);
								list2.Add(serviceInfoDTO);
							}
						}
						foreach (ServiceInfoDTO serviceInfoDTO2 in list)
						{
							DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = P4258EBF.AFA7138A.M6233B19[467](65, 5);
							P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "Service received: ");
							P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler, serviceInfoDTO2.Name);
							P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, " | DisplayName=");
							P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler, serviceInfoDTO2.DisplayName);
							P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, " | StartMode=");
							defaultInterpolatedStringHandler.AppendFormatted<ServiceStartupType>(serviceInfoDTO2.StartMode);
							P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, " | State=");
							P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler, serviceInfoDTO2.State);
							P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, " | Status=");
							P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler, serviceInfoDTO2.Status);
							Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler));
						}
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = P4258EBF.AFA7138A.M6233B19[467](25, 1);
						P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler2, "Total services received: ");
						defaultInterpolatedStringHandler2.AppendFormatted<int>(list.Count);
						Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler2));
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Log("Failed to read services");
				Logger.Log(ex);
			}
			return list;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x001508C0 File Offset: 0x0014E0C0
		private static IReadOnlyList<string> DetectUnsupportedServiceNames(IReadOnlyList<ServiceConfig> configuredServices, IReadOnlyList<ServiceInfoDTO> allServices)
		{
			List<string> list = new List<string>();
			using (IEnumerator<ServiceConfig> enumerator = configuredServices.GetEnumerator())
			{
				while (P4258EBF.AFA7138A.M6233B19[411](enumerator))
				{
					ServiceConfig serviceConfig = enumerator.Current;
					if (!DisableUnusedServices.FindServices(allServices, serviceConfig.Name).Any<ServiceInfoDTO>())
					{
						list.Add(serviceConfig.Name);
					}
				}
			}
			return list.Distinct<string>(P4258EBF.AFA7138A.M6233B19[28]()).ToList<string>();
		}

		// Token: 0x0600014E RID: 334 RVA: 0x001470EC File Offset: 0x001448EC
		private static IReadOnlyList<string> DetectUnknownStartupServiceNames(IReadOnlyList<ServiceConfig> configuredServices, IReadOnlyList<ServiceInfoDTO> allServices)
		{
			List<string> list = new List<string>();
			using (IEnumerator<ServiceConfig> enumerator = configuredServices.GetEnumerator())
			{
				while (P4258EBF.AFA7138A.M6233B19[411](enumerator))
				{
					ServiceConfig serviceConfig = enumerator.Current;
					IEnumerable<ServiceInfoDTO> enumerable = DisableUnusedServices.FindServices(allServices, serviceConfig.Name);
					if (enumerable.Any<ServiceInfoDTO>((ServiceInfoDTO service) => service.StartMode == ServiceStartupType.Unknown))
					{
						list.Add(serviceConfig.Name);
					}
				}
			}
			return list.Distinct<string>(P4258EBF.AFA7138A.M6233B19[28]()).ToList<string>();
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00155E00 File Offset: 0x00153600
		private static ServiceStartupType GetServiceStartupType(ManagementBaseObject service)
		{
			object obj = P4258EBF.AFA7138A.M6233B19[491](service, "StartMode");
			ServiceStartupType serviceStartupType = DisableUnusedServices.ParseServiceStartupType((obj != null) ? obj.ToString() : null);
			if (serviceStartupType != ServiceStartupType.Automatic)
			{
				return serviceStartupType;
			}
			object obj2 = P4258EBF.AFA7138A.M6233B19[491](service, "Name");
			string text = ((obj2 != null) ? obj2.ToString() : null);
			if (P4258EBF.AFA7138A.M6233B19[426](text))
			{
				return serviceStartupType;
			}
			if (!DisableUnusedServices.IsDelayedAutoStart(text))
			{
				return ServiceStartupType.Automatic;
			}
			return ServiceStartupType.AutomaticDelayedStart;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x001520E4 File Offset: 0x0014F8E4
		private static bool IsDelayedAutoStart(string serviceName)
		{
			bool flag;
			using (RegistryKey registryKey = P4258EBF.AFA7138A.M6233B19[415](P4258EBF.AFA7138A.M6233B19[298](), P4258EBF.AFA7138A.M6233B19[478]("SYSTEM\\CurrentControlSet\\Services\\", serviceName)))
			{
				object obj = ((registryKey != null) ? P4258EBF.AFA7138A.M6233B19[450](registryKey, "DelayedAutoStart") : null);
				flag = obj != null && P4258EBF.AFA7138A.M6233B19[228](obj) == 1;
			}
			return flag;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00142938 File Offset: 0x00140138
		private static void AddServiceIfExists(List<ServiceInfoDTO> services, IEnumerable<ServiceInfoDTO> allServices, string serviceName)
		{
			using (IEnumerator<ServiceInfoDTO> enumerator = DisableUnusedServices.FindServices(allServices, serviceName).GetEnumerator())
			{
				while (P4258EBF.AFA7138A.M6233B19[411](enumerator))
				{
					ServiceInfoDTO serviceInfo = enumerator.Current;
					if (!services.Exists((ServiceInfoDTO x) => P4258EBF.AFA7138A.M6233B19[492](x.Name, serviceInfo.Name, StringComparison.OrdinalIgnoreCase)))
					{
						services.Add(serviceInfo);
					}
				}
			}
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00144CAC File Offset: 0x001424AC
		private static IEnumerable<ServiceInfoDTO> FindServices(IEnumerable<ServiceInfoDTO> services, string serviceName)
		{
			if (P4258EBF.AFA7138A.M6233B19[470](serviceName, '*'))
			{
				string prefix = P4258EBF.AFA7138A.M6233B19[178](serviceName, '*');
				return services.Where<ServiceInfoDTO>((ServiceInfoDTO x) => P4258EBF.AFA7138A.M6233B19[44](x.Name, prefix, StringComparison.OrdinalIgnoreCase));
			}
			return services.Where<ServiceInfoDTO>((ServiceInfoDTO x) => P4258EBF.AFA7138A.M6233B19[492](x.Name, serviceName, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00007CEC File Offset: 0x000060EC
		private static IReadOnlyList<ServiceConfig> FilterUnsupportedServices(IReadOnlyList<ServiceConfig> config, IReadOnlyCollection<string> unsupportedServiceNames)
		{
			if (unsupportedServiceNames.Count == 0)
			{
				return config;
			}
			return config.Where<ServiceConfig>((ServiceConfig item) => !unsupportedServiceNames.Contains(item.Name)).ToList<ServiceConfig>();
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0014C79C File Offset: 0x00149F9C
		private static void SetServiceStartupType(string serviceName, ServiceStartupType startupType)
		{
			string text;
			switch (startupType)
			{
			case ServiceStartupType.Automatic:
				text = "auto";
				break;
			case ServiceStartupType.AutomaticDelayedStart:
				text = "delayed-auto";
				break;
			case ServiceStartupType.Manual:
				text = "demand";
				break;
			case ServiceStartupType.Disabled:
				text = "disabled";
				break;
			default:
				throw P4258EBF.AFA7138A.M6233B19[80]("startupType", startupType, "Unsupported service startup type.");
			}
			string text2 = text;
			ProcessStartInfo processStartInfo = P4258EBF.AFA7138A.M6233B19[394]();
			N62EB38A.CB1145A6(processStartInfo, "sc.exe");
			AA2B3D09.ND86FA10(processStartInfo, P4258EBF.AFA7138A.M6233B19[259]("config \"", serviceName, "\" start= ", text2));
			O8258311.M5A8918D(processStartInfo, false);
			OD07B821.I71E06A0(processStartInfo, true);
			DAB6CE3D.G1236E97(processStartInfo, true);
			JD06799C.I832069D(processStartInfo, true);
			using (Process process = JC11021F.C827CF8C(processStartInfo))
			{
				if (process == null)
				{
					throw P4258EBF.AFA7138A.M6233B19[115]("Failed to start sc.exe.");
				}
				string text3 = P4258EBF.AFA7138A.M6233B19[355](P4258EBF.AFA7138A.M6233B19[399](process));
				string text4 = P4258EBF.AFA7138A.M6233B19[355](P4258EBF.AFA7138A.M6233B19[403](process));
				P4258EBF.AFA7138A.M6233B19[341](process);
				if (P4258EBF.AFA7138A.M6233B19[373](process) != 0)
				{
					throw BC04CB32.OC845221(P4258EBF.AFA7138A.M6233B19[426](text4) ? P4258EBF.AFA7138A.M6233B19[597](text3) : P4258EBF.AFA7138A.M6233B19[597](text4));
				}
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0013CA6C File Offset: 0x0013A26C
		private static void ApplyServiceConfig(IReadOnlyList<ServiceConfig> config, IReadOnlyList<ServiceInfoDTO> allServices)
		{
			using (IEnumerator<ServiceConfig> enumerator = config.GetEnumerator())
			{
				while (P4258EBF.AFA7138A.M6233B19[411](enumerator))
				{
					ServiceConfig serviceConfig = enumerator.Current;
					List<ServiceInfoDTO> list = DisableUnusedServices.FindServices(allServices, serviceConfig.Name).ToList<ServiceInfoDTO>();
					string configServiceName = DisableUnusedServices.GetConfigServiceName(serviceConfig.Name);
					if (list.Count == 0)
					{
						Logger.Log(P4258EBF.AFA7138A.M6233B19[64]("Failed to set ", serviceConfig.Name, ": service was not found"));
					}
					else
					{
						foreach (ServiceInfoDTO serviceInfoDTO in list)
						{
							try
							{
								DisableUnusedServices.SetServiceStartupType(configServiceName, serviceConfig.StartupType);
								DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = P4258EBF.AFA7138A.M6233B19[467](10, 3);
								P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "Set ");
								P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler, serviceInfoDTO.Name);
								P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, ": ");
								defaultInterpolatedStringHandler.AppendFormatted<ServiceStartupType>(serviceInfoDTO.StartMode);
								P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, " to ");
								defaultInterpolatedStringHandler.AppendFormatted<ServiceStartupType>(serviceConfig.StartupType);
								Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler));
							}
							catch (Exception ex)
							{
								Logger.Log(P4258EBF.AFA7138A.M6233B19[478]("Failed to set ", serviceInfoDTO.Name));
								Logger.Log(ex);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00151D84 File Offset: 0x0014F584
		private static string GetConfigServiceName(string serviceName)
		{
			if (!P4258EBF.AFA7138A.M6233B19[470](serviceName, '*'))
			{
				return serviceName;
			}
			return P4258EBF.AFA7138A.M6233B19[178](serviceName, '*');
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0015D9C0 File Offset: 0x0015B1C0
		private static string GetUnsupportedServicesJson(IReadOnlyCollection<string> unsupportedServiceNames)
		{
			List<string> list = unsupportedServiceNames.Distinct<string>(P4258EBF.AFA7138A.M6233B19[28]()).ToList<string>();
			return JsonSerializer.Serialize<List<string>>(list, DisableUnusedServices.JsonOptions);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00160AE8 File Offset: 0x0015E2E8
		private static void LogUnsupportedServices(string unsupportedServicesJson)
		{
			Logger.Log(P4258EBF.AFA7138A.M6233B19[478]("Unsupported services: ", unsupportedServicesJson));
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0015B4D0 File Offset: 0x00158CD0
		private static void SaveUnsupportedServices(string unsupportedServicesJson)
		{
			try
			{
				P4258EBF.AFA7138A.M6233B19[111](DisableUnusedServices.UnsupportedServicesStateDirectory);
				P4258EBF.AFA7138A.M6233B19[94](DisableUnusedServices.UnsupportedServicesPath, unsupportedServicesJson);
			}
			catch (Exception ex)
			{
				Logger.Log("Failed to save unsupported services");
				Logger.Log(ex);
			}
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0014A45C File Offset: 0x00147C5C
		private static IReadOnlyList<string> ReadUnsupportedServicesNameList()
		{
			IReadOnlyList<string> readOnlyList;
			try
			{
				if (!P4258EBF.AFA7138A.M6233B19[627](DisableUnusedServices.UnsupportedServicesPath))
				{
					readOnlyList = Array.Empty<string>();
				}
				else
				{
					string text = P4258EBF.AFA7138A.M6233B19[267](DisableUnusedServices.UnsupportedServicesPath);
					List<string> list = JsonSerializer.Deserialize<List<string>>(text, DisableUnusedServices.JsonOptions);
					readOnlyList = list ?? new List<string>();
				}
			}
			catch (Exception ex)
			{
				Logger.Log("Failed to read unsupported services");
				Logger.Log(ex);
				readOnlyList = Array.Empty<string>();
			}
			return readOnlyList;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0014B53C File Offset: 0x00148D3C
		private static OptimizationStatus GetStatusForConfig(IReadOnlyList<ServiceConfig> config, IReadOnlyList<ServiceInfoDTO> currentServices)
		{
			int num = 0;
			int num2 = 0;
			using (IEnumerator<ServiceConfig> enumerator = config.GetEnumerator())
			{
				while (P4258EBF.AFA7138A.M6233B19[411](enumerator))
				{
					ServiceConfig serviceConfig = enumerator.Current;
					List<ServiceInfoDTO> list = DisableUnusedServices.FindServices(currentServices, serviceConfig.Name).ToList<ServiceInfoDTO>();
					if (list.Count != 0)
					{
						foreach (ServiceInfoDTO serviceInfoDTO in list)
						{
							num++;
							if (serviceInfoDTO.StartMode == serviceConfig.StartupType)
							{
								num2++;
							}
						}
					}
				}
			}
			if (num == 0)
			{
				return OptimizationStatus.Bad;
			}
			double num3 = (double)num2 / (double)num;
			if (num3 < 0.95)
			{
				return OptimizationStatus.Bad;
			}
			return OptimizationStatus.Good;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0015DA88 File Offset: 0x0015B288
		public DisableUnusedServices()
		{
			P4258EBF.AFA7138A.M6233B19[130](this);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00154B00 File Offset: 0x00152300
		// Note: this type is marked as 'beforefieldinit'.
		static DisableUnusedServices()
		{
			JsonSerializerOptions jsonSerializerOptions = P4258EBF.AFA7138A.M6233B19[14]();
			N2389A30.H1B6258D(jsonSerializerOptions, true);
			BD018D8C.AF87751F(jsonSerializerOptions).Add(P4258EBF.AFA7138A.M6233B19[299]());
			DisableUnusedServices.JsonOptions = jsonSerializerOptions;
		}

		// Token: 0x04000078 RID: 120
		private const string _badConfigPath = "Assets/ServicesConfigBad.json";

		// Token: 0x04000079 RID: 121
		private const string _goodConfigPath = "Assets/ServicesConfigGood.json";

		// Token: 0x0400007A RID: 122
		private const string _unsupportedServicesStateFileName = "DisableUnusedServices.UnsupportedServices.json";

		// Token: 0x0400007B RID: 123
		private static readonly JsonSerializerOptions JsonOptions;
	}
}
