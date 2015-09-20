using UnityEngine;
using System.Collections;

public abstract class Gun : MonoBehaviour {

	public Camera myCamera;
	//public int bulletSpeed;
	//public GameObject bullet;
	public string name;
	public bool owned;
	public Collider myCollider;


	public GameObject crossHair;
	public int gunRange;
	//center the raycast
	public int aimerXCoordinates;
	public int aimerYCoordinates;


	// Use this for initialization
	void Start () {

		myCollider = this.gameObject.GetComponent<Collider> ();
		//crossHair.transform.position = new Vector3 (Screen.width / 2, Screen.height / 2, this.gameObject.transform.position.z + 8);
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	public abstract void fireShot();
	public abstract void aim();

	public abstract void initializeCamera();

	public abstract void setOwned(bool switchbool);





}
