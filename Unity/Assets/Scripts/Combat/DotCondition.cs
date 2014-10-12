using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DotCondition : MonoBehaviour
{
		public float IntervalToDisplayDotDamage;

		private List<DamageOverTime> dotList = new List<DamageOverTime>();
		private float _damageToApply;
		private float _timeLastDamageApplied;
		
		public void Apply(DamageOverTime dot)
		{
				dotList.Add(dot);
		}

		void Update()
		{
				for (int i = dotList.Count - 1; i >= 0; i--)
				{
						DamageOverTime dot = dotList[i];
						_damageToApply += dot.DamagePerSecond * Time.deltaTime;
						dot.Duration = dot.Duration - Time.deltaTime;

						if (dot.Duration <= 0)
						{
								dotList.RemoveAt(i);
						}
				}

				if (Time.time > _timeLastDamageApplied + IntervalToDisplayDotDamage)
				{
						_timeLastDamageApplied = Time.time;

						if (_damageToApply > 0)
						{
								gameObject.SendMessage("TakeDamage", _damageToApply);
								_damageToApply = 0;
						}
				}
		}
}
