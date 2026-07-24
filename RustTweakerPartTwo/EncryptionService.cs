using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

// Token: 0x0200000D RID: 13
public static class EncryptionService
{
	// Token: 0x06000044 RID: 68 RVA: 0x00142D20 File Offset: 0x00140520
	public static string EncryptToHex(string plainText)
	{
		string text;
		using (Aes aes = P4258EBF.AFA7138A.M6233B19[33]())
		{
			P4258EBF.AFA7138A.M6233B19[595](aes, EncryptionService.Key);
			P4258EBF.AFA7138A.M6233B19[392](aes, EncryptionService.IV);
			ICryptoTransform cryptoTransform = P4258EBF.AFA7138A.M6233B19[17](aes, P4258EBF.AFA7138A.M6233B19[30](aes), P4258EBF.AFA7138A.M6233B19[588](aes));
			using (MemoryStream memoryStream = P4258EBF.AFA7138A.M6233B19[106]())
			{
				using (CryptoStream cryptoStream = P4258EBF.AFA7138A.M6233B19[276](memoryStream, cryptoTransform, CryptoStreamMode.Write))
				{
					using (StreamWriter streamWriter = P4258EBF.AFA7138A.M6233B19[424](cryptoStream))
					{
						P4258EBF.AFA7138A.M6233B19[279](streamWriter, plainText);
					}
				}
				byte[] array = P4258EBF.AFA7138A.M6233B19[254](memoryStream);
				StringBuilder stringBuilder = P4258EBF.AFA7138A.M6233B19[561](array.Length * 2);
				foreach (byte b in array)
				{
					P4258EBF.AFA7138A.M6233B19[159](stringBuilder, "{0:X2}", b);
				}
				text = stringBuilder.ToString();
			}
		}
		return text;
	}

	// Token: 0x04000017 RID: 23
	private static readonly byte[] Key = P4258EBF.AFA7138A.M6233B19[240](P4258EBF.AFA7138A.M6233B19[204](), "1234567890123456");

	// Token: 0x04000018 RID: 24
	private static readonly byte[] IV = P4258EBF.AFA7138A.M6233B19[240](P4258EBF.AFA7138A.M6233B19[204](), "1234567890123456");
}
