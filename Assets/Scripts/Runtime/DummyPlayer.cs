using System;
using UnityEngine;
using UnityEngine.AI;

namespace CQ.LeagueOfLegends.TFT.Network
{
	public class DummyPlayer : MonoBehaviour
	{
		public uint uniqueIndex = 100;
		
		public float surfaceOffset = 0.2f;
		public float threshold = 1.5f;
		
		Vector3 lastPosition;
		public NavMeshAgent agent { get; private set; }

		void Awake()
		{
			LocalPlayer.Instance.localPlayer = this;
			agent = GetComponent<NavMeshAgent>();
		}

		void Update()
		{
			if (Input.GetMouseButtonDown(1))
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (!Physics.Raycast(ray, out RaycastHit hit))
				{
					return;
				}

				Vector3 dest = hit.point + hit.normal * surfaceOffset;
				if (dest != lastPosition)
				{
					lastPosition = dest;
					agent.SetDestination(dest);
					
					CursorEffect.Spawn(dest, null);
				}
			}
		}
	}

	public class LocalPlayer : Singleton<LocalPlayer>
	{
		public DummyPlayer localPlayer;
	}
}