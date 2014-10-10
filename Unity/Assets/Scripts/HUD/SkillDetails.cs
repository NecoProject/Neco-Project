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
				String result = "Cost: " + skill.Cost.ToString("f1") + "\n" +
						"Min Damage: " + skill.MinDamage.ToString("f1") + "\n" +
						"Max Damage: " + skill.MaxDamage.ToString("f1") + "\n" +
						"Cooldown: " + skill.CoolDown.ToString("f1") + "\n" +
						"Radius: " + skill.Radius.ToString("f1")
						;
				return result;
		}
}
