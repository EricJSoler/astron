using UnityEngine;
using System.Collections;

public class BurstShotFireControl : GunFireControl
{


    private int burstRate;
    private float burstSpeed;
    private float nextFire = 0.0f;


    public BurstShotFireControl(int inRate, float inSpeed, WeaponI myGun)
    {
        burstRate = inRate;
        myCurrentGun = myGun;
        burstSpeed = inSpeed;
    }


    public override void gunControl()
    {

        if (Input.GetMouseButtonDown(0)) {

            for (int i = 0; i < burstRate; i++) {
                if (Time.time > nextFire) {

                    nextFire = Time.time + burstSpeed;
                    myCurrentGun.currentState.shoot();
                }
            }

        }
    }

}
