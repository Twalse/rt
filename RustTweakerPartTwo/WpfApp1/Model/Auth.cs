using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;

namespace WpfApp1.Model
{
	// Token: 0x0200005E RID: 94
	internal static class Auth
	{
		// Token: 0x06000352 RID: 850 RVA: 0x0015D588 File Offset: 0x0015AD88
		public static bool checkAuth()
		{
			return (bool)new H52867B7().E4B97194(null, 93842);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00011D70 File Offset: 0x00010170
		[return: TupleElementNames(new string[] { "Result", "StatusCode" })]
		public static async Task<ValueTuple<bool, int>> userAuth(JObject _payload)
		{
			try
			{
				Auth.AuthPayload authPayload = _payload.ToObject<Auth.AuthPayload>();
				ValueTuple<bool, int> valueTuple = await Auth.CheckCredentialsOnServer(authPayload.Email, authPayload.Password);
				ValueTuple<bool, int> valueTuple2 = valueTuple;
				if (valueTuple2.Item1)
				{
					return new ValueTuple<bool, int>(true, valueTuple2.Item2);
				}
				return new ValueTuple<bool, int>(false, valueTuple2.Item2);
			}
			catch
			{
			}
			return new ValueTuple<bool, int>(false, 0);
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00160590 File Offset: 0x0015DD90
		[return: TupleElementNames(new string[] { "Result", "StatusCode" })]
		private static Task<ValueTuple<bool, int>> CheckCredentialsOnServer(string email, string password)
		{
			return (Task<ValueTuple<bool, int>>)new H52867B7().PA803524(new object[] { email, password }, 81500);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0015CD94 File Offset: 0x0015A594
		public static void setToken(string newToken)
		{
			new H52867B7().PA803524(new object[] { newToken }, 79384);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00140378 File Offset: 0x0013DB78
		public static string getToken()
		{
			return (string)new H52867B7().N128E129(null, 58450);
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00153B00 File Offset: 0x00151300
		public static void removeToken()
		{
			using (RegistryKey registryKey = P4258EBF.AFA7138A.M6233B19[591](P4258EBF.AFA7138A.M6233B19[378](), "Software\\RustTweaker", true))
			{
				if (registryKey != null)
				{
					P4258EBF.AFA7138A.M6233B19[555](registryKey, "AuthToken", "", RegistryValueKind.String);
				}
			}
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00161930 File Offset: 0x0015F130
		public static bool checkAuthToken(string targetToken)
		{
			RegistryKey registryKey = P4258EBF.AFA7138A.M6233B19[591](P4258EBF.AFA7138A.M6233B19[378](), "Software\\RustTweaker", true);
			if (registryKey == null)
			{
				registryKey = P4258EBF.AFA7138A.M6233B19[591](P4258EBF.AFA7138A.M6233B19[378](), "Software\\RustTweaker", true);
			}
			object obj = P4258EBF.AFA7138A.M6233B19[451](registryKey, "AuthToken");
			return obj != null && P4258EBF.AFA7138A.M6233B19[250](targetToken, obj.ToString());
		}

		// Token: 0x0200010E RID: 270
		public class AuthPayload
		{
			// Token: 0x170000D4 RID: 212
			// (get) Token: 0x060005C7 RID: 1479 RVA: 0x0001F4BE File Offset: 0x0001D8BE
			// (set) Token: 0x060005C8 RID: 1480 RVA: 0x0001F4C6 File Offset: 0x0001D8C6
			public string Email { get; set; }

			// Token: 0x170000D5 RID: 213
			// (get) Token: 0x060005C9 RID: 1481 RVA: 0x0001F4CF File Offset: 0x0001D8CF
			// (set) Token: 0x060005CA RID: 1482 RVA: 0x0001F4D7 File Offset: 0x0001D8D7
			public string Password { get; set; }

			// Token: 0x060005CB RID: 1483 RVA: 0x00161500 File Offset: 0x0015ED00
			public AuthPayload()
			{
				P4258EBF.AFA7138A.M6233B19[130](this);
			}
		}
	}
}
