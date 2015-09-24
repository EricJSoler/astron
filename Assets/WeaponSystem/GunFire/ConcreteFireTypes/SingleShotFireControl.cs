using UnityEngine;
using System.Collections;

public class SingleShotFireControl : GunFireControl {
	
	public SingleShotFireControl(WeaponI myGun)
	{
		myCurrentGun = myGun;
	}

	public override void gunControl()
	{
		if (Input.GetMouseButtonDown (0)) {
            myCurrentGun.currentState.shoot();
		}

	}

	


}
