using UnityEngine;

public class PlayerScript : MonoBehaviour
{
		public Transform spell1Sprite;

		void Update ()
		{
				shootAtMousePosition ();
		}

		void shootAtMousePosition ()
		{
				if (Input.GetButtonDown ("Fire1")) {
						Vector3 screenTarget = Input.mousePosition;
						// Get the correct Z, because the current one is the Camera, circa -10
						var correctZ = transform.position.z;
						screenTarget.z = correctZ;
						Vector3 spaceTarget = Camera.main.ScreenToWorldPoint (screenTarget);
						// KABOOM
						Instantiate (spell1Sprite, spaceTarget, Quaternion.identity);
				}
		}
} 