using System;
using UnityEngine;

namespace CQ.LeagueOfLegends.TFT.Network
{
	public sealed class LocalSetUp : MonoBehaviour
	{
		[SerializeField] int frameRate = 60;
		[SerializeField] Vector2 resolution = new Vector2(1280, 720);
	
		void Start()
		{
			Application.targetFrameRate = frameRate;
			
#if UNITY_STANDALONE_WIN
			Screen.SetResolution((int)resolution.x, (int)resolution.y, FullScreenMode.Windowed);
#endif
			
		}
	}
}