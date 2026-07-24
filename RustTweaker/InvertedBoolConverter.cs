using System;
using System.Globalization;
using System.Windows.Data;

namespace RustTweakerDemo
{
	// Token: 0x0200000E RID: 14
	public class InvertedBoolConverter : IValueConverter
	{
		// Token: 0x06000046 RID: 70 RVA: 0x00002DBC File Offset: 0x000011BC
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is bool)
			{
				bool flag = (bool)value;
				return !flag;
			}
			return false;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002DE8 File Offset: 0x000011E8
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is bool)
			{
				bool flag = (bool)value;
				return !flag;
			}
			return false;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0015AF28 File Offset: 0x00158728
		public InvertedBoolConverter()
		{
			P4258EBF.AFA7138A.M6233B19[130](this);
		}
	}
}
