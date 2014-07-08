using UnityEngine;
using System.Collections;

public class SpellScript : MonoBehaviour
{
		public SkillStats Stats;
		
		public override string ToString()
		{
				return "SpellScript[" + Stats.ToString() + "]";
		}
}
