using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EndScript : MonoBehaviour
{
		public int CurrentLevel;

		void Update()
		{
				if (Input.GetKeyDown(KeyCode.Escape))
				{
						Application.LoadLevel("Stage1");
				}
		}
}