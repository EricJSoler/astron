using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIPatroller : AINavigation {

    public int visionDistance = 20;
    List<Transform> playerTransforms;
    List<Transform> wayPoints;
    NavMeshAgent myAgent;
    public LayerMask ground;
    int currentWayPoint = -1;
    public int patrolSpeed = 3;
    bool chasing;
    Transform chaseTarget;
	// Use this for initialization

    float lastTimeLookedForPlayer;
    float waitTime = 5f;
	void Awake () {
        myAgent = GetComponent<NavMeshAgent>();
        chasing = false;
        playerTransforms = new List<Transform>();
        lastTimeLookedForPlayer = Time.time;
	}

    void Start()
    {
        myAgent.speed = patrolSpeed;
    }
	
	// Update is called once per frame
	void Update () {
        if (!chasing) {
            if (!myAgent.pathPending) {
                if (myAgent.remainingDistance <= myAgent.stoppingDistance) {
                    if (!myAgent.hasPath || myAgent.velocity.sqrMagnitude == 0f) {
                        repath();
                    }
                }
            }
            lookForPlayers();
            if (Time.time - lastTimeLookedForPlayer > waitTime) {
                findPlayers();
            }
        }
        else if (chaseTarget != null) {
            myAgent.SetDestination(chaseTarget.position);
        }
        
	}
    

    void findPlayers()
    {
        if (photonView.isMine) {
            playerTransforms = new List<Transform>();
            GameObject[] playersObj = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject element in playersObj) {
                playerTransforms.Add(element.transform);
            }
        }
    }

    void lookForPlayers()
    {
        if (photonView.isMine) {
            foreach (Transform element in playerTransforms) {

                if (Vector3.Distance(element.position, gameObject.transform.position) < visionDistance) {
                    chasing = true;
                    chaseTarget = element;
                    break;
                }
            }
        }
       
       
    }
    public void addWayPointToPatrol(Transform wayPoint)
    {
        if (photonView.isMine) {
            if (wayPoint == null)
                Debug.Log("transform didnt show up");
            if (wayPoints == null)
                wayPoints = new List<Transform>();
            if (wayPoints != null)
                wayPoints.Add(wayPoint);
        }
    }

    public void startPatrol()
    {
        if (photonView.isMine) {
            repath();
        }
    }

    void repath()
    {
        if (photonView.isMine) {
            currentWayPoint = (currentWayPoint + 1) % wayPoints.Count;
            myAgent.SetDestination(wayPoints[currentWayPoint].position);
        }
    }
}
