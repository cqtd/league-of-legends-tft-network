using System;
using System.Collections;
using MEC;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CQ.LeagueOfLegends.TFT.Network
{
	public class GameSession : SingletonMono<GameSession>
	{
		public KeyCode rerollKey = KeyCode.D;
		public KeyCode levelUpKey = KeyCode.F;
		public KeyCode sellKey = KeyCode.E;
		public KeyCode placeKey = KeyCode.W;
		
		public int reRollSequence = 15;
		public int roundMainSequence = 30;
		public int roundExtraSequence = 10;
		public int waitForRoundEnd = 5;

		[NonSerialized] public int gold = 10;
		int exp = 0;
		
		public TextMeshProUGUI text;
		public TextMeshProUGUI time;

		public Action onPropertyUpdate;

		public int[] levelXP = new[] {2, 6, 10, 20, 36, 56, 80, -1};
		public int level = 1;
		int maxLevel = 9;
		public int EXP
		{
			get
			{
				return exp;
			}
		}

		public int maxExp {
			get
			{
				return levelXP[level-1];
			}
		}
		
		public void AddExp(int add)
		{
			int newExp = exp + add;
			if (newExp >= maxExp)
			{
				exp = newExp - maxExp;
				level += 1;
			}
			else
			{
				exp = newExp;
			}
		}

		public void AddGold(int value)
		{
			this.gold += value;
		}
		
		void Start()
		{
			StartCoroutine(Session());
		}

		IEnumerator Session()
		{
			while (true)
			{
				text.text = "리롤 시퀀스";
				for (int i = 0; i < reRollSequence; i++)
				{
					time.text = $"{reRollSequence - i}";
					yield return new WaitForSeconds(1);
				}
				
				text.text = "전투로 이동";
				for (int i = 0; i < waitForRoundEnd; i++)
				{
					time.text = $"{waitForRoundEnd - i}";
					yield return new WaitForSeconds(1);
				}

				text.text = "메인 전투";
				for (int i = 0; i < roundMainSequence; i++)
				{
					time.text = $"{roundMainSequence - i}";
					yield return new WaitForSeconds(1); 
				}

				if (!BattleManager.Instance.ExistWinner)
				{
					text.text = "보너스 타임";
					for (int i = 0; i < roundExtraSequence; i++)
					{
						time.text = $"{roundExtraSequence - i}";
						yield return new WaitForSeconds(1); 
					}
				}
				
				text.text = "체스판으로 이동";
				for (int i = 0; i < waitForRoundEnd; i++)
				{
					time.text = $"{waitForRoundEnd - i}";
					yield return new WaitForSeconds(1);
				}
				
				AddGold(Random.Range(13, 25));
				AddExp(2);
				
				onPropertyUpdate?.Invoke();
			}
		}

		public bool CanBuyReroll()
		{
			return gold >= 2;
		}

		public void DoBuyReroll()
		{
			gold -= 2;
			
			onPropertyUpdate?.Invoke();
		}

		public bool CanBuyExperience()
		{
			if (gold < 4)
			{
				return false;
			}

			if (level == maxLevel) return false;

			return true;
		}

		public void DoBuyExperience()
		{
			gold -= 4;
			AddExp(4);
			
			onPropertyUpdate?.Invoke();
		}


		public void Sell()
		{
			
		}

		public bool CanPlace()
		{
			return true;
		}
		
		public void DoPlace()
		{
			
		}

		void Update()
		{
			if (Input.GetKeyDown(rerollKey))
			{
				if (CanBuyReroll())
				{
					DoBuyReroll();
				}
			}
			
			if (Input.GetKeyDown(levelUpKey))
			{
				if (CanBuyExperience())
				{
					DoBuyExperience();
				}
			}
			
			if (Input.GetKeyDown(sellKey))
			{
				Sell();
			}
			
			if (Input.GetKeyDown(placeKey))
			{
				if (CanPlace())
				{
					DoPlace();
				}
			}
		}
	}
}