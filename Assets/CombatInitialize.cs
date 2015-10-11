﻿using UnityEngine;
using System.Collections;

public class CombatInitialize : MonoBehaviour {
    public GameObject cameraPrefab;

    public Vector3 playerSpawnLocation = Vector3.zero;
	GameObject[] spawnLocations;

    public Vector3[] weaponSpawns = new Vector3[4];

    public Quaternion playerRotation = Quaternion.identity;
	bool doOnce;
	// Use this for initialization
	void Awake()
    {
		spawnLocations = GameObject.FindGameObjectsWithTag ("Spawn");

        if (PhotonNetwork.connectedAndReady) {
            initializeOnePlayerWithACameraFollowing();
        }
        else
            Debug.Log("Failed to connect to network ");
    }
    
    void Start () {



	}
	
	// Update is called once per frame
	void Update () {

	}

    void initializeOnePlayerWithACameraFollowing()
    {
        GameObject player;
		if (PhotonNetwork.isMasterClient) {
            player = PhotonNetwork.Instantiate("Ethan1", spawnLocations[0].transform.position, playerRotation, 0) as UnityEngine.GameObject;
            PhotonNetwork.Instantiate("Leviathan", weaponSpawns[0], Quaternion.identity, 0);
            PhotonNetwork.Instantiate("Arion", weaponSpawns[1], Quaternion.identity, 0);
        }
		else
		{
	        player = PhotonNetwork.Instantiate("Ethan1", spawnLocations[0].transform.position, playerRotation, 0) as UnityEngine.GameObject;
            PhotonNetwork.Instantiate("Leviathan", weaponSpawns[2], Quaternion.identity, 0);
            PhotonNetwork.Instantiate("Leviathan", weaponSpawns[3], Quaternion.identity, 0);
        }

        GameObject playerCameraObj = (GameObject)Instantiate(cameraPrefab, Vector3.zero, Quaternion.identity);
        CameraController camera = playerCameraObj.GetComponent<CameraController>();
        camera.setCameraTarget(player);
    }
}
