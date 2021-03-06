﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Control the camera attached to the local player.
/// Needs a reference to the gameObject that it is following
/// </summary>
public class CameraController : MonoBehaviour
{

    //[System.Serializable]
    public class PositionSettings
    {
        public Vector3 targetPosOffSet = new Vector3(0, 1, 0);
        public float lookSmooth = 100f;
        public float distanceFromTarget = -3;
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
    bool aiming = false;
    public Transform target;
    bool initializedWithPlayer;
    Vector3 targetPos = Vector3.zero;
    Vector3 destination = Vector3.zero;
    float vOrbitInput, hOrbitInput, zoomInput, hOrbitSnapInput;
    // Use this for initialization
    float adjustedDistance = 0;
    bool collision = false;
    void Awake()
    {
        initializedWithPlayer = false;
    }
    void Start()
    {
        vOrbitInput = hOrbitInput = zoomInput = 0;
        hOrbitSnapInput = 1;
        if (initializedWithPlayer) {
            moveToTarget();
            orbitTarget();
        }

    }


    // Update is called once per frame
    void Update()
    {
        if (initializedWithPlayer && target) {
            // getInput();
            orbitTarget();
            zoomInOnTarget();
           // if (collision) {
            //    position.distanceFromTarget = adjustedDistance;
           // }
            if (aiming) {
                position.distanceFromTarget = -1.5f;
            }
            else
                position.distanceFromTarget = -3;
        }

    }
    void LateUpdate()
    {
        if (initializedWithPlayer && target) {
            moveToTarget();
            lookAtTarget();
            //checkColision();
        }
    }

    public void checkColision()
    {
        bool forwardHit = false;
        bool rightHit = false;
        bool leftHit = false;
        float  forwardDistance = (target.position - transform.position).magnitude;
        RaycastHit hit;
        Ray ray = new Ray(transform.position, target.position);

        if (Physics.Raycast(ray ,out hit ,forwardDistance)) {
            Debug.Log("RegisteredHit");
            if (hit.collider.gameObject.tag != "Player") {
                collision = true;
                adjustedDistance = position.distanceFromTarget - hit.distance;
            }
        }
        Debug.DrawRay(ray.origin, ray.direction * forwardDistance, Color.red, 1f);
        
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
    public void setCameraTarget(GameObject playerObj)
    {
        //GEt The Players Shoulder
        Player thePlayer = playerObj.GetComponent<Player>();
        target = thePlayer.myShoulder;
        if (target != null) {
            initializedWithPlayer = true;
        }
        else
            Debug.LogError("Camera needs a target");
    }

    public void receieveInput(float orbitX, bool aim)
    {
        vOrbitInput = orbitX;
        aiming = aim;
    }

    Camera m_Camera;
    public Camera Camera
    {
        get
        {
            if (m_Camera == null)
                m_Camera = GetComponent<Camera>();
            return m_Camera;
        }
    }
}
