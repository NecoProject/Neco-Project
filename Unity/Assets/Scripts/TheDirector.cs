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
				//SavedData.activeSkills = player.activeSkills;

				SkillBarItem[] skillItems = SavedData.GetComponents<SkillBarItem>();
				/*Debug.Log("On level complete  before destroy");
				foreach (SkillBarItem sk in SavedData.GetComponents<SkillBarItem>())
				{
						Debug.Log(sk.skill);
				};*/

				List<SpellScript> activeSkills = new List<SpellScript>();
				foreach (SkillBarItem skillItem in skillItems)
				{
						Destroy(skillItem);
				}

				// Put them back 
				//EditorApplication.isPaused = true;

				/*Debug.Log("Logging player skills");*/
				foreach (SpellScript skill in player.activeSkills)
				{
						/*Debug.Log("skill is " + skill);*/
						SkillBarItem skillItem = SavedData.gameObject.AddComponent<SkillBarItem>();
						skillItem.skill = skill;
				}

				/*Debug.Log("On level complete");
				foreach (SkillBarItem sk in SavedData.GetComponents<SkillBarItem>())
				{
						Debug.Log(sk.skill);
				};*/
				// Load the end screen
				Application.LoadLevel("LevelClear");
		}


}
