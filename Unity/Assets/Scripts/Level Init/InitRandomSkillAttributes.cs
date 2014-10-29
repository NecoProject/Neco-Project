using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InitRandomSkillAttributes : MonoBehaviour
{
		public List<Image> SkillAttributeIcons;
		public PrefabManager PrefabManager;

		public List<SkillAttribute.Type> SkillAttributes;
				
		void Start()
		{
				// Get all the unlocked skills
				List<SkillAttribute.Type> unlockedAttributes = FindObjectOfType<Save>().SaveData.UnlockedAttributes;

				// Get the non-always-present attributes
				List<SkillAttribute.Type> possibleAttributes = ResourceLoader.GetInstance().Attributes.NonBaseAttributes();

				// Merge the list
				List<SkillAttribute.Type> allEligibleAttributes = new List<SkillAttribute.Type>();
				allEligibleAttributes.AddRange(unlockedAttributes);
				allEligibleAttributes.AddRange(possibleAttributes);

				// Pick up to six different attributes
				allEligibleAttributes.Shuffle();
				for (int i = 0; i < Mathf.Min(SkillAttributeIcons.Count, allEligibleAttributes.Count); i++)
				{
						SkillAttributes.Add(allEligibleAttributes[i]);
				}

				// And now add the relevant icon
				for (int i = 0; i < SkillAttributes.Count; i++)
				{
						SkillAttributeIcons[i].sprite = PrefabManager.GetAttributeSprite(SkillAttributes[i].ToString().ToLower());
				}

				// Add the basic attributes
				SkillAttributes.Add(SkillAttribute.Type.COST);
				SkillAttributes.Add(SkillAttribute.Type.DAMAGE);
				SkillAttributes.Add(SkillAttribute.Type.RADIUS);
		}
}
