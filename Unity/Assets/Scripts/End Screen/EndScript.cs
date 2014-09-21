using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class EndScript : MonoBehaviour
{
		public GameObject CustomizationPanel;

		private  Save _savedData;
		private SurvivalOfTheFittest _fittestSkillSelection;
		private GeneticAlgorithm _geneticAlgorithm;
		private SkillBarItem _selectedSkill, _skillToReplace;
		private GameObject _selectedImage;

		void Start()
		{
				_savedData = FindObjectOfType<Save>();
				_fittestSkillSelection = new SurvivalOfTheFittest();
				_geneticAlgorithm = new GeneticAlgorithm();

				DisplayCurrentSkills(_savedData.SaveData.ActiveSkills);

				// Get the skills that will be used to create a new offspring
				Tuple<SkillStats, SkillStats> parentSkills = _fittestSkillSelection.GetParentSkills(_savedData.NumberOfUses);
				DisplayParentSkills(parentSkills.First, parentSkills.Second);

				// Generate the new skills
				List<SkillStats> newSkills = _geneticAlgorithm.Evolve(parentSkills.First, parentSkills.Second, _savedData.SaveData.CurrentLevel);
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

		public void DisplaySkillCustomization()
		{
				CustomizationPanel.SetActive(true);

				// TODO: Bad! Will need a proper handling of custom skill items, possibly
				// even generate them procedurally too, and offer 3-4 of them
				for (int i = 0; i < 4; i++)
				{
						GameObject.Find("SkillCusto" + i).GetComponent<Image>().sprite =
								GameObject.Find("PrefabManager").GetComponent<PrefabManager>().prefabs[i].Sprite;
				}
		}

		public void SelectImage(GameObject image)
		{
				if (_selectedImage == image)
				{
						_selectedImage.GetComponent<Outline>().enabled = false;
						_selectedImage = null;
				}
				else
				{
						if (_selectedImage != null)
						{
								_selectedImage.GetComponent<Outline>().enabled = false;
						}
						_selectedImage = image;
						_selectedImage.GetComponent<Outline>().enabled = true;
				}
				GameObject.Find("CustomizationConfirmationButton").GetComponent<Button>().interactable = _selectedImage != null;
		}

		public void ProceedToNextLevel()
		{
				// But first, build the data we want to propagate (and that we will persist and will use as a save game)
				_savedData.SaveData.CurrentLevel = _savedData.SaveData.CurrentLevel + 1;

				// And finally reload the level, with a new difficulty setting
				Application.LoadLevel("Stage1");
		}

		public void ValidateSkillChoice()
		{
				Debug.Log("Selected script " + _selectedSkill);
				SkillStats skill = _selectedSkill.GetSkill();
				skill.SkillName = GameObject.Find("SkillNameInput").GetComponent<Text>().text;
				skill.SpriteName = _selectedImage.GetComponent<Image>().sprite.name;

				Debug.Log("Skill name is " + skill.SkillName);
				int newIndex = _savedData.SaveData.ActiveSkills.IndexOf(_skillToReplace.GetSkill());
				_savedData.SaveData.SetSkillAt(skill, newIndex);

				ProceedToNextLevel();
		}
}