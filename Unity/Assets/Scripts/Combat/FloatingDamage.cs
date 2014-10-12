using UnityEngine;
using System.Collections;

public class FloatingDamage : MonoBehaviour
{
		public Transform SpawnPoint, DestroyPoint;
		public int InitialSize, FinalSize;
		public float Duration;
		public TextMesh Text;

		public IEnumerator Spawn(float value)
		{
				TextMesh damage = (TextMesh)Instantiate(Text, SpawnPoint.position, Quaternion.identity);
				// Avoid any issue where monster is destroyed but damage is still there
				Destroy(damage.gameObject, Duration);
				damage.text = value.ToString("f0");
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
