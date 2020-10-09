using System;
using UnityEngine;

namespace CQ.LeagueOfLegends.TFT.Network
{
	public sealed class LocalSetUp : MonoBehaviour
	{
		void Start()
		{
			Application.targetFrameRate = 60;
			
#if UNITY_STANDALONE_WIN
			Screen.SetResolution(1280, 720, FullScreenMode.Windowed);
#endif
			
		}
	}
}