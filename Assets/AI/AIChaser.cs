using UnityEngine;
using System.Collections;

public class AIChaser : AINavigation {

    public Transform target;
    NavMeshAgent agent;
    Vector3 destination = Vector3.zero;
    Rigidbody myBody;
    public LayerMask ground;

	// Use this for initialization
	void Start () {
        if (photonView.isMine) {
            target = FindObjectOfType<Player>().gameObject.transform;
            if (target) {
                agent = GetComponent<NavMeshAgent>();
                myBody = GetComponent<Rigidbody>();
                agent.speed = aiStats.movementSpeed;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (photonView.isMine) {
            if (target)
                destination = target.position;
            agent.SetDestination(destination);
        }
	}

    void OnCollisionEnter(Collision col)
    {
        if (photonView.isMine) {
            if (col.gameObject.tag == "Player") {
                Player hitPlayer = col.gameObject.GetComponent<Player>();
                float damageDelt = 15 * aiStats.attackDamage;
                hitPlayer.photonView.RPC("recieveDamage", PhotonTargets.All, damageDelt);
            }
        }
    }
}
