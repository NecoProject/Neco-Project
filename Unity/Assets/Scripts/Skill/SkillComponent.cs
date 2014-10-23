using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class SkillComponent : MonoBehaviour
{
		public SkillAttribute Attribute;

		public abstract void ApplyEffect(GameObject target);

		public abstract SkillAttribute.Type GetAttribute();

		public abstract bool AppliesToSelf();

		public void Init(SkillStats skill)
		{
				Attribute = skill.Attributes.Find(x => x.AttributeType == GetAttribute());
		}
}
