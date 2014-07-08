using UnityEngine;
using System.Collections;

public class SkillBarItem : MonoBehaviour
{
		public SpellScript skill;
		public bool handleClicks = false;

		public void SetSkill(SpellScript skill) 
		{
				this.skill = skill;
				if (skill != null)
				{
						GetComponent<SpriteRenderer>().sprite = skill.GetComponent<SpriteRenderer>().sprite;
				}
		}

		void OnMouseDown()
		{
				if (handleClicks)
				{
						Messenger<SkillBarItem>.Broadcast(EventNames.SKILL_CLICKED, this);
				}
		}

}
