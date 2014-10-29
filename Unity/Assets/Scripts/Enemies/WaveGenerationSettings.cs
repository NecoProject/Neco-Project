using UnityEngine;
using System.Collections;

public class WaveGenerationSettings : MonoBehaviour
{
		// TODO: this should either come from a template / monster 
		// sheet, or be generated
		public float MonsterInitialHp = 6;
		public float MonsterInitialAttackSpeed = 1;
		public float MonsterInitialDamage = 1;
		public float BossFactor = 1.5f;

		public int NumberOfWaves = 2;
		public int LevelDifficulty = 1;
		public int MinimumEnemiesPerWave = 4;
		public int WaveEnemyModifier = 2;
}
