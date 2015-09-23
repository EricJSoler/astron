using UnityEngine;
using System.Collections;

public abstract class GunFireControl {

	public int fire;

	protected Gun myCurrentGun;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public abstract void gunControl();
	
	public abstract void setFire(int set);
	
	public abstract int fireNow();



}
