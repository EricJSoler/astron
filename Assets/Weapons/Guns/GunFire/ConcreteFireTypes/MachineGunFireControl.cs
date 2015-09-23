using UnityEngine;
using System.Collections;

public class MachineGunFireControl : GunFireControl {

	bool isFiring;
	private float nextFire = 0.0f;
	private float fireRate;
	// Use this for initialization
	
	public MachineGunFireControl(float inSpeed, Gun myGun)
	{
		fireRate = inSpeed;
		myCurrentGun = myGun;
	}
	




	public override void gunControl()
	{
		if (Input.GetMouseButtonDown (0)) {

			isFiring = true;
		}

		if (Input.GetMouseButtonUp (0)) {
			
			isFiring = false;
		}

		if (isFiring) {
			
			if(Time.time > nextFire)
			{
				
				nextFire = Time.time + fireRate;
				//PlayerInventory.getCurrentWeapon().GetComponent<Gun>().fireShot();
				myCurrentGun.fireShot();
			}
			
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
