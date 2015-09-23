using UnityEngine;
using System.Collections;

public class AIVisuals : AIBase {
    public GameObject deathExplosion;
    public GameObject sparks;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void deathVisuals()
    {
        Instantiate(deathExplosion, gameObject.transform.position, gameObject.transform.rotation);
    }

    public void beenShot()
    {
        Instantiate(sparks, gameObject.transform.position, gameObject.transform.rotation);
    }
}
