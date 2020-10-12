using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

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
		
		public static ChampionArchetype PickSushiRandom(EMatchTheme theme)
		{
#if UNITY_EDITOR
			var archetypes = UnityEditor.AssetDatabase.FindAssets("t:ChampionArchetype")
				.Select(UnityEditor.AssetDatabase.GUIDToAssetPath)
				.Select(UnityEditor.AssetDatabase.LoadAssetAtPath<ChampionArchetype>).ToList();
			
			// @TODO : Implement random next logic
			return archetypes[Random.Range(0, archetypes.Count)];
#else
			return Resources.Load<ChampionArchetype>("Champion/3_Jinx");
#endif
		}
	}
}