using UnityEngine;
using System.Collections;

public class PlayerController : PlayerBase
{
    WeaponI collisionWeapon;

    [System.Serializable]
    public class InputSettings
    {
        public float inputDelay = .1f;
        public string FORWARD_AXIS = "Vertical";
        public string TURN_AXIS = "Mouse X";
        public string STRAFE_AXIS = "Horizontal";
        public string JUMP_AXIS = "Jump";
        public string CHAT_AXIS = "ChatInput";
        public string ORBIT_VERTICAL = "Mouse Y";//"OrbitVertical";
        public string PAUSE_AXIS = "Pause Menu";
        public string ACTIVE_WEAPONINPUT = "Weapon Input";
    }

    public InputSettings inputSetting = new InputSettings();
    float forwardInput;
    float turnInput;
    float jumpInput;
    float chatInput;
    float strafeInput;
    float vOrbitInput;
    float pauseInput;
    bool aimInput;

    //float weaponInput;

    void Start()
    {
        m_controlsOn = true;
    }

    public void GetInput()
    {
        if (m_controlsOn) {
            getMovementInput();
            getInventoryInput();
            getPickUpWeaponInput();
        }
        getChatInput();
        pauseMenuInput();

    }

    void getInventoryInput()
    {
		if (Input.GetKeyDown (KeyCode.E)) {
			if (photonView.isMine)
			{
				PlayerInventory.photonView.RPC ("switchGun", PhotonTargets.All);//These prolly need to be buffered
			}
		}
        if (Input.GetKeyDown(KeyCode.R)) {
            PlayerInventory.photonView.RPC("reloadWeapon", PhotonTargets.All);//These prolly need to be buffered
        }

		PlayerInventory.listenCurrentGunControl();

    }

    void pauseMenuInput()
    {
        pauseInput = Input.GetAxisRaw(inputSetting.PAUSE_AXIS);
        player.recieveInput(pauseInput);
    }

    void getChatInput()
    {
        chatInput = Input.GetAxisRaw(inputSetting.CHAT_AXIS);
        PlayerChat.recieveInput(chatInput);
    }

    void getMovementInput()
    {
        forwardInput = Input.GetAxis(inputSetting.FORWARD_AXIS);//Get axis returns value from -1 to 1;
        turnInput = Input.GetAxis(inputSetting.TURN_AXIS);
        strafeInput = Input.GetAxis(inputSetting.STRAFE_AXIS);
        vOrbitInput = Input.GetAxisRaw(inputSetting.ORBIT_VERTICAL);//interpolated meaning it will return any value from -1 to 1
        jumpInput = Input.GetAxisRaw(inputSetting.JUMP_AXIS);//not interpolated you will get -1 0 or 1
        PlayerPosition.recieveInput(forwardInput, turnInput, jumpInput, strafeInput);
        aimInput = Input.GetKey(KeyCode.Mouse1);
        CameraController.receieveInput(vOrbitInput, aimInput);
    }

    void getPickUpWeaponInput()
    {
        if (collisionWeapon != null) {
            if (Vector3.Distance(collisionWeapon.transform.position, gameObject.transform.position) <= 3) {


                if (Input.GetKeyDown(KeyCode.F)) {
//                    Debug.Log("GOT KEY F");
                    PlayerInventory.photonView.RPC("attachGun", PhotonTargets.AllBuffered, collisionWeapon.GunID);
                }
            }
            else
                collisionWeapon = null;
        }
    }



    bool m_controlsOn;
    public bool ControlsOn
    {
        get { return m_controlsOn; }
        set { m_controlsOn = value; }
    }

    public void notifyPlayerOfPickUpAbleWeapon(WeaponI weapon)
    {
//        Debug.Log("NOTIFIED of collision with weapon");
        collisionWeapon = weapon;
    }
}
