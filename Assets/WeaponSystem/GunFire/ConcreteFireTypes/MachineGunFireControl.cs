//using UnityEngine;
//using System.Collections;

//public class MachineGunFireControl : GunFireControl {

//    bool isFiring;
//    private float nextFire = 0.0f;
//    private float fireRate;
	
//    public MachineGunFireControl(float inSpeed, Gun myGun)
//    {
//        fireRate = inSpeed;
//        myCurrentGun = myGun;
//    }

//    public override void gunControl()
//    {
//        if (Input.GetMouseButtonDown (0)) {

//            isFiring = true;
//        }

//        if (Input.GetMouseButtonUp (0)) {
			
//            isFiring = false;
//        }

//        if (isFiring) {
			
//            if(Time.time > nextFire)
//            {
				
//                nextFire = Time.time + fireRate;
//                myCurrentGun.fireShot();
//            }
			
//        }

//    }






//}
