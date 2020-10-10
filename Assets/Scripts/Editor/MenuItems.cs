using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace CQ.LeagueOfLegends.TFT.Network.Editor
{
	public static class MenuItems
	{
		[MenuItem("TFT/Kill All Processes", false, 501)]
		static void KillAllProcess()
		{
			var standalone = GetStandAloneFilePath();
			if (standalone == null) return;
			
			Debug.Log(PlayerSettings.productName);
			
			Process[] processes = Process.GetProcesses();
			foreach (Process process in processes)
			{
				if (process.ProcessName == PlayerSettings.productName)
				{
					process.Kill();
				}
				//
				// if (process == null) continue;
				// if (process.MainModule == null) continue;
				// if (process.MainModule.FileName == standalone.FullName)
				// {
				// 	process.Kill();
				// }
			}
		}
		
		[MenuItem("TFT/Run StandAlone 1", false, 201)]
		static void RunStandAlone1()
		{
			for (int i = 0; i < 1; i++)
			{
				RunStandalone();				
			}
		}
		
		[MenuItem("TFT/Run StandAlone 2", false, 202)]
		static void RunStandAlone2()
		{
			for (int i = 0; i < 2; i++)
			{
				RunStandalone();				
			}
		}
		
		[MenuItem("TFT/Run StandAlone 3", false, 203)]
		static void RunStandAlone3()
		{
			for (int i = 0; i < 3; i++)
			{
				RunStandalone();				
			}
		}
		
		[MenuItem("TFT/Run StandAlone 4", false, 204)]
		static void RunStandAlone4()
		{
			for (int i = 0; i < 4; i++)
			{
				RunStandalone();				
			}
		}
		
		[MenuItem("TFT/Run StandAlone 5", false, 205)]
		static void RunStandAlone5()
		{
			for (int i = 0; i < 5; i++)
			{
				RunStandalone();				
			}
		}
		
		[MenuItem("TFT/Run StandAlone 6", false, 206)]
		static void RunStandAlone6()
		{
			for (int i = 0; i < 6; i++)
			{
				RunStandalone();				
			}
		}
		
		[MenuItem("TFT/Run StandAlone 7", false, 207)]
		static void RunStandAlone7()
		{
			for (int i = 0; i < 7; i++)
			{
				RunStandalone();				
			}
		}
		
		[MenuItem("TFT/Run StandAlone 8", false, 208)]
		static void RunStandAlone8()
		{
			for (int i = 0; i < 8; i++)
			{
				RunStandalone();				
			}
		}
		
		static void RunStandalone()
		{
			FileInfo file = GetStandAloneFilePath();
			if (file == null)
			{
				return;
			}

			Process process = new Process {StartInfo = new ProcessStartInfo {FileName = file.FullName}};
			process.Start();
		}

		static FileInfo GetStandAloneFilePath()
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(Application.dataPath.Replace("Assets", "Build"));

			if (!directoryInfo.Exists)
			{
				Debug.LogError("스탠드얼론 빌드가 없음");
				return null;
			}

			foreach (FileInfo file in directoryInfo.GetFiles())
			{
				if (file.Extension == ".exe")
				{
					return file;
				}
			}

			return null;
		}
	}
}