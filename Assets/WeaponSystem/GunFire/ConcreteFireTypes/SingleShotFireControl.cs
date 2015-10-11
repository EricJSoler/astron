using UnityEngine;
using System.Collections;

public class SingleShotFireControl : GunFireControl {
	
	public SingleShotFireControl(WeaponI myGun, float time)
	{
		myCurrentGun = myGun;
        delayBetweenShots = time;
        lastShotTime = Time.time;
	}

	public override void gunControl()
	{
		if (Input.GetMouseButtonDown (0)) {

            if (Time.time - lastShotTime >= delayBetweenShots) {
                myCurrentGun.currentState.shoot();
                lastShotTime = Time.time;
            }
		}

	}

    float delayBetweenShots;
    float lastShotTime;

	


}
