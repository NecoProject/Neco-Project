using UnityEngine;
using System.Collections;

public class HealthPointScript : MonoBehaviour
{
		public Color32 DamageColour; 
		
		public float MaxHp { get; set; }
		public float CurrentHp { get; private set; }
		
		public TextMesh LifeComponent;
		public FloatingDamage FloatingDamage;

		void Start()
		{
				MaxHp = GetComponent<EnemyStats>().MaxHp;
				CurrentHp = MaxHp;
				UpdateDisplayText();
		}
		
		/// <summary>
		/// Modifies the currentHP value by the input value. Returns true if the resulting currentHP is > 0, false otherwise.
		/// value should be negative to decrease currentHP, positive otherwise. A value of 0 does nothing.
		/// </summary>
		public void TakeDamage(float value)
		{
				// TODO: change this to use the new UI components
				CurrentHp = Mathf.Min(MaxHp, CurrentHp - value);
				UpdateDisplayText();
				StartCoroutine(AnimateTakeDamage());
				StartCoroutine(FloatingDamage.Spawn(value));
				if (CurrentHp <= 0)
				{
						Die();
				}
		}

		void UpdateDisplayText()
		{
				LifeComponent.text = CurrentHp.ToString("f0") + " / " + MaxHp.ToString("f0");
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
						spell.ApplyEffects(gameObject);
				}
		}

		float ComputeDamage(float minDmg, float maxDmg)
		{
				return Random.Range(minDmg, maxDmg);
		}

		void Die()
		{
				// Notify listeners
				Messenger<HealthPointScript>.Broadcast(EventNames.MONSTER_KILLED, this);

				Destroy(gameObject);
		}
}
