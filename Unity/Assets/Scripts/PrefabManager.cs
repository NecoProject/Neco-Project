﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Singleton save object.
/// </summary>
public class PrefabManager : MonoBehaviour
{
		private static bool __created = false;

		public List<PrefabDico> Skills;
		public List<PrefabDico> Images;

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
}
