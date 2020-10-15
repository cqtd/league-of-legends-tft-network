using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Bolt;
using CQ.LeagueOfLegends.TFT.Network.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CQ.LeagueOfLegends.TFT.Network
{
	public class TeamFightTactis : GlobalEventListener
	{
		[Header("Path")]
		[SerializeField] GameObject[] championSpawnPoints = new GameObject[0];
		[SerializeField] GameObject[] legendSpawnPoints = new GameObject[0];
		
		[Header("UI")]
		[SerializeField] Button roundStartButton = default;
		
		[NonSerialized] readonly List<string> logMessages = new List<string>();
		
		[NonSerialized] readonly Dictionary<GameObject, FrozenChampBehaviour> figures = new Dictionary<GameObject, FrozenChampBehaviour>();
		[NonSerialized] readonly Dictionary<GameObject, LittleLegendBehaviour> legends = new Dictionary<GameObject, LittleLegendBehaviour>();
		
		void Awake()
		{
			roundStartButton.interactable = false;

			
			roundStartButton.GetComponentInChildren<TextMeshProUGUI>().text = "응답 대기중";
		}

		void BeginRound()
		{
			roundStartButton.gameObject.SetActive(false);

			StartCoroutine(SpawnSushiOnServer());
		}

		public override void SceneLoadLocalDone(string scene, IProtocolToken token)
		{
			base.SceneLoadLocalDone(scene, token);

			// 에디터에서는 서버로 클라이언트 로직 일부 실행
			if (GlobalSetting.IsEditor)
			{
				SceneLoadLocalDoneServer(scene, token);
			}
			else
			{
				if (BoltNetwork.IsServer)
				{
					SceneLoadLocalDoneServer(scene, token);
				}
			
				else if (BoltNetwork.IsClient)
				{
					SceneLoadLocalDoneClient(scene, token);
				}	
			}
			
			HUDManager.Load();
		}

		void SceneLoadLocalDoneServer(string scene, IProtocolToken token)
		{
			roundStartButton.onClick.AddListener(BeginRound);
			roundStartButton.interactable = true;

			roundStartButton.GetComponentInChildren<TextMeshProUGUI>().text = "라운드 시작";
		}
		void SceneLoadLocalDoneClient(string scene, IProtocolToken token)
		{
			roundStartButton.gameObject.SetActive(false);
		}

		IEnumerator SpawnSushiOnServer(float delay = 0.8f)
		{
			// 꼬마 전설이 스폰
			BoltConnection[] connections = BoltNetwork.Connections.ToArray();

			for (int i = 0; i < connections.Length; i++)
			{
				yield return new WaitForSeconds(delay);
				
				BoltConnection connection = connections[i];

				Vector3 position = legendSpawnPoints[i].transform.position;
				Quaternion rotation = legendSpawnPoints[i].transform.rotation * Quaternion.Euler(0, 180, 0);
				
				BoltEntity instance = BoltNetwork.Instantiate(BoltPrefabs.LittleLegend, position, rotation);
				LittleLegendBehaviour littleLegend = instance.GetComponent<LittleLegendBehaviour>();
				
				instance.AssignControl(connection);
				legends[legendSpawnPoints[i]] = littleLegend;
			}
			
			// 초밥 스폰
			foreach (GameObject championSpawnPoint in championSpawnPoints)
			{
				yield return new WaitForSeconds(delay);

				BoltEntity instance = BoltNetwork.Instantiate(BoltPrefabs.Frozen_Champion);
				FrozenChampBehaviour fc = instance.GetComponent<FrozenChampBehaviour>();
				
				fc.SetChampion(ChampionPool.PickSushiRandom(EMatchTheme.None));
				
				instance.transform.SetParent(championSpawnPoint.transform);
				
				instance.transform.localPosition = Vector3.zero;
				instance.transform.localRotation = Quaternion.identity;
				instance.transform.localScale = Vector3.one;

				figures[championSpawnPoint] = fc;
			}
			
			yield return new WaitForSeconds(3.0f);
			yield return ReleaseLittleLegends();
		}

		IEnumerator ReleaseLittleLegends()
		{
			yield return null;

			var legends = FindObjectsOfType<LittleLegendBehaviour>();
			foreach (LittleLegendBehaviour legend in legends)
			{
				legend.state.CanMove = true;
				yield return new WaitForSeconds(0.5f);
			}
		}
		
		public override void OnEvent(LogEvent evnt)
		{
			base.OnEvent(evnt);

			logMessages.Insert(0, evnt.Message);
		}
	}
}