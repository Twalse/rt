using System;
using System.Diagnostics;

namespace SteamGameSearcher
{
	// Token: 0x02000048 RID: 72
	public class FileSystemService : IFileSystemService
	{
		// Token: 0x0600028D RID: 653 RVA: 0x0014E228 File Offset: 0x0014BA28
		public bool FileExists(string path)
		{
			return P4258EBF.AFA7138A.M6233B19[627](path);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00156FF4 File Offset: 0x001547F4
		public bool DirectoryExists(string path)
		{
			return P4258EBF.AFA7138A.M6233B19[89](path);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00151870 File Offset: 0x0014F070
		public string ReadAllText(string path)
		{
			return P4258EBF.AFA7138A.M6233B19[267](path);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0015ECA8 File Offset: 0x0015C4A8
		public void OpenFolderInExplorer(string path)
		{
			if (!this.DirectoryExists(path))
			{
				throw P4258EBF.AFA7138A.M6233B19[449](P4258EBF.AFA7138A.M6233B19[478]("Директория не найдена: ", path));
			}
			ProcessStartInfo processStartInfo = P4258EBF.AFA7138A.M6233B19[394]();
			N62EB38A.CB1145A6(processStartInfo, "explorer.exe");
			AA2B3D09.ND86FA10(processStartInfo, P4258EBF.AFA7138A.M6233B19[64]("\"", path, "\""));
			O8258311.M5A8918D(processStartInfo, true);
			JC11021F.C827CF8C(processStartInfo);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0016099C File Offset: 0x0015E19C
		public FileSystemService()
		{
			P4258EBF.AFA7138A.M6233B19[130](this);
		}
	}
}
