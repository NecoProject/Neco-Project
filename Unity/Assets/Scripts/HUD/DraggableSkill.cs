using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class DraggableSkill : MonoBehaviour, IDragHandler, IDropHandler
{
		public SkillSelection Selection;

		private Image _skillImage;
		private SkillBarItem _skill;

		void Start()
		{
				_skillImage = GetComponent<Image>();
				_skill = GetComponent<SkillBarItem>();
		}

		public void OnDrag(PointerEventData eventData)
		{
				// TODO add condition to prevent dragging when there is no skill
				Selection.Drag(_skillImage, _skill, eventData.position);
		}

		public void OnDrop(PointerEventData eventData)
		{
				Selection.Drop(_skill);
		}
}
