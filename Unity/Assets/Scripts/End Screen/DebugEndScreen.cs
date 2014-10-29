using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Allows to start the game directly from the End Screen
/// </summary>
public class DebugEndScreen : MonoBehaviour
{
		public float SkillLevel;

		void Start()
		{
				// Create a Save object with the current skills of the player
				Save save = GameObject.Find("Save").GetComponent<Save>();
				save.SaveData.CurrentLevel = 1;

				List<SkillStats> activeSkills = new List<SkillStats>();
				for (int i = 0; i < 4; i++)
				{
						SkillStats skill = buildRandomSkill();
						activeSkills.Add(skill);
				}
				save.SaveData.ActiveSkills = activeSkills;

				save.NumberOfUses = buildNumberOfUses(activeSkills);

				DontDestroyOnLoad(this);
		}

		SkillStats buildRandomSkill() {
				SkillStats skill = SkillStats.CreateRandom();
				skill.SkillName = "Random";
				skill.SpriteName = GameObject.Find("PrefabManager").GetComponent<PrefabManager>().Skills[UnityEngine.Random.Range(0, 3)].Sprite.name;
				skill.Level = SkillLevel;
				return skill;
		}

		Dictionary<SkillStats, int> buildNumberOfUses(List<SkillStats> skills)
		{
				Dictionary<SkillStats, int> nbOfUses = new Dictionary<SkillStats, int>();
				foreach (SkillStats skill in skills)
				{
						nbOfUses.Add(skill, UnityEngine.Random.Range(0, 50));
				}
				return nbOfUses;
		}
}