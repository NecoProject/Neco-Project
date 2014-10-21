using UnityEngine;
using System.Collections;

public class SpellObject : MonoBehaviour
{
		public SkillStats Skill;

		public void Start()
		{
				Destroy(gameObject, 1);
		}

		// TODO: not elegant design. Each attribute should be able to act (or not act) on its target, and basically 
		// this would only delegate teh responsibility to each attribute.
		public void ActOnPlayer(GameObject player)
		{
				if (Skill.Armor > 0)
				{
						player.SendMessage("SetArmor", Skill.Armor);
				}
		}
}
