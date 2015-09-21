using UnityEngine;
using System.Collections;

public class PlayerController : PlayerBase
{


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
        }
        getChatInput();
        pauseMenuInput();

    }

    void getInventoryInput()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (photonView.isMine)
                PlayerInventory.photonView.RPC("switchGun", PhotonTargets.All);
                //PlayerInventory.switchGun();
        }

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.T)) {

            PlayerInventory.getCurrentWeapon().GetComponent<Gun>().fireShot();
        }

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
        CameraController.receieveInput(vOrbitInput);
    }



    bool m_controlsOn;
    public bool ControlsOn
    {
        get { return m_controlsOn; }
        set { m_controlsOn = value; }
    }
}
