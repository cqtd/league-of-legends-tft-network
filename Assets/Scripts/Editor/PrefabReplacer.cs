using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CQ.LeagueOfLegends.TFT.Network.Editor
{
	public class PrefabReplacer : ScriptableWizard
	{
		public GameObject[] targets = default;
		public GameObject prefab = default;
		
		[MenuItem("Tools/프리팹 리플레이서")]
		static void OpenWizard()
		{
			DisplayWizard<PrefabReplacer>("프리팹 리플레이서", "닫기", "실행");
		}

		void Awake()
		{
			
		}

		void OnWizardCreate()
		{
			
		}

		void OnWizardOtherButton()
		{
			Undo.RecordObjects(targets, "Remove");
			
			foreach (GameObject o in targets)
			{
				string objName = o.name;
				Transform parent = o.transform.parent;
				Vector3 pos = o.transform.position;
				Quaternion rot = o.transform.rotation;
				Vector3 scale = o.transform.localScale;
				
				GameObject instance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
				
				instance.transform.SetParent(parent);
				instance.transform.position = pos;
				instance.transform.rotation = rot;
				instance.transform.localScale = scale;
				instance.gameObject.name = objName;
				
				DestroyImmediate(o);
			}

			targets = null;
			EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
		}

	}
}