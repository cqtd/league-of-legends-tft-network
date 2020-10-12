using System.Collections.Generic;
using UnityEngine;

namespace CQ.LeagueOfLegends.TFT.Network
{
	public abstract class Traits : ScriptableObject
	{
		public string traitsName = "new traits";
		public List<SubTraits> subTraits = new List<SubTraits>();
	}
}