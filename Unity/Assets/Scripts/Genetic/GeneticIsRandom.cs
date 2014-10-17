using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class GeneticIsRandom : IGeneticAlgorithm
{
		// TODO: pass the script as a MonoBehaviour, so that it can be tweaked in the editor directly? 
		// Not sure we can easily experiment, since it requires an "end level" screen anyway
		private const float BASE_BONUS_MAX = 1f;
		private const float MINIMUM_COOLDOWN = 0.4f;

		private const int MAX_CHILDREN_NUMBER = 3;

		public List<SkillStats> Evolve(SkillStats father, SkillStats mother, int difficultLevel)
		{
				List<SkillStats> children = Breed(father, mother);
				Mutate(children);
				return children;
		}

		/// First version, very basic
		/// TODO: do we simply want to take all properties one by one, and merge them?
		/// I like the idea of each skill having a "genome", i.e. an array of values that then drives its characteristics.
		/// In other words, have the characteristics (each property) of a skill be computed from the skill "DNA", instead of us assigning values to them
		/// See IdeasToDiscuss.txt
		private List<SkillStats> Breed(SkillStats father, SkillStats mother)
		{
				List<SkillStats> children = new List<SkillStats>();
				for (int i = 0; i < MAX_CHILDREN_NUMBER; i++)
				{
						SkillStats stat = BreedSingleChild(father, mother);
						//Debug.Log("Bred: " + stat);
						children.Add(stat);
				}
				return children;
		}

		private SkillStats BreedSingleChild(SkillStats father, SkillStats mother)
		{
				SkillStats child = new SkillStats();

				child.Level = (father.Level + mother.Level) / 2 + 1;

				int numberOfAttributes = 3 + (int)Math.Floor(child.Level / 5);

				// Mandatory elements
				child.SpriteName = PickOne(father.SpriteName, mother.SpriteName);
				// Don't change, as for now this name is used to know when a skill needs to be customized
				child.SkillName = "New Skill";

				List<SkillAttribute> childAttributes = new List<SkillAttribute>();

				List<SkillAttribute> mandatoryAttributes = GenerateMandatoryAttributes(father, mother);
				childAttributes.AddRange(mandatoryAttributes);

				List<SkillAttribute> otherAttributes = GenerateRandomAttributes(father, mother, numberOfAttributes);
				childAttributes.AddRange(otherAttributes);

				child.Attributes = childAttributes;

				return child;
		}

		private List<SkillAttribute> GenerateMandatoryAttributes(SkillStats father, SkillStats mother)
		{
				List<SkillAttribute> mandatoryAttributes = new List<SkillAttribute>();
				// That's easy, since we know the full list of mandatory attributes
				foreach (SkillAttribute.Type mandatoryName in ResourceLoader.GetInstance().Attributes.MandatoryAttributes())
				{
						SkillAttribute fatherAtt = father.GetAttribute(mandatoryName);
						SkillAttribute motherAtt = mother.GetAttribute(mandatoryName);
						SkillAttribute childAtt = BreedAttribute(fatherAtt, motherAtt);
						mandatoryAttributes.Add(childAtt);
				}
				return mandatoryAttributes;
		}

		private List<SkillAttribute> GenerateRandomAttributes(SkillStats father, SkillStats mother, int numberOfAttributes)
		{
				List<SkillAttribute> attributes = new List<SkillAttribute>();
				List<SkillAttribute.Type> names = PickNames(father, mother, numberOfAttributes);
				Debug.Log(String.Join(", ", names.Select(x => x.ToString()).ToArray()));

				// That's easy, since we know the full list of mandatory attributes
				foreach (SkillAttribute.Type name in names)
				{
						SkillAttribute fatherAtt = father.GetAttribute(name);
						SkillAttribute motherAtt = mother.GetAttribute(name);
						SkillAttribute childAtt = BreedAttribute(fatherAtt, motherAtt);
						attributes.Add(childAtt);
				}
				return attributes;
		}

		private SkillAttribute BreedAttribute(SkillAttribute fatherAtt, SkillAttribute motherAtt)
		{
				if (fatherAtt == null) return motherAtt.Clone();
				if (motherAtt == null) return fatherAtt.Clone();

				SkillAttribute attribute = motherAtt.Clone();
				attribute.Value = GenerateValueFromParentsAndLuck(fatherAtt.Value, motherAtt.Value);
				return attribute;
		}

		private string PickOne(string fatherValue, string motherValue)
		{
				float discriminant = UnityEngine.Random.value;
				if (discriminant < 0.5)
				{
						return fatherValue;
				}
				return motherValue;
		}

		private float GenerateValueFromParentsAndLuck(float fatherValue, float motherValue)
		{
				return UnityEngine.Random.Range(Mathf.Min(fatherValue, motherValue), Mathf.Max(fatherValue, motherValue));
		}

		private List<SkillAttribute.Type> PickNames(SkillStats father, SkillStats mother, int numberOfAttributes)
		{
				List<SkillAttribute.Type> selectedNames = new List<SkillAttribute.Type>();
				List<SkillAttribute.Type> possibleNames = new List<SkillAttribute.Type>();
				possibleNames.AddRange(father.GetAttributeNames());
				possibleNames.AddRange(mother.GetAttributeNames());
				possibleNames.Shuffle();

				for (int i = 0; i < numberOfAttributes; i++)
				{
						foreach (SkillAttribute.Type type in possibleNames)
						{
								if (!selectedNames.Contains(type))
								{
										selectedNames.Add(type);
										break;
								}
						}
				}

				return selectedNames;
		}

		/// Again, very basic implementation
		private void Mutate(List<SkillStats> children)
		{
				foreach (SkillStats child in children)
				{
						Mutate(child, child.Level);
				}
		}

		/// Mutate the provided child's statistics taking into account the difficulty. The higher the difficulty, the higher the mutation effects. 
		/// TODO: Do we want to center the mutation around 0? Statistically we may result in pure better or pure worse stats afterwards.
		/// TODO: have a kind of "balance" to avoid having right away skills that are too powerful?
		private void Mutate(SkillStats child, float childLevel)
		{
				foreach (SkillAttribute attribute in child.Attributes)
				{
						Mutate(attribute, childLevel);
				}

				// TODO: Possibility to pop new attributes based on the unlocked attributes 
				// and the attributes available for the level
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
				float random = UnityEngine.Random.Range(-BASE_BONUS_MAX, BASE_BONUS_MAX);
				//Debug.Log("Random is " + random);
				//Debug.Log("Child level is " + childLevel);
				return original +  random * childLevel;
		}
}