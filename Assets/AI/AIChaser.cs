using UnityEngine;
using System.Collections;

public class AIChaser : AINavigation {

    public Transform target;
    NavMeshAgent agent;
    Vector3 destination;
    Rigidbody myBody;
    public LayerMask ground;

	// Use this for initialization
	void Start () {
        target = FindObjectOfType<Player>().gameObject.transform;
        if (target) {
            agent = GetComponent<NavMeshAgent>();
            myBody = GetComponent<Rigidbody>();
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (target)
            agent.SetDestination(target.position);
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player") {

            Player hitPlayer = col.gameObject.GetComponent<Player>();
            float damageDelt = 15 * aiStats.attackDamage;
            hitPlayer.photonView.RPC("recieveDamage", PhotonTargets.All, damageDelt);
        }
    }
}
