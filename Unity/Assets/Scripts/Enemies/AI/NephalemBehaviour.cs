using UnityEngine;
using System.Collections;

public class NephalemBehaviour : EnemyBehaviour
{
		// The Nephalem has two different attacks: a standard attack, and 
		// an attack that drains life


		protected void PerformAttack()
		{
				// Perform the attack
				_target.SendMessage("TakeDamage", _damage);
		}

		protected IEnumerator AnimateAttack()
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
