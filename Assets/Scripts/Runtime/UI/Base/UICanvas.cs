using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CQ.LeagueOfLegends.TFT.Network.UI
{
	[RequireComponent(typeof(Canvas))]
	public abstract class UICanvas : MonoBehaviour
	{
		[Header("UI Canvas")]
		[SerializeField] Canvas canvas = default;
		
		bool bIsInitComponent = false;
		bool bIsInitialized = false;

		void InitComponentInternal()
		{
			if (!bIsInitComponent)
				InitComponent();
		}

		void InitializeInternal()
		{
			if (!bIsInitialized)
				Initialize();
		}
		
		
		protected virtual void InitComponent()
		{
			bIsInitComponent = true;
		}

		protected virtual void Start()
		{
			InitComponent();
			Initialize();
		}

		protected virtual void OnEnable()
		{
			InitComponent();
			Initialize();
		}

		protected void OnDisable()
		{
			
		}

		public virtual void Show()
		{
			gameObject.SetActive(true);
			
			InitComponent();
			Initialize();
		}

		public void Hide()
		{
			gameObject.SetActive(false);
		}

		public virtual void Initialize()
		{
			
		}

		public int GetSortingOrder()
		{
			return canvas.sortingOrder;
		}

		public void SetCanvasConfig(bool overrideSorting, int sortingOrder)
		{
			canvas.overrideSorting = overrideSorting;
			canvas.sortingOrder = sortingOrder;
		}

		public void ResetName(string type)
		{
			gameObject.name = $"[{GetSortingOrder()}] {type}";
		}


#if UNITY_EDITOR
		void Reset()
		{
			canvas = GetComponent<Canvas>();
			
			UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
		}
#endif
	}

	public abstract class UICanvas<T> : UICanvas where T : MonoBehaviour
	{
		public T Entity { get; private set; } = default;

		public virtual void Initialize(T entity)
		{
			this.Entity = entity;
		}
	}
}