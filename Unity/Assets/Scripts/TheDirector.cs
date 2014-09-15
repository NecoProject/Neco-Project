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
				SavedData = FindObjectOfType<Save>();
				GetComponentInChildren<WaveGeneration>().GenerateLevel(SavedData.CurrentLevel);
		}

		void OnLevelComplete()
		{
				// Save the data we want to propagate to the next end screen and the level
				PlayerScript player = FindObjectOfType<PlayerScript>();
				SavedData.ActiveSkills = player.activeSkills;

				SkillBarItem[] skills = FindObjectsOfType<SkillBarItem>();
				SavedData.NumberOfUses.Clear();
				foreach (SkillBarItem skill in skills)
				{
						SavedData.NumberOfUses.Add(skill.GetSkill(), skill.NumberOfUses);
				}

				// Load the end screen
				Application.LoadLevel("LevelClear");
		}


}
