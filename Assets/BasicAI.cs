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
    void Start()
    {
        target = FindObjectOfType<Player>().gameObject.transform;
        agent = GetComponent<NavMeshAgent>();
        myBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
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
