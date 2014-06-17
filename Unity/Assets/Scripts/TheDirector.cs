using UnityEngine;
using System.Collections;

public class TheDirector : MonoBehaviour
{
		void Start ()
		{
				GetComponentInChildren<WaveGeneration> ().GenerateLevel (1);
		}

}
