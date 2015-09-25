using UnityEngine;
using System.Collections;


//Requires Enviroment Settings to be in the scene
public class PlayerPosition : PlayerBase {

    [System.Serializable]
    public class MoveSettings
    {
        public float rotateVel = 100;
        public float distToGrounded = .3f;
        public LayerMask ground;
    }
    public MoveSettings moveSetting = new MoveSettings();

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
        if (photonView.isMine) {
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
            Visuals.updateAnimatorRun(lastRecievedVelocity.z);
           
        }
        else {
            Run();
            Jump();
            rBody.velocity = transform.TransformDirection(velocity);
        }
       
    }

    void LateUpdate()
    {
        Visuals.updateAnimatorJump(Grounded());
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
        else if(stream.isReading){
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
            velocity.z = sCharacterClass.movementSpeed * forwardInput;
        }
        else if (forwardInput < inputDelay * -1) {
            velocity.z = getBackwardVel() * forwardInput;
        }
        else
            velocity.z = 0;// rBody.velocity = Vector3.zero;// zero velocity
        if (Mathf.Abs(strafeInput) > inputDelay) {
            velocity.x = getStrafeVel() * strafeInput;
        }
        else
            velocity.x = 0;
        Visuals.updateAnimatorRun(velocity.z);
    }

    float getForwardVel()
    {
        return sCharacterClass.movementSpeed * EnviromentSettings.enviroment.movementSpeedSlow;
    }

    float getStrafeVel()
    {
        return (sCharacterClass.movementSpeed * .5f) * EnviromentSettings.enviroment.movementSpeedSlow;//Move side to side 50% as fast as forward
    }

    float getBackwardVel()
    {
        return (sCharacterClass.movementSpeed * .8f) * EnviromentSettings.enviroment.movementSpeedSlow;//move backwards 80% as fast as forward
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
            velocity.y = sCharacterClass.jumpPower;
        }
        else if (jumpInput == 0 && Grounded())
            velocity.y = 0;
        else
            velocity.y -= EnviromentSettings.enviroment.downAccel;
    }

    bool Grounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, moveSetting.distToGrounded, moveSetting.ground);
    }
}
