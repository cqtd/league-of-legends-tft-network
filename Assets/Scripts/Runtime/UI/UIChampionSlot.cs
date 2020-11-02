using CQ.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CQ.LeagueOfLegends.TFT.Network.UI
{
	public class UIChampionSlot : UIElement<ChampionArchetype>
	{
		[SerializeField] TextMeshProUGUI championNameText = default;
		[SerializeField] TextMeshProUGUI championGoldText = default;
		
		[SerializeField] RectTransform traitsRoot = default;

		[SerializeField] Image thumbnail = default;

		public override void Set(ChampionArchetype archetype)
		{
			// 텍스트 데이터 업데이트
			championGoldText.text = archetype.championCost.ToString();
			championNameText.text = archetype.championName.ToString();
			
			// 시너지 디스플레이 업데이트
			var traiSlot = Resources.Load<UITraitSlot>("UI/TraitSlotUI");

			foreach (Traits trait in archetype.originTraits)
			{
				var instance = Instantiate(traiSlot, traitsRoot);
				instance.Set(trait);
			}

			foreach (Traits trait in archetype.classTraits)
			{
				var instance = Instantiate(traiSlot, traitsRoot);
				instance.Set(trait);
			}
			
			// 썸네일
			thumbnail.sprite = archetype.sprite;
		}

		public void SetDelegate(UnityAction action)
		{
			
		}
	}
}