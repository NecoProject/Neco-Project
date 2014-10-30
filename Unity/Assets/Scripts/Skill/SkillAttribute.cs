using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SkillAttribute
{
		public enum Type
		{
				COOLDOWN, RADIUS, COST, DAMAGE, 
				ARMOR,
				DAMAGE_OVER_TIME
		}

		public Type AttributeType;
		public bool IsMandatory;
		public bool IsBase;
		public bool NeedUnlock;
		public float SpawnProbability;
		public float MinValue = -100000;
		public float MaxValue = 100000;
		public float MinValue2 = -100000;
		public float MaxValue2 = 100000;
		public float Value
		{
				get { return _value; }
				set { _value = Mathf.Max(MinValue, Mathf.Min(MaxValue, value)); }
		}
		public float Value2
		{
				get { return _value2; }
				set { _value2 = Mathf.Max(MinValue2, Mathf.Min(MaxValue2, value)); }
		}
		public bool IsBonus;
		public float MinimumSkillLevel;
		public string GraphicalEffect;
		public string Icon;

		[SerializeField]
		private float _value, _value2;

		public SkillAttribute Clone()
		{
				SkillAttribute clone = new SkillAttribute();
				clone.AttributeType = AttributeType;
				clone.IsBase = IsBase;
				clone.NeedUnlock = NeedUnlock;
				clone.GraphicalEffect = GraphicalEffect;
				clone.IsBonus = IsBonus;
				clone.IsMandatory = IsMandatory;
				clone.MaxValue = MaxValue;
				clone.MinValue = MinValue;
				clone.MaxValue2 = MaxValue2;
				clone.MinValue2 = MinValue2;
				clone.SpawnProbability = SpawnProbability;
				clone.MinimumSkillLevel = MinimumSkillLevel;
				clone.Icon = Icon;
				return clone;
		}

		public override string ToString()
		{
				return "SkillAttribute[AttributeType=" + AttributeType.ToString() + ", MinValue=" + MinValue +
						", MaxValue=" + MaxValue + ", Value=" + Value + "]";
		}
}
