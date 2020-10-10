using System.Collections.Generic;
using UnityEngine;

namespace CQ.LeagueOfLegends.TFT.Network
{
	[CreateAssetMenu(menuName = "TFT/Champion", order = 202, fileName = "Champion")]
	public class Champion : ScriptableObject
	{
		public string championName = default;
		public int championCost = 1;
		
		public List<Traits> originTraits = new List<Traits>();
		public List<Traits> classTraits = new List<Traits>();
	}
}