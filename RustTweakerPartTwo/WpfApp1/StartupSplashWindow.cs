using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfApp1
{
	// Token: 0x02000053 RID: 83
	internal sealed class StartupSplashWindow : Window
	{
		// Token: 0x060002C2 RID: 706 RVA: 0x0014E248 File Offset: 0x0014BA48
		public StartupSplashWindow()
		{
			P4258EBF.AFA7138A.M6233B19[227](this);
			this.splashBitmap = StartupSplashWindow.LoadSplashBitmap();
			P4258EBF.AFA7138A.M6233B19[162](this, "RustTweaker");
			P4258EBF.AFA7138A.M6233B19[266](this, (double)P4258EBF.AFA7138A.M6233B19[338](this.splashBitmap));
			P4258EBF.AFA7138A.M6233B19[404](this, (double)P4258EBF.AFA7138A.M6233B19[230](this.splashBitmap));
			P4258EBF.AFA7138A.M6233B19[18](this, ResizeMode.NoResize);
			P4258EBF.AFA7138A.M6233B19[223](this, WindowStyle.None);
			P4258EBF.AFA7138A.M6233B19[571](this, false);
			P4258EBF.AFA7138A.M6233B19[333](this, P4258EBF.AFA7138A.M6233B19[482](P4258EBF.AFA7138A.M6233B19[590](9, 9, 13)));
			P4258EBF.AFA7138A.M6233B19[539](this, false);
			P4258EBF.AFA7138A.M6233B19[469](this, true);
			P4258EBF.AFA7138A.M6233B19[566](this, WindowStartupLocation.CenterScreen);
			P4258EBF.AFA7138A.M6233B19[255](this, SizeToContent.Manual);
			Grid grid = P4258EBF.AFA7138A.M6233B19[438]();
			H796B437.K0281AAB(grid, P4258EBF.AFA7138A.M6233B19[556](this));
			object obj = A33CBF35.K8B2728D(grid);
			Image image = P4258EBF.AFA7138A.M6233B19[346]();
			LD10EBAA.K898862B(image, this.splashBitmap);
			F99A4985.M50C3F0E(image, Stretch.Uniform);
			J8B4108C.N004D715(image, HorizontalAlignment.Center);
			LF3F8C03.O6A5B3A6(image, VerticalAlignment.Center);
			P0886190.G5323CA3(image, true);
			K42D03B0.J20BC508(obj, image);
			B30B991A.LEAB0716(this, grid);
			P4258EBF.AFA7138A.M6233B19[283](this, P4258EBF.AFA7138A.M6233B19[529](this, ldftn(<.ctor>b__2_0)));
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x001619F0 File Offset: 0x0015F1F0
		private static BitmapSource LoadSplashBitmap()
		{
			BitmapImage bitmapImage = P4258EBF.AFA7138A.M6233B19[563]();
			P4258EBF.AFA7138A.M6233B19[335](bitmapImage);
			P4258EBF.AFA7138A.M6233B19[430](bitmapImage, P4258EBF.AFA7138A.M6233B19[580]("pack://application:,,,/Assets/splash_v2.png", UriKind.Absolute));
			P4258EBF.AFA7138A.M6233B19[26](bitmapImage, BitmapCacheOption.OnLoad);
			P4258EBF.AFA7138A.M6233B19[310](bitmapImage);
			P4258EBF.AFA7138A.M6233B19[150](bitmapImage);
			return bitmapImage;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x001543C4 File Offset: 0x00151BC4
		private void ApplySplashShape()
		{
			IntPtr intPtr = P4258EBF.AFA7138A.M6233B19[25](P4258EBF.AFA7138A.M6233B19[522](this));
			if (intPtr == P4258EBF.AFA7138A.M6233B19[500]())
			{
				return;
			}
			IntPtr intPtr2 = StartupSplashWindow.CreateAlphaRegion(this.splashBitmap, 8);
			if (intPtr2 == P4258EBF.AFA7138A.M6233B19[500]())
			{
				return;
			}
			if (StartupSplashWindow.SetWindowRgn(intPtr, intPtr2, true) == 0)
			{
				StartupSplashWindow.DeleteObject(intPtr2);
			}
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00156D84 File Offset: 0x00154584
		private static IntPtr CreateAlphaRegion(BitmapSource source, byte alphaThreshold)
		{
			BitmapSource bitmapSource = (P4258EBF.AFA7138A.M6233B19[69](P4258EBF.AFA7138A.M6233B19[290](source), P4258EBF.AFA7138A.M6233B19[61]()) ? source : P4258EBF.AFA7138A.M6233B19[248](source, P4258EBF.AFA7138A.M6233B19[61](), null, 0.0));
			int num = P4258EBF.AFA7138A.M6233B19[338](bitmapSource);
			int num2 = P4258EBF.AFA7138A.M6233B19[230](bitmapSource);
			int num3 = num * 4;
			byte[] array = new byte[num3 * num2];
			P4258EBF.AFA7138A.M6233B19[66](bitmapSource, array, num3, 0);
			IntPtr intPtr = StartupSplashWindow.CreateRectRgn(0, 0, 0, 0);
			if (intPtr == P4258EBF.AFA7138A.M6233B19[500]())
			{
				return P4258EBF.AFA7138A.M6233B19[500]();
			}
			for (int i = 0; i < num2; i++)
			{
				int j = 0;
				while (j < num)
				{
					while (j < num && array[i * num3 + j * 4 + 3] <= alphaThreshold)
					{
						j++;
					}
					int num4 = j;
					while (j < num && array[i * num3 + j * 4 + 3] > alphaThreshold)
					{
						j++;
					}
					if (num4 != j)
					{
						IntPtr intPtr2 = StartupSplashWindow.CreateRectRgn(num4, i, j, i + 1);
						if (intPtr2 == P4258EBF.AFA7138A.M6233B19[500]())
						{
							StartupSplashWindow.DeleteObject(intPtr);
							return P4258EBF.AFA7138A.M6233B19[500]();
						}
						StartupSplashWindow.CombineRgn(intPtr, intPtr, intPtr2, 2);
						StartupSplashWindow.DeleteObject(intPtr2);
					}
				}
			}
			return intPtr;
		}

		// Token: 0x060002C6 RID: 710
		[DllImport("gdi32.dll")]
		private static extern IntPtr CreateRectRgn(int left, int top, int right, int bottom);

		// Token: 0x060002C7 RID: 711
		[DllImport("gdi32.dll")]
		private static extern int CombineRgn(IntPtr destination, IntPtr source1, IntPtr source2, int combineMode);

		// Token: 0x060002C8 RID: 712
		[DllImport("user32.dll")]
		private static extern int SetWindowRgn(IntPtr hwnd, IntPtr region, bool redraw);

		// Token: 0x060002C9 RID: 713
		[DllImport("gdi32.dll")]
		private static extern bool DeleteObject(IntPtr handle);

		// Token: 0x040000DF RID: 223
		private const int RgnOr = 2;

		// Token: 0x040000E0 RID: 224
		private readonly BitmapSource splashBitmap;
	}
}
