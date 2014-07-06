using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EndScript : MonoBehaviour
{
		public SpellScript DefaultPrefab;
		public GameObject ChildrenContainer;

		private Save _savedData;
		private SurvivalOfTheFittest _fittestSkillSelection;
		private GeneticAlgorithm _geneticAlgorithm;


		void Start()
		{
				_savedData = FindObjectOfType<Save>();
				_fittestSkillSelection = new SurvivalOfTheFittest();
				_geneticAlgorithm = new GeneticAlgorithm();

				// Get the skills that will be used to create a new offspring
				Tuple<SpellScript, SpellScript> parentSkills = _fittestSkillSelection.GetParentSkills(_savedData.activeSkills);
				DisplayParentSkills(parentSkills.First, parentSkills.Second);

				// Generate the new skills
				List<SkillStats> newSkills = _geneticAlgorithm.Evolve(parentSkills.First.Stats, parentSkills.Second.Stats, _savedData.CurrentLevel);
				Debug.Log(newSkills.Count);

				// Offer the player the ability to choose one to replace an existing skill
				DisplayNewSkills(newSkills);
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

		private void DisplayParentSkills(SpellScript father, SpellScript mother)
		{
				GameObject parentContainer = GameObject.Find("Parent Skills");
				SkillBarItem[] skillContainers = parentContainer.GetComponentsInChildren<SkillBarItem>();
				skillContainers[0].SetSkill(father);
				skillContainers[1].SetSkill(mother);
		}

		private void DisplayNewSkills(List<SkillStats> newSkills)
		{
				SkillBarItem[] skillBars = ChildrenContainer.GetComponentsInChildren<SkillBarItem>();
				Debug.Log(skillBars.Length);
				// For now, use the default prefab
				// TODO: validate that there are as many possible children as skill bar items
				for (int i = 0; i < skillBars.Length; i++ )
				{
						SkillBarItem skillBar = skillBars[i];
						//Debug.Log("SkillBarItem: " + skillBar);
						SkillStats stat = newSkills[i];
						//Debug.Log("SkillStats: " + stat);

						SpellScript skill = (SpellScript) Instantiate(DefaultPrefab, new Vector3(0, 0, 0), Quaternion.identity);
						//Debug.Log("Skill: " + skill);
						skill.Stats = stat;

						skillBar.SetSkill(skill);
				}
		}
}