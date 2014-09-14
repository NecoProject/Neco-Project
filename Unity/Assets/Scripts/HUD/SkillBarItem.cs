using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class SkillBarItem : MonoBehaviour
{
		public Transform DefaultSkill;
		public int NumberOfUses;

		private SkillStats _skill;
		public Image _coolDownImage;

		public float _timeOfLastUse;
		public float _timeOfNextPossibleUse;
		public bool _isCoolingDown;

		void Start()
		{
				Transform cd = transform.parent.transform.Find("Cooldown");
				if (cd)
				{
						_coolDownImage = cd.GetComponent<Image>();
				}
		}

		public void SetSkill(SkillStats skill)
		{
				this._skill = skill;
				if (skill != null)
				{
						GetComponent<Image>().sprite = GameObject.Find("PrefabManager").GetComponent<PrefabManager>().GetSprite(skill.SpriteName);
				}
		}

		public SkillStats GetSkill() { return _skill; }

		public void Fire(Vector3 spaceTarget, PlayerStats playerStats)
		{
				if (_skill.SkillName == null || _skill.SpriteName.Length == 0) return;
						
				bool canPay = _skill.Cost <= playerStats.CurrentMana;

				if (!_isCoolingDown && canPay)
				{
						// Simulate a click on the button to trigger the nice effects
						EventSystemManager.currentSystem.SetSelectedGameObject(GetComponentInChildren<Button>().gameObject, null);
						GetComponentInChildren<Button>().OnSubmit(null);
						//_button.OnSubmit(null);

						// Pay the cost
						PayManaCost(playerStats, _skill.Cost);

						// Increase number of uses
						NumberOfUses++;

						// Show the animation, that will then interact with the enemy to actually damage them
						Debug.Log("Firing at space target: " + spaceTarget);
						Sprite sprite = GameObject.Find("PrefabManager").GetComponent<PrefabManager>().GetSprite(_skill.SpriteName);
						Transform spellObject = (Transform)Instantiate(DefaultSkill, spaceTarget, Quaternion.identity);
						spellObject.localScale = new Vector3(_skill.Radius, _skill.Radius);
						spellObject.GetComponent<SpriteRenderer>().sprite = sprite;
						spellObject.GetComponent<SpellObject>().Skill = _skill;

						// Run the cooldown
						// TODO: dirty! Use a State Machine instead, as shown in http://unitygems.com/fsm1/
						_isCoolingDown = true;
						_timeOfNextPossibleUse = Time.time + _skill.CoolDown;

						_timeOfLastUse = Time.time;

				}
		}

		void Update()
		{
				if (!_isCoolingDown)
				{
						return;
				}

				if (_timeOfNextPossibleUse < Time.time)
				{
						_isCoolingDown = false;
						//_coolDownImage.fillAmount = 0;
						return;
				}

				float coolDownPercentage = (_timeOfNextPossibleUse - Time.time) / _skill.CoolDown;
				// http://issuetracker.unity3d.com/issues/ui-image-fill-amount-breaks-when-value-is-set-to-0-through-script
				_coolDownImage.fillAmount = Mathf.Max(0.001f, coolDownPercentage);
		}


		/// TODO: have a dedicated script handle life / mana, as you will have to 
		/// take care of regeneration and other stuff that would make the player script 
		/// too crowded
		private void PayManaCost(PlayerStats stats, float cost)
		{
				stats.CurrentMana = Mathf.Min(stats.MaxMana, stats.CurrentMana - cost);
		}
}
