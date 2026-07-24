using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Input;

namespace RustTweakerDemo
{
	// Token: 0x0200000F RID: 15
	public class RelayCommand : ICommand
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000049 RID: 73 RVA: 0x0015FAE0 File Offset: 0x0015D2E0
		// (remove) Token: 0x0600004A RID: 74 RVA: 0x0015FA4C File Offset: 0x0015D24C
		public event EventHandler CanExecuteChanged
		{
			[CompilerGenerated]
			add
			{
				EventHandler eventHandler = this.CanExecuteChanged;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler eventHandler3 = (EventHandler)P4258EBF.AFA7138A.M6233B19[237](eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.CanExecuteChanged, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler eventHandler = this.CanExecuteChanged;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler eventHandler3 = (EventHandler)P4258EBF.AFA7138A.M6233B19[137](eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.CanExecuteChanged, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002E8C File Offset: 0x0000128C
		public RelayCommand(Action execute, Func<bool> canExecute = null)
			: this(delegate(object _)
			{
				P4258EBF.AFA7138A.M6233B19[292](execute);
			}, (canExecute != null) ? ((object _) => canExecute()) : null)
		{
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0015EA84 File Offset: 0x0015C284
		public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
		{
			P4258EBF.AFA7138A.M6233B19[130](this);
			if (execute == null)
			{
				throw P4258EBF.AFA7138A.M6233B19[562]("execute");
			}
			this._execute = execute;
			this._canExecute = canExecute;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002EFB File Offset: 0x000012FB
		public bool CanExecute(object parameter)
		{
			Func<object, bool> canExecute = this._canExecute;
			return canExecute == null || canExecute(parameter);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002F0F File Offset: 0x0000130F
		public void Execute(object parameter)
		{
			this._execute(parameter);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x0014D120 File Offset: 0x0014A920
		public void RaiseCanExecuteChanged()
		{
			EventHandler canExecuteChanged = this.CanExecuteChanged;
			if (canExecuteChanged == null)
			{
				return;
			}
			G4B3E9BC.ADB8FD3A(canExecuteChanged, this, P4258EBF.AFA7138A.M6233B19[108]());
		}

		// Token: 0x04000019 RID: 25
		private readonly Action<object> _execute;

		// Token: 0x0400001A RID: 26
		private readonly Func<object, bool> _canExecute;
	}
}
