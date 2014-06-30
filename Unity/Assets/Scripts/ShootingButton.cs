using UnityEngine;
using System.Collections;

public sealed class ShootingButton
{
		private readonly string buttonName;
		private readonly int skillReference;
				
		public static readonly ShootingButton FIRE1 = new ShootingButton ("Fire1", 0);
		public static readonly ShootingButton FIRE2 = new ShootingButton ("Fire2", 1);
		public static readonly ShootingButton FIRE3 = new ShootingButton ("Fire3", 2);
		public static readonly ShootingButton FIRE4 = new ShootingButton ("Fire4", 3);

		private ShootingButton (string name, int skillRef)
		{
				buttonName = name;
				skillReference = skillRef;
		}

		public string GetButtonName ()
		{
				return buttonName;
		}

		public int GetSkillReference ()
		{
				return skillReference;
		}

		public static ShootingButton[] GetEnumeration ()
		{
				return new ShootingButton[]{FIRE1, FIRE2, FIRE3, FIRE4};
		} 
}