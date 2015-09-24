using UnityEngine;
using System.Collections;

public class LeviathanInInventory : WeapStateInInventory {
    
    public LeviathanInInventory(WeaponI passed)
    {
        myWeapon = passed;
    }
    public override void drop()
    {
        myWeapon.Visuals.turnOnRenderers();
        myWeapon.switchToNotOnPlayerState();
    }

    public override void switchTo()//Turn Stuff On Here
    {
        myWeapon.Visuals.turnOnRenderers();
        myWeapon.switchToOnPlayer();
    }
}
