using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InitRandomEnemies : MonoBehaviour
{
		public List<Image> EnemyIcons;
		public PrefabManager PrefabManager;

		public List<string> Enemies = new List<string>();

		void Start()
		{
				// Basic random
				List<PrefabDico> copies = new List<PrefabDico>(PrefabManager.Enemies);
				for (int i = 0; i < EnemyIcons.Count; i++)
				{
						int index = UnityEngine.Random.Range(0, copies.Count - 1);
						PrefabDico prefab = copies[index];
						copies.RemoveAt(index);
						EnemyIcons[i].sprite = prefab.Sprite;
						Enemies.Add(prefab.Name);
				}
		}
}
