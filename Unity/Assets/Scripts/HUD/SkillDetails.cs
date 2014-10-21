using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class SkillDetails : MonoBehaviour
{
		private Text _skillName;
		private Text _skillDescription;
		private Image _skillImage;

		private SkillBarItem _currentSkill;

		void Start()
		{
				_skillName = GetComponentsInChildren<Text>().Where(x => x.gameObject.name == "SkillName").First<Text>();
				_skillDescription = GetComponentsInChildren<Text>().Where(x => x.gameObject.name == "SkillDescription").First<Text>();
				_skillImage = GetComponentsInChildren<Image>().Where(x => x.gameObject.name == "SkillImage").First<Image>();
				gameObject.SetActive(false);
		}

		public void ToggleDetails(SkillBarItem stats)
		{
				SkillStats skill = stats.GetSkill();
				bool shouldBeActive = _currentSkill != stats && skill.SkillName.Length > 0;

				if (shouldBeActive)
				{
						_currentSkill = stats;

						_skillName.text = skill.SkillName;
						_skillImage.sprite = GameObject.Find("PrefabManager").GetComponent<PrefabManager>().GetSkillSprite(skill.SpriteName);
						_skillDescription.text = BuildDescription(skill);
				}
				else
				{
						_currentSkill = null;
				}

				gameObject.SetActive(shouldBeActive);
		}

		String BuildDescription(SkillStats skill)
		{
				String result = "Cooldown: " + skill.CoolDown.ToString("f1") + "\n";

				if (skill.Cost != 0) result += "Cost: " + skill.Cost.ToString("f1") + "\n";

				if (skill.MinDamage != 0 && skill.MaxDamage != 0) result += "Damage: " + skill.MinDamage.ToString("f1") + "-" + skill.MaxDamage.ToString("f1") + "\n";

				if (skill.Radius != 0) result += "Radius: " + skill.Radius.ToString("f1") + "\n";

				if (skill.Armor != 0) result += "Armor: " + skill.Armor.ToString("f1") + "\n";

				return result;
		}
}
