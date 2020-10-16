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
		}

		void OnReRoll()
		{

			
			Refresh();
		}

		void OnLevelUp()
		{
			
			Refresh();
		}

		void OnLockToggle(bool isOn)
		{
			
			Refresh();
		}

		void Refresh()
		{
			levelText.text = $"{Random.Range(2, 7)} 레벨";
			
			int max = Random.Range(12, 24);
			int cur = Random.Range(2, max - 2);
			expText.text = $"{cur}/{max}";
			goldText.text = $"{Random.Range(0, 48)} 골드";

			expSlider.value = (float)cur / max;
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