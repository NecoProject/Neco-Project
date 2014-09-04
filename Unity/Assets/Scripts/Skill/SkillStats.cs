using UnityEngine;
using System.Collections;

[System.Serializable]
public class SkillStats
{
		public float Damage;
		public float Cost;
		public float CoolDown;

		public string SpriteName;
		public string Name;
		public int NumberOfUses;

		public override string ToString ()
		{
				return "SkillStats[damage=" + Damage + ", cost=" + Cost + ", coolDown=" + CoolDown + "]";
		}
}
