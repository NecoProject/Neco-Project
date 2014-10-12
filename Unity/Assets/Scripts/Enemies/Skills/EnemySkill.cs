using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public abstract class EnemySkill : MonoBehaviour
{
		public abstract bool CanUse();

		public abstract void Attack(Transform target, float baseDamage);

		public abstract IEnumerator Animate(Transform target);
}
