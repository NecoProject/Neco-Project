using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class SkillCustomization : MonoBehaviour
{
		public SkillEvolution SkillEvolution;
		public Button ValidationButton, CustomizationConfirmationButton, CustomizationValidateChoiceButton;
		public Text SkillNewName;
		public GameObject CustomizationPanel;
		public List<Image> PossibleImages;

		public bool HasCustomized = false;

		private SkillBarItem _skillToCustomize;
		private PrefabManager _prefabManager;
		public GameObject _selectedImage;

		void Start()
		{
				_prefabManager = GameObject.Find("PrefabManager").GetComponent<PrefabManager>();
		}

		public void UpdateValidationButton()
		{
				// Only one!
				_skillToCustomize = FindSkillToCustomize(SkillEvolution.Current);
				ValidationButton.gameObject.SetActive(!HasCustomized && _skillToCustomize != null);
		}

		public void DisplaySkillCustomization()
		{
				// Set when dropping a skill in the skill bar
				CustomizationPanel.SetActive(true);

				// TODO: Bad! Will need a proper handling of custom skill items, possibly
				// even generate them procedurally too, and offer 3-4 of them
				for (int i = 0; i < PossibleImages.Count; i++)
				{
						PossibleImages[i].sprite = _prefabManager.prefabs[i].Sprite;
				}
		}

		public void SelectImage(GameObject image)
		{
				if (_selectedImage == image)
				{
						_selectedImage.GetComponent<Outline>().enabled = false;
						_selectedImage = null;
				}
				else
				{
						if (_selectedImage != null)
						{
								_selectedImage.GetComponent<Outline>().enabled = false;
						}
						_selectedImage = image;
						_selectedImage.GetComponent<Outline>().enabled = true;
				}
				CustomizationValidateChoiceButton.interactable = _selectedImage != null;
		}

		public void ValidateCustomization()
		{
				_skillToCustomize.GetSkill().SkillName = SkillNewName.text;
				_skillToCustomize.GetSkill().SpriteName = _selectedImage.GetComponent<Image>().sprite.name;
				// Rfresh the image. Not very clean
				_skillToCustomize.SetSkill(_skillToCustomize.GetSkill());
				CustomizationPanel.SetActive(false);
				ValidationButton.gameObject.SetActive(false);
				HasCustomized = true;
		}

		SkillBarItem FindSkillToCustomize(List<SkillBarItem> haystack)
		{
				SkillBarItem needle = null;
				foreach (SkillBarItem current in haystack)
				{
						if (current.GetSkill().SkillName == "New Skill")
						{
								// Already one skill to customize, don't allow that (ideally, should be done
								// pre-emptively instead of at validation)
								if (needle != null) return null;

								needle = current;
						}
				}
				return needle;
		}
}