using UnityEngine;
using System.Collections;

public class LeviathanReloading : WeapStateReloading {

    public LeviathanReloading(WeaponI passed)
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
