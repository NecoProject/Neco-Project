using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class Health : MonoBehaviour
{
		private PlayerScript _player;
		private Text _text;
		private Slider _slider;

		void Start()
		{
				_player = FindObjectOfType<PlayerScript>();
				_text = GameObject.Find("HealthText").GetComponent<Text>();
				_slider = GetComponent<Slider>();
		}

		// TODO: maybe better to not redraw every frame, but rather only when the health changes
		void Update()
		{
				_text.text = _player.Stats.CurrentHealth + " / " + _player.Stats.MaxHealth;
				_slider.normalizedValue = _player.Stats.CurrentHealth * 1.0f / _player.Stats.MaxHealth;
		}
}
