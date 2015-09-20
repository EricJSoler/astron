using UnityEngine;
using System.Collections;

public class KyleInstantiator : MonoBehaviour {

	// Use this for initialization
	void Start () {
            startInstantiatingOnNetwork();    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void startInstantiatingOnNetwork()
    {
        Debug.Log("made it here");
        StartCoroutine(performInstantiate());
    }

    IEnumerator performInstantiate()
    {
        PhotonNetwork.Instantiate("AIKyle", new Vector3(20, -20, 20), Quaternion.identity, 0);
        Debug.Log("should be instantiating");
        yield return new WaitForSeconds(5);
        startInstantiatingOnNetwork();
    }
    void startInstantiateingLocally()
    {

    }
}
