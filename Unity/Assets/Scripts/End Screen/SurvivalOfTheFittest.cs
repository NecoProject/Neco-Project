using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SurvivalOfTheFittest
{

		/// Not sure how we should determine the skills that should serve as parents. Ideally, it 
		/// would be the "most used skilled", but how do you really measure that? If you're against 
		/// a boss, and have two skills, one with a DoT and a high cooldown, the other a rapid 
		/// fire, and do the DoT as much as possible, and fire only from time to time. The fire 
		/// will have been used most, but in fact it's the DoT that's most used. Maybe something along 
		/// the lines of "the skill that has been most used compared to how often it could be used"?
		/// For now, only pick two skills at random
		public Tuple<SpellScript, SpellScript> GetParentSkills(List<SpellScript> activeSkills)
		{
				// For now, take two random parentslls.Count)];
				SpellScript skill1 = activeSkills[UnityEngine.Random.Range(0, activeSkills.Count)];
				SpellScript skill2 = activeSkills[UnityEngine.Random.Range(0, activeSkills.Count)];

				Tuple<SpellScript, SpellScript> parents = Tuple.New<SpellScript, SpellScript>(skill1, skill2);

				return parents;
		}
}