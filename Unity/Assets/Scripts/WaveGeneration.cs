using UnityEngine;
using System.Collections;

public class WaveGeneration : MonoBehaviour
{
		public Transform enemyModel;

		public float levelDurationInSeconds = 120;
		public int numberOfWaves = 4;
		public int levelDifficulty = 1;

		private int _currentWave = 0;

		public void GenerateLevel (int difficulty)
		{
				Debug.Log ("Generating level " + difficulty);
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
				float width = Screen.width, height = Screen.height;
				float enemyX = Random.Range (-width / 2, width / 2);
				float enemyY = Random.Range (-height / 2, height / 2);
				Debug.Log ("Generating enemy at position (" + enemyX + ", " + enemyY + ")");

				Instantiate (enemyModel, new Vector3 (enemyX, enemyY), Quaternion.identity);
		}
}
