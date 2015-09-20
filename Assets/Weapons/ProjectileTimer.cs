using UnityEngine;
using System.Collections;

public class ProjectileTimer : MonoBehaviour {

	
    // Use this for initialization

    float startingTime;
    float lifespan;
    void Start () {
        startingTime = Time.time;
        lifespan = 15f;
	}
	
	// Update is called once per frame
	void Update () {
        if (startingTime + lifespan <= Time.time) {
            Destroy(gameObject);
        }
	}


}
