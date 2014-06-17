using UnityEngine;
using System.Collections;

public class WaveGeneration : MonoBehaviour
{
		public float levelDurationInSeconds = 120;
		public int numberOfWaves = 4;
		public int levelDifficulty = 1;

		private int _currentWave = 0;

		public void GenerateLevel (int difficulty)
		{
				this.levelDifficulty = difficulty;
				GenerateWave (_currentWave);
		}

		void GenerateWave (int waveNumber)
		{
				int numberOfEnemiesInWave = 4 * (1 + 2 * waveNumber);
				for (int i = 0; i < numberOfEnemiesInWave; i++) {
						GenerateEnemy ();
				}
		}

		void GenerateEnemy ()
		{

		}
}
