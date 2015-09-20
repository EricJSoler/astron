using UnityEngine;
using System.Collections;

public class PlayerPosition : PlayerBase {

    [System.Serializable]
    public class MoveSettings
    {
        public float forwardVel = 12;
        public float backwardVel = 8;
        public float strafeVel = 3;
        public float rotateVel = 100;
        public float jumpVel = 25;
        public float distToGrounded = .3f;
        public LayerMask ground;
    }

    [System.Serializable]
    public class PhysSettings
    {
        public float downAccel = .75f;
    }


    public MoveSettings moveSetting = new MoveSettings();
    public PhysSettings physSetting = new PhysSettings();

    //Local Movement variables
    Vector3 velocity = Vector3.zero;
    public Quaternion targetRotation;

    //inputValues
    float forwardInput;
    float turnInput;
    float jumpInput;
    float strafeInput;
    float inputDelay = .1f;
    /// Variables for the proxy copy on the otherside of the network
    private Vector3 correctPlayerPos;
    private Quaternion correctPlayerRot;
    private Vector3 lastRecievedVelocity;
    double m_LastNetworkDataRecievedTime;

    Rigidbody rBody;

    void Start()
    {
        if (GetComponent<Rigidbody>()) {
            rBody = GetComponent<Rigidbody>();
        }
        PhotonNetwork.sendRate = 20;
        PhotonNetwork.sendRateOnSerialize = 10;
        targetRotation = transform.rotation;
    }

    void Update()
    {
        if (photonview.isMine) {
            Turn();
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!photonView.isMine) 
        {
            //Make prediction of my locatoin and position and update it
            transform.position = Vector3.Lerp(transform.position, this.correctPlayerPos, Time.deltaTime * 5);
            //UpdateNetworkedPosition();
            //transform.rotation = Quaternion.Lerp(transform.rotation, this.correctPlayerRot, Time.deltaTime * 5);
            UpdateNetworkedRotation();
            //rBody.velocity = lastRecievedVelocity * Time.deltaTime;
        }
        else {
            Run();
            Jump();
            rBody.velocity = transform.TransformDirection(velocity);
        }

    }

    void UpdateNetworkedPosition()
    {
        //Doesnt work at all
        float pingInSeconds = (float)PhotonNetwork.GetPing() * .001f;
        float timeSinceLastUpdate = (float)(PhotonNetwork.time - m_LastNetworkDataRecievedTime);
        float totalTimePassed = pingInSeconds + timeSinceLastUpdate;
        Vector3 exterpolatedTargetPosition = correctPlayerPos + transform.forward * moveSetting.forwardVel * totalTimePassed;

        Vector3 newPosition = Vector3.MoveTowards(transform.position, exterpolatedTargetPosition, moveSetting.forwardVel * Time.deltaTime);
        if (Vector3.Distance(transform.position, exterpolatedTargetPosition) > 2f) {
            correctPlayerPos = exterpolatedTargetPosition;
        }

        correctPlayerPos.y = Mathf.Clamp(correctPlayerPos.y, 0.5f, 50f);
        transform.position = correctPlayerPos;
    
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
        else {
            // Network player, receive data
            this.correctPlayerPos = (Vector3)stream.ReceiveNext();
            this.correctPlayerRot = (Quaternion)stream.ReceiveNext();
            this.lastRecievedVelocity = (Vector3)stream.ReceiveNext();
            m_LastNetworkDataRecievedTime = info.timestamp;

        }
    }

    public void recieveInput(float fInput, float tInput, float jInput, float sInput)
    {
        this.forwardInput = fInput;
        this.turnInput = tInput;
        this.jumpInput = jInput;
        this.strafeInput = sInput;
    }

    void Run()
    {
     //   if (Mathf.Abs(forwardInput) > inputDelay) {
            //Move
       //     velocity.z = moveSetting.forwardVel * forwardInput;
            //rBody.velocity = transform.forward * forwardInput * moveSetting.forwardVel;
        //}
        if (forwardInput > inputDelay) {
            velocity.z = moveSetting.forwardVel * forwardInput;
        }
        else if (forwardInput < inputDelay * -1) {
            velocity.z = moveSetting.backwardVel * forwardInput;
        }
        else
            velocity.z = 0;// rBody.velocity = Vector3.zero;// zero velocity
        if (Mathf.Abs(strafeInput) > inputDelay) {
            velocity.x = moveSetting.strafeVel * strafeInput;
        }
        else
            velocity.x = 0;
    }

    void Turn()
    {
        if (Mathf.Abs(turnInput) > inputDelay) {
            targetRotation *= Quaternion.AngleAxis(moveSetting.rotateVel * turnInput * Time.deltaTime, Vector3.up);

        }
        transform.rotation = targetRotation;
    }

    void Jump()
    {
        if (jumpInput > 0 && Grounded()) {
            velocity.y = moveSetting.jumpVel;
        }
        else if (jumpInput == 0 && Grounded())
            velocity.y = 0;
        else
            velocity.y -= physSetting.downAccel;
    }

    bool Grounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, moveSetting.distToGrounded, moveSetting.ground);
    }
}
