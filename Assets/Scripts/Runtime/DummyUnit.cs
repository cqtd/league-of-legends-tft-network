using System;
using UnityEngine;

namespace CQ.LeagueOfLegends.TFT.Network
{
	public class DummyUnit : MonoBehaviour
	{
		public PadBase platform = default;
		public Vector3 offset = Vector3.up * 2;

		Collider col = default;

		void Awake()
		{
			col = GetComponent<Collider>();
		}

		public void BeginDrag()
		{
			// col.enabled = false;
		}

		public void OnEndDrag()
		{
			// col.enabled = true;
		}
	}
}