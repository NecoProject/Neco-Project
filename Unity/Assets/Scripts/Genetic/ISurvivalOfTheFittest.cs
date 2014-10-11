using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public interface ISurvivalOfTheFittest
{
		Tuple<SkillStats, SkillStats> GetParentSkills(Dictionary<SkillStats, int> numberOfUses);
}