using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EndScript : MonoBehaviour
{
		private Save _savedData;

		void Start()
		{
				_savedData = FindObjectOfType<Save>();

				List<SpellScript> parentSkills = GetParentSkills();
				DisplayParentSkills(parentSkills);
				// List<SpellScript> newSkills = Evolve(mostUsedSkillsInLastLevel);
				// DisplayNewSkills(newSkills);
		}

		void Update()
		{
				// Leave the end game screen and load the next level
				if (Input.GetKeyDown(KeyCode.Escape))
				{
						// But first, build the data we want to propagate (and that we will persist and will use as a save game)
						_savedData.CurrentLevel = _savedData.CurrentLevel + 1;

						// And finally reload the level, with a new difficulty setting
						Application.LoadLevel("Stage1");
				}
		}

		/// Not sure how we should determine the skills that should serve as parents. Ideally, it 
		/// would be the "most used skilled", but how do you really measure that? If you're against 
		/// a boss, and have two skills, one with a DoT and a high cooldown, the other a rapid 
		/// fire, and do the DoT as much as possible, and fire only from time to time. The fire 
		/// will have been used most, but in fact it's the DoT that's most used. Maybe something along 
		/// the lines of "the skill that has been most used compared to how often it could be used"?
		/// For now, only pick two skills at random
		private List<SpellScript> GetParentSkills()
		{
				List<SpellScript> activeSkills = _savedData.activeSkills;
				Debug.Log(activeSkills);
				Debug.Log(activeSkills.Count);

				// For now, take two random parents
				List<SpellScript> parents = new List<SpellScript>();
				SpellScript skill1 = activeSkills[UnityEngine.Random.Range(0, activeSkills.Count)];
				Debug.Log(skill1);
				parents.Add(skill1);
				SpellScript skill2 = activeSkills[UnityEngine.Random.Range(0, activeSkills.Count)];
				Debug.Log(skill2);
				parents.Add(skill2);
				Debug.Log(parents);

				return parents;
		}

		private void DisplayParentSkills(List<SpellScript> parents)
		{
				GameObject parentContainer = GameObject.Find("Parent Skills");
				SkillBarItem[] skillContainers = parentContainer.GetComponentsInChildren<SkillBarItem>();
				skillContainers[0].SetSkill(parents[0]);
				skillContainers[1].SetSkill(parents[1]);
		}
}