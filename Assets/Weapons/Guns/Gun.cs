﻿using UnityEngine;
using System.Collections;

public abstract class Gun : MonoBehaviour {

    [System.Serializable]
    public class CrossHairSettings
    {
        public Texture2D fTexture;
        public Texture2D eTexture;
        public Vector2 location = Vector2.zero;
        public Vector2 scale = Vector2.zero;
    }

	protected ParticleSystem muzzleFlash;

    public CrossHairSettings crossHairSettings = new CrossHairSettings();
    protected Player ownedPlayer;
	public new string name;
	protected bool owned;
    protected bool active;

    protected int gunRange;


	protected GunFireControl myGunFireControl;
	public abstract GunFireControl getControls();
	
    //Use crosshair settings and some sort of raycast
    //to see if a player is infront of the gun
	public abstract void fireShot();
	public abstract void aim();

    //Give a gun a reference to a player this will mean it is owned
    //this is only ran if photonview is mine
	public abstract void setOwned(bool switchbool, Player player);
    public abstract void setActive(bool switchActivation);








    protected virtual void doDamage(float amount, Player target)
    {
        target.photonView.RPC("recieveDamage", PhotonTargets.All, amount);
    }
    protected virtual void doDamage(float amount, AIEnemy target)
    {
        target.photonView.RPC("recieveDamage", PhotonTargets.All, amount);
    }

    protected virtual void getKillCredit(Player shooter, AIEnemy shot)
    {
        shot.requestXPOnDeath(shooter);
    }

    protected virtual void getKillCredit(Player shooter, Player shot)
    {
        shot.requestXPOnDeath(shooter);
    }






}
