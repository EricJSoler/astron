using UnityEngine;
using System.Collections;

public class SingleShotFireControl : GunFireControl {



	public SingleShotFireControl(Gun myGun)
	{
		myCurrentGun = myGun;
	}



	public override void gunControl()
	{
		if (Input.GetMouseButtonDown (0)) {
			
			//PlayerInventory.getCurrentWeapon().GetComponent<Gun>().fireShot();
			myCurrentGun.fireShot();
		}

	}


	public override int fireNow()
	{
		return fire;
	}
	

	public override void setFire(int set)
	{
		fire = set;

	}


}
