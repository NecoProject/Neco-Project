using UnityEngine;
using System.Collections;

public class NephalemBehaviour : EnemyBehaviour
{
		// Don't really like this, should refactor with better design. Need brainstorm!
		void OnDestroy()
		{
				GameObject.Find("Save").GetComponent<Save>().SaveData.LastUnlockedAttribute =
						ResourceLoader.GetInstance().Attributes.GetAttribute(SkillAttribute.Type.DAMAGE_OVER_TIME);
		}

}
