using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Singleton to retrieve our prefabs and sprites from their name
/// </summary>
public class PrefabManager : MonoBehaviour
{
		private static bool __created = false;

		public List<PrefabDico> Skills;
		public List<PrefabDico> Images;
		public List<PrefabDico> Boss;
		public List<PrefabDico> Enemies;

		void Awake()
		{
				//Debug.Log("Instance " + this.GetInstanceID());
				if (!__created)
				{
						// this is the first instance - make it persist
						DontDestroyOnLoad(this.gameObject);
						__created = true;
				}
				else
				{
						// this must be a duplicate from a scene reload - DESTROY!
						//Debug.Log("Destroying me");
						DestroyImmediate(this.gameObject);
				}
		}

		public Sprite GetSkillSprite(string name)
		{
				foreach (PrefabDico dico in Skills)
				{
						if (dico.Name == name) return dico.Sprite;
				}
				return null;
		}

		public Sprite GetImageSprite(string name)
		{
				foreach (PrefabDico dico in Images)
				{
						if (dico.Name == name) return dico.Sprite;
				}
				return null;
		}

		public Transform GetSkillTransform(string name)
		{
				foreach (PrefabDico dico in Skills)
				{
						if (dico.Name == name) return dico.Prefab;
				}
				return null;
		}

		public Transform GetBossPrefab(string name)
		{
				foreach (PrefabDico dico in Boss)
				{
						if (dico.Name == name) return dico.Prefab;
				}
				return null;
		}

		public Transform GetEnemyPrefab(string name)
		{
				foreach (PrefabDico dico in Enemies)
				{
						if (dico.Name == name) return dico.Prefab;
				}
				return null;
		}

		public List<Transform> GetEnemiesPrefab(List<string> names)
		{
				List<Transform> prefabs = new List<Transform>();
				foreach (string name in names)
				{
						prefabs.Add(GetEnemyPrefab(name));
				}
				return prefabs;
		}
}
