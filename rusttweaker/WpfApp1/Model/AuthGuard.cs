using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace WpfApp1.Model
{
	// Token: 0x0200005F RID: 95
	internal static class AuthGuard
	{
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000359 RID: 857 RVA: 0x00011FD2 File Offset: 0x000103D2
		public static bool IsAuthenticated
		{
			get
			{
				return AuthGuard._isAuthenticated;
			}
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0015E928 File Offset: 0x0015C128
		public static void StartContinuousValidation()
		{
			new H52867B7().E4B97194(null, 183003);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0014B46C File Offset: 0x00148C6C
		public static void StopValidation()
		{
			Timer validationTimer = AuthGuard._validationTimer;
			if (validationTimer != null)
			{
				CF2C64A0.F4230684(validationTimer);
			}
			AuthGuard._validationTimer = null;
			AuthGuard._started = false;
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0015A690 File Offset: 0x00157E90
		public static void SetAuthenticated(bool value)
		{
			new H52867B7().C68FE888(new object[] { value }, 37374);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0015DCB8 File Offset: 0x0015B4B8
		public static void EnforceAuth()
		{
			new H52867B7().C68FE888(null, 41409);
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0015DAA8 File Offset: 0x0015B2A8
		public static void OnAction()
		{
			new H52867B7().E4B97194(null, 180845);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0015BBF0 File Offset: 0x001593F0
		public static bool ValidateEntryPoint()
		{
			return (bool)new H52867B7().KA927C1D(null, 34823);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00140254 File Offset: 0x0013DA54
		private static void OnValidationTick(object state)
		{
			new H52867B7().KA927C1D(new object[] { state }, 45420);
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00160128 File Offset: 0x0015D928
		private static void ForceShutdown()
		{
			new H52867B7().FCA6C832(null, 38236);
		}

		// Token: 0x040000EE RID: 238
		private static Timer _validationTimer;

		// Token: 0x040000EF RID: 239
		private static volatile bool _isAuthenticated;

		// Token: 0x040000F0 RID: 240
		private static readonly object _lock = P4258EBF.AFA7138A.M6233B19[131]();

		// Token: 0x040000F1 RID: 241
		private static readonly TimeSpan ValidationInterval = P4258EBF.AFA7138A.M6233B19[9](30L);

		// Token: 0x040000F2 RID: 242
		private static volatile bool _started;

		// Token: 0x040000F3 RID: 243
		private static DateTime _lastActionCheck = P4258EBF.AFA7138A.M6233B19[301]();

		// Token: 0x040000F4 RID: 244
		private static readonly TimeSpan ActionCheckCooldown = P4258EBF.AFA7138A.M6233B19[9](60L);

		// Token: 0x040000F5 RID: 245
		private static volatile int _actionCounter;

		// Token: 0x040000F6 RID: 246
		private static readonly int ActionsBeforeCheck = 0;

		// Token: 0x02000111 RID: 273
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040003C9 RID: 969
			public static TimerCallback <0>__OnValidationTick;
		}
	}
}
