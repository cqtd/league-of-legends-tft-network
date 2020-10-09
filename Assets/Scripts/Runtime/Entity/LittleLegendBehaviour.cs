using Bolt;
using UnityEngine;

namespace CQ.LeagueOfLegends.TFT.Network
{
	public class LittleLegendBehaviour : EntityEventListener<ILittleLegend>
	{
		Camera mainCamera;
		
		public override void Attached()
		{
			base.Attached();
			
			Debug.Log("Attached");
			
			state.SetTransforms(state.Transform, transform);
			
			// 카메라 붙이기

			mainCamera = Camera.main;
			mainCamera.transform.SetParent(this.transform);
			mainCamera.transform.localPosition = new Vector3(0, 10, -10);
			mainCamera.transform.localRotation = Quaternion.Euler(Vector3.right*35f);
			mainCamera.fieldOfView = 45;
		}
		
		public override void SimulateOwner()
		{
			base.SimulateOwner();
			
			float speed = 4f;
			Vector3 movement = Vector3.zero;
			
			if (Input.GetKey(KeyCode.W)) movement.z += 1;
			if (Input.GetKey(KeyCode.S)) movement.z -= 1;
			if (Input.GetKey(KeyCode.A)) movement.x -= 1;
			if (Input.GetKey(KeyCode.D)) movement.x += 1;
			
			if (movement != Vector3.zero)
			{
				transform.position += (movement.normalized * speed * BoltNetwork.FrameDeltaTime);
			}
		}
	}
}