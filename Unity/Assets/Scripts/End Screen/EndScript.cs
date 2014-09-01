using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class EndScript : MonoBehaviour
{
		private Save _savedData;
		private SurvivalOfTheFittest _fittestSkillSelection;
		private GeneticAlgorithm _geneticAlgorithm;
		private SkillStats _selectedSkill;


		void OnEnable()
		{
				Messenger<SkillBarItem>.AddListener(EventNames.SKILL_CLICKED, OnSkillClicked);
		}

		void OnDisable()
		{
				Messenger<SkillBarItem>.RemoveListener(EventNames.SKILL_CLICKED, OnSkillClicked);
		}

		void Start()
		{
				_savedData = FindObjectOfType<Save>();
				_fittestSkillSelection = new SurvivalOfTheFittest();
				_geneticAlgorithm = new GeneticAlgorithm();


				// Get the skills that will be used to create a new offspring
				List<SkillStats> activeSkills = _savedData.activeSkills;

				Tuple<SkillStats, SkillStats> parentSkills = _fittestSkillSelection.GetParentSkills(activeSkills);
				DisplayParentSkills(parentSkills.First, parentSkills.Second);

				// Generate the new skills
				List<SkillStats> newSkills = _geneticAlgorithm.Evolve(parentSkills.First, parentSkills.Second, _savedData.CurrentLevel);
				//Debug.Log(newSkills.Count);

				// Offer the player the ability to choose one to replace an existing skill
				DisplayNewSkills(newSkills);
		}

		void Update()
		{
				// Leave the end game screen and load the next level
				if (Input.GetKeyDown(KeyCode.Escape))
				{
						ProceedToNextLevel();
				}
				// Save the new skill and proceed to next level
				// TODO: for now, always replace the last skill
				else if (Input.GetKeyDown(KeyCode.Return))
				{
						Debug.Log("Selected script " + _selectedSkill);
						_savedData.activeSkills[3] = _selectedSkill;

						ProceedToNextLevel();
				}
		}

		private void ProceedToNextLevel()
		{
				// But first, build the data we want to propagate (and that we will persist and will use as a save game)
				_savedData.CurrentLevel = _savedData.CurrentLevel + 1;

				// And finally reload the level, with a new difficulty setting
				Application.LoadLevel("Stage1");
		}

		private void DisplayParentSkills(SkillStats father, SkillStats mother)
		{
				GameObject.Find("Father").GetComponent<SkillBarItem>().SetSkill(father);
				GameObject.Find("Mother").GetComponent<SkillBarItem>().SetSkill(father);
		}

		private void DisplayNewSkills(List<SkillStats> newSkills)
		{
				// For now, use the default prefab
				// TODO: validate that there are as many possible children as skill bar items
				for (int i = 0; i < newSkills.Count; i++)
				{
						SkillStats stat = newSkills[i];
						stat.Name = "New Skill";
						stat.SpriteName = "fireball-red-3";
						GameObject.Find("Child" + i).GetComponent<SkillBarItem>().SetSkill(stat);
				}
		}

		private void OnSkillClicked(SkillBarItem skill)
		{
				GameObject.Find("Selected").GetComponent<Text>().text = "Cost: " + skill.skill.Cost + "\n" + "Damage: " + skill.skill.Damage;
				if (_selectedSkill == null)
				{
						GameObject.Find("Children Text").GetComponent<Text>().text += "\n(Enter to validate)";
				}
				_selectedSkill = skill.skill;
		}


}