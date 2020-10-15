using System.Collections.Generic;
using UnityEngine;

namespace CQ.LeagueOfLegends.TFT.Network
{
	[CreateAssetMenu(menuName = "TFT/Champion", order = 202, fileName = "Champion")]
	public class ChampionArchetype : Archetype
	{
		public string championName = default;
		public ECost championCost = ECost.Cost1;
		
		public List<Traits> originTraits = new List<Traits>();
		public List<Traits> classTraits = new List<Traits>();

		public GameObject figure = default;
		public Sprite sprite = default;
	}

	public enum ECost
	{
		Cost0,
		Cost1,
		Cost2,
		Cost3,
		Cost4,
		Cost5,
	}
}