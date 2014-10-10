using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class WaveGeneration : MonoBehaviour
{
		public WaveGenerationSettings Settings;

		public Transform enemyModel;
		public Transform enemySpawnArena;
		public Transform foreground;
		public Save Save;
		public GameObject EnemyWaveDestroyedText;

		private int _currentWave = 0;

		private float _minX, _maxX;
		private float _minY, _maxY;

		private List<Transform> _waveMonsters = new List<Transform>();

		void Awake()
		{
				_minX = enemySpawnArena.renderer.bounds.min.x;
				_maxX = enemySpawnArena.renderer.bounds.max.x;
				_minY = enemySpawnArena.renderer.bounds.min.y;
				_maxY = enemySpawnArena.renderer.bounds.max.y;
		}

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
				//Debug.Log("Generating level " + difficulty);
				this.Settings.LevelDifficulty = difficulty;
				GenerateNewWave();
		}

		void GenerateWave(int waveNumber, int difficulty)
		{
				//Debug.Log("Generating wave " + waveNumber);
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
						GenerateWave(_currentWave, this.Settings.LevelDifficulty);
						_currentWave++;
				}
				else if (_currentWave == Settings.NumberOfWaves)
				{
						SpawnBoss(Settings.LevelDifficulty);
						_currentWave++;
				}
				else
				{
						Messenger.Broadcast(EventNames.LEVEL_COMPLETE);
				}
		}

		void SpawnBoss(int levelDifficulty)
		{
				//Debug.Log("Spawning boss for difficulty " + levelDifficulty);
				Transform boss = GenerateBoss(levelDifficulty);
				_waveMonsters.Add(boss);
		}

		Transform GenerateEnemy(int difficulty)
		{
				float enemyX = Random.Range(_minX, _maxX);
				float enemyY = Random.Range(_minY, _maxY);
				Vector3 position = new Vector3(enemyX, enemyY);
				Transform monster = (Transform)Instantiate(enemyModel, position, Quaternion.identity);
				monster.parent = foreground;

				EnemyStats stats = monster.GetComponent<EnemyStats>();
				stats.MaxHp = Settings.MonsterInitialHp * difficulty;
				stats.AttackSpeed = Settings.MonsterInitialAttackSpeed;
				stats.Damage = Settings.MonsterInitialDamage * (1 + difficulty * 0.2f);

				return monster;
		}

		Transform GenerateBoss(int difficulty)
		{
				float enemyX = Random.Range(_minX, _maxX);
				float enemyY = Random.Range(_minY, _maxY);
				Vector3 position = new Vector3(enemyX, enemyY);

				Transform monster = (Transform)Instantiate(enemyModel, position, Quaternion.identity);
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
						StartCoroutine(EndOfWave());
				}
		}

		private IEnumerator EndOfWave()
		{
				if (_currentWave > Settings.NumberOfWaves)
				{
						EnemyWaveDestroyedText.GetComponent<Text>().text = "Level complete!!";
				}
				// Display victory text
				EnemyWaveDestroyedText.SetActive(true);

				yield return new WaitForSeconds(2f);

				EnemyWaveDestroyedText.SetActive(false);
				Messenger<int, int>.Broadcast(EventNames.WAVE_COMPLETE, _currentWave, Settings.NumberOfWaves);
				GenerateNewWave();
		}
}
