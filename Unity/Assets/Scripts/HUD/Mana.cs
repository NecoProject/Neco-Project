using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class Mana : MonoBehaviour
{
		private PlayerScript _player;
		private Text _text;
		private Slider _slider;

		void Start()
		{
				_player = FindObjectOfType<PlayerScript>();
				_text = GameObject.Find("ManaText").GetComponent<Text>();
				_slider = GetComponent<Slider>();
		}

		void Update()
		{
				_text.text = _player.Stats.CurrentMana + " / " + _player.Stats.MaxMana;
				_slider.normalizedValue = _player.Stats.CurrentMana * 1.0f / _player.Stats.MaxMana;
		}
}
