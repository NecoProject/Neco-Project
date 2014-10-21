using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class Mutator
{
		/// Mutate the provided child's statistics taking into account the difficulty. The higher the difficulty, the higher the mutation effects. 
		/// TODO: Do we want to center the mutation around 0? Statistically we may result in pure better or pure worse stats afterwards.
		/// TODO: have a kind of "balance" to avoid having right away skills that are too powerful?
		public void Mutate(SkillStats child, float childLevel, List<SkillAttribute.Type> availableAttributes)
		{
				foreach (SkillAttribute attribute in child.Attributes)
				{
						Mutate(attribute, childLevel);
				}

				// TODO: Possibility to pop new attributes based on the unlocked attributes 
				// and the attributes available for the level
				SkillAttribute newAttribute = BuildGeneticMutation(childLevel, availableAttributes);

				if (newAttribute != null) child.Attributes.Add(newAttribute);

				// TODO: automatically refresh each time an attribute is updated?
				child.RefreshCachedAttributes();
		}

		private void Mutate(SkillAttribute attribute, float childLevel)
		{
				//Debug.Log("Value before mutation for " + attribute.AttributeType + " is " + attribute.Value);
				attribute.Value = Mutate(attribute.Value, childLevel);
				//Debug.Log("Value after mutation for " + attribute.AttributeType + " is " + attribute.Value);
		}

		// TODO: will need something more evolved than that in the future
		private float Mutate(float original, float childLevel)
		{
				float random = UnityEngine.Random.Range(-GeneticsConstants.BASE_BONUS_MAX, GeneticsConstants.BASE_BONUS_MAX);
				//Debug.Log("Random is " + random);
				//Debug.Log("Child level is " + childLevel);
				return original +  random * childLevel;
		}

		private SkillAttribute BuildGeneticMutation(float childLevel, List<SkillAttribute.Type> availableAttributes)
		{
				if (availableAttributes.Count == 0) return null;

				// TODO take childLevel into account, and add some random. Possibly add a "probability" to the Attribute?
				availableAttributes.Shuffle();
				SkillAttribute.Type type = availableAttributes[0];
				return ResourceLoader.GetInstance().Attributes.GetAttribute(type);
		}
}