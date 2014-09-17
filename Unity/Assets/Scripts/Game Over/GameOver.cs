using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameOver : MonoBehaviour
{
		public void Retry()
		{
				// And finally reload the level, with a new difficulty setting
				Application.LoadLevel("Stage1");
		}
}