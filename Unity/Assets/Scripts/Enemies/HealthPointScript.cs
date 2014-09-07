using UnityEngine;
using System.Collections;

public class HealthPointScript : MonoBehaviour
{
		public Color32 DamageColour; 
		
		public float MaxHp { get; set; }
		public float CurrentHp { get; private set; }
		
		private TextMesh _lifeComponent;

		void Start()
		{
				// TODO: change this to use the new UI components
				_lifeComponent = GetComponentInChildren<TextMesh>();

				MaxHp = GetComponent<EnemyStats>().MaxHp;
				CurrentHp = MaxHp;
				UpdateDisplayText();
		}
		
		/// <summary>
		/// Modifies the currentHP value by the input value. Returns true if the resulting currentHP is > 0, false otherwise.
		/// value should be negative to decrease currentHP, positive otherwise. A value of 0 does nothing.
		/// </summary>
		bool TakeDamage(float value)
		{
				// TODO: change this to use the new UI components
				CurrentHp = Mathf.Min(MaxHp, CurrentHp - value);
				UpdateDisplayText();
				StartCoroutine(AnimateTakeDamage());
				return CurrentHp > 0;
		}

		void UpdateDisplayText()
		{
				_lifeComponent.text = CurrentHp + " / " + MaxHp;
		}


		IEnumerator AnimateTakeDamage()
		{
				var previousColor = GetComponent<SpriteRenderer>().material.GetColor("_FlashColor");

				GetComponent<SpriteRenderer>().material.SetColor("_FlashColor", DamageColour);
				GetComponent<SpriteRenderer>().material.SetFloat("_FlashAmount", 1);

				yield return new WaitForSeconds(0.5f);

				// Set everything back to normal
				GetComponent<SpriteRenderer>().material.SetFloat("_FlashAmount", 0);
				GetComponent<SpriteRenderer>().material.SetColor("_FlashColor", previousColor);
		}

		void OnTriggerEnter2D(Collider2D otherCollider)
		{
				SpellObject spell = otherCollider.gameObject.GetComponent<SpellObject>();
				if (spell != null)
				{
						bool survives = TakeDamage(spell.Skill.Damage);
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
