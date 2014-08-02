using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class TheDirector : MonoBehaviour
{

		public Save SavedData;

		void OnEnable()
		{
				Messenger.AddListener(EventNames.LEVEL_COMPLETE, OnLevelComplete);
		}

		void OnDisable()
		{
				Messenger.RemoveListener(EventNames.LEVEL_COMPLETE, OnLevelComplete);
		}

		void Start()
		{
				GetComponentInChildren<WaveGeneration>().GenerateLevel(SavedData.CurrentLevel);
				SavedData = FindObjectOfType<Save>();
		}

		void OnLevelComplete()
		{
				// Save the data we want to propagate to the next end screen and the level
				PlayerScript player = FindObjectOfType<PlayerScript>();
				SavedData.activeSkills = player.activeSkills;

				// Load the end screen
				Application.LoadLevel("LevelClear");
		}


}
