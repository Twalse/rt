using System;
using System.Threading.Tasks;
using RustTweaker;

namespace RustTweaker_NET.Model.Http
{
	// Token: 0x02000017 RID: 23
	internal static class ResponseCache
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x000056DC File Offset: 0x00003ADC
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x000056E3 File Offset: 0x00003AE3
		public static bool Enabled { get; set; } = true;

		// Token: 0x060000D5 RID: 213 RVA: 0x00150CD0 File Offset: 0x0014E4D0
		public static bool TryRead(string url, TimeSpan ttl, out string responseBody)
		{
			responseBody = null;
			if (!ResponseCache.Enabled)
			{
				return false;
			}
			bool flag2;
			try
			{
				string cacheFilePath = ResponseCache.GetCacheFilePath(url);
				object cacheLock = ResponseCache.CacheLock;
				bool flag = false;
				try
				{
					P4258EBF.AFA7138A.M6233B19[520](cacheLock, ref flag);
					if (!P4258EBF.AFA7138A.M6233B19[627](cacheFilePath))
					{
						flag2 = false;
					}
					else
					{
						TimeSpan timeSpan = P4258EBF.AFA7138A.M6233B19[188](DateTime.UtcNow, P4258EBF.AFA7138A.M6233B19[363](cacheFilePath));
						if (P4258EBF.AFA7138A.M6233B19[218](timeSpan, ttl))
						{
							flag2 = false;
						}
						else
						{
							responseBody = P4258EBF.AFA7138A.M6233B19[77](cacheFilePath, P4258EBF.AFA7138A.M6233B19[204]());
							flag2 = !P4258EBF.AFA7138A.M6233B19[426](responseBody);
						}
					}
				}
				finally
				{
					if (flag)
					{
						P4258EBF.AFA7138A.M6233B19[631](cacheLock);
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00161C98 File Offset: 0x0015F498
		public static Task WriteAsync(string url, string responseBody)
		{
			ResponseCache.<WriteAsync>d__7 <WriteAsync>d__;
			<WriteAsync>d__.<>t__builder = P4258EBF.AFA7138A.M6233B19[20]();
			<WriteAsync>d__.url = url;
			<WriteAsync>d__.responseBody = responseBody;
			<WriteAsync>d__.<>1__state = -1;
			<WriteAsync>d__.<>t__builder.Start<ResponseCache.<WriteAsync>d__7>(ref <WriteAsync>d__);
			return P4258EBF.AFA7138A.M6233B19[19](ref <WriteAsync>d__.<>t__builder);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0015E21C File Offset: 0x0015BA1C
		private static string GetCacheFilePath(string url)
		{
			byte[] array = L52007B6.JDA3D9AA(P4258EBF.AFA7138A.M6233B19[204](), url ?? P4258EBF.AFA7138A.M6233B19[280]());
			byte[] array2 = P4258EBF.AFA7138A.M6233B19[256](array);
			string text = P4258EBF.AFA7138A.M6233B19[432](P4258EBF.AFA7138A.M6233B19[103](array2));
			return P4258EBF.AFA7138A.M6233B19[158](ResponseCache.CacheDirectory, P4258EBF.AFA7138A.M6233B19[478](text, ".json"));
		}

		// Token: 0x0400004B RID: 75
		private static readonly object CacheLock = P4258EBF.AFA7138A.M6233B19[131]();

		// Token: 0x0400004C RID: 76
		private static readonly string CacheDirectory = P4258EBF.AFA7138A.M6233B19[158](P4258EBF.AFA7138A.M6233B19[587](), "HttpCache");
	}
}
