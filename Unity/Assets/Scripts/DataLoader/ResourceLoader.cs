using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ResourceLoader : MonoBehaviour
{
		private const string RESOURCE_FOLDER = "Resources/Data";

		private static bool __created = false;
		private static ResourceLoader __instance;

		public AttributesLoader Attributes;

		void Awake()
		{
				//Debug.Log("Instance " + this.GetInstanceID());
				if (!__created)
				{
						// this is the first instance - make it persist
						DontDestroyOnLoad(this.gameObject);
						__created = true;
						__instance = this;
						Init();
				}
				else
				{
						// this must be a duplicate from a scene reload - DESTROY!
						//Debug.Log("Destroying me");
						DestroyImmediate(this.gameObject);
				}
		}


		void Init()
		{
				Attributes = new AttributesLoader();
				Attributes.LoadAttributes();
		}

		public static ResourceLoader GetInstance()
		{
				return __instance;
		}

}
