using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerScript : MonoBehaviour
{
		// Storing the state of the player in a serializable script will make it easier to save / load data, and pass data between levels
		public PlayerStats Stats;
		public List<SkillStats> activeSkills;
		public Transform DefaultSkill;
		public Button[] buttons;

		private Save _savedData;

		void Start ()
		{
				_savedData = FindObjectOfType<Save> ();
				activeSkills = _savedData.activeSkills;

				buttons = Button.FindObjectsOfType<Button> ();
				Array.Sort (buttons, delegate(Button first, Button second) {
						return first.name.CompareTo (second.name);
				});
				for (int i = 0; i < buttons.Length; i++) {
						buttons [i].GetComponent<SkillBarItem> ().SetSkill (activeSkills [i]);
				}
		}

		void Update ()
		{
				shootAtMousePosition ();
		}

		void shootAtMousePosition ()
		{
				foreach (ShootingButton button in ShootingButton.GetEnumeration()) {
						if (Input.GetButtonDown (button.GetButtonName ())) {
								Vector3 screenTarget = Input.mousePosition;
								// Get the correct Z, because the current one is the Camera, circa -10
								var correctZ = transform.position.z;
								screenTarget.z = correctZ;
								Vector3 spaceTarget = Camera.main.ScreenToWorldPoint (screenTarget);
								// KABOOM
								SkillStats spell = activeSkills [button.GetSkillReference ()];
								Button skillButton = buttons [button.GetSkillReference ()];
								EventSystemManager.currentSystem.SetSelectedGameObject (skillButton.gameObject, null);
								skillButton.OnSubmit (null);
								if (spell != null) {
										Fire (spell, spaceTarget);
								}
						}
				}
		}

		private void Fire (SkillStats skill, Vector3 spaceTarget)
		{
				bool canPayManaCost = skill.Cost <= Stats.CurrentMana;

				// If enough mana to pay the cost, fire the spell
				if (canPayManaCost) {
						PayManaCost (skill.Cost);

						Sprite sprite = GameObject.Find ("PrefabManager").GetComponent<PrefabManager> ().GetSprite (skill.SpriteName);
						Transform spellObject = (Transform)Instantiate (DefaultSkill, spaceTarget, Quaternion.identity);
						spellObject.GetComponent<SpriteRenderer> ().sprite = sprite;
						spellObject.GetComponent<SpellObject> ().Skill = skill;

						// Increment number of uses of this skill
						skill.NumberOfUses++;
				}
		}

		/// TODO: have a dedicated script handle life / mana, as you will have to 
		/// take care of regeneration and other stuff that would make the player script 
		/// too crowded
		private void PayManaCost (float cost)
		{
				Stats.CurrentMana = Mathf.Min (Stats.MaxMana, Stats.CurrentMana - cost);
		}
}