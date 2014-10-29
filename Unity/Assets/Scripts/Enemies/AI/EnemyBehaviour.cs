using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// TODO: use a state machine, as illustrated at http://unitygems.com/fsm1/
/// for cleaner code
/// For now, enemy behaviour is really simple: either it is waiting to attack (because it 
/// has attacked recently) or it is attacking
/// </summary>
public abstract class EnemyBehaviour : MonoBehaviour
{
		public Color32 AttackFlashColor;
		public EnemyStats Stats;
		
		protected List<EnemySkill> _skills = new List<EnemySkill>();
		protected Transform _target;

		void Start()
		{
				_target = GameObject.Find("Player").transform;
				_skills = new List<EnemySkill>(GetComponentsInChildren<EnemySkill>());
				foreach (EnemySkill skill in _skills)
				{
						skill.ApplyStats(Stats);
				}
		}

		void Update()
		{
				EnemySkill attackingSkill = PerformAttack();

				// Visual cue to show the monster has attacked
				StartCoroutine(AnimateAttack(attackingSkill));
		}

		protected virtual EnemySkill PerformAttack()
		{
				// Perform the attack

				// Show the animation, that will then interact with the enemy to actually damage them
				// Priority is always given on the first skill
				EnemySkill activeAttack = null;
				foreach (EnemySkill enemySkill in _skills)
				{
						if (!enemySkill.IsCoolingDown)
						{
								activeAttack = enemySkill;
								break;
						}
				}

				// All skills are in cooldown
				if (activeAttack == null) return null;

				SkillStats skill = activeAttack.Skill;

				// Should probably be done differently
				Sprite sprite = GameObject.Find("PrefabManager").GetComponent<PrefabManager>().GetSkillSprite(skill.SpriteName);
				EnemySkill spellObject = (EnemySkill)Instantiate(activeAttack, _target.position, Quaternion.identity);
				spellObject.transform.localScale = new Vector3(skill.Radius, skill.Radius);
				spellObject.GetComponent<SpriteRenderer>().sprite = sprite;
				spellObject.GetComponent<SpellObject>().Skill = skill;
				spellObject.GetComponent<SpellObject>().Origin = gameObject;
				spellObject.GetComponent<SpellObject>().ShouldSkipDestroyOnCreate = false;

				// Run the cooldown
				activeAttack.IsCoolingDown = true;

				return activeAttack;
		}

		protected virtual IEnumerator AnimateAttack(EnemySkill attackingSkill)
		{
				if (attackingSkill != null)
				{
						var previousColor = GetComponent<SpriteRenderer>().material.GetColor("_FlashColor");

						GetComponent<SpriteRenderer>().material.SetColor("_FlashColor", AttackFlashColor);
						GetComponent<SpriteRenderer>().material.SetFloat("_FlashAmount", 1);

						yield return new WaitForSeconds(0.5f);

						// Set everything back to normal
						GetComponent<SpriteRenderer>().material.SetFloat("_FlashAmount", 0);
						GetComponent<SpriteRenderer>().material.SetColor("_FlashColor", previousColor);
				}
		}
}
