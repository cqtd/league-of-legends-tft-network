using Bolt;
using UnityEngine;

namespace CQ.LeagueOfLegends.TFT.Network
{
	public class FrozenChampion : EntityEventListener<IFrozenChamp>
	{
		[SerializeField] MeshRenderer plateMeshRenderer = default;
		static readonly int pColor = Shader.PropertyToID("_Color");
		
		int intensity = 2;

		public override void Attached()
		{
			base.Attached();
			
			state.SetTransforms(state.Transform, transform);
		}

		public void SetChampion(ChampionArchetype archetype)
		{
			GameObject figure = Instantiate(archetype.figure, transform);

			plateMeshRenderer.material = Instantiate(plateMeshRenderer.sharedMaterial);

			switch (archetype.championCost)
			{
				case 1:
					plateMeshRenderer.material.SetColor(pColor, CO.Cost1 * intensity);
					break;
				
				case 2:
					plateMeshRenderer.material.SetColor(pColor, CO.Cost2 * intensity);
					break;
				
				case 3:
					plateMeshRenderer.material.SetColor(pColor, CO.Cost3 * intensity);
					break;
				
				case 4:
					plateMeshRenderer.material.SetColor(pColor, CO.Cost4 * intensity);
					break;
				
				case 5:
					plateMeshRenderer.material.SetColor(pColor, CO.Cost5 * intensity);
					break;
				
				default:
					break;
			}
		}
	}
}