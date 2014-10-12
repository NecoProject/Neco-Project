using UnityEngine;
using System.Collections;

[System.Serializable]
public class DamageOverTime
{
		public float Duration;
		public float DamagePerSecond;

		public DamageOverTime(float duration, float damagePerSecond)
		{
				Duration = duration;
				DamagePerSecond = damagePerSecond;
		}
}
