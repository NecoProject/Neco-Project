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

		private Save _save;
		private ISurvivalOfTheFittest _fittestSkillSelection;
		private IGeneticAlgorithm _geneticAlgorithm;

		void Start()
		{
				_save = FindObjectOfType<Save>();
				_fittestSkillSelection = GetFittestAlgorithm(_save.SaveData.Algorithm);
				_geneticAlgorithm = GetGeneticAlgorithm(_save.SaveData.Algorithm);

				DisplayCurrentSkills(_save.SaveData.ActiveSkills);

				// Get the skills that will be used to create a new offspring
				Tuple<SkillStats, SkillStats> parentSkills = _fittestSkillSelection.GetParentSkills(_save.NumberOfUses);
				DisplayParentSkills(parentSkills.First, parentSkills.Second);

				// Generate the new skills
				List<SkillStats> newSkills = _geneticAlgorithm.Evolve(parentSkills.First, parentSkills.Second, _save.SaveData.CurrentLevel);
				//,_save.SaveData.SkillAttributes);
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

		ISurvivalOfTheFittest GetFittestAlgorithm(Algorithm algo)
		{
				switch (algo.FittestAlgo)
				{
						case Algorithm.Fittest.MOST_USED:
						default:
								return new FittestIsMostUsed();
				}
		}

		IGeneticAlgorithm GetGeneticAlgorithm(Algorithm algo)
		{
				switch (algo.GeneticAlgo)
				{
						case Algorithm.Genetic.RANDOM:
						default:
								return new GeneticIsRandom();
				}
		}
}