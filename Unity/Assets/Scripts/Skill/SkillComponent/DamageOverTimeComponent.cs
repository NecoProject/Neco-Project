using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DamageOverTimeComponent : SkillComponent
{
		public override SkillAttribute.Type GetAttribute()
		{
				return SkillAttribute.Type.DAMAGE_OVER_TIME;
		}

		public override bool AppliesToSelf()
		{
				return false;
		}

		public override void ApplyEffect(GameObject target)
		{
				if (Attribute == null || Attribute.Value == 0) return;

				target.SendMessage("AddDamageOverTime", new DamageOverTime(Attribute.Value2, Attribute.Value));
		}

}
