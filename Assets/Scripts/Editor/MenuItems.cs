using System.IO;
using UnityEditor;
using UnityEngine;

namespace CQ.LeagueOfLegends.TFT.Network.Editor
{
	public static class MenuItems
	{
		[MenuItem("TFT/Run StandAlone 1", false, 101)]
		static void RunStandAlone1()
		{
			RunStandalone();
		}
		
		[MenuItem("TFT/Run StandAlone 2", false, 102)]
		static void RunStandAlone2()
		{
			RunStandalone();
			RunStandalone();
		}
		
		[MenuItem("TFT/Run StandAlone 3", false, 103)]
		static void RunStandAlone3()
		{
			RunStandalone();
			RunStandalone();
			RunStandalone();
		}
		
		[MenuItem("TFT/Run StandAlone 4", false, 104)]
		static void RunStandAlone4()
		{
			RunStandalone();
			RunStandalone();
			RunStandalone();
			RunStandalone();
		}
		
		static void RunStandalone()
		{
			Debug.Log(Application.dataPath);
			DirectoryInfo directoryInfo = new DirectoryInfo(Application.dataPath.Replace("Assets", "Build"));

			if (!directoryInfo.Exists)
			{
				Debug.LogError("스탠드얼론 빌드가 없음");
				return;
			}

			foreach (FileInfo file in directoryInfo.GetFiles())
			{
				if (file.Extension == ".exe")
				{

					System.Diagnostics.Process process = new System.Diagnostics.Process();
					System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
					startInfo.FileName = file.FullName;
					process.StartInfo = startInfo;
					process.Start();
					
					return;
				}
			}
			
			Debug.LogError("빌드된 exe 찾을 수 없음");
		}
	}
}