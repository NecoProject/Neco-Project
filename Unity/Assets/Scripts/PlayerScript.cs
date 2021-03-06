﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class PlayerScript : MonoBehaviour
{
		public Color32 DamageColour, ArmorColour, HealColour;

		// Storing the state of the player in a serializable script will make it easier to save / load data, and pass data between levels
		public PlayerStats Stats;
		public InputDetection InputDetection;
		public List<SkillStats> activeSkills;
		//public float timeBetweenAttacks;
		public DotCondition dotCondition;
		public FloatingDamage FloatingDamage;

		[SerializeField]
		private float _armor;

		private Button[] _buttons;
		private Save _savedData;
		// Don't prevent the player from attacking right away
		//private float _timeOfLastAttack = -1000;

		void Start()
		{
				_savedData = FindObjectOfType<Save>();
				activeSkills = _savedData.SaveData.ActiveSkills;

				// We want to select only the Button component of GameObject that are tagged with HUDSkillUI
				// To do so, we first get all the tagged objects - this return an array of GameObject
				// Then we want to build a new Array containing only the Button component of these GameObjects, 
				// and we do so using System.Linq.Select, that returns a list that we finally convert to an Array
				_buttons = GameObject.FindGameObjectsWithTag("HUDSkillUI").Select(x => x.GetComponent<Button>()).ToArray<Button>();
				Array.Sort(_buttons, delegate(Button first, Button second)
				{
						return first.name.CompareTo(second.name);
				});
				for (int i = 0; i < _buttons.Length; i++)
				{
						_buttons[i].GetComponent<SkillBarItem>().SetSkill(activeSkills[i]);
				}
		}

		void Update()
		{
				RegenerateMana();
		}

		public void ShootAtMousePosition(int buttonIndex)
		{
				if (buttonIndex == -1) return;

				SkillStats spell = activeSkills[buttonIndex];
				if (spell == null) return;

				// Simulate a click on the button to trigger the nice effects
				Button skillButton = _buttons[buttonIndex];

				Vector3 screenTarget = Input.mousePosition;
				// Get the correct Z, because the current one is the Camera, circa -10
				var correctZ = transform.position.z;
				screenTarget.z = correctZ;
				Vector3 spaceTarget = Camera.main.ScreenToWorldPoint(screenTarget);

				// Can't fire too quickly
				//bool canFire = (Time.time > _timeOfLastAttack + timeBetweenAttacks);

				/*if (canFire)
				{*/
						// Don't really like that I need to pass the stats. I think we should be able to 
						// say to the skill script "try to fire this" and it handles everything, but 
						// it isn't done really elegantly here
						skillButton.GetComponent<SkillBarItem>().Fire(gameObject, spaceTarget, Stats);

						// Update time of last attack
						//_timeOfLastAttack = Time.time;
				//}
		}

		void RegenerateMana()
		{
				Stats.CurrentMana = Mathf.Min(Stats.MaxMana, Stats.CurrentMana + Stats.ManaRegenerationSpeed * Time.deltaTime);
		}

		public bool TakeDamage(float amount)
		{
				float amountDamage = (1 - _armor / (30 + _armor)) * amount;

				Stats.CurrentHealth = Mathf.Min(Stats.CurrentHealth - amountDamage, Stats.MaxHealth);

				if (amountDamage > 0) StartCoroutine(AnimateFlash(DamageColour));
				else StartCoroutine(AnimateFlash(HealColour));

				StartCoroutine(FloatingDamage.Spawn(amountDamage));
				if (Stats.CurrentHealth <= 0)
				{
						Application.LoadLevel(SceneNames.GAME_OVER);
				}
				return true;
		}

		public void AddDamageOverTime(DamageOverTime dot)
		{
				// TODO:
				// - Apply damage regularly
				// - Clean condition when dot expires
				// - Add visual indicator of dot (player color + status item?)
				dotCondition.Apply(dot);
		}

		public void SetArmor(float armor)
		{
				this._armor = armor;
				StartCoroutine(AnimateFlash(ArmorColour, 0.8f));
		}

		IEnumerator AnimateFlash(Color32 flashColor, float duration = 0.5f)
		{
				var previousColor = GetComponent<SpriteRenderer>().material.GetColor("_FlashColor");

				GetComponent<SpriteRenderer>().material.SetColor("_FlashColor", flashColor);
				GetComponent<SpriteRenderer>().material.SetFloat("_FlashAmount", 1);

				yield return new WaitForSeconds(duration);

				// Set everything back to normal
				GetComponent<SpriteRenderer>().material.SetFloat("_FlashAmount", 0);
				GetComponent<SpriteRenderer>().material.SetColor("_FlashColor", previousColor);
		}

		void OnTriggerEnter2D(Collider2D otherCollider)
		{
				SpellObject spell = otherCollider.gameObject.GetComponent<SpellObject>();
				if (spell != null)
				{
						spell.ApplyEffects(gameObject);
				}
		}
}