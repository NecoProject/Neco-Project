using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerScript : MonoBehaviour
{
		public List<SpellScript> activeSkills = new List<SpellScript>();

		void Start()
		{
				SkillBarItem[] skillBarItems = FindObjectsOfType<SkillBarItem>();
				Array.Sort(skillBarItems, delegate(SkillBarItem first, SkillBarItem second)
				{
						return first.name.CompareTo(second.name);
				});
				for (int i = 0; i < skillBarItems.Length; i++)
				{
						if (activeSkills.Count > i)
						{
								skillBarItems[i].SetSkill(activeSkills[i]);
						}
				}
		}

		void Update()
		{
				shootAtMousePosition();
		}

		void shootAtMousePosition()
		{
				foreach (ShootingButton button in ShootingButton.GetEnumeration())
				{
						if (Input.GetButtonDown(button.GetButtonName()))
						{
								Vector3 screenTarget = Input.mousePosition;
								// Get the correct Z, because the current one is the Camera, circa -10
								var correctZ = transform.position.z;
								screenTarget.z = correctZ;
								Vector3 spaceTarget = Camera.main.ScreenToWorldPoint(screenTarget);
								// KABOOM
								SpellScript spell = activeSkills[button.GetSkillReference()];
								if (spell != null)
								{
										SpellScript spellObject = (SpellScript)Instantiate(spell, spaceTarget, Quaternion.identity);
										Destroy(spellObject.gameObject, spell.duration);
								}
						}
				}
		}
}