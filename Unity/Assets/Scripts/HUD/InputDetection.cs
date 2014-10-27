using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class InputDetection : MonoBehaviour
{
		public PlayerScript Consumer;
		public float DoubleTapTolerance = 0.05f;

		private float _touchDuration;
		private Touch _touch;
		private bool _tapping, _swiping;

		void Update()
		{
				_tapping = false;
				// Handle first the touch elements
				// Touch controls are:
				// - Tap for skill 0
				// - Double tap for skill 1
				// - Tap with two fingers for skill 2
				// - Swipe for skill 3
				StartCoroutine(GetButtonIndexForTouch());
				if (_tapping) return;

				// Then fallback to the mouse controls
				StartCoroutine(GetButtonIndexForMouse());
		}

		IEnumerator GetButtonIndexForTouch()
		{
				if (Input.touchCount > 0)
				{
						_touchDuration += Time.deltaTime;
						_touch = Input.GetTouch(0);
						_tapping = true;


						// Tap with two finger
						if (Input.touchCount == 2 && _touch.phase == TouchPhase.Ended)
						{
								_swiping = false;
								StartCoroutine(DoubleFingers());
								yield return null;
						}

						// making sure it only check the touch once && it was a short touch/tap and not a dragging.
						if (Input.touchCount == 1 && _touch.phase == TouchPhase.Ended && _touch.tapCount == 1 && _touchDuration < 0.2f)
						{
								_swiping = false;
								StartCoroutine(SingleOrDouble());
								yield return null;
						}

						if (Input.touchCount == 1 && _touch.phase == TouchPhase.Moved)
						{
								_swiping = true;
						}

						if (Input.touchCount == 1 && _touch.phase == TouchPhase.Ended && _swiping)
						{
								StartCoroutine(Swipe());
								_swiping = false;
								yield return null;
						}

				}
				else
				{
						_touchDuration = 0f;
						_swiping = false;
				}
		}

		IEnumerator Swipe()
		{
				yield return new WaitForSeconds(DoubleTapTolerance);
				if (_touch.tapCount == 1)
				{
						Consumer.ShootAtMousePosition(3);
				}
		}

		IEnumerator DoubleFingers()
		{
				yield return new WaitForSeconds(DoubleTapTolerance);
				if (_touch.tapCount == 1)
				{
						Consumer.ShootAtMousePosition(2);
				}
		}

		IEnumerator SingleOrDouble()
		{
				yield return new WaitForSeconds(DoubleTapTolerance);
				if (_touch.tapCount == 1)
				{
						Consumer.ShootAtMousePosition(0);
				}
				else if (_touch.tapCount == 2)
				{
						Consumer.ShootAtMousePosition(1);
				}
		}

		IEnumerator GetButtonIndexForMouse()
		{
				foreach (ShootingButton button in ShootingButton.GetEnumeration())
				{
						if (Input.GetButtonDown(button.GetButtonName()) && !EventSystem.current.IsPointerOverGameObject())
						{
								Consumer.ShootAtMousePosition(button.GetSkillReference());
								yield return null;
						}
				}
		}

}