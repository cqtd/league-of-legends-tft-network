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

			if (BoltNetwork.IsServer)
			{
				cameraPoints = GameObject.FindGameObjectsWithTag("CameraPoint");
				if (cameraPoints.Length > 0)
				{
					Camera.main.transform.position = cameraPoints[cameraIndex].transform.position;
					Camera.main.transform.rotation = cameraPoints[cameraIndex].transform.rotation;
				}
			}
			
			else if (BoltNetwork.IsClient)
			{
				// 스폰 포인트 가져오기
				GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
				Vector3 position = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
			
				// 볼트 인스턴시에이트
				BoltNetwork.Instantiate(BoltPrefabs.LittleLegend, position, Quaternion.identity);
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