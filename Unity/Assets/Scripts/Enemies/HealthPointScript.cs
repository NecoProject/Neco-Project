using UnityEngine;
using System.Collections;

public class HealthPointScript : MonoBehaviour
{
		public float MaxHp { get; set; }
		public float CurrentHp { get; private set; }

		private TextMesh _lifeComponent;

		void Start()
		{
				// TODO: change this to use the new UI components
				_lifeComponent = GetComponentInChildren<TextMesh>();

				MaxHp = GetComponent<EnemyStats>().MaxHp;
				CurrentHp = MaxHp;
				undergoHpModification(0);
		}
		
		/// <summary>
		/// Modifies the currentHP value by the input value. Returns true if the resulting currentHP is > 0, false otherwise.
		/// value should be negative to decrease currentHP, positive otherwise. A value of 0 does nothing.
		/// </summary>
		bool undergoHpModification(float value)
		{
				// TODO: change this to use the new UI components
				CurrentHp = Mathf.Min(MaxHp, CurrentHp + value);
				_lifeComponent.text = CurrentHp + " / " + MaxHp;
				return CurrentHp > 0;
		}



		void OnTriggerEnter2D(Collider2D otherCollider)
		{
				SpellObject spell = otherCollider.gameObject.GetComponent<SpellObject>();
				if (spell != null)
				{
						bool survives = undergoHpModification(-spell.Skill.Damage);
						if (!survives)
						{
								Die();
						}
				}
		}

		void Die()
		{
				Destroy(gameObject);
				// Notify listeners
				Messenger<HealthPointScript>.Broadcast(EventNames.MONSTER_KILLED, this);
		}
}
