using UnityEngine;

namespace CQ.LeagueOfLegends.TFT
{
	public class Rotator : MonoBehaviour
	{
		public Vector3 speed = Vector3.up;

		void Update()
		{
			transform.localRotation *= Quaternion.Euler(speed * Time.deltaTime);
		}
	}
}