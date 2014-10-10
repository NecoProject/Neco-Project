using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Choose the skills you want to continue the adventure with
/// </summary>
public class SkillSelection : MonoBehaviour
{
		public SkillEvolution SkillEvolution;
		public SkillCustomization SkillCustomization;
		public Image DraggedIcon;

		private SkillBarItem _dragged;

		public void BeginDrag(Image draggedImage, SkillBarItem draggedSkill)
		{
				if (!SkillCustomization.HasCustomized)
				{
						DraggedIcon.gameObject.SetActive(true);
						DraggedIcon.sprite = draggedImage.sprite;
						_dragged = draggedSkill;
				}
		}

		public void Drag(Vector2 position)
		{
				if (!SkillCustomization.HasCustomized)
				{
						DraggedIcon.transform.position = new Vector2(position.x + 1, position.y - 1);
				}
		}

		public void EndDrag()
		{
				DraggedIcon.gameObject.SetActive(false);
		}

		public void Drop(SkillBarItem dropped)
		{
				if (!SkillCustomization.HasCustomized)
				{
						// Exchange place for the skills
						SkillStats draggedSkill = _dragged.GetSkill();
						SkillStats droppedSkill = dropped.GetSkill();

						_dragged.SetSkill(droppedSkill);
						dropped.SetSkill(draggedSkill);

						SkillCustomization.UpdateValidationButton();
				}
		}
}