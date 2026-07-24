using System;

namespace WpfApp1.Model
{
	// Token: 0x02000069 RID: 105
	internal static class SecureStrings
	{
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060003AA RID: 938 RVA: 0x00015F39 File Offset: 0x00014339
		// (set) Token: 0x060003AB RID: 939 RVA: 0x00015F40 File Offset: 0x00014340
		public static string ApiUrlDebug { get; private set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060003AC RID: 940 RVA: 0x00015F48 File Offset: 0x00014348
		// (set) Token: 0x060003AD RID: 941 RVA: 0x00015F4F File Offset: 0x0001434F
		public static string ApiUrlRelease { get; private set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060003AE RID: 942 RVA: 0x00015F57 File Offset: 0x00014357
		// (set) Token: 0x060003AF RID: 943 RVA: 0x00015F5E File Offset: 0x0001435E
		public static string AppDataFolder { get; private set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x00015F66 File Offset: 0x00014366
		// (set) Token: 0x060003B1 RID: 945 RVA: 0x00015F6D File Offset: 0x0001436D
		public static string ConfigFileName { get; private set; }

		// Token: 0x060003B2 RID: 946 RVA: 0x00140F70 File Offset: 0x0013E770
		static SecureStrings()
		{
			byte[] array = new byte[27];
			C413B6B5.G4AB6A1D(array, fieldof(<PrivateImplementationDetails>.CE04D9C464DC2ABB1C85613AEE6493746A5FB712AB3EA82793041FFF4F19AF97).FieldHandle);
			SecureStrings._data1 = array;
			byte[] array2 = new byte[28];
			C413B6B5.G4AB6A1D(array2, fieldof(<PrivateImplementationDetails>.5E41469FF8AECE9C030B03D05326AB363AEFA79445ED7CF5FCEDA8F94EFEFF28).FieldHandle);
			SecureStrings._data2 = array2;
			byte[] array3 = new byte[11];
			C413B6B5.G4AB6A1D(array3, fieldof(<PrivateImplementationDetails>.6813CDBC649E87FC39B49F6B7F5C57054D89573700D7A0657C03395A967C5FE7).FieldHandle);
			SecureStrings._data5 = array3;
			byte[] array4 = new byte[12];
			C413B6B5.G4AB6A1D(array4, fieldof(<PrivateImplementationDetails>.6423CA14B49B9D7FF934C8C66DC13A1B969DA1AD388CAF9B7F897A56817B0C6F).FieldHandle);
			SecureStrings._data6 = array4;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					DateTime dateTime = P4258EBF.AFA7138A.M6233B19[300]();
					byte b = (byte)(P4258EBF.AFA7138A.M6233B19[615](ref dateTime) % 256);
					num = ((b > 128) ? 1 : 2);
					break;
				}
				case 1:
					SecureStrings.ApiUrlDebug = SecureStrings.DecodeString(SecureStrings._data1, 0);
					num = 3;
					break;
				case 2:
					SecureStrings.ApiUrlDebug = SecureStrings.DecodeString(SecureStrings._data1, 0);
					num = 3;
					break;
				case 3:
					if (SecureStrings.ApiUrlDebug != null)
					{
						num = 4;
					}
					else
					{
						num = 1;
					}
					break;
				case 4:
					SecureStrings.ApiUrlRelease = SecureStrings.DecodeString(SecureStrings._data2, 0);
					num = ((P4258EBF.AFA7138A.M6233B19[152](SecureStrings.ApiUrlRelease) > 10) ? 5 : 4);
					break;
				case 5:
					num = 8;
					break;
				case 6:
					num = 8;
					break;
				case 7:
					num = 8;
					break;
				case 8:
					SecureStrings.AppDataFolder = SecureStrings.DecodeString(SecureStrings._data5, 0);
					num = ((SecureStrings.AppDataFolder != null) ? 9 : 8);
					break;
				case 9:
					goto IL_0193;
				default:
					num = 0;
					break;
				}
			}
			IL_0193:
			SecureStrings.ConfigFileName = SecureStrings.DecodeString(SecureStrings._data6, 0);
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0014EEFC File Offset: 0x0014C6FC
		private static string DecodeString(byte[] data, byte key)
		{
			return (string)new H52867B7().E4B97194(new object[] { data, key }, 186344);
		}

		// Token: 0x04000111 RID: 273
		private static readonly byte[] _data1;

		// Token: 0x04000112 RID: 274
		private static readonly byte[] _data2;

		// Token: 0x04000113 RID: 275
		private static readonly byte[] _data5;

		// Token: 0x04000114 RID: 276
		private static readonly byte[] _data6;
	}
}
