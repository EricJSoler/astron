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
        if (photonview.isMine) {
            PlayerController.GetInput();
        }
        playerControls();

        if (DataHolder.CharacterClass.CurrentHealth <= 0) {
            if (photonview.isMine)
			{
                PhotonNetwork.Destroy(this.gameObject);
			}
            else
			{
                Destroy(this.gameObject);
			}
			PhotonNetwork.Instantiate("CartoonExplosion",this.transform.position,transform.rotation,0);
        }

    }

    public void playerControls()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (photonview.isMine)
                PlayerInventory.photonView.RPC("switchGun", PhotonTargets.All);
        }

        if (Input.GetMouseButtonDown(0)) {

            PlayerInventory.getCurrentWeapon().GetComponent<Gun>().fireShot();
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
        if (photonview.isMine) {
            DataHolder.CharacterClass.CurrentHealth -= (int)amount;
        }
    }

    void OnGUI(){
        GUILayout.Label(DataHolder.CharacterClass.CurrentHealth.ToString());
    }



}
