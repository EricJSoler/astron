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
            forwardInput = Input.GetAxis(inputSetting.FORWARD_AXIS);//Get axis returns value from -1 to 1;
            turnInput = Input.GetAxis(inputSetting.TURN_AXIS);
            strafeInput = Input.GetAxis(inputSetting.STRAFE_AXIS);
            vOrbitInput = Input.GetAxisRaw(inputSetting.ORBIT_VERTICAL);//interpolated meaning it will return any value from -1 to 1
            jumpInput = Input.GetAxisRaw(inputSetting.JUMP_AXIS);//not interpolated you will get -1 0 or 1
            chatInput = Input.GetAxisRaw(inputSetting.CHAT_AXIS);
            pauseInput = Input.GetAxisRaw(inputSetting.PAUSE_AXIS);
            //weaponInput = Input.GetAxisRaw(inputSetting.ACTIVE_WEAPONINPUT);
      //      PlayerInventory.recieveInput(weaponInput);
            PlayerPosition.recieveInput(forwardInput, turnInput, jumpInput, strafeInput);
            CameraController.receieveInput(vOrbitInput);
            PlayerChat.recieveInput(chatInput);
        }
        pauseInput = Input.GetAxisRaw(inputSetting.PAUSE_AXIS);
        player.recieveInput(pauseInput);
    }


    bool m_controlsOn;
    public bool ControlsOn
    {
        get { return m_controlsOn; }
        set { m_controlsOn = value; }
    }
}
