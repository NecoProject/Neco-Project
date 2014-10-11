using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class DraggableSkill : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
		public SkillSelection Selection;

		private Image _skillImage;
		private SkillBarItem _skill;

		void Start()
		{
				_skillImage = GetComponent<Image>();
				_skill = GetComponent<SkillBarItem>();
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
				// TODO add condition to prevent dragging when there is no skill
				Selection.BeginDrag(_skillImage, _skill);
		}

		public void OnDrag(PointerEventData eventData)
		{
				// TODO add condition to prevent dragging when there is no skill
				Selection.Drag(eventData.position);
		}

		public void OnEndDrag(PointerEventData eventData)
		{
				// TODO add condition to prevent dragging when there is no skill
				Selection.EndDrag();
		}

		public void OnDrop(PointerEventData eventData)
		{
				Selection.Drop(_skill);
		}
}
