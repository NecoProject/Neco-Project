using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class Victory : MonoBehaviour
{
		private Save _save;

		void Start()
		{
				_save = FindObjectOfType<Save>();
		}

		public void NewGame()
		{
				_save.Init();

				// And finally reload the level, with a new difficulty setting
				Application.LoadLevel("Stage1");
		}
}