using System.Collections.Generic;
using UnityEngine;

namespace CQ.LeagueOfLegends.TFT.Network
{
	[CreateAssetMenu(menuName = "TFT/Champion Set", order = 200, fileName = "Season - 2020-10-2")]
	public partial class ChampionSet : Archetype
	{
		public List<ChampionArchetype> availableChampions = new List<ChampionArchetype>();

	}
	
	
}