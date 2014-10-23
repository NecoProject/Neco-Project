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

		[SerializeField]
		private SkillStats _skill;
		[SerializeField]
		private List<SkillComponent> _allComponents;

		public void Start()
		{
				Destroy(gameObject, 1);
		}

		public void ApplyEffects(GameObject target)
		{
				if (target == Origin)
				{
						ApplySelfEffects(target);
				}
				else
				{
						ApplyEnemyEffects(target);
				}
		}

		void ApplySelfEffects(GameObject target)
		{
				List<SkillComponent> applicableSkillEffects = GetSelfSkillEffects();
				ApplyEffects(target, applicableSkillEffects);
		}

		void ApplyEnemyEffects(GameObject target)
		{
				List<SkillComponent> applicableSkillEffects = GetEnemySkillEffects();
				ApplyEffects(target, applicableSkillEffects);
		}

		void ApplyEffects(GameObject target, List<SkillComponent> effects)
		{
				foreach (SkillComponent component in effects)
				{
						component.ApplyEffect(target);
				}
		}

		List<SkillComponent> GetSelfSkillEffects()
		{
				return _allComponents.FindAll(x => x.AppliesToSelf());
		}

		List<SkillComponent> GetEnemySkillEffects()
		{
				return _allComponents.FindAll(x => !x.AppliesToSelf());
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
