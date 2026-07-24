using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace RustTweakerDemo
{
	// Token: 0x02000010 RID: 16
	public class SteamAccount : INotifyPropertyChanged
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002F35 File Offset: 0x00001335
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00002F3D File Offset: 0x0000133D
		public string SteamId { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002F46 File Offset: 0x00001346
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00002F4E File Offset: 0x0000134E
		public string PersonaName { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002F57 File Offset: 0x00001357
		// (set) Token: 0x06000055 RID: 85 RVA: 0x00002F5F File Offset: 0x0000135F
		public string LastUsed { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002F68 File Offset: 0x00001368
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00002F70 File Offset: 0x00001370
		public string ConfigPath { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002F79 File Offset: 0x00001379
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00002F81 File Offset: 0x00001381
		public string AvatarPath
		{
			get
			{
				return this._avatarPath;
			}
			set
			{
				this._avatarPath = value;
				this.OnPropertyChanged("AvatarPath");
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002F95 File Offset: 0x00001395
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00002F9D File Offset: 0x0000139D
		public bool IsSelected
		{
			get
			{
				return this._isSelected;
			}
			set
			{
				this._isSelected = value;
				this.OnPropertyChanged("IsSelected");
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600005C RID: 92 RVA: 0x0015A444 File Offset: 0x00157C44
		public string SteamId64
		{
			get
			{
				long num;
				if (P4258EBF.AFA7138A.M6233B19[472](this.SteamId, ref num))
				{
					long num2 = num + 76561197960265728L;
					return P4258EBF.AFA7138A.M6233B19[277](ref num2);
				}
				return this.SteamId;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00157034 File Offset: 0x00154834
		public bool HasAvatar
		{
			get
			{
				return !P4258EBF.AFA7138A.M6233B19[88](this.AvatarPath) && P4258EBF.AFA7138A.M6233B19[627](this.AvatarPath);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00158578 File Offset: 0x00155D78
		public string DisplayName
		{
			get
			{
				if (P4258EBF.AFA7138A.M6233B19[88](this.PersonaName))
				{
					return P4258EBF.AFA7138A.M6233B19[478]("User ", this.SteamId);
				}
				return this.PersonaName;
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600005F RID: 95 RVA: 0x00149058 File Offset: 0x00146858
		// (remove) Token: 0x06000060 RID: 96 RVA: 0x00160C60 File Offset: 0x0015E460
		public event PropertyChangedEventHandler PropertyChanged
		{
			[CompilerGenerated]
			add
			{
				PropertyChangedEventHandler propertyChangedEventHandler = this.PropertyChanged;
				PropertyChangedEventHandler propertyChangedEventHandler2;
				do
				{
					propertyChangedEventHandler2 = propertyChangedEventHandler;
					PropertyChangedEventHandler propertyChangedEventHandler3 = (PropertyChangedEventHandler)P4258EBF.AFA7138A.M6233B19[237](propertyChangedEventHandler2, value);
					propertyChangedEventHandler = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref this.PropertyChanged, propertyChangedEventHandler3, propertyChangedEventHandler2);
				}
				while (propertyChangedEventHandler != propertyChangedEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				PropertyChangedEventHandler propertyChangedEventHandler = this.PropertyChanged;
				PropertyChangedEventHandler propertyChangedEventHandler2;
				do
				{
					propertyChangedEventHandler2 = propertyChangedEventHandler;
					PropertyChangedEventHandler propertyChangedEventHandler3 = (PropertyChangedEventHandler)P4258EBF.AFA7138A.M6233B19[137](propertyChangedEventHandler2, value);
					propertyChangedEventHandler = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref this.PropertyChanged, propertyChangedEventHandler3, propertyChangedEventHandler2);
				}
				while (propertyChangedEventHandler != propertyChangedEventHandler2);
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0015E670 File Offset: 0x0015BE70
		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged == null)
			{
				return;
			}
			O011C40B.PBA2A6BA(propertyChanged, this, P4258EBF.AFA7138A.M6233B19[164](propertyName));
		}

		// Token: 0x06000062 RID: 98 RVA: 0x0015F680 File Offset: 0x0015CE80
		public SteamAccount()
		{
			P4258EBF.AFA7138A.M6233B19[130](this);
		}

		// Token: 0x0400001C RID: 28
		private const long STEAM_ID_BASE = 76561197960265728L;

		// Token: 0x04000021 RID: 33
		private string _avatarPath;

		// Token: 0x04000022 RID: 34
		private bool _isSelected;
	}
}
