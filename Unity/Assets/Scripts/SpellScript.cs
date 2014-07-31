using UnityEngine;
using System.Collections;

public class SpellScript : MonoBehaviour
{
		public SkillStats Stats;

		void Awake()
		{
				DontDestroyOnLoad(this.gameObject);
		}
		
		public override string ToString()
		{
				return "SpellScript[" + Stats.ToString() + "]";
		}
}
