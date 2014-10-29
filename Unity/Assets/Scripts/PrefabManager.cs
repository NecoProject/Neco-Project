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
		public List<PrefabDico> Attributes;
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
				return GetSprite(name, Skills);
		}

		public Sprite GetAttributeSprite(string name)
		{
				return GetSprite(name, Attributes);
		}

		public Sprite GetImageSprite(string name)
		{
				return GetSprite(name, Images);
		}

		public Transform GetSkillTransform(string name)
		{
				return GetTransform(name, Skills);
		}

		public Transform GetBossPrefab(string name)
		{
				return GetTransform(name, Boss);
		}

		public Transform GetEnemyPrefab(string name)
		{
				return GetTransform(name, Enemies);
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


		Sprite GetSprite(string name, List<PrefabDico> list)
		{
				foreach (PrefabDico dico in list)
				{
						if (dico.Name == name) return dico.Sprite;
				}
				return null;
		}

		Transform GetTransform(string name, List<PrefabDico> list)
		{
				foreach (PrefabDico dico in list)
				{
						if (dico.Name == name) return dico.Prefab;
				}
				return null;
		}
}
