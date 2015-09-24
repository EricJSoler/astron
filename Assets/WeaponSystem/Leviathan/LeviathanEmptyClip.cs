using UnityEngine;
using System.Collections;

public class LeviathanEmptyClip : WeapStateEmptyClip {

    public LeviathanEmptyClip(WeaponI passed)
    {
        myWeapon = passed;
    }
    public override void reload()
    {
        myWeapon.switchToReloadState();
    }
    public override void aim()
    {
        float crossHairRectLocX = Screen.width / 2 - (myWeapon.crossHairSettings.scale.x / 2);
        float crossHairRectLocY = (Screen.height / 2) - (myWeapon.crossHairSettings.scale.y / 2) - 10;
        GUI.DrawTexture(new Rect(new Vector2(crossHairRectLocX, crossHairRectLocY)
            , myWeapon.crossHairSettings.scale), myWeapon.guiCrossHairTexture);
    }

    public override void switchFrom()
    {
        //need to define something here to turn off the visuals
        myWeapon.Visuals.turnOffRenderers();
        myWeapon.switchToInInventoryState();
    }

    public override void drop()
    {
        myWeapon.Visuals.turnOnRenderers();
        myWeapon.switchToNotOnPlayerState();//need to write something here to drop the gameObject
        
    }
}
