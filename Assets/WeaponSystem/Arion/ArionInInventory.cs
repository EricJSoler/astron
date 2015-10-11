using UnityEngine;
using System.Collections;

public class ArionInInventory : WeapStateInInventory {

    public ArionInInventory(WeaponI passed)
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
        
    }
}
