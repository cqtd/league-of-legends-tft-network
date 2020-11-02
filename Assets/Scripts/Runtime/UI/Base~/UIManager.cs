﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace CQ.UI
{
	public sealed class UIManager : SingletonMono<UIManager>
	{
		[SerializeField] Canvas root = default;
		[SerializeField] CanvasScaler scaler = default;

		// TransitionCanvas transition = default;
		
		Dictionary<string, UICanvas> cachedCanvases = default;

		Dictionary<string, UICanvas> CachedCanvases {
			get
			{
				if (cachedCanvases == null) 
					cachedCanvases = new Dictionary<string, UICanvas>();

				return cachedCanvases;
			}
		}

		const string prefabRootPath = "UI/Canvas/";

		protected override void Awake()
		{
			base.Awake();
			
			SetResolutionInternal();
			// TransitionCanvas prefab = Resources.Load<TransitionCanvas>(GetPrefabPathInternal("TransitionCanvas"));
			// transition = Instantiate(prefab, root.transform);
			//
			// transition.transform.localPosition = Vector3.zero;
			// transition.transform.localScale = Vector3.one;
			//
			// transition.transform.SetAsLastSibling();
			// transition.SetCanvasConfig(true, 999);
			//
			// SetCanvasNameInternal(transition, "Transition Canvas");
		}

		#region Public

		public T GetCanvas<T>() where T : UICanvas
		{
			if (!CachedCanvases.TryGetValue(typeof(T).Name, out UICanvas canvas))
			{
				T prefab = Resources.Load<T>(GetPrefabPathInternal(typeof(T).Name));
				canvas = Instantiate(prefab, root.transform);

				canvas.transform.localPosition = Vector3.zero;
				canvas.transform.localScale = Vector3.one;
				canvas.transform.SetAsLastSibling();
				
				// transition.transform.SetAsLastSibling();

				CachedCanvases[typeof(T).Name] = canvas;
			}
			
			canvas.SetCanvasConfig(true, GetTopWindowCanvasInternal() + 1);
			SetCanvasNameInternal(canvas, typeof(T).Name);
			
			return canvas as T;
		}
		
		public T Open<T>() where T : UICanvas
		{
			T canvas = GetCanvas<T>();
			canvas.gameObject.SetActive(true);
			
			return canvas;
		}
		
		public void Open(string canvasName)
		{
			if (!CachedCanvases.TryGetValue(canvasName, out UICanvas canvas))
			{
				UICanvas prefab = Resources.Load<UICanvas>(GetPrefabPathInternal(canvasName));
				canvas = Instantiate(prefab, root.transform);
				
				canvas.transform.localPosition = Vector3.zero;
				canvas.transform.localScale = Vector3.one;
				canvas.transform.SetAsLastSibling();
				
				// transition.transform.SetAsLastSibling();
				
				CachedCanvases[canvasName] = canvas;
			}

			canvas.SetCanvasConfig(true, GetTopWindowCanvasInternal() + 1);
			SetCanvasNameInternal(canvas, canvasName);
			
			canvas.gameObject.SetActive(true);
		}

		public void Close<T>(bool destroy = false) where T : UICanvas
		{
			if (!CachedCanvases.TryGetValue(typeof(T).Name, out UICanvas canvas))
			{
#if UNITY_EDITOR
				Assert.IsNotNull(canvas);
#endif
				return;
			}
			
			canvas.Close();

			if (destroy)
			{
				CachedCanvases.Remove(typeof(T).Name);
				Destroy(canvas.gameObject);
			}
		}
		
		public void Close(string canvasName, bool destroy = false)
		{
			if (!CachedCanvases.TryGetValue(canvasName, out UICanvas canvas))
			{
#if UNITY_EDITOR
				Assert.IsNotNull(canvas);
#endif
				return;
			}
			
			canvas.Close();
			
			if (destroy)
			{
				CachedCanvases.Remove(canvasName);
				Destroy(canvas.gameObject);
			}
		}
		
		#endregion

		#region Private

		void SetCanvasNameInternal<T>(T canvas, string canvasName) where T : UICanvas
		{
			canvas.ResetName(canvasName);
		}

		int GetTopWindowCanvasInternal()
		{
			UICanvas topCanvas = CachedCanvases.OrderByDescending(e => e.Value.GetSortingOrder())
				.FirstOrDefault().Value;
			
			return topCanvas.GetSortingOrder();
		}

		void SetResolutionInternal()
		{
			if (scaler == null)
			{
#if UNITY_EDITOR
				Assert.IsNotNull(scaler);
#endif
				return;
			}

			float WHvalue = (float) Screen.width / Screen.height;
			float value_1 = 2 - (float) WHvalue;
			float value_2 = value_1 / 0.7f;
			float result = 1 - value_2;

			if (result <= 0.7f)
			{
				result = 0;
			}
			else
			{
				result = 1;
			}

			scaler.matchWidthOrHeight = result;
		}
		
		string GetPrefabPathInternal(string canvasName)
		{
			return $"{prefabRootPath}{canvasName}";
		}
		
		#endregion

	}
}