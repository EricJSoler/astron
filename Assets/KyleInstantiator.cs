using UnityEngine;
using System.Collections;

public class KyleInstantiator : MonoBehaviour {

    public int level;
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
        GameObject lastCylinder = PhotonNetwork.Instantiate("Cylinder", gameObject.transform.position, Quaternion.identity, 0);
        lastCylinder.GetComponent<AIStats>().level = level;
        yield return new WaitForSeconds(5);
        startInstantiatingOnNetwork();
        count++;
    }
    void startInstantiateingLocally()
    {

    }
}
