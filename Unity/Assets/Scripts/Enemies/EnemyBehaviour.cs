using UnityEngine;
using System.Collections;

/// <summary>
/// TODO: use a state machine, as illustrated at http://unitygems.com/fsm1/
/// for cleaner code
/// For now, enemy behaviour is really simple: either it is waiting to attack (because it 
/// has attacked recently) or it is attacking
/// </summary>
public class EnemyBehaviour : MonoBehaviour
{
		public Color32 AttackFlashColor; 

		private Transform _target;

		private float _attackSpeed;
		private float _damage;
		private float _timeOfLastAttack;
		private float _timeOfNextAttack;

		void Start()
		{
				_target = GameObject.Find("Player").transform;
				_attackSpeed = GetComponent<EnemyStats>().AttackSpeed;
				_damage = GetComponent<EnemyStats>().Damage;
		}

		void Update()
		{
				// The lesser the attack speed, the most frequent the attacks
				// (not that intuitive, but used in many games)
				if (Time.time < _timeOfNextAttack) return;

				// Perform the attack
				_target.SendMessage("TakeDamage", _damage);
				_timeOfLastAttack = Time.time;
				// Avoid being too uniform. Possibly could be based on distance to player instead
				_timeOfNextAttack = _timeOfLastAttack + _attackSpeed + Random.Range(-1f, 1f);

				// Visual cue to show the monster has attacked
				StartCoroutine(AnimateAttack()); 
		}

		IEnumerator AnimateAttack()
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
