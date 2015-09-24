using UnityEngine;
using System.Collections;

public class PortalGun : Gun {


    Texture2D guiCrossHairTexture;
    // Use this for initialization
    void Start()
    {
        //bulletSpeed = 100;

        name = "PortalGun";
        gunRange = 300;
        crossHairSettings.location.x = Screen.width / 2;
        crossHairSettings.location.y = (Screen.height / 2);
        crossHairSettings.scale.x = 100;
        crossHairSettings.scale.y = 100;
        guiCrossHairTexture = crossHairSettings.eTexture;
        myGunFireControl = new MachineGunFireControl(0.5f, this);
		//myGunFireControl = new BurstShotFireControl (3, 0.6f, this);
		muzzleFlash = GetComponentInChildren<ParticleSystem> ();
    }

    // Update is called once per frame
    void Update()
    {
        aim();
        crossHairSettings.location.x = Screen.width / 2;
        crossHairSettings.location.y = (Screen.height / 2) - 10;
    }

    public override void aim()
    {
        if (owned && active) {
            Ray ray = ownedPlayer.CameraController.Camera.ScreenPointToRay(
                new Vector3(crossHairSettings.location.x, crossHairSettings.location.y));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, gunRange)) {

                if (hit.collider.gameObject.tag == "Player" || hit.collider.gameObject.tag == "Enemy") {
                    guiCrossHairTexture = crossHairSettings.fTexture;
                }
                else {
                    guiCrossHairTexture = crossHairSettings.eTexture;

                }
            }
            else {
				guiCrossHairTexture = crossHairSettings.eTexture;

            }
        }


    }

    public override GunFireControl getControls()
    {
        return myGunFireControl;
    }

    public override void fireShot()
    {
        if (owned && active) {
            Ray ray = ownedPlayer.CameraController.Camera.ScreenPointToRay(
                new Vector3(crossHairSettings.location.x, crossHairSettings.location.y));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, gunRange)) {
                if (hit.collider.gameObject.tag == "Player") {
                    Player enemy = hit.collider.gameObject.GetComponent<Player>();
                    getKillCredit(ownedPlayer, enemy);
                    float damage = (25 * ownedPlayer.sCharacterClass.attackDamage);
                    doDamage(damage, enemy);
                }
                else if (hit.collider.gameObject.tag == "Enemy") {
                    AIEnemy enemy = hit.collider.gameObject.GetComponent<AIEnemy>();
                    getKillCredit(ownedPlayer, enemy);
                    float damage = (25 * ownedPlayer.sCharacterClass.attackDamage);
                    doDamage(damage, enemy);
                     
                }

            }

			muzzleFlash.Play();

        }

    }

    public override void setOwned(bool switchbool, Player player)
    {
        owned = switchbool;
        ownedPlayer = player;
    }

    public override void setActive(bool switchActivation)
    {
     
        active = switchActivation;
        if (active) {
            MeshRenderer mesh = gameObject.GetComponentInChildren<MeshRenderer>();
            mesh.enabled = true;
        }
        else {
            MeshRenderer mesh = gameObject.GetComponentInChildren<MeshRenderer>();
            mesh.enabled = false;
        }
   
    }

    void OnGUI()
    {
        if (owned && active) {
            drawCrossHair();
        }
    }

    void drawCrossHair()
    {
        float crossHairRectLocX = Screen.width / 2 - (crossHairSettings.scale.x / 2);
        float crossHairRectLocY = (Screen.height / 2) - (crossHairSettings.scale.y / 2) - 10;
        GUI.DrawTexture(new Rect(new Vector2(crossHairRectLocX, crossHairRectLocY)
            , crossHairSettings.scale), guiCrossHairTexture);
    }
}
