﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class HomeScreen : MonoBehaviour
{
		public GameObject ContinueButton;

		void Start()
		{
				ContinueButton.GetComponent<Button>().interactable = SaveLoad.HasSavedGame();
		}
		public void StartNewGame()
		{
				Application.LoadLevel(SceneNames.LEVEL_INIT);
		}

		public void LoadLastGame()
		{
				// Retrieved saved data

				// Already instanciate a Save object, and feed it the data
				Save save = GameObject.Find("Save").GetComponent<Save>();
				save.SaveData = SaveLoad.Load();

				// Load the level
				Application.LoadLevel(SceneNames.LEVEL);
		}

}