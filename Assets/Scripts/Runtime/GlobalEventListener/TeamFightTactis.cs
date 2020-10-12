using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Bolt;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CQ.LeagueOfLegends.TFT.Network
{
	public class TeamFightTactis : GlobalEventListener
	{
		[SerializeField] GameObject[] championSpawnPoints = new GameObject[0];
		[SerializeField] Button roundStartButton = default;
		
		[NonSerialized] readonly List<string> logMessages = new List<string>();
		
		void Awake()
		{
			roundStartButton.interactable = false;
			roundStartButton.onClick.AddListener(BeginRound);
			
			roundStartButton.GetComponentInChildren<TextMeshProUGUI>().text = "응답 대기중";
		}

		void BeginRound()
		{
			roundStartButton.gameObject.SetActive(false);

			StartCoroutine(SpawnSushiOnServer(championSpawnPoints));
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
		}

		void SceneLoadLocalDoneServer(string scene, IProtocolToken token)
		{
			roundStartButton.gameObject.SetActive(true);
			roundStartButton.interactable = true;

			roundStartButton.GetComponentInChildren<TextMeshProUGUI>().text = "라운드 시작";
		}
		void SceneLoadLocalDoneClient(string scene, IProtocolToken token)
		{
			roundStartButton.gameObject.SetActive(false);
		}

		IEnumerator SpawnSushiOnServer(GameObject[] points, float delay = 0.8f)
		{
			GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

			var connections = BoltNetwork.Connections.ToArray();

			for (int i = 0; i < connections.Length; i++)
			{
				BoltConnection connection = connections[i];

				Vector3 position = spawnPoints[i].transform.position;
				Quaternion rotation = spawnPoints[i].transform.rotation * Quaternion.Euler(0, 180, 0);
				
				BoltEntity instance = BoltNetwork.Instantiate(BoltPrefabs.LittleLegend, position, rotation);
				LittleLegendBehaviour littleLegend = instance.GetComponent<LittleLegendBehaviour>();
				
				instance.AssignControl(connection);
			}
			
			GameObject prefab = Resources.Load<GameObject>("Frozen Champion");

			foreach (GameObject championSpawnPoint in points)
			{
				yield return new WaitForSeconds(delay);

				BoltEntity instance = BoltNetwork.Instantiate(BoltPrefabs.Frozen_Champion);
				FrozenChampBehaviour fc = instance.GetComponent<FrozenChampBehaviour>();
				
				fc.SetChampion(ChampionPool.PickSushiRandom(EMatchTheme.None));
				
				instance.transform.SetParent(championSpawnPoint.transform);
				
				instance.transform.localPosition = Vector3.zero;
				instance.transform.localRotation = Quaternion.identity;
				instance.transform.localScale = Vector3.one;
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