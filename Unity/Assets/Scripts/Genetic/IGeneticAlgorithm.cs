using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public interface IGeneticAlgorithm
{
		List<SkillStats> Evolve(SkillStats father, SkillStats mother, int difficultLevel, List<SkillAttribute.Type> availableAttributes);
}