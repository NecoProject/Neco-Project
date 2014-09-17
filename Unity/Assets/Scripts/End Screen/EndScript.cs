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
		private SkillBarItem _selectedSkill, _skillToReplace;

		void Start()
		{
				_savedData = FindObjectOfType<Save>();
				_fittestSkillSelection = new SurvivalOfTheFittest();
				_geneticAlgorithm = new GeneticAlgorithm();

				DisplayCurrentSkills(_savedData.ActiveSkills);

				// Get the skills that will be used to create a new offspring
				Tuple<SkillStats, SkillStats> parentSkills = _fittestSkillSelection.GetParentSkills(_savedData.NumberOfUses);
				DisplayParentSkills(parentSkills.First, parentSkills.Second);

				// Generate the new skills
				List<SkillStats> newSkills = _geneticAlgorithm.Evolve(parentSkills.First, parentSkills.Second, _savedData.CurrentLevel);
				//Debug.Log(newSkills.Count);

				// Offer the player the ability to choose one to replace an existing skill
				DisplayNewSkills(newSkills);
		}

		private void DisplayParentSkills(SkillStats father, SkillStats mother)
		{
				GameObject.Find("Father").GetComponent<SkillBarItem>().SetSkill(father);
				GameObject.Find("Mother").GetComponent<SkillBarItem>().SetSkill(mother);
		}

		private void DisplayNewSkills(List<SkillStats> newSkills)
		{
				// For now, use the default prefab
				for (int i = 0; i < newSkills.Count; i++)
				{
						SkillStats stat = newSkills[i];
						stat.SkillName = "New Skill";
						GameObject.Find("Child" + i).GetComponent<SkillBarItem>().SetSkill(stat);
				}
		}

		private void DisplayCurrentSkills(List<SkillStats> currentSkills)
		{
				for (int i = 0; i < currentSkills.Count; i++)
				{
						SkillStats stat = currentSkills[i];
						GameObject.Find("Current" + i).GetComponent<SkillBarItem>().SetSkill(stat);
				}
		}

		/*private void OnSkillClicked(SkillBarItem skill)
		{
				GameObject.Find("Selected").GetComponent<Text>().text =
						"Cost: " + skill.GetSkill().Cost.ToString("f1") + "\n" +
						"Damage: " + skill.GetSkill().Damage.ToString("f1") + "\n" +
						"Cooldown: " + skill.GetSkill().CoolDown.ToString("f1") + "\n" +
						"Radius: " + skill.GetSkill().Radius.ToString("f1")
						;
				if (_selectedSkill == null)
				{
						GameObject.Find("Children Text").GetComponent<Text>().text += "\n(Enter to validate)";
				}
				_selectedSkill = skill.GetSkill();
		}*/

		public void ChooseNewSkill(SkillBarItem skill)
		{
				if (_selectedSkill == skill)
				{
						_selectedSkill.GetComponent<Outline>().enabled = false;
						_selectedSkill = null;
				}
				else
				{
						if (_selectedSkill != null)
						{
								_selectedSkill.GetComponent<Outline>().enabled = false;
						}
						_selectedSkill = skill;
						_selectedSkill.GetComponent<Outline>().enabled = true;
				}
		}

		public void ChooseSkillToReplace(SkillBarItem skill)
		{
				if (_skillToReplace == skill)
				{
						_skillToReplace.GetComponent<Outline>().enabled = false;
						_skillToReplace = null;
				}
				else
				{
						if (_skillToReplace != null)
						{
								_skillToReplace.GetComponent<Outline>().enabled = false;
						}
						_skillToReplace = skill;
						_skillToReplace.GetComponent<Outline>().enabled = true;
				}
		}

		public void ShouldEnableConfirmation()
		{
				GameObject.Find("Select skill").GetComponent<Button>().interactable = (_selectedSkill != null && _skillToReplace != null);
		}

		public void ProceedToNextLevel()
		{
				// But first, build the data we want to propagate (and that we will persist and will use as a save game)
				_savedData.CurrentLevel = _savedData.CurrentLevel + 1;

				// And finally reload the level, with a new difficulty setting
				Application.LoadLevel("Stage1");
		}

		public void ValidateSkillChoice()
		{
				Debug.Log("Selected script " + _selectedSkill);
				int newIndex = _savedData.ActiveSkills.IndexOf(_skillToReplace.GetSkill());
				_savedData.SetSkillAt(_selectedSkill.GetSkill(), newIndex);

				ProceedToNextLevel();
		}
}