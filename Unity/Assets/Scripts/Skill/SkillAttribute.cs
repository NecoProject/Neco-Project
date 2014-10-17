using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SkillAttribute
{
		public enum Type
		{
				COOLDOWN, RADIUS, COST, DAMAGE
		}

		public Type AttributeType;
		public bool IsMandatory;
		public float MinValue = -100000;
		public float MaxValue = 100000;
		public float Value
		{
				get { return _value; }
				set { _value = Mathf.Max(MinValue, Mathf.Min(MaxValue, value)); }
		}
		public bool IsBonus;
		public float MinimumSkillLevel;
		public string GraphicalEffect;

		[SerializeField]
		private float _value;

		public SkillAttribute Clone()
		{
				SkillAttribute clone = new SkillAttribute();
				clone.AttributeType = AttributeType;
				clone.GraphicalEffect = GraphicalEffect;
				clone.IsBonus = IsBonus;
				clone.IsMandatory = IsMandatory;
				clone.MaxValue = MaxValue;
				clone.MinValue = MinValue;
				clone.MinimumSkillLevel = MinimumSkillLevel;
				return clone;
		}

		public override string ToString()
		{
				return "SkillAttribute[AttributeType=" + AttributeType.ToString() + ", MinValue=" + MinValue +
						", MaxValue=" + MaxValue + ", Value=" + Value + "]";
		}
}
