using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SpellObject : MonoBehaviour
{
		public SkillStats Skill
		{
				get { return _skill; }
				set
				{
						_skill = value;
						InitSkillComponents();
				}
		}

		public GameObject Origin;
		public bool ShouldSkipDestroyOnCreate;

		private SkillStats _skill;
		private List<SkillComponent> _allComponents;

		public void Start()
		{
				if (!ShouldSkipDestroyOnCreate)
				{
						Destroy(gameObject, 1);
				}
		}

		public void ApplyEffects(GameObject target)
		{
				foreach (SkillComponent component in _allComponents)
				{
						component.ApplyEffect(target);
				}
		}
		
		void InitSkillComponents()
		{
				_allComponents = new List<SkillComponent>(GetComponentsInChildren<SkillComponent>());
				foreach (SkillComponent component in _allComponents)
				{
						component.Init(_skill);
				}
		}
}
