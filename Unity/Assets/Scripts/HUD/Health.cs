using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Health : MonoBehaviour
{
		private PlayerScript _player;
		private TextMesh _display;

		void Start()
		{
				_player = FindObjectOfType<PlayerScript>();
				_display = GetComponent<TextMesh>();
		}

		// TODO: maybe better to not redraw every frame, but rather only when the health changes
		void Update()
		{
				_display.text = _player.Stats.CurrentHealth + " / " + _player.Stats.MaxHealth;
		}
}
