using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Display the skills on the ClearLevel screen
/// </summary>
public class SkillEvolution : MonoBehaviour
{
		public SkillBarItem Father, Mother;
		public List<SkillBarItem> Children;
		public List<SkillBarItem> Current;

		private Save _savedData;
		private SurvivalOfTheFittest _fittestSkillSelection;
		private GeneticAlgorithm _geneticAlgorithm;

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

		void DisplayParentSkills(SkillStats father, SkillStats mother)
		{
				Father.SetSkill(father);
				Mother.SetSkill(mother);
		}

		void DisplayNewSkills(List<SkillStats> newSkills)
		{
				// For now, use the default prefab
				for (int i = 0; i < newSkills.Count; i++)
				{
						SkillStats stat = newSkills[i];
						Children[i].SetSkill(stat);
				}
		}

		void DisplayCurrentSkills(List<SkillStats> currentSkills)
		{
				for (int i = 0; i < currentSkills.Count; i++)
				{
						SkillStats stat = currentSkills[i];
						Current[i].SetSkill(stat);
				}
		}

		
}