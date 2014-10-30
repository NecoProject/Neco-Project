using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class AttributesLoader : AbstractLoader
{

		private const string TYPE = "type";
		private const string IS_BASE = "is_base";
		private const string NEED_UNLOCK = "need_unlock";
		private const string IS_MANDATORY = "is_mandatory";
		private const string IS_BONUS = "is_bonus";
		//private const string ACT_ON_SELF = "act_on_self";
		private const string MIN_VALUE = "min_value";
		private const string MAX_VALUE = "max_value";
		private const string MIN_VALUE_2 = "min_value_2";
		private const string MAX_VALUE_2 = "max_value_2";
		private const string MIN_SKILL_LEVEL = "min_skill_level";
		private const string SPAWN_PROBABILITY = "spawn_probability";
		private const string GRAPHICAL_EFFECT = "graphical_effect";
		private const string ICON = "icon";

		public List<SkillAttribute> Attributes;

		private List<SkillAttribute.Type> _mandatoryAttributes, _nonBaseAttributes;

		public void LoadAttributes()
		{
				Attributes = new List<SkillAttribute>();
				Load();
		}

		public SkillAttribute GetAttribute(SkillAttribute.Type type) {
				return Attributes.Where(x => x.AttributeType == type).FirstOrDefault(); 
		}

		public List<SkillAttribute.Type> MandatoryAttributes()
		{
				if (_mandatoryAttributes == null)
				{
						_mandatoryAttributes = Attributes.Where(x => x.IsMandatory).Select(x => x.AttributeType).ToList();
				}
				return _mandatoryAttributes;
		}

		public List<SkillAttribute.Type> NonBaseAttributes()
		{
				if (_nonBaseAttributes == null)
				{
						_nonBaseAttributes = Attributes.Where(x => !x.IsBase && !x.NeedUnlock).Select(x => x.AttributeType).ToList();
				}
				return _nonBaseAttributes;
		}

		protected override void LoadItem(string[] data)
		{
				SkillAttribute attribute = new SkillAttribute();

				attribute.AttributeType = (SkillAttribute.Type)Enum.Parse(typeof(SkillAttribute.Type), getValue(data, TYPE));
				attribute.GraphicalEffect = getValue(data, GRAPHICAL_EFFECT);
				attribute.Icon = getValue(data, ICON);
				attribute.IsBonus = bool.Parse(getValue(data, IS_BONUS));
				attribute.IsMandatory = bool.Parse(getValue(data, IS_MANDATORY));
				attribute.IsBase = bool.Parse(getValue(data, IS_BASE));
				attribute.NeedUnlock = bool.Parse(getValue(data, NEED_UNLOCK));
				//attribute.ActOnSelf = bool.Parse(getValue(data, ACT_ON_SELF));

				float ignoreMe;
				if (float.TryParse(getValue(data, SPAWN_PROBABILITY), out ignoreMe))
				{
						attribute.SpawnProbability = float.Parse(getValue(data, SPAWN_PROBABILITY));
				}
				if (float.TryParse(getValue(data, MIN_VALUE), out ignoreMe))
				{
						attribute.MinValue = float.Parse(getValue(data, MIN_VALUE));
				}
				if (float.TryParse(getValue(data, MAX_VALUE), out ignoreMe))
				{
						attribute.MaxValue = float.Parse(getValue(data, MAX_VALUE));
				}
				if (float.TryParse(getValue(data, MIN_VALUE_2), out ignoreMe))
				{
						attribute.MinValue2 = float.Parse(getValue(data, MIN_VALUE_2));
				}
				if (float.TryParse(getValue(data, MAX_VALUE_2), out ignoreMe))
				{
						attribute.MaxValue2 = float.Parse(getValue(data, MAX_VALUE_2));
				}
				attribute.MinimumSkillLevel = float.Parse(getValue(data, MIN_SKILL_LEVEL));

				Attributes.Add(attribute);
		}

		protected override string File()
		{
				return "skill_attributes";
		}
}
