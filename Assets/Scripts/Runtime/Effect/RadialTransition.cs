using System.Diagnostics;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CQ.LeagueOfLegends.TFT
{
	[RequireComponent(typeof(Image))]
	public class RadialTransition : MonoBehaviour
	{
		[SerializeField] Image image;
		static readonly int pivot = Shader.PropertyToID("_Pivot");
		static readonly int radius = Shader.PropertyToID("_Radius");

		Tweener fadeIn;
		Tweener fadeOut;
		static readonly int color1 = Shader.PropertyToID("_Color");

#if UNITY_EDITOR
		void Reset()
		{
			image = GetComponent<Image>();
			UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
		}
#endif

		void Awake()
		{
			var matInstance = Instantiate(image.material);
			image.material = matInstance;
		}

		public void SetFocus(int screenX, int screenY)
		{
#if UNITY_EDITOR
			if (!Application.isPlaying)
				return;
#endif
			
			image.material.SetVector(pivot, new Vector4((float)screenX / Screen.width, (float)screenY / Screen.height, 0, 0));
		}

		public void FadeOut(float duration)
		{
#if UNITY_EDITOR
			if (!Application.isPlaying)
				return;
#endif
			
			image.material.SetFloat(radius, 2);
			var tweener = image.material.DOFloat(0, radius, duration);
			tweener.SetEase(Ease.Linear);
		}

		public void FadeIn(float duration)
		{
#if UNITY_EDITOR
			if (!Application.isPlaying)
				return;
#endif
			
			image.material.SetFloat(radius, -1);
			var tweener = image.material.DOFloat(1, radius, duration);
			tweener.SetEase(Ease.Linear);
		}

		public void Clear()
		{
#if UNITY_EDITOR
			if (!Application.isPlaying)
				return;
#endif
			
			image.material.SetFloat(radius, 2);
		}

		public void SetColor(Color color)
		{
#if UNITY_EDITOR
			if (!Application.isPlaying)
				return;
#endif
			
			image.material.SetColor(color1, color);
		}
	}
}