using CQ.UI;
using UnityEngine;

namespace CQ.LeagueOfLegends.TFT.Network.UI
{
	public class TFTUIManager : UIManager
	{
		protected TransitionCanvas transition = default;
		
		protected override void Awake()
		{
			base.Awake();
			
			TransitionCanvas prefab = Resources.Load<TransitionCanvas>(GetPrefabPathInternal("TransitionCanvas"));
			transition = Instantiate(prefab, root.transform);
			
			transition.transform.localPosition = Vector3.zero;
			transition.transform.localScale = Vector3.one;
			
			transition.transform.SetAsLastSibling();
			transition.SetCanvasConfig(true, 999);
			
			SetCanvasNameInternal(transition, "Transition Canvas");
		}

		public override T GetCanvas<T>()
		{
			T instance = base.GetCanvas<T>();
			transition.transform.SetAsLastSibling();

			return instance;
		}

		public override void Open(string canvasName)
		{
			base.Open(canvasName);
			
			transition.transform.SetAsLastSibling();
		}
	}
}