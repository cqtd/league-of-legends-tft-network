using UnityEngine;

namespace CQ.LeagueOfLegends.TFT.Network
{
	[CreateAssetMenu(menuName = "Archetype/Color Archetype", order = 200, fileName = "Color Archetype")]
	public class ColorArchetype : Archetype
	{
		public Color cost1;
		public Color cost2;
		public Color cost3;
		public Color cost4;
		public Color cost5;
	}
}