using System;
using System.Runtime.InteropServices;
using Bolt;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

namespace CQ.LeagueOfLegends.TFT.Network
{
	[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
	public class LittleLegendBehaviour : EntityEventListener<ILittleLegendState>
	{
		Camera mainCamera;
		Vector3 lastPosition;
		public float surfaceOffset = 0.2f;
		public float threshold = 1.5f;

		[Header("Debug View")] 
		public float velocityMag_Debug;
		public float speedMax_Debug;
		
		static readonly int pSpeed = Animator.StringToHash("Speed");
		static readonly int pIsMoving = Animator.StringToHash("IsMoving");

		FrozenChampBehaviour takenChampion;
		Transform nearBy;

		public NavMeshAgent agent { get; private set; }
		
		public override void Attached()
		{
			base.Attached();
			
			Debug.Log("Attached");
			
			state.SetTransforms(state.Transform, transform);
			state.SetAnimator(GetComponent<Animator>());

			// need to chk how this works.
			state.Animator.applyRootMotion = entity.IsOwner;

			if (entity.IsOwner)
			{
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
			
			state.AddCallback("CanMove", OnCanMoveChanged);
		}

		void OnCanMoveChanged()
		{
			if (state.CanMove)
			{
				nearBy.gameObject.SetActive(false);
			}
			else
			{
				nearBy.gameObject.SetActive(true);
			}
		}

		public void Set(Transform trm)
		{
			this.nearBy = trm;
		}

		void Update()
		{
			if (entity.IsOwner)
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
						if (!state.CanMove)
						{
							if (Vector3.Distance(dest, nearBy.transform.position) < threshold)
							{
								lastPosition = dest;
				
								SetTarget(dest);								
							}
						}
						else
						{
							lastPosition = dest;
				
							SetTarget(dest);
						}
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
		}

		void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawSphere(lastPosition, threshold);
		}

		void SetTarget(Vector3 position)
		{
			this.agent.SetDestination(position);
			
			CursorEffect.Spawn(position, null);
		}
		
		void OnTriggerEnter(Collider other)
		{
			if (BoltNetwork.IsServer)
			{
				if (takenChampion != null)
					return;
				
				FrozenChampBehaviour fc = other.GetComponent<FrozenChampBehaviour>();
				
				if (fc == null) return;
				if (fc.HasOwner == true) return;
				
				TakeFrozenChamp(fc);
			}
		}

		void TakeFrozenChamp(FrozenChampBehaviour champion)
		{
			champion.Taken(this);
			takenChampion = champion;
		}
	}
}