using UnityEngine;
using System.Collections;

public class StandardAttack : EnemySkill
{
		public Color32 AttackFlashColor;

		public override bool CanUse()
		{
				return true;
		}

		public override void Attack(Transform target, float baseDamage)
		{
				target.SendMessage("TakeDamage", baseDamage);
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
		}
}
