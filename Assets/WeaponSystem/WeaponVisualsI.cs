using UnityEngine;
using System.Collections;

public abstract class WeaponVisualsI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public abstract void muzzleFlash();

    public abstract void turnOffRenderers();

    public abstract void turnOnRenderers();

    public abstract void reloadAnimation();
}
