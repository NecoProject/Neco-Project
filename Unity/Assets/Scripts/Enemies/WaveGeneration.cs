using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class WaveGeneration : MonoBehaviour
{
		public WaveGenerationSettings Settings;

		public Transform enemySpawnArena;
		public Transform foreground;
		public GameObject EnemyWaveDestroyedText;

		public Transform Boss;
		public List<Transform> Enemies;

		private int _currentWave = 0;

		private float _minX, _maxX;
		private float _minY, _maxY;

		private List<Transform> _waveMonsters = new List<Transform>();
		private PrefabManager _prefabManager;
		private Save _save;

		void Awake()
		{
				_minX = enemySpawnArena.renderer.bounds.min.x;
				_maxX = enemySpawnArena.renderer.bounds.max.x;
				_minY = enemySpawnArena.renderer.bounds.min.y;
				_maxY = enemySpawnArena.renderer.bounds.max.y;

				_save = GameObject.Find("Save").GetComponent<Save>();

				_prefabManager = GameObject.Find("PrefabManager").GetComponent<PrefabManager>();
				Boss = _prefabManager.GetBossPrefab(_save.SaveData.Boss);
				Enemies = _prefabManager.GetEnemiesPrefab(_save.SaveData.Enemies);
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
				int index = Random.Range(0, Enemies.Count - 1);
				Transform enemyModel = Enemies[index];

				Transform monster = (Transform)Instantiate(enemyModel);
				monster.parent = foreground;

				Vector3 position = GeneratePosition(monster);
				monster.position = position;

				// TODO: don't have general settings for monsters, each enemy should have their own stats.
				// Use that only to scale
				EnemyStats stats = monster.GetComponent<EnemyStats>();
				stats.MaxHp = Settings.MonsterInitialHp * (1 + difficulty * 0.3f);
				stats.AttackSpeed = Settings.MonsterInitialAttackSpeed;
				stats.Damage = Settings.MonsterInitialDamage * (1 + difficulty * 0.15f);

				return monster;
		}

		Transform GenerateBoss(int difficulty)
		{
				Transform enemyModel = Enemies[Random.Range(0, Enemies.Count - 1)];


				Transform monster = (Transform)Instantiate(enemyModel);
				monster.localScale = new Vector3(2 * monster.localScale.x, 2 * monster.localScale.y);
				monster.parent = foreground;

				Vector3 position = GeneratePosition(monster);
				monster.position = position;

				EnemyStats stats = monster.GetComponent<EnemyStats>();
				stats.MaxHp = Settings.MonsterInitialHp * Settings.BossFactor * (1 + difficulty * 0.3f);
				stats.AttackSpeed = Settings.MonsterInitialAttackSpeed * Settings.BossFactor;
				stats.Damage = Settings.MonsterInitialDamage * Settings.BossFactor * (1 + difficulty * 0.15f);

				return monster;
		}

		// Assumption is that the pivot point is always at the bottom center
		Vector3 GeneratePosition(Transform monster)
		{
				Vector3 size = monster.renderer.bounds.size;
				float enemyX = Random.Range(_minX + size.x / 2, _maxX - size.x / 2);
				float enemyY = Random.Range(_minY, _maxY - size.y);


				Vector3 position = new Vector3(enemyX, enemyY);
				return position;
		}

		void OnMonsterKilled(HealthPointScript monster)
		{
				_waveMonsters.Remove(monster.transform);

				if (monster.gameObject.GetComponent<EnemyStats>().IsBoss)
				{
						//_save.UnlockedSkill = monster.gameObject.GetComponent<EnemyBehaviour>().KillReward();
						Messenger.Broadcast(EventNames.LEVEL_COMPLETE);
				}
				else if (_waveMonsters.Count == 0)
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

		public void GenerateFinalBoss()
		{
				// Center vertically, and on the right
				Vector3 position = new Vector3(_maxX, (_minY + _maxY) / 2);

				Transform boss = (Transform)Instantiate(Boss, position, Quaternion.identity);
				boss.parent = foreground;

				_waveMonsters.Add(boss);
		}
}
