using UnityEngine;

namespace CQ.LeagueOfLegends.TFT.Network
{
	public class PadBaseTest : MonoBehaviour
	{
		public PadBase pad = default;

		void Start()
		{
			pad.SetUnit(GetComponent<DummyUnit>());
		}
	}
}