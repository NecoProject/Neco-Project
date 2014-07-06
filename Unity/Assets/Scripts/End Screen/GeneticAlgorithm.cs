using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GeneticAlgorithm
{
		private const float BASE_BONUS_MAX = 1.5f;

		public List<SkillStats> Evolve(SkillStats father, SkillStats mother, int difficultLevel)
		{
				List<SkillStats> children = Breed(father, mother);
				Mutate(children, difficultLevel);
				return children;
		}

		/// First version, very basic
		/// TODO: do we simply want to take all properties one by one, and merge them?
		/// I like the idea of each skill having a "genome", i.e. an array of values that then drives its characteristics.
		/// In other words, have the characteristics (each property) of a skill be computed from the skill "DNA", instead of us assigning values to them
		private List<SkillStats> Breed(SkillStats father, SkillStats mother)
		{
				List<SkillStats> children = new List<SkillStats>();
				for (int i = 0; i < 3; i++)
				{
						SkillStats stat = BreedSingleChild(father, mother);
						//Debug.Log("Bred: " + stat);
						children.Add(stat);
				}
				return children;
		}

		private SkillStats BreedSingleChild(SkillStats father, SkillStats mother)
		{
				SkillStats child = new SkillStats();

				child.damage = UnityEngine.Random.Range(Mathf.Min(father.damage, mother.damage), Mathf.Max(father.damage, mother.damage));

				return child;
		}

		/// Again, very basic implementation
		private void Mutate(List<SkillStats> children, int difficultyLevel)
		{
				foreach (SkillStats child in children)
				{
						Mutate(child, difficultyLevel);
				}
		}

		private void Mutate(SkillStats child, int difficultLevel)
		{
				float bonus = UnityEngine.Random.Range(-BASE_BONUS_MAX, BASE_BONUS_MAX) * difficultLevel;
				//Debug.Log("Bonus: " + bonus);
				child.damage = child.damage + bonus;
				//Debug.Log("Mutated: " + child);
		}
}