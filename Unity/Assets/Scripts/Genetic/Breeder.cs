using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class Breeder
{
		public SkillStats BreedSingleChild(SkillStats father, SkillStats mother)
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
}