#if UNITY_EDITOR
using System.Linq;
using UnityEngine;
using UnityEditor;

using static UnityEditor.AssetDatabase;

namespace CQ.LeagueOfLegends.TFT.Network
{
	public partial class ChampionSet : ScriptableObject
	{
		[MenuItem("TFT/모든 챔피언 등록")]
		static void AddAllChampionEditor()
		{
			var champSet = FindAssets("t:ChampionSet")
				.Select(GUIDToAssetPath)
				.Select(LoadAssetAtPath<ChampionSet>).FirstOrDefault();
			
			champSet.availableChampions = FindAssets("t:Champion")
				.Select(GUIDToAssetPath)
				.Select(LoadAssetAtPath<Champion>).ToList();
			
			EditorUtility.SetDirty(champSet);
			SaveAssets();
		}
	}
}
#endif