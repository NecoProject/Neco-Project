using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Singleton save object.
/// </summary>
public class Save : MonoBehaviour
{
		private static bool __created = false;

		public int CurrentLevel;
		public List<SkillStats> InitialSkills;

		private List<SkillStats> _activeSkills;
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
						foreach (SkillStats skill in _activeSkills)
						{
								//Debug.Log(skill.SkillName.ToString());
						}
				}
		}
		public Dictionary<SkillStats, int> NumberOfUses;


		void Awake()
		{
				//Debug.Log("Instance " + this.GetInstanceID());
				if (!__created)
				{
						// this is the first instance - make it persist
						DontDestroyOnLoad(this.gameObject);
						__created = true;
						ActiveSkills = InitialSkills;
						NumberOfUses = new Dictionary<SkillStats, int>();
				}
				else
				{
						// this must be a duplicate from a scene reload - DESTROY!
						//Debug.Log("Destroying me");
						DestroyImmediate(this.gameObject);
				}

				if (CurrentLevel == 0)
				{
						Debug.LogWarning("CurrentLevel is 0, probably forgot to set it in the editor");
				}
		}

		public void SetSkillAt(SkillStats value, int index)
		{
				_activeSkills[index] = value;
		}
}
