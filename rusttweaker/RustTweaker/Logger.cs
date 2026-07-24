using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace RustTweaker
{
	// Token: 0x02000018 RID: 24
	public static class Logger
	{
		// Token: 0x060000D9 RID: 217 RVA: 0x00157074 File Offset: 0x00154874
		private static string GetFullExceptionMessage(Exception ex)
		{
			StringBuilder stringBuilder = P4258EBF.AFA7138A.M6233B19[353]();
			P4258EBF.AFA7138A.M6233B19[5](stringBuilder, ex.ToString());
			for (Exception ex2 = P4258EBF.AFA7138A.M6233B19[70](ex); ex2 != null; ex2 = P4258EBF.AFA7138A.M6233B19[70](ex2))
			{
				P4258EBF.AFA7138A.M6233B19[5](stringBuilder, ex2.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00153664 File Offset: 0x00150E64
		public static string StringToHex(string input)
		{
			StringBuilder stringBuilder = P4258EBF.AFA7138A.M6233B19[353]();
			foreach (byte b in P4258EBF.AFA7138A.M6233B19[240](P4258EBF.AFA7138A.M6233B19[204](), input))
			{
				P4258EBF.AFA7138A.M6233B19[468](stringBuilder, P4258EBF.AFA7138A.M6233B19[41](ref b, "X2"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000DB RID: 219 RVA: 0x001612D4 File Offset: 0x0015EAD4
		public static void Start()
		{
			P4258EBF.AFA7138A.M6233B19[111](Logger._logDir);
			Logger._writer = P4258EBF.AFA7138A.M6233B19[200](Logger._filePath);
			P4258EBF.AFA7138A.M6233B19[144](Logger._writer, true);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0015F654 File Offset: 0x0015CE54
		public static void Stop()
		{
			if (Logger._writer != null)
			{
				P4258EBF.AFA7138A.M6233B19[607](Logger._writer);
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x001477A0 File Offset: 0x00144FA0
		public static void Log(string msg)
		{
			if (Logger._writer == null)
			{
				Logger.Start();
			}
			O4AE0E90 o4AE0E = P4258EBF.AFA7138A.M6233B19[279];
			object writer = Logger._writer;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			P4258EBF.AFA7138A.M6233B19[466](ref defaultInterpolatedStringHandler, 4, 2);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "[");
			A4A931B4 a4A931B = P4258EBF.AFA7138A.M6233B19[318];
			DateTime dateTime = P4258EBF.AFA7138A.M6233B19[300]();
			a4A931B(ref defaultInterpolatedStringHandler, P4258EBF.AFA7138A.M6233B19[362](ref dateTime, "HH:mm:ss"));
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "] ");
			P4258EBF.AFA7138A.M6233B19[318](ref defaultInterpolatedStringHandler, msg);
			P4258EBF.AFA7138A.M6233B19[423](ref defaultInterpolatedStringHandler, "\n");
			o4AE0E(writer, Logger.StringToHex(P4258EBF.AFA7138A.M6233B19[360](ref defaultInterpolatedStringHandler)));
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000059A1 File Offset: 0x00003DA1
		public static void Log(Exception ex)
		{
			Logger.Log(Logger.GetFullExceptionMessage(ex));
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000059AE File Offset: 0x00003DAE
		public static void Log(object obj)
		{
			Logger.Log(obj.ToString());
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00160E78 File Offset: 0x0015E678
		// Note: this type is marked as 'beforefieldinit'.
		static Logger()
		{
			DateTime dateTime = P4258EBF.AFA7138A.M6233B19[300]();
			Logger._fileName = P4258EBF.AFA7138A.M6233B19[478](P4258EBF.AFA7138A.M6233B19[362](ref dateTime, "yyyy-MM-dd_HH-mm-ss"), ".log");
			Logger._filePath = P4258EBF.AFA7138A.M6233B19[158](Logger._logDir, Logger._fileName);
		}

		// Token: 0x0400004E RID: 78
		private static string _logDir = P4258EBF.AFA7138A.M6233B19[158](P4258EBF.AFA7138A.M6233B19[109](P4258EBF.AFA7138A.M6233B19[63]()), "logs");

		// Token: 0x0400004F RID: 79
		private static string _fileName;

		// Token: 0x04000050 RID: 80
		private static string _filePath;

		// Token: 0x04000051 RID: 81
		private static StreamWriter _writer;
	}
}
