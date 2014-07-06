using UnityEngine;
using System.Collections;

public class HealthPointScript : MonoBehaviour
{ 
		public float currentHP = 1;

		private TextMesh _lifeComponent;
		private float _maxHp;

		public void Start ()
		{
				_lifeComponent = GetComponentInChildren<TextMesh> ();
				_maxHp = currentHP;
				_lifeComponent.text = currentHP + " / " + _maxHp;
		}

		// Modifies the currentHP value by the input value. Returns true if the resulting currentHP is > 0, false otherwise.
		// value should be negative to decrease currentHP, positive otherwise. A value of 0 does nothing.
		bool undergoHpModification (float value)
		{
				currentHP += value;
				_lifeComponent.text = currentHP + " / " + _maxHp;
				return currentHP > 0;
		}



		void OnTriggerEnter2D (Collider2D otherCollider)
		{
				SpellScript spell = otherCollider.gameObject.GetComponent<SpellScript> ();
				if (spell != null) {
						bool survives = undergoHpModification (-spell.Stats.Damage);
						if (!survives) {
								Die (); 
						}
				}
		}

		void Die ()
		{
				Destroy (gameObject);
				// Notify listeners
				Messenger<HealthPointScript>.Broadcast (EventNames.MONSTER_KILLED, this);
		}
}
