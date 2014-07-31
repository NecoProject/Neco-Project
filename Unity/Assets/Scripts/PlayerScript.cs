using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerScript : MonoBehaviour
{
		// Storing the state of the player in a serializable script will make it easier to save / load data, and pass data between levels
		public PlayerStats Stats;

		// TODO: move this to the PlayerStats scripts?
		public List<SpellScript> activeSkills = new List<SpellScript>();

		private Save _savedData;

		void Start()
		{
				Debug.Log("player start " + FindObjectsOfType<Save>().Length);
				_savedData = FindObjectOfType<Save>();

				SkillBarItem[] skillItems = _savedData.GetComponents<SkillBarItem>();
				if (skillItems != null && skillItems.Length > 0)
				{
						activeSkills = new List<SpellScript>();
						foreach (SkillBarItem skillItem in skillItems)
						{
								activeSkills.Add(skillItem.skill);
						}
				}

				GameObject[] skillBarItems = GameObject.FindGameObjectsWithTag("HUDSkill");
				Debug.Log(skillBarItems.Length);
				Array.Sort(skillBarItems, delegate(GameObject first, GameObject second)
				{
						return first.name.CompareTo(second.name);
				});
				for (int i = 0; i < skillBarItems.Length; i++)
				{
						skillBarItems[i].GetComponent<SkillBarItem>().SetSkill(activeSkills[i]);
				}
				Debug.Log("player start " + FindObjectsOfType<Save>().Length);
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
										Fire(spell, spaceTarget);
								}
						}
				}
		}

		private void Fire(SpellScript spell, Vector3 spaceTarget)
		{
				// Pay the cost
				bool canPay = spell.Stats.Cost <= Stats.CurrentMana;

				// If enough mana to pay the cost, fire the spell
				if (canPay)
				{
						PayManaCost(spell.Stats.Cost);
						SpellScript spellObject = (SpellScript)Instantiate(spell, spaceTarget, Quaternion.identity);
						Destroy(spellObject.gameObject, 1);
				}
		}

		/// TODO: have a dedicated script handle life / mana, as you will have to 
		/// take care of regeneration and other stuff that would make the player script 
		/// too crowded
		private void PayManaCost(float cost)
		{
				// This is how I like to broadcast this kind of info, not sure it's the best way. To be discussed
				// Messenger<float>.Broadcast(EventNames.MANA_SPENT, cost);
				Stats.CurrentMana = Stats.CurrentMana - cost;
		}
}