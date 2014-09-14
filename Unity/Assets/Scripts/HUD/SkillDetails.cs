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

		void Start()
		{
				Debug.Log("Starting debug");
				Debug.Log(GetComponentsInChildren<Text>().Length);
				Debug.Log(GetComponentsInChildren<Text>().Where(x => x.gameObject.name == "SkillName").ToList<Text>().Count);
				_skillName = GetComponentsInChildren<Text>().Where(x => x.gameObject.name == "SkillName").First<Text>();
				_skillDescription = GetComponentsInChildren<Text>().Where(x => x.gameObject.name == "SkillDescription").First<Text>();
				_skillImage = GetComponentsInChildren<Image>().Where(x => x.gameObject.name == "SkillImage").First<Image>();
				gameObject.SetActive(false);
		}

		public void ToggleDetails(SkillBarItem stats)
		{
				Debug.Log("Toggling details");
				SkillStats skill = stats.GetSkill();

				_skillName.text = skill.SkillName;
				_skillImage.sprite = GameObject.Find("PrefabManager").GetComponent<PrefabManager>().GetSprite(skill.SpriteName);
				_skillDescription.text = BuildDescription(skill);

				gameObject.SetActive(!gameObject.activeSelf);
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
