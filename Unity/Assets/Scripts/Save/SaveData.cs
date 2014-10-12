using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class SaveData
{
		public int CurrentLevel;
		public bool IsCurrentLevelBossLevel;

		public Algorithm Algorithm;
		public string Boss;
		public List<string> Enemies;
		public List<string> SkillAttributes;

		public List<SkillStats> ActiveSkills
		{
				get
				{
						// Making sure that our own representation won't be 
						// affected by whatever is done to the list (e.g. sorting it 
						// differently)
						SkillStats[] copy = new SkillStats[4];
						_activeSkills.CopyTo(copy);
						return new List<SkillStats>(copy);
				}
				set
				{
						_activeSkills = value;
				}
		}

		private List<SkillStats> _activeSkills;

		public void SetSkillAt(SkillStats value, int index)
		{
				_activeSkills[index] = value;
		}

}
