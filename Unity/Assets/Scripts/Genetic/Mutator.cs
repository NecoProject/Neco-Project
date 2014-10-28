using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class Mutator
{
		private const float FACTOR = 1.3f;
		private const float MINIMUM_VALUE = 6;

		/// Mutate the provided child's statistics taking into account the difficulty. The higher the difficulty, the higher the mutation effects. 
		/// TODO: Do we want to center the mutation around 0? Statistically we may result in pure better or pure worse stats afterwards.
		/// TODO: have a kind of "balance" to avoid having right away skills that are too powerful?
		public void Mutate(SkillStats child, float childLevel, List<SkillAttribute.Type> availableAttributes)
		{
				// TODO: Possibility to pop new attributes based on the unlocked attributes 
				// and the attributes available for the level
				//Debug.Log("New attribute generation");
				SkillAttribute newAttribute = BuildGeneticMutation(child.GetAttributeNames(), childLevel, availableAttributes);
				if (newAttribute != null) child.Attributes.Add(newAttribute);

				foreach (SkillAttribute attribute in child.Attributes)
				{
						Mutate(attribute, childLevel);
				}

				Scale(child, childLevel);

				// TODO: automatically refresh each time an attribute is updated?
				child.RefreshCachedAttributes();
		}

		private void Mutate(SkillAttribute attribute, float childLevel)
		{
				//Debug.Log("Value before mutation for " + attribute.AttributeType + " is " + attribute.Value);
				attribute.Value = Mutate(attribute.Value, childLevel);
				attribute.Value2 = Mutate(attribute.Value2, childLevel);
				//Debug.Log("Value after mutation for " + attribute.AttributeType + " is " + attribute.Value);
		}

		// TODO: will need something more evolved than that in the future
		private float Mutate(float original, float childLevel)
		{
				float random = UnityEngine.Random.Range(-GeneticsConstants.BASE_BONUS_MAX, GeneticsConstants.BASE_BONUS_MAX);
				//Debug.Log("Random is " + random);
				//Debug.Log("Child level is " + childLevel);
				return original + random * childLevel;
		}

		private SkillAttribute BuildGeneticMutation(List<SkillAttribute.Type> existingAttributes, float childLevel, List<SkillAttribute.Type> attributesForLevel)
		{
				//Debug.Log("Existing: " + String.Join(", ", existingAttributes.Select(x => x.ToString()).ToArray()));
				//Debug.Log("For level: " + String.Join(", ", attributesForLevel.Select(x => x.ToString()).ToArray()));
				if (attributesForLevel.Count == 0) return null;

				// Keep only the attributes whose level us below the skill level
				List<SkillAttribute.Type> availableAttributes = FilterByChildLevel(childLevel, attributesForLevel);

				// Choose the candidate
				// TODO: all attributes have an equal chance of being chosen here. Spice this a little
				availableAttributes.Shuffle();
				//Debug.Log("For level: " + String.Join(", ", availableAttributes.Select(x => x.ToString()).ToArray()));
				int i = 0;
				SkillAttribute.Type type = availableAttributes[i];
				while (existingAttributes.Contains(type) && i < availableAttributes.Count)
				{
						type = availableAttributes[i++];
				}

				SkillAttribute candidateAttribute = existingAttributes.Contains(type) ? null : ResourceLoader.GetInstance().Attributes.GetAttribute(type);
				//Debug.Log("Candidate attribute is " + type + " giving " + candidateAttribute);

				if (candidateAttribute != null)
				{
						// Now, it's random generation time!
						float random = UnityEngine.Random.Range(0f, 1f);
						//Debug.Log("Comparing " + random + " to " + candidateAttribute.SpawnProbability);
						if (random > candidateAttribute.SpawnProbability) return null;
				}

				return candidateAttribute;
		}

		private List<SkillAttribute.Type> FilterByChildLevel(float childLevel, List<SkillAttribute.Type> attributesForLevel)
		{
				//Debug.Log("Child level is " + childLevel);
				List<SkillAttribute.Type> availableAttributes = new List<SkillAttribute.Type>();

				foreach (SkillAttribute.Type type in attributesForLevel)
				{
						SkillAttribute attribute = ResourceLoader.GetInstance().Attributes.GetAttribute(type);
						//Debug.Log("Minimum skill level for " + attribute.AttributeType + " is " + attribute.MinimumSkillLevel);
						if (attribute.MinimumSkillLevel <= childLevel)
						{
								availableAttributes.Add(type);
								//Debug.Log("Adding attribute");
						}
				}

				return availableAttributes;
		}

		private void Scale(SkillStats skill, float level)
		{
				// TODO / Assumption: All skills should be on a similar scale

				// Compute the total weight of the attributes
				float totalWeight = 0;
				foreach (SkillAttribute attribute in skill.Attributes)
				{
						//Debug.Log("Total weight contribution: " + attribute.Value + " for " + attribute.AttributeType);
						if (attribute.IsBonus) totalWeight += attribute.Value;
						else totalWeight -= attribute.Value;
						//Debug.Log("Debut weight after computation is " + totalWeight);
				}
				//Debug.Log("Debug weight is " + totalWeight);
				float adjustmentFactor = (level * FACTOR + MINIMUM_VALUE) / totalWeight;
				foreach (SkillAttribute attribute in skill.Attributes)
				{
						attribute.Value = attribute.Value * adjustmentFactor;
				}
		}
}