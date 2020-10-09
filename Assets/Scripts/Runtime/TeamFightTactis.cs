using System.Collections.Generic;
using Bolt;
using UnityEngine;

namespace CQ.LeagueOfLegends.TFT.Network
{
	public class TeamFightTactis : GlobalEventListener
	{
		readonly List<string> logMessages = new List<string>();
		
		public override void SceneLoadLocalDone(string scene, IProtocolToken token)
		{
			base.SceneLoadLocalDone(scene, token);

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
	}
}