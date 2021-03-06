﻿using UnityEngine;
using System.Collections;


//Handle very non-specific information about the player
//such as the team the player is on
// contain methods to manipulate the other components attached to the player
public class Player : PlayerBase
{
    public Transform myShoulder;
    PauseMenu pauseMenuInScene;
    XpTimer myXpTimer;

    void Start()
    {
        if(photonView.isMine)
            pauseMenuInScene = FindObjectOfType<PauseMenu>();
        myXpTimer = new XpTimer();
    }


    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        PlayerPosition.SerializeState(stream, info);
        Visuals.serializeState(stream, info);
    }

    void Update()
    {
        if (photonView.isMine) {
            PlayerController.GetInput();
            if (sCharacterClass.currentHealth <= 0) {
                Debug.Log("your dead");
               
                photonView.RPC("destroyThisPlayer", PhotonTargets.AllBuffered, sCharacterClass.level);
            }
        }
    }

    public void recieveInput(float pauseInput)
    {
        if (pauseInput > .1f) {
            PlayerController.ControlsOn = false;
            pauseMenuInScene.turnOn();
        }
        else if (pauseInput < -.1f) {
            pauseMenuInScene.turnOff();
            PlayerController.ControlsOn = true;
        }
    }
	[PunRPC]
    public void recieveDamage(float amount)
    {
        if (photonView.isMine) {
            sCharacterClass.loseHealth(amount);
        }
    }

    //The the PHOTONVIEW WHO OWNS THE PLAYER SHOULD BE THE ONLY ONE CALLING THIS RPC
    [PunRPC]
    public void destroyThisPlayer(int myLevel)
    {
        myXpTimer.iDied(myLevel, Time.time);
        if (photonView.isMine) {
            Debug.Log("your dead");
            //Post to the screen taht you died probably do this  a cooler way later
            CameraController.enabled = false;//turn off the camera controller avoid crash
            gameObject.SetActive(false);
            this.recieveInput(1);
        }
        else if (!photonView.isMine) {
            gameObject.SetActive(false);//(gameObject);
        }
        Visuals.deathVisuals();
    }

    public void requestXPOnDeath(Player shotMe)
    {
        myXpTimer.addComponent(shotMe);
        myXpTimer.getRidOfExtras();
    }

}
