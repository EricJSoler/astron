using UnityEngine;
using System.Collections;

public class PatrolAIInstantiator : MonoBehaviour {
    public Vector3 location;
    public GameObject wayPointParent;
    public string prefabName;
    public float respawnTime;
    public int startingLevel = 2;
    GameObject ai;
    float timeOfDeath;// Use this for initialization
	void Start () {
        if(PhotonNetwork.isMasterClient)
        {
            GameObject wayPointParentInstance = Instantiate(wayPointParent, location, Quaternion.identity) as GameObject;
            Transform[] wayPoints = wayPointParentInstance.GetComponentsInChildren<Transform>();
            ai = PhotonNetwork.Instantiate(prefabName, wayPointParentInstance.transform.position
                , wayPointParentInstance.transform.rotation, 0);
            AIPatroller patrolScript = ai.GetComponent<AIPatroller>();
            foreach (Transform element in wayPoints) {
                patrolScript.addWayPointToPatrol(element);
            }

            patrolScript.startPatrol();
        }
        
	
	}
    bool foundDead = false;
	// Update is called once per frame
	void Update () {
        if (ai == null) {
            if (!foundDead) {
                timeOfDeath = Time.time;
                foundDead = true;
            }
            if (Time.time >= timeOfDeath + respawnTime) {
                spawn();
            }

        }
	}
   
    public void spawn()
    {
        GameObject wayPointParentInstance = Instantiate(wayPointParent, location, Quaternion.identity) as GameObject;
        Transform[] wayPoints = wayPointParentInstance.GetComponentsInChildren<Transform>();
        ai = PhotonNetwork.Instantiate(prefabName, wayPointParentInstance.transform.position
            , wayPointParentInstance.transform.rotation, 0);
        AIPatroller patrolScript = ai.GetComponent<AIPatroller>();
        foreach (Transform element in wayPoints) {
            patrolScript.addWayPointToPatrol(element);
        }
        patrolScript.GetComponent<AIStats>().level = startingLevel++;
        patrolScript.startPatrol();
        foundDead = false;
    }
}
