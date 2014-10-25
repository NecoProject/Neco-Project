using UnityEngine;
using System.Collections;

/// <summary>
/// No direct damage, but add DoT to the target
/// </summary>
public class NephalemNova : EnemySkill
{
		/*public Color32 AttackFlashColor;

		public float Cooldown;
		public float DamageModifier;
		public float Duration;

		private float _lastUse;

		void Start()
		{
				_lastUse = Time.time;
		}

		public override bool CanUse()
		{
				return Time.time > _lastUse + Cooldown;
		}

		public override void Attack(Transform target, float baseDamage)
		{
				DamageOverTime dot = new DamageOverTime(Duration, baseDamage * DamageModifier);
				target.SendMessage("AddDamageOverTime", dot);
				_lastUse = Time.time;
		}

		public override IEnumerator Animate(Transform target)
		{
				var previousColor = GetComponent<SpriteRenderer>().material.GetColor("_FlashColor");

				GetComponent<SpriteRenderer>().material.SetColor("_FlashColor", AttackFlashColor);
				GetComponent<SpriteRenderer>().material.SetFloat("_FlashAmount", 1);

				yield return new WaitForSeconds(0.5f);

				// Set everything back to normal
				GetComponent<SpriteRenderer>().material.SetFloat("_FlashAmount", 0);
				GetComponent<SpriteRenderer>().material.SetColor("_FlashColor", previousColor);
		}*/
}
