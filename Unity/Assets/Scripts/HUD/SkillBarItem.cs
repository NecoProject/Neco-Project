using UnityEngine;
using System.Collections;

public class SkillBarItem : MonoBehaviour
{
		public SkillStats skill;
		public bool handleClicks = false;

		public void SetSkill(SkillStats skill) 
		{
				this.skill = skill;
				if (skill != null)
				{
						GetComponent<SpriteRenderer>().sprite = GameObject.Find("PrefabManager").GetComponent<PrefabManager>().GetSprite(skill.SpriteName);
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
