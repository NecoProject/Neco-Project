using UnityEngine;
using System.Collections;

public class SpellScript : MonoBehaviour
{

		public float duration;

		// Use this for initialization
		void Start ()
		{
				Destroy (gameObject, duration);
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
}
