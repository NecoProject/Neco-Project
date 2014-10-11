using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InitRandomBoss : MonoBehaviour
{
		public Image BossIcon;
		public PrefabManager PrefabManager;
		
		public string Boss;

		void Start()
		{
				// Basic random
				PrefabDico prefab = PrefabManager.Boss[UnityEngine.Random.Range(0, PrefabManager.Boss.Count - 1)];
				Boss = prefab.Name;
				BossIcon.sprite = prefab.Sprite;
		}
}
