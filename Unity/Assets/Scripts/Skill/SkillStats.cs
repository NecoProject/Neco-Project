using UnityEngine;
using System.Collections;

[System.Serializable]
public class SkillStats
{
		public float MinDamage, MaxDamage;
		public float Cost;
		public float CoolDown
		{
				get { return _coolDown; }
				set { _coolDown = Mathf.Max(0, value); }
		}
		public float Radius
		{
				get { return _radius; }
				set { _radius = Mathf.Max(0.1f, value); }
		}

		public string SpriteName;
		public string SkillName;

		//public int NumberOfUses;

		// Keeping that to change them in the editor, not really clean...
		// Will have to populate that from a stats sheet sooner or later
		public float _coolDown;
		public float _radius;
}
