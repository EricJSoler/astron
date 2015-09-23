using UnityEngine;
using System.Collections;

public class BasicAI : MonoBehaviour
{

    // Use this for initialization
    public Transform target;
    NavMeshAgent agent;
    Vector3 destination;
    Rigidbody myBody;
    public LayerMask ground;
    MeleeEnemy myMelee;

    void Start()
    {
        target = FindObjectOfType<Player>().gameObject.transform;
        if (target) {
            agent = GetComponent<NavMeshAgent>();
            myBody = GetComponent<Rigidbody>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(target)
            agent.SetDestination(target.position);
    }

    bool Grounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, .3f, ground);
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info )
    {

    }

}
