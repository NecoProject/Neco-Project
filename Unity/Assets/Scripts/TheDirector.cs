using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TheDirector : MonoBehaviour
{
		public Save Save;

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
				Save = FindObjectOfType<Save>();
				SaveLoad.Save(Save.SaveData);
				if (Save.SaveData.IsCurrentLevelBossLevel)
				{
						GetComponentInChildren<WaveGeneration>().GenerateFinalBoss();
				}
				else
				{
						GetComponentInChildren<WaveGeneration>().GenerateLevel(Save.SaveData.CurrentLevel);
				}
		}

		void OnLevelComplete()
		{
				// Save the data we want to propagate to the next end screen and the level
				PlayerScript player = FindObjectOfType<PlayerScript>();
				Save.SaveData.ActiveSkills = player.activeSkills;

				// Serialize the data to retrieve it later on
				SaveLoad.Save(Save.SaveData);

				// Proceed to the elements needed for the "end of level" screen
				SkillBarItem[] skills = FindObjectsOfType<SkillBarItem>();
				Save.NumberOfUses.Clear();
				foreach (SkillBarItem skill in skills)
				{
						Save.NumberOfUses.Add(skill.GetSkill(), skill.NumberOfUses);
				}

				// Load the end screen
				Application.LoadLevel("LevelClear");
		}


}
