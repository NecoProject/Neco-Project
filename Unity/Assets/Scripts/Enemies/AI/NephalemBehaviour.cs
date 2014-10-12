using UnityEngine;
using System.Collections;

public class NephalemBehaviour : EnemyBehaviour
{
		// The Nephalem has two different attacks: a standard attack, and 
		// an attack that drains life
		public StandardAttack StandardAttack;
		public NephalemNova NephalemNova;

		private EnemySkill _currentSkill;

		protected override void PerformAttack()
		{
				if (NephalemNova.CanUse())
				{
						_currentSkill = NephalemNova;
				}
				else
				{
						_currentSkill = StandardAttack;
				}

				_currentSkill.Attack(_target, _damage);
		}

		protected override IEnumerator AnimateAttack()
		{
				return _currentSkill.Animate(_target);
		}
}
