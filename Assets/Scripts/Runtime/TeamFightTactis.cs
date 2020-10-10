using System;
using System.Collections;
using System.Collections.Generic;
using Bolt;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CQ.LeagueOfLegends.TFT.Network
{
	public class TeamFightTactis : GlobalEventListener
	{
		readonly List<string> logMessages = new List<string>();
		
		GameObject[] cameraPoints = new GameObject[0];
		int cameraIndex = 0;
		
		public override void SceneLoadLocalDone(string scene, IProtocolToken token)
		{
			base.SceneLoadLocalDone(scene, token);

			// 에디터에서는 서버로 클라이언트 로직 일부 실행
#if UNITY_EDITOR
			SceneLoadLocalDoneClient(scene, token);
#else

			if (BoltNetwork.IsServer)
			{
				SceneLoadLocalDoneServer(scene, token);
			}
			
			else if (BoltNetwork.IsClient)
			{
				SceneLoadLocalDoneClient(scene, token);
			}
#endif

		}

		void SceneLoadLocalDoneServer(string scene, IProtocolToken token)
		{
			cameraPoints = GameObject.FindGameObjectsWithTag("CameraPoint");
			if (cameraPoints.Length > 0)
			{
				Camera.main.transform.position = cameraPoints[cameraIndex].transform.position;
				Camera.main.transform.rotation = cameraPoints[cameraIndex].transform.rotation;
			}			
		}

		void SceneLoadLocalDoneClient(string scene, IProtocolToken token)
		{
			// 스폰 포인트 가져오기
			GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

			int pickedIndex = Random.Range(0, spawnPoints.Length);
			Vector3 position = spawnPoints[pickedIndex].transform.position;
			Quaternion rotation = spawnPoints[pickedIndex].transform.rotation * Quaternion.Euler(0, 180, 0);
			
			// 볼트 인스턴시에이트
			BoltNetwork.Instantiate(BoltPrefabs.LittleLegend, position, rotation);

			GameObject[] championSpawnPoints = GameObject.FindGameObjectsWithTag("SushiPoint");
			StartCoroutine(SpawnSushi(championSpawnPoints));
		}

		IEnumerator SpawnSushi(GameObject[] points, float delay = 0.8f)
		{
			var prefab = Resources.Load("Champion_Prize");

			foreach (GameObject championSpawnPoint in points)
			{
				yield return new WaitForSeconds(delay);
				
				GameObject instance = Instantiate(prefab) as GameObject;
				
				instance.transform.SetParent(championSpawnPoint.transform);
				
				instance.transform.localPosition = Vector3.one * 0.01f;
				instance.transform.localRotation = Quaternion.identity;
				instance.transform.localScale = 0.8f * Vector3.one;
			}
		}
		
		public override void OnEvent(LogEvent evnt)
		{
			base.OnEvent(evnt);

			logMessages.Insert(0, evnt.Message);
		}

		void Update()
		{
			if (BoltNetwork.IsServer)
			{
				if (Input.GetKeyDown(KeyCode.C))
				{
					cameraIndex += 1;
					cameraIndex %= cameraPoints.Length;
					
					Camera.main.transform.position = cameraPoints[cameraIndex].transform.position;
					Camera.main.transform.rotation = cameraPoints[cameraIndex].transform.rotation;
				}
			}
		}
	}
}