using UnityEngine;
using System.Collections;

public class SpellObject : MonoBehaviour
{
		public SkillStats Skill;

		public void Start()
		{
				Destroy(gameObject, 1);
		}
}
