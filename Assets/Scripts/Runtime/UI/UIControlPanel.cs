using CQ.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CQ.LeagueOfLegends.TFT.Network.UI
{
	public class UIControlPanel : UIElement
	{
		[SerializeField] Button reRollButton = default;
		[SerializeField] Button levelUpButton = default;
		[SerializeField] Toggle lockToggle = default;

		[SerializeField] TextMeshProUGUI levelText = default;
		[SerializeField] TextMeshProUGUI expText = default;
		[SerializeField] TextMeshProUGUI goldText = default;
		
		[SerializeField] Slider expSlider = default;

		[SerializeField] UIChampionSlot[] slots = new UIChampionSlot[0];

		void Awake()
		{
			reRollButton.onClick.AddListener(OnReRoll);
			levelUpButton.onClick.AddListener(OnLevelUp);
			lockToggle.onValueChanged.AddListener(OnLockToggle);

			for (int i = 0; i < 5; i++)
			{
				int localIndex = i;
				slots[i].SetDelegate(() => OnBuyChampion(localIndex));
			}
			
			Refresh();

			GameSession.Instance.onPropertyUpdate += Repaint;
		}

		void OnReRoll()
		{
			GameSession.Instance.DoBuyReroll();
			
			// Refresh();
		}

		void OnLevelUp()
		{
			GameSession.Instance.DoBuyExperience();
			
			// Refresh();
		}

		void OnLockToggle(bool isOn)
		{
			
			Refresh();
		}

		void Refresh()
		{
			levelText.text = $"{GameSession.Instance.level} 레벨";
			
			int max = GameSession.Instance.maxExp;
			int cur = GameSession.Instance.EXP;
			
			expText.text = $"{cur}/{max}";
			goldText.text = $"{GameSession.Instance.gold} 골드";

			expSlider.value = (float)cur / max;

			reRollButton.interactable = GameSession.Instance.CanBuyReroll();
			levelUpButton.interactable = GameSession.Instance.CanBuyExperience();
		}

		void OnBuyChampion(int index)
		{
			
		}

		public override void Repaint()
		{
			base.Repaint();
			
			Refresh();
		}
	}
}