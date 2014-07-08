using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SurvivalOfTheFittest
{

		/// Cf IdeasToDiscuss
		/// For now, only pick two skills at random
		public Tuple<SpellScript, SpellScript> GetParentSkills (List<SpellScript> activeSkills)
		{
				// For now, take two random parents
				SpellScript skill1 = activeSkills [UnityEngine.Random.Range (0, activeSkills.Count)];
				SpellScript skill2 = activeSkills [UnityEngine.Random.Range (0, activeSkills.Count)];

				Tuple<SpellScript, SpellScript> parents = Tuple.New<SpellScript, SpellScript> (skill1, skill2);

				return parents;
		}
}