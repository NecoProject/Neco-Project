using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Validate the skill choice and proceed to next level
/// </summary>
public class SkillValidation : MonoBehaviour
{
		public SkillEvolution SkillEvolution;

		private Save _savedData;

		void Start()
		{
				_savedData = FindObjectOfType<Save>();
		}

		public void ProceedToNextLevel()
		{
				// But first, build the data we want to propagate (and that we will persist and will use as a save game)
				_savedData.SaveData.IsCurrentLevelBossLevel = false;
				_savedData.SaveData.CurrentLevel = _savedData.SaveData.CurrentLevel + 1;

				ValidateSkillChoice();

				// And finally reload the level, with a new difficulty setting
				Application.LoadLevel("Stage1");
		}

		public void FightBoss()
		{
				_savedData.SaveData.IsCurrentLevelBossLevel = true;

				ValidateSkillChoice();

				// And finally reload the level, with a new difficulty setting
				Application.LoadLevel("Stage1");
		}
		
		void ValidateSkillChoice()
		{
				// Set the skills
				for (int i = 0; i < SkillEvolution.Current.Count; i++)
				{
						_savedData.SaveData.SetSkillAt(SkillEvolution.Current[i].GetSkill(), i);
				}
		}
}