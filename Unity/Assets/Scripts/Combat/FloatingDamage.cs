using UnityEngine;
using System.Collections;

public class FloatingDamage : MonoBehaviour
{
		public Color32 DamageColour, HealColour;
		public Transform SpawnPoint, DestroyPoint;
		public int InitialSize, FinalSize;
		public float Duration;
		public TextMesh Text;

		public IEnumerator Spawn(float value)
		{
				TextMesh damage = (TextMesh)Instantiate(Text, SpawnPoint.position, Quaternion.identity);

				if (value > 0)
				{
						damage.color = DamageColour;
						damage.text = value.ToString("f0");		
				}
				else
				{
						damage.color = HealColour;
						damage.text = (-value).ToString("f0");		
				}

				// Avoid any issue where monster is destroyed but damage is still there
				Destroy(damage.gameObject, Duration);
				damage.fontSize = InitialSize;

				float t = 0f;
				while (t < 1 && damage)
				{
						int fontSize = Mathf.RoundToInt(InitialSize - t * (InitialSize - FinalSize));
						damage.fontSize = fontSize;

						damage.transform.position = Vector3.Lerp(SpawnPoint.position, DestroyPoint.position, t);
						t += Time.deltaTime / Duration;

						yield return null;
				}

				//Destroy(damage);
		}
}
