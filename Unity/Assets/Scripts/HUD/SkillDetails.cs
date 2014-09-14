﻿using UnityEngine;
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

		private SkillStats _currentSkill;

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
				bool shouldBeActive = (_currentSkill != skill);

				if (shouldBeActive)
				{
						_currentSkill = skill;

						_skillName.text = skill.SkillName;
						_skillImage.sprite = GameObject.Find("PrefabManager").GetComponent<PrefabManager>().GetSprite(skill.SpriteName);
						_skillDescription.text = BuildDescription(skill);
				}

				gameObject.SetActive(shouldBeActive);
		}

		String BuildDescription(SkillStats skill)
		{
				String result = "Cost: " + skill.Cost.ToString("f1") + "\n" +
						"Damage: " + skill.Damage.ToString("f1") + "\n" +
						"Cooldown: " + skill.CoolDown.ToString("f1") + "\n" +
						"Radius: " + skill.Radius.ToString("f1")
						;
				return result;
		}
}