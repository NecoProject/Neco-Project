using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class GeneticIsRandom : IGeneticAlgorithm
{
		private Breeder _breeder = new Breeder();
		private Mutator _mutator = new Mutator();

		public List<SkillStats> Evolve(List<SkillStats> parents, int difficultLevel, List<SkillAttribute.Type> availableAttributes)
		{
				if (parents.Count != 2)
				{
						throw new Exception("Invalid number of parents, should be 2, was " + parents.Count);
				}
				SkillStats father = parents[0];
				SkillStats mother = parents[1];
				List<SkillStats> children = Breed(father, mother);
				Mutate(children, availableAttributes);
				return children;
		}

		/// First version, very basic
		/// TODO: do we simply want to take all properties one by one, and merge them?
		/// I like the idea of each skill having a "genome", i.e. an array of values that then drives its characteristics.
		/// In other words, have the characteristics (each property) of a skill be computed from the skill "DNA", instead of us assigning values to them
		/// See IdeasToDiscuss.txt
		private List<SkillStats> Breed(SkillStats father, SkillStats mother)
		{
				List<SkillStats> children = new List<SkillStats>();
				for (int i = 0; i < GeneticsConstants.MAX_CHILDREN_NUMBER; i++)
				{
						SkillStats stat = _breeder.BreedSingleChild(father, mother);
						//Debug.Log("Bred: " + stat);
						children.Add(stat);
				}
				return children;
		}


		/// Again, very basic implementation
		private void Mutate(List<SkillStats> children, List<SkillAttribute.Type> availableAttributes)
		{
				foreach (SkillStats child in children)
				{
						_mutator.Mutate(child, child.Level, availableAttributes);
				}
		}
}