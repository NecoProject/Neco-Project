using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class FittestIsMostUsed : ISurvivalOfTheFittest
{

		// Pick the two most used skills (arguable, cf IdeasToDiscuss.txt)
		public Tuple<SkillStats, SkillStats> GetParentSkills(Dictionary<SkillStats, int> numberOfUses)
		{
				List<SkillStats> activeSkillsOrderedByUses = new List<SkillStats>(numberOfUses.Keys);

				// Filter out the ones that have never been used
				activeSkillsOrderedByUses.RemoveAll(x => numberOfUses[x] == 0);

				// Always at least one skill used, enemies don't die on their own
				if (activeSkillsOrderedByUses.Count == 1)
				{	
						return Tuple.New<SkillStats, SkillStats>(activeSkillsOrderedByUses[0], activeSkillsOrderedByUses[0]);
				}
								
				activeSkillsOrderedByUses.Sort(delegate(SkillStats p1, SkillStats p2)
				{
						// By default CompareTo puts in the ascending order, so put p2 before p1 
						return numberOfUses[p2].CompareTo(numberOfUses[p1]);
				});

				SkillStats mostUsedSkill1 = activeSkillsOrderedByUses[0];
				SkillStats mostUsedSkill2 = activeSkillsOrderedByUses[1];

				Tuple<SkillStats, SkillStats> parents = Tuple.New<SkillStats, SkillStats>(mostUsedSkill1, mostUsedSkill2);

				return parents;
		}
}