using System;
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
			Vector3 position = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
			
			// 볼트 인스턴시에이트
			BoltNetwork.Instantiate(BoltPrefabs.LittleLegend, position, Quaternion.identity);			
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