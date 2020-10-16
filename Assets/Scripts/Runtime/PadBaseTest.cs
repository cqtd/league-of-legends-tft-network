using UnityEngine;

namespace CQ.LeagueOfLegends.TFT.Network
{
	public class PadBaseTest : MonoBehaviour
	{
		public DummyUnit unit = default;

		void Start()
		{
			GetComponent<PadBase>().SetUnit(unit);
		}
	}
}