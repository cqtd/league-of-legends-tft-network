using System.Collections;
using Bolt;
using DG.Tweening;
using UnityEngine;

namespace CQ.LeagueOfLegends.TFT.Network
{
	public class FrozenChampBehaviour : EntityEventListener<IFrozenChampState>
	{
		[SerializeField] MeshRenderer plateMeshRenderer = default;
		static readonly int pColor = Shader.PropertyToID("_Color");

		GameObject figurePrefab;
		GameObject figureInstance;
		
		float intensity = 1.6f;
		Collider col = default;

		public override void Attached()
		{
			base.Attached();
			
			state.SetTransforms(state.Transform, transform);

			col = GetComponent<Collider>();
			StartCoroutine(SpawnFigure());
			
			state.AddCallback("FigureKey", SpawnFigureCallback);
			state.AddCallback("FigureData", FigureObjectCallback);
		}

		void SpawnFigureCallback()
		{
			figurePrefab = Resources.Load<GameObject>(state.FigureKey);
		}

		void FigureObjectCallback()
		{
			plateMeshRenderer.material = Instantiate(plateMeshRenderer.sharedMaterial);
			plateMeshRenderer.material.SetColor(pColor, state.FigureData.Color);
		}

		IEnumerator SpawnFigure()
		{
			while (figurePrefab == null)
				yield return null;

			figureInstance = Instantiate(figurePrefab, state.Transform.Transform);
			figurePrefab = null;
		}

		/// <summary>
		/// Run on Server
		/// </summary>
		/// <param name="archetype"></param>
		public void SetChampion(ChampionArchetype archetype)
		{
			state.FigureKey = $"Figures/{archetype.figure.name}";
			
			switch (archetype.championCost)
			{
				case ECost.Cost1:
					state.FigureData.Color = CO.Cost1 * intensity;
					break;
				
				case ECost.Cost2:
					state.FigureData.Color = CO.Cost2 * intensity;
					break;
				
				case ECost.Cost3:
					state.FigureData.Color = CO.Cost3 * intensity;
					break;
				
				case ECost.Cost4:
					state.FigureData.Color = CO.Cost4 * intensity;
					break;
				
				case ECost.Cost5:
					state.FigureData.Color = CO.Cost5 * intensity;
					break;
				
				default:
					break;
			}
		}

		public bool HasOwner {
			get
			{
				return state.OwnerLegend != null;
			}
		}

		public override void OnEvent(FrozenChampTakenEvent evnt)
		{
			base.OnEvent(evnt);
			
			col.enabled = false;
			plateMeshRenderer.enabled = false;
		}

		public void Taken(LittleLegendBehaviour takenBy)
		{
			transform.parent = takenBy.transform;
			transform.DOLocalMove(Vector3.back * 0.6f, 1.0f);
			
			if (BoltNetwork.IsServer)
			{
				TakenServer(takenBy);
			}
			else if (BoltNetwork.IsClient)
			{
				TakenClient();
			}

			FrozenChampTakenEvent.Create(entity).Send();
		}

		void TakenServer(LittleLegendBehaviour takenBy)
		{
			state.OwnerLegend = takenBy.entity;
			
		}

		void TakenClient()
		{
			EmitTakenEffect();
		}

		void EmitTakenEffect()
		{
			
		}
	}
}