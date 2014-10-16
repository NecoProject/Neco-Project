using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class SkillStats
{
		public List<SkillAttribute> Attributes
		{
				get { return _attributes; }
				set
				{
						_attributes = value;
						RefreshCachedAttributes();
				}
		}

		public float MinDamage
		{
				get { return _minDmg; }
				private set { _minDmg = value; }
		}

		public float MaxDamage
		{
				get { return _maxDmg; }
				private set { _maxDmg = value; }
		}

		public float Cost
		{
				get { return _cost; }
				private set { _cost = value; }
		}

		public float CoolDown
		{
				get { return _coolDown; }
				private set { _coolDown = value; }
		}

		public float Radius
		{
				get { return _radius; }
				private set { _radius = Mathf.Max(0.1f, value); }
		}

		public string SpriteName;
		public string SkillName;
		public float Level;

		//public int NumberOfUses;

		// Keeping that to change them in the editor, not really clean...
		// Will have to populate that from a stats sheet sooner or later
		private List<SkillAttribute> _attributes;
		private float _minDmg;
		private float _maxDmg;
		private float _cost;
		private float _coolDown;
		private float _radius;


		void RefreshCachedAttributes()
		{
				SkillAttribute damageAtt = _attributes.Find(x => SkillAttribute.Type.DAMAGE == x.AttributeType);
				if (damageAtt != null) MinDamage = damageAtt.Value * 0.6f;
				if (damageAtt != null) MaxDamage = damageAtt.Value / 0.6f;

				SkillAttribute costAtt = _attributes.Find(x => SkillAttribute.Type.COST == x.AttributeType);
				if (costAtt != null) Cost = costAtt.Value;

				SkillAttribute cooldownAtt = _attributes.Find(x => SkillAttribute.Type.COOLDOWN == x.AttributeType);
				if (cooldownAtt != null) CoolDown = cooldownAtt.Value;

				SkillAttribute radiusAtt = _attributes.Find(x => SkillAttribute.Type.RADIUS == x.AttributeType);
				if (radiusAtt != null) Radius = radiusAtt.Value;
		}

		public SkillAttribute GetAttribute(SkillAttribute.Type type)
		{
				return Attributes.Where(x => x.AttributeType == type).FirstOrDefault();
		}

		public List<SkillAttribute.Type> GetAttributeNames()
		{
				return Attributes.Select(x => x.AttributeType).ToList();
		}

		public static SkillStats CreateRandom()
		{
				SkillStats skill = new SkillStats();
				List<SkillAttribute> debugAtt = ResourceLoader.Attributes.Attributes;
				foreach (SkillAttribute att in debugAtt)
				{
						att.Value = UnityEngine.Random.Range(0, 10);
				}
				skill.Attributes = debugAtt;
				return skill;
		}
}
