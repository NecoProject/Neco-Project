using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveGeneration : MonoBehaviour
{
		public Transform enemyModel;
		public Transform foreground;
		public Save Save;

		public int NumberOfWaves = 2;
		public int LevelDifficulty = 1;
		public int MinimumEnemiesPerWave = 4;
		public int WaveEnemyModifier = 2;

		private int _currentWave = 1;


		private List<Transform> _waveMonsters = new List<Transform>();


		void OnEnable()
		{
				Messenger<HealthPointScript>.AddListener(EventNames.MONSTER_KILLED, OnMonsterKilled);
		}

		void OnDisable()
		{
				Messenger<HealthPointScript>.RemoveListener(EventNames.MONSTER_KILLED, OnMonsterKilled);
		}

		public void GenerateLevel(int difficulty)
		{
				Debug.Log("Generating level " + difficulty);
				this.LevelDifficulty = difficulty;
				GenerateWave(_currentWave);
		}

		void GenerateWave(int waveNumber)
		{
				Debug.Log("Generating wave " + waveNumber);
				int numberOfEnemiesInWave = MinimumEnemiesPerWave + (WaveEnemyModifier * waveNumber);
				for (int i = 0; i < numberOfEnemiesInWave; i++)
				{
						Transform monster = GenerateEnemy();
						_waveMonsters.Add(monster);
				}
		}

		void GenerateNewWave()
		{
				if (_currentWave < NumberOfWaves)
				{
						_currentWave++;
						GenerateWave(_currentWave);
				}
				else
				{
						Messenger.Broadcast(EventNames.LEVEL_COMPLETE);
				}
		}

		Transform GenerateEnemy()
		{
				float width = Screen.width, height = Screen.height;
				float enemyX = Random.Range(0, width);
				float enemyY = Random.Range(0, height);
				Vector3 position = new Vector3(enemyX, enemyY);

				// Get the correct Z, because the current one is the Camera, circa -10
				Vector3 spaceTarget = Camera.main.ScreenToWorldPoint(position);
				spaceTarget.z = 0;

				Transform monster = (Transform)Instantiate(enemyModel, spaceTarget, Quaternion.identity);
				Debug.Log(monster.GetType());
				monster.parent = foreground;

				return monster;
		}

		void OnMonsterKilled(HealthPointScript monster)
		{
				_waveMonsters.Remove(monster.transform);

				if (_waveMonsters.Count == 0)
				{
						Debug.Log("Generating new wave");
						GenerateNewWave();
				}
		}
}
