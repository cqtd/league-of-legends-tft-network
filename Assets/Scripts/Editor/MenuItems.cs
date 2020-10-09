using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace CQ.LeagueOfLegends.TFT.Network.Editor
{
	public static class MenuItems
	{
		[MenuItem("TFT/Kill All Processes", false, 201)]
		static void KillAllProcess()
		{
			var standalone = GetStandAloneFilePath();
			if (standalone == null) return;
			
			Process[] processes = Process.GetProcesses();
			foreach (Process process in processes)
			{
				if (process == null) continue;
				if (process.MainModule == null) continue;
				if (process.MainModule.FileName == standalone.FullName)
				{
					process.Kill();
				}
			}
		}
		
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