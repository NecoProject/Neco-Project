using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SurvivalOfTheFittest
{

		/// Cf IdeasToDiscuss
		/// For now, only pick two skills at random
		public Tuple<SkillStats, SkillStats> GetParentSkills(List<SkillStats> activeSkills)
		{ 
				// For now, take two random parents
				SkillStats skill1 = activeSkills[UnityEngine.Random.Range(0, activeSkills.Count)];
				SkillStats skill2 = activeSkills[UnityEngine.Random.Range(0, activeSkills.Count)];

				Tuple<SkillStats, SkillStats> parents = Tuple.New<SkillStats, SkillStats>(skill1, skill2);

				return parents;
		}
}