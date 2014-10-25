using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class EnemySkill : MonoBehaviour
{
		[SerializeField]
		public SkillStats Skill;
		public bool IsCoolingDown
		{
				get { return _coolingDown; }
				set
				{
						_coolingDown = value;
						if (_coolingDown)
						{
								_coolDownLeft = Skill.CoolDown + Random.Range(-1f, 1f);
						}
				}
		}

		private float _coolDownLeft;
		private bool _coolingDown;

		void Awake()
		{
				Skill.RefreshCachedAttributes();
				IsCoolingDown = true;
		}

		void Update()
		{
				if (!IsCoolingDown) return;

				_coolDownLeft = _coolDownLeft - Time.deltaTime;
				if (_coolDownLeft <= 0)
				{
						IsCoolingDown = false;
						_coolDownLeft = 0;
				}
		}

		public void ApplyStats(EnemyStats stats)
		{
				Skill.ApplyModifiers(stats);
		}
}
