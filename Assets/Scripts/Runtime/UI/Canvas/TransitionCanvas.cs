using CQ.UI;
using UnityEngine;

namespace CQ.LeagueOfLegends.TFT.Network.UI
{
	public class TransitionCanvas : UICanvas
	{
		[SerializeField] RadialTransition radialTransition = default;
		
		protected override void InitComponent()
		{
			radialTransition.SetFocus((int) (Screen.width * 0.5f), (int) (Screen.height * 0.5f));
			radialTransition.SetColor(Color.gray);
		}

		public override void Dispose()
		{
			
		}
	}
}