using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class Victory : MonoBehaviour
{
		public Image UnlockedAttributeImage;

		private Save _save;
		[SerializeField]
		private SkillAttribute _unlockedAttribute;
		private PrefabManager _prefabManager;

		void Start()
		{
				_save = FindObjectOfType<Save>();
				_unlockedAttribute = _save.SaveData.LastUnlockedAttribute;
				_prefabManager = GameObject.Find("PrefabManager").GetComponent<PrefabManager>();

				if (_unlockedAttribute != null)
				{
						UnlockedAttributeImage.sprite = _prefabManager.GetAttributeSprite(_unlockedAttribute.Icon);
				}
		}

		public void NewGame()
		{
				// Add it to the list if never unlocked, TODO: increase its stats if already present
				if (_unlockedAttribute != null && !_save.SaveData.UnlockedAttributes.Contains(_unlockedAttribute.AttributeType))
				{
						_save.SaveData.UnlockedAttributes.Add(_unlockedAttribute.AttributeType);
				}
				_save.Init();


				// And finally reload the level, with a new difficulty setting
				Application.LoadLevel(SceneNames.LEVEL_INIT);
		}
}