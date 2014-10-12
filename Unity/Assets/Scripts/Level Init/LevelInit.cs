using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LevelInit : MonoBehaviour
{
		public InitRandomAlgorithm AlgorithmChoice;
		public InitRandomBoss BossChoice;
		public InitRandomEnemies EnemiesChoice;
		public InitRandomSkillAttributes SkillsChoice;

		private Save _save;

		void Start()
		{
				_save = FindObjectOfType<Save>();
		}

		public void StartGame()
		{
				_save.SaveData.Enemies = EnemiesChoice.Enemies;
				_save.SaveData.Boss = BossChoice.Boss;
				_save.SaveData.Algorithm = AlgorithmChoice.Algorithm;
				_save.SaveData.SkillAttributes = SkillsChoice.SkillAttributes;

				Application.LoadLevel(SceneNames.LEVEL);
		}
}
