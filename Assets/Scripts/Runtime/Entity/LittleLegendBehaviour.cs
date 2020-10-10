using System;
using System.Runtime.InteropServices;
using Bolt;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

namespace CQ.LeagueOfLegends.TFT.Network
{
	[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
	public class LittleLegendBehaviour : EntityEventListener<ILittleLegend>
	{
		Camera mainCamera;
		Vector3 lastPosition;
		public float surfaceOffset = 0.2f;

		[Header("Debug View")] 
		public float velocityMag_Debug;
		public float speedMax_Debug;
		static readonly int pSpeed = Animator.StringToHash("Speed");
		static readonly int pIsMoving = Animator.StringToHash("IsMoving");

		public NavMeshAgent agent { get; private set; }
		
		public override void Attached()
		{
			base.Attached();
			
			Debug.Log("Attached");
			
			state.SetTransforms(state.Transform, transform);
			state.SetAnimator(GetComponent<Animator>());

			// need to chk how this works.
			state.Animator.applyRootMotion = entity.IsOwner;
			
			// 카메라 붙이기
			mainCamera = Camera.main;
			Assert.IsNotNull(mainCamera);
			
			GameObject gimbal = new GameObject("Gimbal");
			gimbal.transform.position = state.Transform.Position;
			gimbal.transform.rotation = state.Transform.Rotation;
			
			mainCamera.transform.SetParent(gimbal.transform);
			mainCamera.transform.localPosition = new Vector3(0, 32 ,-20);
			mainCamera.transform.localRotation = Quaternion.Euler(Vector3.right * 50);

			mainCamera.fieldOfView = 25;

			agent = GetComponent<NavMeshAgent>();

			agent.updateRotation = true;
			agent.updatePosition = true;
		}
		
		public override void SimulateOwner()
		{
			base.SimulateOwner();
			
			// float speed = 4f;
			// Vector3 movement = Vector3.zero;
			//
			// if (Input.GetKey(KeyCode.W)) movement.z += 1;
			// if (Input.GetKey(KeyCode.S)) movement.z -= 1;
			// if (Input.GetKey(KeyCode.A)) movement.x -= 1;
			// if (Input.GetKey(KeyCode.D)) movement.x += 1;
			//
			// if (movement != Vector3.zero)
			// {
			// 	// transform.position += (movement.normalized * speed * BoltNetwork.FrameDeltaTime);
			// }
		}

		void Update()
		{
			if (Input.GetMouseButtonDown(1))
			{
				Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
				if (!Physics.Raycast(ray, out RaycastHit hit))
				{
					return;
				}

				Vector3 dest = hit.point + hit.normal * surfaceOffset;
				if (dest != lastPosition)
				{
					lastPosition = dest;
				
					SetTarget(dest);
				}
			}

			float speed = agent.velocity.magnitude;
			float speedRate = speed / (agent.speed + 0.1f);
			
			state.Animator.SetFloat(pSpeed, speedRate);
			if (speedRate > 0.1f)
			{
				state.Animator.SetBool(pIsMoving, true);
			}
			else
			{
				state.Animator.SetBool(pIsMoving, false);
			}
		}

		void SetTarget(Vector3 position)
		{
			this.agent.SetDestination(position);
			
			CursorEffect.Spawn(position, null);
		}
	}
}