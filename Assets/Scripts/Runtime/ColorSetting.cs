using System;
using UnityEngine;

namespace CQ.LeagueOfLegends.TFT.Network
{
	public class ColorSetting : MonoBehaviour
	{
		[SerializeField] ColorArchetype archetype = default;

		void Awake()
		{
			CO.Init(archetype);
		}
	}

	public sealed class CO
	{
		public static void Init(ColorArchetype archetype)
		{
			instance = new CO();
			instance.archetype = archetype;
		}

		static CO instance;
		ColorArchetype archetype;

		public static Color Cost1 {
			get
			{
				return instance.archetype.cost1;
			}
		}
		
		public static Color Cost2 {
			get
			{
				return instance.archetype.cost2;
			}
		}
		
		public static Color Cost3 {
			get
			{
				return instance.archetype.cost3;
			}
		}
		
		public static Color Cost4 {
			get
			{
				return instance.archetype.cost4;
			}
		}
		
		public static Color Cost5 {
			get
			{
				return instance.archetype.cost5;
			}
		}
	}
}