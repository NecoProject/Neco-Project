﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GeneticAlgorithm
{
		// TODO: pass the script as a MonoBehaviour, so that it can be tweaked in the editor directly? 
		// Not sure we can easily experiment, since it requires an "end level" screen anyway
		private const float BASE_BONUS_MAX = .5f;

		private const int MAX_CHILDREN_NUMBER = 3; 

		public List<SkillStats> Evolve (SkillStats father, SkillStats mother, int difficultLevel)
		{
				List<SkillStats> children = Breed (father, mother);
				Mutate (children, difficultLevel);
				return children;
		}

		/// First version, very basic
		/// TODO: do we simply want to take all properties one by one, and merge them?
		/// I like the idea of each skill having a "genome", i.e. an array of values that then drives its characteristics.
		/// In other words, have the characteristics (each property) of a skill be computed from the skill "DNA", instead of us assigning values to them
		/// See IdeasToDiscuss.txt
		private List<SkillStats> Breed (SkillStats father, SkillStats mother)
		{
				List<SkillStats> children = new List<SkillStats> ();
				for (int i = 0; i < MAX_CHILDREN_NUMBER; i++) {
						SkillStats stat = BreedSingleChild (father, mother);
						//Debug.Log("Bred: " + stat);
						children.Add (stat);
				}
				return children;
		}

		private SkillStats BreedSingleChild (SkillStats father, SkillStats mother)
		{
				SkillStats child = new SkillStats ();

				child.Damage = GenerateValueFromParentsAndLuck (father.Damage, mother.Damage);
				child.Cost = GenerateValueFromParentsAndLuck (father.Cost, mother.Cost);
				child.SpriteName = PickOne (father.SpriteName, mother.SpriteName);
				/*child.Name = */

				return child;
		}

		private string PickOne (string fatherValue, string motherValue)
		{
				float discriminant = UnityEngine.Random.value;
				if (discriminant < 0.5) {
						return fatherValue;
				}
				return motherValue;
		}

		private float GenerateValueFromParentsAndLuck (float fatherValue, float motherValue)
		{
				return UnityEngine.Random.Range (Mathf.Min (fatherValue, motherValue), Mathf.Max (fatherValue, motherValue));
		}

		/// Again, very basic implementation
		private void Mutate (List<SkillStats> children, int difficultyLevel)
		{
				foreach (SkillStats child in children) {
						Mutate (child, difficultyLevel);
				}
		}

		/// Mutate the provided child's statistics taking into account the difficulty. The higher the difficulty, the higher the mutation effects. 
		/// TODO: Do we want to center the mutation around 0? Statistically we may result in pure better or pure worse stats afterwards.
		private void Mutate (SkillStats child, int difficultLevel)
		{
				child.Damage = child.Damage + child.Damage * UnityEngine.Random.Range (-BASE_BONUS_MAX, BASE_BONUS_MAX) * difficultLevel;
				child.Cost = child.Cost + child.Cost * UnityEngine.Random.Range (-BASE_BONUS_MAX, BASE_BONUS_MAX) * difficultLevel;
				//Debug.Log("Mutated: " + child);
		}
}