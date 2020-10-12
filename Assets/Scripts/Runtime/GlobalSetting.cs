using UnityEngine;

namespace CQ.LeagueOfLegends.TFT.Network
{
	public static class GlobalSetting
	{
		/// <summary>
		/// Use IsEditor instead of #if UNITY_EDITOR because of compiling error from using namespaces.
		/// </summary>
		public static bool IsEditor {
			get
			{
#if UNITY_EDITOR
				if (PlayerPrefs.GetInt("Run.Editor.As.Standalone", 1) == 1)
				{
					PlayerPrefs.SetInt("Run.Editor.As.Standalone", 1);
					return false;
				}
				
				return true;
#else
				return false;
#endif
			}
		}
	}
}