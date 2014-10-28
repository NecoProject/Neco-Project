using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class SaveData
{
		public int CurrentLevel;
		public bool IsCurrentLevelBossLevel;
		public SkillAttribute LastUnlockedAttribute;

		// Level-specific
		public Algorithm Algorithm;
		public string Boss;
		public List<string> Enemies;
		public List<SkillAttribute.Type> SkillAttributes;

		// Permanent upgrades
		public List<SkillAttribute.Type> UnlockedAttributes = new List<SkillAttribute.Type>();

		public List<SkillStats> ActiveSkills
		{
				get
				{
						// Making sure that our own representation won't be 
						// affected by whatever is done to the list (e.g. sorting it 
						// differently)
						SkillStats[] copy = new SkillStats[4];
						// TODO: not the cleanest way, but didn't find any other good method to have this 
						// work with setting the data via the inspector, as the cache is calculated 
						// when calling the set {} property
						RefreshSkillsCachedData();
						_activeSkills.CopyTo(copy);
						return new List<SkillStats>(copy);
				}
				set
				{
						_activeSkills = value;
				}
		}

		[SerializeField]
		private List<SkillStats> _activeSkills;

		public void SetSkillAt(SkillStats value, int index)
		{
				_activeSkills[index] = value;
		}

		void RefreshSkillsCachedData()
		{
				foreach (SkillStats skill in _activeSkills)
				{
						skill.RefreshCachedAttributes();
				}
		}
}
