using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveGeneration : MonoBehaviour
{
		public WaveGenerationSettings Settings;

		public Transform enemyModel;
		public Transform foreground;
		public Save Save;

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
				this.Settings.LevelDifficulty = difficulty;
				GenerateWave(_currentWave, difficulty);
		}

		void GenerateWave(int waveNumber, int difficulty)
		{
				Debug.Log("Generating wave " + waveNumber);
				int numberOfEnemiesInWave = Settings.MinimumEnemiesPerWave + (Settings.WaveEnemyModifier * waveNumber);
				for (int i = 0; i < numberOfEnemiesInWave; i++)
				{
						Transform monster = GenerateEnemy(difficulty);
						_waveMonsters.Add(monster);
				}
		}

		void GenerateNewWave()
		{
				if (_currentWave < Settings.NumberOfWaves)
				{
						_currentWave++;
						GenerateWave(_currentWave, this.Settings.LevelDifficulty);
				}
				else if (_currentWave == Settings.NumberOfWaves)
				{
						_currentWave++;
						SpawnBoss(Settings.LevelDifficulty);
				}
				else
				{
						Messenger.Broadcast(EventNames.LEVEL_COMPLETE);
				}
		}

		void SpawnBoss(int levelDifficulty)
		{
				Debug.Log("Spawning boss for difficulty " + levelDifficulty);
				Transform boss = GenerateBoss(levelDifficulty);
				_waveMonsters.Add(boss);
		}

		Transform GenerateEnemy(int difficulty)
		{
				float width = Screen.width, height = Screen.height;
				float enemyX = Random.Range(0, width);
				float enemyY = Random.Range(0, height);
				Vector3 position = new Vector3(enemyX, enemyY);

				// Get the correct Z, because the current one is the Camera, circa -10
				Vector3 spaceTarget = Camera.main.ScreenToWorldPoint(position);
				spaceTarget.z = 0;

				Transform monster = (Transform)Instantiate(enemyModel, spaceTarget, Quaternion.identity);
				monster.parent = foreground;

				EnemyStats stats = monster.GetComponent<EnemyStats>();
				stats.MaxHp = Settings.MonsterInitialHp * difficulty;
				stats.AttackSpeed = Settings.MonsterInitialAttackSpeed;
				stats.Damage = Settings.MonsterInitialDamage * (1 + difficulty * 0.2f);

				return monster;
		}

		Transform GenerateBoss(int difficulty)
		{
				float width = Screen.width, height = Screen.height;
				float enemyX = Random.Range(0, width);
				float enemyY = Random.Range(0, height);
				Vector3 position = new Vector3(enemyX, enemyY);

				// Get the correct Z, because the current one is the Camera, circa -10
				Vector3 spaceTarget = Camera.main.ScreenToWorldPoint(position);
				spaceTarget.z = 0;

				Transform monster = (Transform)Instantiate(enemyModel, spaceTarget, Quaternion.identity);
				monster.localScale = new Vector3(2 * monster.localScale.x, 2 * monster.localScale.y);
				monster.parent = foreground;

				EnemyStats stats = monster.GetComponent<EnemyStats>();
				stats.MaxHp = Settings.MonsterInitialHp * Settings.BossFactor * difficulty;
				stats.AttackSpeed = Settings.MonsterInitialAttackSpeed * Settings.BossFactor;
				stats.Damage = Settings.MonsterInitialDamage * Settings.BossFactor * (1 + difficulty * 0.2f);

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
