using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Mana : MonoBehaviour
{
		private PlayerScript _player;
		private TextMesh _display;

		void Start()
		{
				_player = FindObjectOfType<PlayerScript>();
				_display = GetComponent<TextMesh>();
		}

		void Update()
		{
				_display.text = _player.Stats.CurrentMana + " / " + _player.Stats.MaxMana;
		}
}
