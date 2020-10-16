using System;
using UnityEngine;

namespace CQ.LeagueOfLegends.TFT.Network
{
	public class PadBase : MonoBehaviour
	{
		DummyUnit unit = default;
		
		public static PadBase dragStart;
		public static PadBase dragEnd;
		public static PadBase lastEnter;

		public static PadBase mouseHovered;

		MeshRenderer render = default;
		Material mat = default;
		Color color = default;
		static readonly int pColor = Shader.PropertyToID("_Color");

		public float dragThreshold = 0.15f;
		float downTime = default;

		bool IsDragging = false;

		bool CanStartDrag()
		{
			return unit != null;
		}

		public DummyUnit GetUnit()
		{
			return unit;
		}

		public void SetUnit(DummyUnit unit)
		{
			this.unit = unit;
			if (unit != null)
			{
				unit.transform.position = transform.position + unit.offset;
			}
		}

		void Awake()
		{
			render = GetComponent<MeshRenderer>();
			mat = Instantiate(render.material);
			render.material = mat;
			color = mat.GetColor(pColor);

			var inst = UnitPlaceTool.Instance;
		}

		void OnMouseDown()
		{
			if (!CanStartDrag()) return;
			
			DragUtility.BeginDrag(this);
		}

		void OnMouseDrag()
		{
			// if (downTime + dragThreshold < Time.time)
			// {
			// 	mat.SetColor(pColor, Color.red);
			//
			// 	if (dragStart != null)
			// 	{
			// 		Debug.DrawLine(dragStart.transform.position, CursorManager.Instance.HitPoint, Color.red);
			// 	}
			// }
			// Debug.Log("");
		}

		void OnMouseEnter()
		{
			mouseHovered = this;
			
			// mat.SetColor(pColor, Color.yellow);
			// lastEnter = this;
			// Debug.Log("");
		}

		void OnMouseExit()
		{
			mouseHovered = null;
			
			// mat.SetColor(pColor, color);
			// downTime = 0;
			// Debug.Log("");
		}

		void OnMouseUp()
		{
			DragUtility.EndDrag(mouseHovered);
			
			if (IsDragging)
			{
				mat.SetColor(pColor, color);
				downTime = 0;
				// Debug.Log("");

				dragEnd = this;
			}
		}

		
		void Update()
		{
			if (dragStart != null && dragEnd != null)
				Debug.DrawLine(dragStart.transform.position, lastEnter.transform.position, Color.magenta);
			
		}
	}
}