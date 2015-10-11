using UnityEngine;
using System.Collections;

public class LeviathanLoadedClip : WeapStateLoadedClip {

    public LeviathanLoadedClip(WeaponI passed)
    {
        myWeapon = passed;
    }

    public override void shoot()
    {
        if (myWeapon.weaponStats.clipAmmo <= 0) {
            myWeapon.switchToEmptyClipState();
        }
        else {


            Ray ray = myWeapon.OwnedPlayer.CameraController.Camera.ScreenPointToRay(
                    new Vector3(myWeapon.crossHairSettings.location.x, myWeapon.crossHairSettings.location.y));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, myWeapon.weaponStats.range)) {
                if (hit.collider.gameObject.tag == "Player") {
                    Player enemy = hit.collider.gameObject.GetComponent<Player>();
                    myWeapon.getKillCredit(enemy);
                    myWeapon.doDamage(enemy);
                }
                else if (hit.collider.gameObject.tag == "Enemy") {
                    AIEnemy enemy = hit.collider.gameObject.GetComponent<AIEnemy>();
                    myWeapon.getKillCredit(enemy);
                    myWeapon.doDamage(enemy);

                }

            }

            myWeapon.Visuals.muzzleFlash();
            myWeapon.weaponStats.clipAmmo -= 1;
        }
    }

    public override void aim()
    {
        Ray ray = myWeapon.OwnedPlayer.CameraController.Camera.ScreenPointToRay(
                new Vector3(myWeapon.crossHairSettings.location.x, myWeapon.crossHairSettings.location.y));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, myWeapon.weaponStats.range)) {

            if (hit.collider.gameObject.tag == "Player" || hit.collider.gameObject.tag == "Enemy") {
                myWeapon.guiCrossHairTexture = myWeapon.crossHairSettings.fTexture;

            }
            else {
                myWeapon.guiCrossHairTexture = myWeapon.crossHairSettings.eTexture;
            }
        }
        float crossHairRectLocX = Screen.width / 2 - (myWeapon.crossHairSettings.scale.x / 2);
        float crossHairRectLocY = (Screen.height / 2) - (myWeapon.crossHairSettings.scale.y / 2);
        GUI.DrawTexture(new Rect(new Vector2(crossHairRectLocX, crossHairRectLocY)
            , myWeapon.crossHairSettings.scale), myWeapon.guiCrossHairTexture);
       
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.blue);
    }

    public override void drop()
    {
        myWeapon.Visuals.turnOnRenderers();
        myWeapon.switchToNotOnPlayerState();
    }

    public override void switchFrom()
    {
        
    }
    public override void reload()
    {
        
    }
}
