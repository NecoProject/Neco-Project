using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class UnicellularGenetics : IGeneticAlgorithm
{
		private Breeder _breeder = new Breeder();
		private Mutator _mutator = new Mutator();

		public List<SkillStats> Evolve(List<SkillStats> parents, int difficultLevel, List<SkillAttribute.Type> availableAttributes)
		{
				if (parents.Count != GeneticsConstants.MAX_CHILDREN_NUMBER)
				{
						throw new Exception("Invalid number of parents, should be " + GeneticsConstants.MAX_CHILDREN_NUMBER + " , was " + parents.Count);
				}

				List<SkillStats> children = new List<SkillStats>();
				foreach (SkillStats parent in parents) {
						SkillStats child = _breeder.BreedSingleChild(parent, parent);
						_mutator.Mutate(child, child.Level, availableAttributes);
						children.Add(child);
				}

				return children;
		}
}