using UnityEngine;
using UnityEngine.SceneManagement;

namespace CQ.LeagueOfLegends.TFT.Network.UI
{
	public sealed class HUDCanvas : UICanvas
	{
		[Header("HUD")]
		[SerializeField] UIControlPanel panel = default;

		protected override void InitComponent()
		{
			
		}

		public override void Initialize()
		{
			base.Initialize();

			panel.Repaint();
			
		}

		public override void Dispose()
		{
			
		}
	}
}