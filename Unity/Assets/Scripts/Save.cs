using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Singleton save object.
/// </summary>
public class Save : MonoBehaviour
{
		private static bool __created = false;

		public int CurrentLevel;
		//public List<SpellScript> activeSkills;

		void Awake()
		{
				Debug.Log("Instance " + this.GetInstanceID());
				if (!__created)
				{
						// this is the first instance - make it persist
						DontDestroyOnLoad(this.gameObject);
						__created = true;
				}
				else
				{
						// this must be a duplicate from a scene reload - DESTROY!
						Debug.Log("Destroying me");
						DestroyImmediate(this.gameObject);
				}

				if (CurrentLevel == 0)
				{
						Debug.LogWarning("CurrentLevel is 0, probably forgot to set it in the editor");
				}
		}
}
