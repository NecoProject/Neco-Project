using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public interface IGeneticAlgorithm
{
		List<SkillStats> Evolve(List<SkillStats> parents, int difficultLevel, List<SkillAttribute.Type> availableAttributes);
}