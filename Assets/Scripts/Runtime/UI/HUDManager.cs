using UnityEngine.SceneManagement;

namespace CQ.LeagueOfLegends.TFT.Network.UI
{
	public sealed class HUDManager : SingletonMono<HUDManager>
	{
		public static void Load()
		{
			SceneManager.LoadScene("HUD", LoadSceneMode.Additive);
			SetInstance(FindObjectOfType<HUDManager>());
		}
	}
}