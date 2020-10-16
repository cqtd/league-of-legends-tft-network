using System;
using UnityEngine;

namespace CQ.LeagueOfLegends.TFT.Network
{
	public class UnitDragManager : SingletonMono<UnitDragManager>
	{
		void Update()
		{
			DragUtility.OnDrag();
		}
	}

	public static class DragUtility
	{
		public static bool isDragging = false;
		static PadBase startPad;
		static PadBase endPad;
		
		public static void BeginDrag(PadBase start)
		{
			isDragging = true;
			startPad = start;
			
			startPad.GetUnit().BeginDrag();
		}

		public static void OnDrag()
		{
			if (isDragging)
			{
				startPad.GetUnit().transform.position = CursorManager.Instance.HitPoint + Vector3.up * 2;
			}
		}

		public static void EndDrag(PadBase end)
		{
			isDragging = false;
			endPad = end;

			if (startPad && endPad)
			{
				if (CanEndDrag(end))
				{
					DummyUnit temp = endPad.GetUnit();
					if (temp != null)
					{
						endPad.SetUnit(startPad.GetUnit());
						startPad.SetUnit(temp);
			
						startPad.GetUnit()?.OnEndDrag();
						endPad.GetUnit()?.OnEndDrag();
						
						startPad.OnUnitSwap();
						endPad.OnUnitSwap();
					}
					else
					{
						endPad.SetUnit(startPad.GetUnit());
						startPad.SetUnit(temp);
			
						startPad.GetUnit()?.OnEndDrag();
						endPad.GetUnit()?.OnEndDrag();
						
						startPad.OnUnitMove();
						endPad.OnUnitMove();
					}
				}
				else
				{
					startPad.OnCancelDrag();
				}
			}
			else
			{
				if (startPad)
					startPad.OnCancelDrag();
			}

			startPad = null;
			endPad = null;
		}

		public static bool CanEndDrag(PadBase end)
		{
			if (end.padOwner != LocalPlayer.Instance.localPlayer.uniqueIndex)
				return false;

			return true;
		}

		public static bool CanBeginDrag()
		{
			if (PadBase.mouseHovered) return true;
			if (CursorManager.dummy) return true;
			
			return false;
		}
	}
}