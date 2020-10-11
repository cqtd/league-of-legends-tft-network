using System;
using System.Collections.Generic;
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

		ChampionPool()
		{

		}

		public static ChampionArchetype PickSushi1(EMatchTheme theme)
		{
			// @TODO : Implement random next logic
			return Resources.Load<ChampionArchetype>("Champion/3_Jinx");
		}
	}
}