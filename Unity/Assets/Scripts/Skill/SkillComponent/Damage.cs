using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Damage : SkillComponent
{
		public override SkillAttribute.Type GetAttribute()
		{
				return SkillAttribute.Type.DAMAGE;
		}

		public override bool AppliesToSelf()
		{
				return false;
		}

		public override void ApplyEffect(GameObject target)
		{
				if (Attribute == null || Attribute.Value == 0) return;

				target.SendMessage("TakeDamage", Attribute.Value);
		}

}
