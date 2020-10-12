using Bolt;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

namespace CQ.LeagueOfLegends.TFT.Network
{
	[RequireComponent(typeof(NavMeshAgent))]
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

		Vector3 localSpawnPosition;

		FrozenChampBehaviour takenChampion;

		public NavMeshAgent agent { get; private set; }
		
		public override void Attached()
		{
			base.Attached();
			
			InitComponents();
			InitStates();
			InitCallbacks();
		}

		void InitComponents()
		{
			agent = GetComponent<NavMeshAgent>();
			
			agent.updateRotation = true;
			agent.updatePosition = true;
		}

		void InitStates()
		{
			//state init
			state.SetTransforms(state.Transform, transform);
			state.SetAnimator(GetComponent<Animator>());
			
			// need to chk how this works.
			state.Animator.applyRootMotion = entity.IsOwner;
		}

		void InitCallbacks()
		{
			// state.AddCallback("CanMove", OnCanMoveChanged);
			// state.AddCallback("Destination", OnDestinationChanged);
		}

		public override void OnEvent(DestinationSetEvent evnt)
		{
			base.OnEvent(evnt);

			if (entity.IsOwner)
			{
				agent.SetDestination(evnt.Destination);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public override void SimulateOwner()
		{
			base.SimulateOwner();
			
			// 서버(Owner)에서 Animator 업데이트 합니다.
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

		public override void SimulateController()
		{
			base.SimulateController();
			
			// 컨트롤러에서 입력을 받습니다.
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
						if (Vector3.Distance(dest, localSpawnPosition) < threshold)
						{
							lastPosition = dest;
				
							SetDestination(dest);								
						}
					}
					else
					{
						lastPosition = dest;
				
						SetDestination(dest);
					}
				}
			}
		}

		public override void ControlGained()
		{
			base.ControlGained();

			localSpawnPosition = transform.position;
			
			// 로컬 클라이언트 컨트롤에서 트랜스폼을 할당해야 모든 클라이언트로 동기화됨
			// -> 이렇게 하면 결국 클라이언트에게 무브먼트 동작을 맡기는 거 아닌가? 검증은? 시발
			// -> Owner가 아니면 state를 변경할 수가 없게 되어있다.
			state.SetTransforms(state.Transform, transform);
			state.SetAnimator(GetComponent<Animator>());

			// need to chk how this works.
			state.Animator.applyRootMotion = entity.HasControl;
			
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
		}

		void SetDestination(Vector3 position)
		{
			// state.Destination = position;
			
			DestinationSetEvent evnt = DestinationSetEvent.Create(entity);
			evnt.Destination = position;
			
			evnt.Send();
			
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