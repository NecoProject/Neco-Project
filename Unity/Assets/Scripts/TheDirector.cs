using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
		}



		void OnLevelComplete()
		{
				// Save the data we want to propagate to the next end screen and the level
				PlayerScript player = FindObjectOfType<PlayerScript>();
				//SavedData.activeSkills = player.activeSkills;

				SkillBarItem[] skillItems = SavedData.GetComponents<SkillBarItem>();
				List<SpellScript> activeSkills = new List<SpellScript>();
				foreach (SkillBarItem skillItem in skillItems)
				{
						Destroy(skillItem);
				}

				// Put them back 
				foreach (SpellScript skill in player.activeSkills)
				{
						SavedData.gameObject.AddComponent<SkillBarItem>();
						SavedData.gameObject.GetComponent<SkillBarItem>().skill = skill;

				}

				// Load the end screen
				Application.LoadLevel("LevelClear");
		}


}
