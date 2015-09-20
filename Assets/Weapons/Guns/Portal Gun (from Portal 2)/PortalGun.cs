using UnityEngine;
using System.Collections;

public class PortalGun : Gun {

	// Use this for initialization
	void Start () {
		//bulletSpeed = 100;
		name = "PortalGun";
		gunRange = 300;
		initializeCamera ();
		aimerXCoordinates = Screen.width / 2;
		aimerYCoordinates = (Screen.height / 2) - 10;
	
	}
	
	// Update is called once per frame
	void Update () {
		aim ();
	}

	public override void aim()
	{
		Ray ray = myCamera.ScreenPointToRay(new Vector3(aimerXCoordinates, aimerYCoordinates));	
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, gunRange)) {
			
			if (hit.collider.gameObject.tag == "Player" || hit.collider.gameObject.tag == "Enemy") {
				crossHair.GetComponent<Renderer> ().material.color = Color.green;
			} else {
				crossHair.GetComponent<Renderer> ().material.color = Color.black;
				
			}
		} else {
			crossHair.GetComponent<Renderer> ().material.color = Color.black;
			
		}
		
	}
	
	public override void fireShot()
	{
		if (owned) {
			//GameObject projectile = PhotonNetwork.Instantiate (
			//"projectile", transform.position + myCamera.transform.forward * 2, Quaternion.identity, 0);
			//projectile.GetPhotonView ().RPC (
			//"shootFoward", PhotonTargets.All, bulletSpeed, Camera.main.transform.forward);

			Ray ray = myCamera.ScreenPointToRay (new Vector3 (aimerXCoordinates, aimerYCoordinates));	
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, gunRange)) {
				
				if (hit.collider.gameObject.tag == "Player") {
					hit.collider.gameObject.GetComponent<Player> ().photonview.RPC ("recieveDamage", PhotonTargets.All, 25f);
					Debug.DrawLine (ray.origin, hit.point);
				} else if (hit.collider.gameObject.tag == "Enemy") {

					MeleeEnemy enemy = hit.collider.gameObject.GetComponent<MeleeEnemy>();
					enemy.health -= 25;
					Debug.DrawLine (ray.origin, hit.point);
				}
				
			}
		
		}

	}

	public override void setOwned(bool switchbool)
	{
		owned = switchbool;
	
	}

	public override void initializeCamera()
	{
		GameObject[] cameraObj = GameObject.FindGameObjectsWithTag("MainCamera");
		foreach (GameObject element in cameraObj) {
			myCamera = element.GetComponent<Camera>();
		}

	}

}
