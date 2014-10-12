using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameOver : MonoBehaviour
{
		private Save _save;

		void Start()
		{
				_save = FindObjectOfType<Save>();
		}

		public void FightLevel()
		{
				_save.SaveData.IsCurrentLevelBossLevel = false;

				// And finally reload the level, with a new difficulty setting
				Application.LoadLevel(SceneNames.LEVEL);
		}

		public void FightPreviousLevel()
		{
				_save.SaveData.CurrentLevel = _save.SaveData.CurrentLevel - 1;
				_save.SaveData.IsCurrentLevelBossLevel = false;

				// And finally reload the level, with a new difficulty setting
				Application.LoadLevel(SceneNames.LEVEL);
		}

		public void FightBoss()
		{
				_save.SaveData.IsCurrentLevelBossLevel = true;

				// And finally reload the level, with a new difficulty setting
				Application.LoadLevel(SceneNames.LEVEL);
		}
}