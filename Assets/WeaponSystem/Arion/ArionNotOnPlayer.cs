using UnityEngine;
using System.Collections;

public class ArionNotOnPlayer : WeaponStateNotOnPlayer {


    public ArionNotOnPlayer(WeaponI passed)
    {
        myWeapon = passed;
    }
    public override void pickup()
    {
        Debug.Log("Havent Done PickUpYet");
    }

}
