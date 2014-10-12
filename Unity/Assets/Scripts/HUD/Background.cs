using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class Background : MonoBehaviour
{
		public SpriteRenderer BackgroundRenderer;
		public SpriteRenderer ArenaRenderer;

		private Save _save;
		private PrefabManager _prefabManager;

		void Start()
		{
				_save = FindObjectOfType<Save>();
				_prefabManager = FindObjectOfType<PrefabManager>();

				if (_save.SaveData.IsCurrentLevelBossLevel)
				{
						BackgroundRenderer.sprite = _prefabManager.GetImageSprite("boss-background");
						ArenaRenderer.sprite = _prefabManager.GetImageSprite("boss-arena");
				}
				else
				{
						BackgroundRenderer.sprite = _prefabManager.GetImageSprite("level-background");
						ArenaRenderer.sprite = _prefabManager.GetImageSprite("level-arena");
				}
		}
}
