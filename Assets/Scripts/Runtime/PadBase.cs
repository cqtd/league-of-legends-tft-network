using System;
using UnityEngine;

namespace CQ.LeagueOfLegends.TFT.Network
{
	public class PadBase : MonoBehaviour
	{
		public uint padOwner = 0;
		DummyUnit unit = default;
		
		public static PadBase mouseHovered;

		bool CanStartDrag()
		{
			return unit != null;
		}

		public DummyUnit GetUnit()
		{
			return unit;
		}

		public void SetUnit(DummyUnit newUnit)
		{
			this.unit = newUnit;
			OnUnitUpdate();
		}

		public void OnUnitUpdate()
		{
			if (unit != null)
			{
				unit.transform.position = transform.position + unit.offset;
			}
		}

		void Awake()
		{
			UnitDragManager inst = UnitDragManager.Instance;
		}

		public void OnMouseDown()
		{
			if (!CanStartDrag()) return;
			
			DragUtility.BeginDrag(this);
		}

		void OnMouseEnter()
		{
			mouseHovered = this;
		}

		void OnMouseExit()
		{
			mouseHovered = null;
		}

		void OnMouseUp()
		{
			DragUtility.EndDrag(mouseHovered);
		}
	}
}