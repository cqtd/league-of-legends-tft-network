using System;
using Bolt.Compiler;
using DG.Tweening;
using UnityEditor;
using UnityEngine;

namespace CQ.LeagueOfLegends.TFT.Network
{
	public class PadBase : MonoBehaviour
	{
		public uint padOwner = 0;
		DummyUnit unit = default;
		
		public static PadBase mouseHovered;
		[SerializeField] MeshRenderer highlightMesh = default;

		[SerializeField] Material defaultMat = default;
		[SerializeField] Material emissiveMat = default;

		static Action onBeginDrag;
		static Action onEndDrag;

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
		}

		public void OnUnitMove()
		{
			if (unit != null)
			{
				unit.transform.position = transform.position + unit.offset;
			}
		}

		public void OnUnitSwap()
		{
			float distance = Vector3.Distance(transform.position + unit.offset, unit.transform.position);
			unit.transform.DOMove(transform.position + unit.offset, distance * 0.1f);
		}

		public void OnCancelDrag()
		{
			float distance = Vector3.Distance(transform.position + unit.offset, unit.transform.position);
			unit.transform.DOMove(transform.position + unit.offset, distance * 0.1f);
		}

		void Awake()
		{
			UnitDragManager inst = UnitDragManager.Instance;

			highlightMesh.enabled = false;
			highlightMesh.material = defaultMat;

			onBeginDrag += OnBeginDrag;
			onEndDrag += OnEndDrag;
		}

		public void OnMouseDown()
		{
			if (!CanStartDrag()) return;
			if (LocalPlayer.Instance.localPlayer.uniqueIndex != padOwner) return;
			
			DragUtility.BeginDrag(this);
			
			onBeginDrag?.Invoke();
		}

		void OnMouseEnter()
		{
			if (DragUtility.isDragging)
			{
				mouseHovered = this;
				highlightMesh.material = emissiveMat;
			}
			else
			{
				highlightMesh.enabled = true;
			}
		}

		void OnMouseExit()
		{
			if (DragUtility.isDragging)
			{
				mouseHovered = null;
				highlightMesh.material = defaultMat;
			}
			else
			{
				highlightMesh.enabled = false;
			}
		}

		void OnMouseUp()
		{
			DragUtility.EndDrag(mouseHovered);
			
			onEndDrag?.Invoke();
		}

		void OnBeginDrag()
		{
			if (LocalPlayer.Instance.localPlayer.uniqueIndex != padOwner) return;
			highlightMesh.enabled = true;
		}

		void OnEndDrag()
		{
			if (LocalPlayer.Instance.localPlayer.uniqueIndex != padOwner) return;
			
			highlightMesh.enabled = false;
			highlightMesh.material = defaultMat;

			mouseHovered = null;
		}

		void OnDestroy()
		{
			DomainReset();
		}

		void OnApplicationQuit()
		{
			DomainReset();
		}

		static void DomainReset()
		{
			onBeginDrag = null;
			onEndDrag = null;
		}
	}
}