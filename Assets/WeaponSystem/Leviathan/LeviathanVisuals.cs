using UnityEngine;
using System.Collections;


public class LeviathanVisuals : WeaponVisualsI {
    public ParticleSystem muzzleFlashParticle;
	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
    void Update()
    {

    }

    public override void muzzleFlash()
    {
        muzzleFlashParticle.Play();
    }

    public override void turnOffRenderers()
    {
        Debug.Log("yoTurnOFFTHISGUNSRENDERERS");
    }

    public override void turnOnRenderers()
    {
        Debug.Log("yoTurnOnYourRenderers");
    }

    public override void reloadAnimation()
    {
        //animation here
    }
}









