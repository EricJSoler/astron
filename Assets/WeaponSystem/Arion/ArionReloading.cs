using UnityEngine;
using System.Collections;

public class ArionReloading : WeapStateReloading {

    public ArionReloading(WeaponI passed)
    {
        myWeapon = passed;
    }
    public override void drop()
    {
        myWeapon.Visuals.turnOnRenderers();
        myWeapon.switchToNotOnPlayerState();
    }

    public override void switchFrom()
    {
      
    }
}
