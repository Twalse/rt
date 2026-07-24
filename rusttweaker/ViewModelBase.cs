using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace RustTweakerDemo
{
	// Token: 0x02000012 RID: 18
	public abstract class ViewModelBase : INotifyPropertyChanged
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600007F RID: 127 RVA: 0x0013F4B4 File Offset: 0x0013CCB4
		// (remove) Token: 0x06000080 RID: 128 RVA: 0x0015F0BC File Offset: 0x0015C8BC
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

		// Token: 0x06000081 RID: 129 RVA: 0x0015CAA8 File Offset: 0x0015A2A8
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged == null)
			{
				return;
			}
			O011C40B.PBA2A6BA(propertyChanged, this, P4258EBF.AFA7138A.M6233B19[164](propertyName));
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003AAA File Offset: 0x00001EAA
		protected virtual bool SetProperty<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
		{
			if (EqualityComparer<T>.Default.Equals(backingField, value))
			{
				return false;
			}
			backingField = value;
			this.OnPropertyChanged(propertyName);
			return true;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00161EF8 File Offset: 0x0015F6F8
		protected ViewModelBase()
		{
			P4258EBF.AFA7138A.M6233B19[130](this);
		}
	}
}
