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
		private Transform _target;

		private float _attackSpeed;
		private float _damage;
		private float _timeOfLastAttack;

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
				if (Time.time < _timeOfLastAttack + _attackSpeed) return;

				// Perform the attack
				_target.SendMessage("TakeDamage", _damage);
				_timeOfLastAttack = Time.time;

				// Visual cue to show the monster has attacked
				StartCoroutine(AnimateAttack()); 
		}

		IEnumerator AnimateAttack()
		{
				GetComponent<SpriteRenderer>().material.SetFloat("_FlashAmount", 1);
				yield return new WaitForSeconds(0.5f);
				GetComponent<SpriteRenderer>().material.SetFloat("_FlashAmount", 0);
		}

}
