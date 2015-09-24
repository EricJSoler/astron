using UnityEngine;
using System.Collections;

public class PatrolAIInstantiator : MonoBehaviour {
    public Vector3 location;
    public GameObject wayPointParent;
    public string prefabName;
	// Use this for initialization
	void Start () {
        if(PhotonNetwork.isMasterClient)
        {
            GameObject wayPointParentInstance = Instantiate(wayPointParent);
            Transform[] wayPoints = wayPointParentInstance.GetComponentsInChildren<Transform>();
            GameObject ai = PhotonNetwork.Instantiate(prefabName, wayPointParentInstance.transform.position
                , wayPointParentInstance.transform.rotation, 0);
            AIPatroller patrolScript = ai.GetComponent<AIPatroller>();
            foreach (Transform element in wayPoints) {
                patrolScript.addWayPointToPatrol(element);
            }

            patrolScript.startPatrol();
        }
        
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
