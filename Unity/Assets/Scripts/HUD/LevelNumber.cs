using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class LevelNumber : MonoBehaviour
{
		void Start()
		{
				GetComponent<Text>().text = "Level " + FindObjectOfType<Save>().SaveData.CurrentLevel;
		}
}
