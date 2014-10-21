using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class GeneticsConstants
{
		// TODO: pass the script as a MonoBehaviour, so that it can be tweaked in the editor directly? 
		// Not sure we can easily experiment, since it requires an "end level" screen anyway
		public const float BASE_BONUS_MAX = 1f;
		public const float MINIMUM_COOLDOWN = 0.4f;
		public const int MAX_CHILDREN_NUMBER = 3;
}