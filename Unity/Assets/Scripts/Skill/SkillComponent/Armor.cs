using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Armor : SkillComponent
{
		public override SkillAttribute.Type GetAttribute()
		{
				return SkillAttribute.Type.ARMOR;
		}

		public override bool AppliesToSelf()
		{
				return true;
		}

		public override void ApplyEffect(GameObject target)
		{
				if (Attribute == null || Attribute.Value == 0) return;

				target.SendMessage("SetArmor", Attribute.Value);
		}

}
