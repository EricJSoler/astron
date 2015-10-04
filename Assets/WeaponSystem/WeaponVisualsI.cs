using UnityEngine;
using System.Collections;

//Handle the visuals of the weapon that are attached to the player
public abstract class WeaponVisualsI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public abstract void muzzleFlash();

    //Turn off the renderers for a weapon when it is inside a players inventory
    public abstract void turnOffRenderers();

    //Turn on the renderers for a weapon when it is an active weapon or on the ground
    public abstract void turnOnRenderers();

    public abstract void reloadAnimation();
}
