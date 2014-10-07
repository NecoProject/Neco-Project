using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class LevelProgress : MonoBehaviour
{
		public Slider slider;

		void OnEnable()
		{
				Messenger<int, int>.AddListener(EventNames.WAVE_COMPLETE, OnWaveComplete);
		}

		void OnDisable()
		{
				Messenger<int, int>.RemoveListener(EventNames.WAVE_COMPLETE, OnWaveComplete);
		}

		void OnWaveComplete(int currentWave, int totalWaves)
		{
				slider.maxValue = totalWaves;
				slider.value = currentWave;
		}
}
