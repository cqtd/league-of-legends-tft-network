using UnityEngine;

namespace CQ.LeagueOfLegends.TFT.Network
{
	public enum EMatchTheme
	{
		None,
	}
	
	public class ChampionPool
	{
		static ChampionPool instance = null;
		public static ChampionPool Instance {
			get
			{
				if (instance == null)
				{
					instance = new ChampionPool();
				}

				return instance;
			}
		}
		
		public ChampionArchetype PickSushi1(EMatchTheme theme)
		{
			// @TODO : Implement random next logic
			return Resources.Load<ChampionArchetype>("3_Jinx");
		}
	}
}