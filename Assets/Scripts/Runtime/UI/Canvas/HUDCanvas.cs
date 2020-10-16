using UnityEngine;
using UnityEngine.SceneManagement;

namespace CQ.LeagueOfLegends.TFT.Network.UI
{
	public sealed class HUDCanvas : UICanvas
	{
		[Header("HUD")]
		[SerializeField] UIControlPanel panel = default;

		public override void Initialize()
		{
			base.Initialize();

			panel.Repaint();
		}
	}
}