using UnityEngine;

namespace CQ.LeagueOfLegends.TFT
{
	[RequireComponent(typeof(RadialTransition))]
	public class TransitionTester : MonoBehaviour
	{
		public float fadeIn = 2.0f;
		public float fadeOut = 2.0f;

		public bool syncPivot = false;
		[Range(0, 1920)] public int x = 0;
		[Range(0, 1080)] public int y = 0;

		RadialTransition radialTransition;

		void Awake()
		{
			radialTransition = GetComponent<RadialTransition>();
		}
		
		[ContextMenu("Fade Out")]
		void FadeOut()
		{
			radialTransition.FadeOut(fadeOut);
		}

		[ContextMenu("Fade In")]
		void FadeIn()
		{
			radialTransition.FadeIn(fadeIn);
		}

		void Update()
		{
			if (syncPivot)
				radialTransition.SetFocus(x, y);
		}
	}
}