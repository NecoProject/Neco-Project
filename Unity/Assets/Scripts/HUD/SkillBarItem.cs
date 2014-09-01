using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkillBarItem : MonoBehaviour
{
		public SkillStats skill;

		public void SetSkill(SkillStats skill) 
		{
				this.skill = skill;
				if (skill != null)
				{
						GetComponent<Image>().sprite = GameObject.Find("PrefabManager").GetComponent<PrefabManager>().GetSprite(skill.SpriteName);
				}
		}

		public void OnMouseDown()
		{
				Messenger<SkillBarItem>.Broadcast(EventNames.SKILL_CLICKED, this);
		}

}
