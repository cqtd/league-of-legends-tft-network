using System;
using UnityEngine;

namespace CQ.LeagueOfLegends.TFT.Network
{
	public class UnitPlaceTool : SingletonMono<UnitPlaceTool>
	{
		void Update()
		{
			DragUtility.OnDrag();
		}
	}

	public static class DragUtility
	{
		static bool isDragging = false;
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
				return;
				
				if (PadBase.mouseHovered)
				{
					
					startPad.GetUnit().transform.position = CursorManager.Instance.HitPoint + Vector3.up * 2;
				}
			
				else if (CursorManager.dummy)
				{
					startPad.GetUnit().transform.position = CursorManager.Instance.HitPoint + Vector3.up * 2;
				}
				else
				{
					// startPad.GetUnit().transform.position = CursorManager.Instance.HitPoint + Vector
				}
			}
		}

		public static void EndDrag(PadBase end)
		{
			isDragging = false;
			endPad = end;

			if (endPad)
			{
				var temp = endPad.GetUnit();
				endPad.SetUnit(startPad.GetUnit());
				startPad.SetUnit(temp);
			
				startPad.GetUnit()?.OnEndDrag();
				endPad.GetUnit()?.OnEndDrag();
			}
			else
			{
				startPad.SetUnit(startPad.GetUnit());
			}

			startPad = null;
			endPad = null;
		}

		public static bool CanEndDrag(Vector3 worldPos)
		{
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