using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Save : MonoBehaviour
{
		private static bool __created = false;

		void Awake()
		{
				if (!__created)
				{
						// this is the first instance - make it persist
						DontDestroyOnLoad(this.gameObject);
						__created = true;
				}
				else
				{
						// this must be a duplicate from a scene reload - DESTROY!
						Destroy(this.gameObject);
				}
		}
}
