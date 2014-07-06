using UnityEngine;
using System.Collections;

[System.Serializable]
public class SkillStats 
{
		// NOTE: I removed the "duration" attribute, since it had no real gameplay effect for now

		public float Damage;
		public float Cost;

		public override string ToString()
		{
				return "SkillStats[damage=" + Damage + ", cost=" + Cost + "]";
		}
}
