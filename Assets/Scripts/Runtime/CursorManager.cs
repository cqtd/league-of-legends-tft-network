using UnityEngine;

namespace CQ.LeagueOfLegends.TFT.Network
{
	public class CursorManager : SingletonMono<CursorManager>
	{
		[Header("Cursor Manager")]
		[SerializeField] LayerMask layerMask = default;
		[SerializeField] float rayLength = 1000f;
		
		Camera current = default;

		Vector3 hitPoint;
		public static DummyUnit dummy;
		
		public Vector3 HitPoint {
			get
			{
				return hitPoint;
			}
		}
		
		protected override void Awake()
		{
			base.Awake();
			
			current = Camera.main;
		}

		void Update()
		{
			Ray ray = current.ScreenPointToRay(Input.mousePosition);
			
			if (!Physics.Raycast(ray, out RaycastHit hit, rayLength, layerMask))
			{
				Debug.DrawRay(current.transform.position, ray.direction * rayLength, Color.red);
				hitPoint = Vector3.zero;
				dummy = null;
				return;
			}
			
			Debug.DrawRay(current.transform.position, ray.direction * rayLength, Color.green);
			
			hitPoint = hit.point;
			
			DummyUnit dum = hit.collider.GetComponent<DummyUnit>();
			dummy = dum;
		}
	}
}