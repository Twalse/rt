using System;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using RustTweaker;

namespace WpfApp1.Model
{
	// Token: 0x02000068 RID: 104
	internal class SecureHttp
	{
		// Token: 0x060003A5 RID: 933 RVA: 0x001537F8 File Offset: 0x00150FF8
		public SecureHttp()
		{
			P4258EBF.AFA7138A.M6233B19[130](this);
			this._cookies = P4258EBF.AFA7138A.M6233B19[497]();
			HttpClientHandler httpClientHandler = P4258EBF.AFA7138A.M6233B19[16]();
			KC8F972B.C4B15297(httpClientHandler, new Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, bool>(this.ValidateCertificate));
			G5AF2408.F48D97BD(httpClientHandler, true);
			GAAABBB0.HE14843D(httpClientHandler, this._cookies);
			HttpClientHandler httpClientHandler2 = httpClientHandler;
			string apiUrlRelease = SecureStrings.ApiUrlRelease;
			HttpClient httpClient = P4258EBF.AFA7138A.M6233B19[140](httpClientHandler2);
			I600C799.O593741D(httpClient, P4258EBF.AFA7138A.M6233B19[105](apiUrlRelease));
			this._httpClient = httpClient;
			string hardwareId = HardwareId.GetHardwareId();
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			P4258EBF.AFA7138A.M6233B19[466](ref defaultInterpolatedStringHandler, 48, 2);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "SecureHttp raw HWID before encryption: '");
			P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler, hardwareId);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "' (len=");
			defaultInterpolatedStringHandler.AppendFormatted<int>(P4258EBF.AFA7138A.M6233B19[152](hardwareId));
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, ")");
			Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler));
			string text = EncryptionService.EncryptToHex(hardwareId);
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2;
			P4258EBF.AFA7138A.M6233B19[466](ref defaultInterpolatedStringHandler2, 45, 2);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler2, "SecureHttp encrypted HWID (x-hwid): '");
			P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler2, text);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler2, "' (len=");
			defaultInterpolatedStringHandler2.AppendFormatted<int>(P4258EBF.AFA7138A.M6233B19[152](text));
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler2, ")");
			Logger.Log(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler2));
			string token = Auth.getToken();
			P4258EBF.AFA7138A.M6233B19[307](P4258EBF.AFA7138A.M6233B19[313](this._httpClient), "x-hwid", text);
			P4258EBF.AFA7138A.M6233B19[307](P4258EBF.AFA7138A.M6233B19[313](this._httpClient), "x-auth-token", token);
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00151F0C File Offset: 0x0014F70C
		private bool ValidateCertificate(HttpRequestMessage request, X509Certificate2 certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			return (bool)new H52867B7().O0290C29(new object[] { this, request, certificate, chain, sslPolicyErrors }, 32312);
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00015E64 File Offset: 0x00014264
		public HttpClient GetClient()
		{
			return this._httpClient;
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00154BAC File Offset: 0x001523AC
		public void SetCookie(string name, string value, string domain = null, string path = "/")
		{
			if (P4258EBF.AFA7138A.M6233B19[426](name))
			{
				throw P4258EBF.AFA7138A.M6233B19[263]("name");
			}
			Uri uri = P4258EBF.AFA7138A.M6233B19[486](this._httpClient);
			string text = ((uri != null) ? C4AAF91B.K19DEF20(uri) : null);
			if (text == null)
			{
				throw P4258EBF.AFA7138A.M6233B19[115]("BaseAddress or domain required.");
			}
			domain = text;
			Cookie cookie = M7A18038.KFBB7036(name, value ?? P4258EBF.AFA7138A.M6233B19[280](), path, domain);
			object cookieLock = this._cookieLock;
			bool flag = false;
			try
			{
				P4258EBF.AFA7138A.M6233B19[520](cookieLock, ref flag);
				Uri uri2 = P4258EBF.AFA7138A.M6233B19[105](P4258EBF.AFA7138A.M6233B19[489](P4258EBF.AFA7138A.M6233B19[486](this._httpClient), UriPartial.Authority));
				P4258EBF.AFA7138A.M6233B19[382](this._cookies, uri2, cookie);
			}
			finally
			{
				if (flag)
				{
					P4258EBF.AFA7138A.M6233B19[631](cookieLock);
				}
			}
		}

		// Token: 0x04000109 RID: 265
		private static readonly string[] PinnedPublicKeyHashes = new string[] { "7pUu03LOyuYJm6vONAdTv+ng07FtuB1qwILuMX1sn3A=", "YQ7UKZSNZQIaCOa7g3igK2kM25GwpMClkkMaznW6gQo=" };

		// Token: 0x0400010A RID: 266
		private readonly object _cookieLock = P4258EBF.AFA7138A.M6233B19[131]();

		// Token: 0x0400010B RID: 267
		private readonly CookieContainer _cookies;

		// Token: 0x0400010C RID: 268
		private readonly HttpClient _httpClient;
	}
}
