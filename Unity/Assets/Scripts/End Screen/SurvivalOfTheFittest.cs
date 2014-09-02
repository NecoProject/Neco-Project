using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SurvivalOfTheFittest
{

		// Pick the two most used skills (arguable, cf IdeasToDiscuss.txt)
		public Tuple<SkillStats, SkillStats> GetParentSkills (List<SkillStats> activeSkills)
		{ 

				List<SkillStats> activeSkillsOrderedByUses = activeSkills;
			
				activeSkillsOrderedByUses.Sort (delegate (SkillStats p1, SkillStats p2) {
						// By default CompareTo puts in the ascending order, so put p2 before p1 
						return p2.NumberOfUses.CompareTo (p1.NumberOfUses);
				});

				SkillStats mostUsedSkill1 = activeSkillsOrderedByUses [0];
				SkillStats mostUsedSkill2 = activeSkillsOrderedByUses [1];
			
				Tuple<SkillStats, SkillStats> parents = Tuple.New<SkillStats, SkillStats> (mostUsedSkill1, mostUsedSkill2);

				return parents;
		}
}