using UnityEngine;
using System.Collections;


//Handle very non-specific information about the player
//such as the team the player is on
// contain methods to manipulate the other components attached to the player
public class Player : PlayerBase
{
    PauseMenu pauseMenuInScene;

    void Start()
    {
        pauseMenuInScene = FindObjectOfType<PauseMenu>();
    }


    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        PlayerPosition.SerializeState(stream, info);
    }

    void Update()
    {
        if (photonView.isMine) {
            PlayerController.GetInput();
            if (sCharacterClass.currentHealth <= 0) {
                Debug.Log("your dead");
                PhotonNetwork.Instantiate("CartoonExplosion", this.transform.position, transform.rotation, 0);
                photonView.RPC("destroyThisPlayer", PhotonTargets.AllBuffered);
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

    [PunRPC]
    public void destroyThisPlayer()
    {

        if (photonView.isMine) {
            Debug.Log("your dead");
            //Post to the screen taht you died probably do this  a cooler way later
            CameraController.enabled = false;//turn off the camera controller avoid crash
            gameObject.SetActive(false);
            this.recieveInput(1);
        }
        else if (!photonView.isMine) {
            Destroy(gameObject);
        }
       
    }

}
