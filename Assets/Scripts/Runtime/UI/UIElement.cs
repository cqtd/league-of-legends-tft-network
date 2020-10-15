using UnityEngine;

namespace CQ.LeagueOfLegends.TFT.Network.UI
{
	public abstract class UIElement : MonoBehaviour
	{
		public virtual void OnSchemeUpdate()
		{
			
		}
	}

	public abstract class UIElement<T> : UIElement where T : Object
	{
		public abstract void Set(T element);
	}
}