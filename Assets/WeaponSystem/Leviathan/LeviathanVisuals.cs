using UnityEngine;
using System.Collections;


public class LeviathanVisuals : WeaponVisualsI {
    private ParticleSystem muzzleFlashParticle;

    public GameObject[] meshes;
    public Collider collider;
	// Use this for initialization
	void Start () {
		muzzleFlashParticle = GetComponentInChildren<ParticleSystem> ();
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
        foreach (GameObject element in meshes) {
            element.SetActive(false);          
        }
        collider.enabled = false;
    }

    public override void turnOnRenderers()
    {
        foreach (GameObject element in meshes) {
            element.SetActive(true);
        }
        collider.enabled = true;        
    }

    public override void reloadAnimation()
    {
        //animation here
    }
}









