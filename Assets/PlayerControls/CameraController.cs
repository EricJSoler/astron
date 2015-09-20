using UnityEngine;
using System.Collections;

/// <summary>
/// Control the camera attached to the local player.
/// Needs a reference to the gameObject that it is following
/// </summary>
public class CameraController : MonoBehaviour
{

    [System.Serializable]
    public class PositionSettings
    {
        public Vector3 targetPosOffSet = new Vector3(0, 3.4f, 0);
        public float lookSmooth = 100f;
        public float distanceFromTarget = -6;
        public float zoomSmooth = 100;
        public float maxZoom = -2;
        public float minZoom = -15;
    }

    [System.Serializable]
    public class OrbitSettings
    {
        public float xRotation;
        public float yRotation;
        public float maxXRotation = 25;
        public float minXRotation = -85;
        public float vOrbitSmooth = 150;
        public float hOrbitSmooth = 150;
    }
    [System.Serializable]
    public class InputSettings
    {
        public string ORBIT_HORIZONTAL_SNAP = "OrbitHorizontalSnap";
        public string ORBIT_HORIZONTAL = "Mouse X";//"OrbitHorizontal";
        public string ORBIT_VERTICAL = "Mouse Y";//"OrbitVertical";
        public string ZOOM = "Mouse ScrollWheel";
        public string LOCK = "CameraLock";
    }
    public PositionSettings position = new PositionSettings();
    public OrbitSettings orbit = new OrbitSettings();
    public InputSettings input = new InputSettings();


    //old
    public Transform target;
    bool initializedWithPlayer;
    Vector3 targetPos = Vector3.zero;
    Vector3 destination = Vector3.zero;
    PlayerController charController;
    float vOrbitInput, hOrbitInput, zoomInput, hOrbitSnapInput;
    // Use this for initialization
    void Awake()
    {
        if (!GetComponentInParent<PhotonView>().isMine) {
            GetComponent<Camera>().enabled = false;
            this.enabled = false;
        }
    }
    void Start()
    {
        setCameraTarget(target);
        moveToTarget();
        vOrbitInput = hOrbitInput = zoomInput = 0;
        hOrbitSnapInput = 1;
        orbitTarget();
    }


    // Update is called once per frame
    void Update()
    {
        if (initializedWithPlayer) {
            // getInput();
            orbitTarget();
            zoomInOnTarget();
        }

    }
    void LateUpdate()
    {
        if (initializedWithPlayer) {
            moveToTarget();
            lookAtTarget();
        }
    }
    void getInput()
    {

        vOrbitInput = Input.GetAxisRaw(input.ORBIT_VERTICAL);
        hOrbitInput = Input.GetAxisRaw(input.ORBIT_HORIZONTAL);
        hOrbitSnapInput = Input.GetAxisRaw(input.ORBIT_HORIZONTAL_SNAP);
        zoomInput = Input.GetAxisRaw(input.ZOOM);

    }

    void orbitTarget()
    {
        if (hOrbitSnapInput > 0) {
            orbit.yRotation = -180;
        }
        orbit.xRotation += vOrbitInput * orbit.vOrbitSmooth * Time.deltaTime;
        //orbit.yRotation += hOrbitInput * orbit.hOrbitSmooth * Time.deltaTime;

        if (orbit.xRotation > orbit.maxXRotation) {
            orbit.xRotation = orbit.maxXRotation;
        }
        if (orbit.xRotation < orbit.minXRotation) {
            orbit.xRotation = orbit.minXRotation;
        }
    }

    void zoomInOnTarget()
    {
        position.distanceFromTarget += zoomInput * position.zoomSmooth * Time.deltaTime;
        if (position.distanceFromTarget > position.maxZoom) {
            position.distanceFromTarget = position.maxZoom;
        }

        if (position.distanceFromTarget < position.minZoom) {
            position.distanceFromTarget = position.minZoom;
        }
    }



    void moveToTarget()
    {

        targetPos = target.position + position.targetPosOffSet;
        destination = Quaternion.Euler(orbit.xRotation, orbit.yRotation + target.eulerAngles.y, 0) * -Vector3.forward * position.distanceFromTarget;
        destination += targetPos;//make sure the rotated point is relative to our target
        transform.position = destination;


    }

    void lookAtTarget()
    {
        Quaternion targetRotation = Quaternion.LookRotation(targetPos - transform.position); //subtract our transform from the target we want to look at
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, position.lookSmooth * Time.deltaTime);
    }
    public void setCameraTarget(Transform t)
    {
        target = t;
        if (target != null) {
            if (target.GetComponent<PlayerController>()) {
                charController = target.GetComponent<PlayerController>();
                initializedWithPlayer = true;

            }
            else
                Debug.LogError("the camera needs  a PLayerController");
        }
        else
            Debug.LogError("Camera needs a target");
    }

    public void initializeCameraOnPlayer(Transform t)
    {
        setCameraTarget(t);
        Debug.Log("setCameraDone");
        Debug.Log(t);
        if (initializedWithPlayer) {

        }


    }

    public void receieveInput(float orbitX)
    {
        vOrbitInput = orbitX;
    }
}
