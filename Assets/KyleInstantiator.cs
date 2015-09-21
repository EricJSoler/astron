﻿using UnityEngine;
using System.Collections;

public class KyleInstantiator : MonoBehaviour {

	// Use this for initialization
    int count = 0;
	void Start () {
        if(PhotonNetwork.isMasterClient)
            startInstantiatingOnNetwork();    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void startInstantiatingOnNetwork()
    {
        Debug.Log("made it here");
        StartCoroutine(performInstantiate());
        if (count == 5) {
            if(transform.position.x >= -150)
            {
                Vector3 nextSpawn = new Vector3(transform.position.x - 5, transform.position.y, transform.position.z);
                Instantiate(gameObject, nextSpawn, Quaternion.identity);
            }
            
        }
    }

    IEnumerator performInstantiate()
    {
        PhotonNetwork.Instantiate("AIKyle", gameObject.transform.position, Quaternion.identity, 0);
        Debug.Log("should be instantiating");
        yield return new WaitForSeconds(5);
        startInstantiatingOnNetwork();
        count++;
    }
    void startInstantiateingLocally()
    {

    }
}
