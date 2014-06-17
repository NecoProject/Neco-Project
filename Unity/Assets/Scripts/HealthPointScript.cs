using UnityEngine;
using System.Collections;

public class HealthPointScript : MonoBehaviour
{ 
		public float currentHP = 1;

		// Modifies the currentHP value by the input value. Returns true if the resulting currentHP is > 0, false otherwise.
		// value should be negative to decrease currentHP, positive otherwise. A value of 0 does nothing.
		bool undergoHpModification (float value)
		{
				currentHP += value;
				return currentHP > 0;
		}

		void OnTriggerEnter2D (Collider2D otherCollider)
		{
				Debug.Log ("Entering collision");
				SpellScript spell = otherCollider.gameObject.GetComponent<SpellScript> ();
				if (spell != null) {
						bool survives = undergoHpModification (-spell.damage);
						if (!survives) {
								Destroy (gameObject);
						}
				}
		}
}
