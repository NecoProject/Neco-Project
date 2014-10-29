using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class FittestIsMostUsed : ISurvivalOfTheFittest
{

		private int _numberOfParents;

		public FittestIsMostUsed(int numberOfParents)
		{
				_numberOfParents = numberOfParents;
		}

		// Pick the two most used skills (arguable, cf IdeasToDiscuss.txt)
		public List<SkillStats> GetParentSkills(Dictionary<SkillStats, int> numberOfUses)
		{
				List<SkillStats> activeSkillsOrderedByUses = new List<SkillStats>(numberOfUses.Keys);

				// Filter out the ones that have never been used
				activeSkillsOrderedByUses.RemoveAll(x => numberOfUses[x] == 0);

				List<SkillStats> parents = new List<SkillStats>();
				// Always at least one skill used, enemies don't die on their own
				/*if (activeSkillsOrderedByUses.Count == 1)
				{
						for (int i = 0; i < _numberOfParents; i++) parents.Add(activeSkillsOrderedByUses[0]);
						return parents;
				}*/
								
				activeSkillsOrderedByUses.Sort(delegate(SkillStats p1, SkillStats p2)
				{
						// By default CompareTo puts in the ascending order, so put p2 before p1 
						return numberOfUses[p2].CompareTo(numberOfUses[p1]);
				});

				for (int i = 0; i < _numberOfParents; i++)
				{
						parents.Add(activeSkillsOrderedByUses[i % activeSkillsOrderedByUses.Count]);
				}

				return parents;
		}
}