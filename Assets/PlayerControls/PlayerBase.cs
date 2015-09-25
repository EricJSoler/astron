using UnityEngine;
using System.Collections;


/// <summary>
/// PlayerBase contains getter methods for all the components that are attached
/// to the player game object. This allows for the scripts to easily reference
/// each-other
/// </summary>
public class PlayerBase : Photon.MonoBehaviour
{

    Player m_Player;
    public Player player
    {
        get
        {
            if (m_Player == null) {
                m_Player = GetComponent<Player>();
            }
            return m_Player;
        }
    }

    PlayerPosition m_PlayerPosition;
    public PlayerPosition PlayerPosition
    {
        get
        {
            if (m_PlayerPosition == null) {
                m_PlayerPosition = GetComponent<PlayerPosition>();
            }

            return m_PlayerPosition;
        }
    }

    PlayerController m_playerController;
    public PlayerController PlayerController
    {
        get
        {
            if (m_playerController == null && photonView.isMine) {
                m_playerController = GetComponent<PlayerController>();
            }
            return m_playerController;
        }
    }

    PlayerInventory m_playerInventory;
    public PlayerInventory PlayerInventory
    {
        get
        {
            if (m_playerInventory == null) {
                m_playerInventory = GetComponent<PlayerInventory>();
            }
            return m_playerInventory;
        }
    }

    PlayerChat m_playerChat;
    public PlayerChat PlayerChat
    {
        get
        {
            if (m_playerChat == null) {
                m_playerChat = GetComponent<PlayerChat>();
            }
            return m_playerChat;
        }
    }

    CameraController m_playerCamera;
    public CameraController CameraController
    {
        get
        {
            if (m_playerCamera == null) {
                GameObject cameraObj = GameObject.FindGameObjectWithTag("PlayerCamera");
                m_playerCamera = cameraObj.GetComponent<CameraController>();
            }
            return m_playerCamera;
        }
    }

    BaseCharacterClass m_characterClass;
    public BaseCharacterClass sCharacterClass
    {
        get
        {
            if (photonView.isMine) {
                if(m_characterClass == null)
                    m_characterClass = DataHolder.CharacterClass;

                return m_characterClass;
            }
            else {
                Debug.Log("you shouldnt be accessing this BASECHARACTERCLASS");
                return null;
            }

        }
    }

    PlayerVisuals m_playerVisuals;
    public PlayerVisuals Visuals
    {
        get{
            if(m_playerVisuals == null)
                m_playerVisuals = GetComponent<PlayerVisuals>();
            
            return m_playerVisuals;
        }
    }

	PlayerHUD m_playerHUD;
	public PlayerHUD HUD
	{
		get{
			if(m_playerHUD == null)
				m_playerHUD = GetComponent<PlayerHUD>();
			
			return m_playerHUD;
		}
	}
    
    DataHolder m_dataHolder;
    public DataHolder DataHolder
    {
        get
        {
            if (photonView.isMine) {
                if (m_dataHolder == null) {
                    m_dataHolder = FindObjectOfType<DataHolder>();
                }
                return m_dataHolder;
            }
            else {
                Debug.LogError("you do not have access to this dataHolder PlayerBase because this isnt your photonview");
                return null;
            }
                
        }
    }
}
