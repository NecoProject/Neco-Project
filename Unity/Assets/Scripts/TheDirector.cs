using UnityEngine;
using System.Collections;

public class TheDirector : MonoBehaviour
{

		public int CurrentLevel = 1;

		void Start()
		{
				GetComponentInChildren<WaveGeneration>().GenerateLevel(CurrentLevel);
		}

}
