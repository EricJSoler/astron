using UnityEngine;
using System.Collections;

public class AIPosition : AIBase {

    Rigidbody rBody;

    /// Variables for the proxy copy on the otherside of the network
    private Vector3 correctPlayerPos;
    private Quaternion correctPlayerRot;
    private Vector3 lastRecievedVelocity;
    double m_LastNetworkDataRecievedTime;

	// Use this for initialization
	void Start () {
        if (GetComponent<Rigidbody>()) {
            rBody = GetComponent<Rigidbody>();
        }
        PhotonNetwork.sendRate = 20;
        PhotonNetwork.sendRateOnSerialize = 10;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void FixedUpdate()
    {
        if (!photonView.isMine) {
            transform.position = Vector3.Lerp(transform.position, this.correctPlayerPos, Time.deltaTime * 5);
            UpdateNetworkedRotation();
        }
    }

    void UpdateNetworkedRotation()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, correctPlayerRot, 180f * Time.deltaTime);
    }

    public void SerializeState(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting) {
            // We own this player: send the others our data
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(rBody.velocity);

        }
        else if (stream.isReading) {
            // Network player, receive data
            this.correctPlayerPos = (Vector3)stream.ReceiveNext();
            this.correctPlayerRot = (Quaternion)stream.ReceiveNext();
            this.lastRecievedVelocity = (Vector3)stream.ReceiveNext();
            m_LastNetworkDataRecievedTime = info.timestamp;

        }
    }
}
