using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour
{
		public List<SpellScript> activeSkills = new List<SpellScript> ();
	 
		void Start ()
		{
				SkillBarItem[] skillBarItems = FindObjectsOfType<SkillBarItem> ();
				for (int i = 0; i < skillBarItems.Length; i++) {
						if (activeSkills.Count > i) {
								skillBarItems [skillBarItems.Length - i - 1].SetSkill (activeSkills [i]);
						}
				}
		}

		void Update ()
		{
				shootAtMousePosition ();
		}

		void shootAtMousePosition ()
		{
				if (Input.GetButtonDown ("Fire1")) {
						Vector3 screenTarget = Input.mousePosition;
						Debug.Log ("Firing at mouse position " + screenTarget);
						// Get the correct Z, because the current one is the Camera, circa -10
						var correctZ = transform.position.z;
						screenTarget.z = correctZ;
						Vector3 spaceTarget = Camera.main.ScreenToWorldPoint (screenTarget);
						// KABOOM
						SpellScript spell = activeSkills [0];
						SpellScript spellObject = (SpellScript)Instantiate (spell, spaceTarget, Quaternion.identity);
			
						Destroy (spellObject.gameObject, spell.duration);
				}
		}
} 