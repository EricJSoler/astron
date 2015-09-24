using UnityEngine;
using System.Collections;

public class LeviathanNotOnPlayer : WeaponStateNotOnPlayer {
    
    public LeviathanNotOnPlayer(WeaponI passed)
    {
        myWeapon = passed;
    }
    public override void pickup()
    {
        Debug.Log("Havent Done PickUpYet");
    }
}
