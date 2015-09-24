using UnityEngine;
using System.Collections;


public delegate void weaponDelegate();
public abstract class WeaponI : WeaponBase {
    [System.Serializable]
    public class CrossHairSettings
    {
        public Texture2D fTexture;
        public Texture2D eTexture;
        public Vector2 location = Vector2.zero;
        public Vector2 scale = Vector2.zero;
    }

    [System.Serializable]
    public class WeaponStats
    {
        public int damage;
        public int electric;
        public int maxAmmo;
        public int currentAmmo;
        public int clipSize;
        public int clipAmmo;
        public int range;
        public float reloadTime;
    }

    public CrossHairSettings crossHairSettings = new CrossHairSettings();
    public WeaponStats weaponStats = new WeaponStats();
    
    public Texture2D guiCrossHairTexture;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    protected WeaponStateI m_currentState;
    public WeaponStateI currentState
    {
        get
        {
            return m_currentState;
            
        }
    }


    protected WeaponStateI m_LoadedClipState;
    protected WeaponStateI m_EmptyClipState;
    protected WeaponStateI m_ReloadingState;
    protected WeaponStateI m_NotOnPlayer;
    protected WeaponStateI m_InInventory;

    protected Player m_ownedPlayer;//need to initialize this somewhere
    public Player OwnedPlayer
    {
        get
        {
            return m_ownedPlayer;
        }
    }


    protected string m_gunId;
    public string GunID
    {
        get
        {
            return m_gunId;
        }
    }
    //Functions For Changing State
    public virtual void switchToReloadState()
    {
        m_currentState = m_ReloadingState;
    }

    public virtual void switchToInInventoryState()
    {
        m_currentState = m_InInventory;
    }

    public virtual void switchToNotOnPlayerState()
    {
        m_currentState = m_NotOnPlayer;
    }

    public virtual void switchToEmptyClipState()
    {
        m_currentState = m_EmptyClipState;
    }

    public virtual void switchToLoadedClipState()
    {
        m_currentState = m_LoadedClipState;
    }
    /// /////////////////
    ///InputHandling
    public abstract void recieveInput(float input);
    public abstract void listenForInput();
    ////////Actions
    public virtual void doDamage(Player target)
    {
        float amount = 0;
        target.photonView.RPC("recieveDamage", PhotonTargets.All, amount);
    }
    public virtual void doDamage(AIEnemy target)
    {
        float amount = 0;
        target.photonView.RPC("recieveDamage", PhotonTargets.All, amount);
    }

    public virtual void getKillCredit(AIEnemy shot)
    {
        shot.requestXPOnDeath(OwnedPlayer);
    }

    public virtual void getKillCredit(Player shot)
    {
        shot.requestXPOnDeath(OwnedPlayer);
    }

    public virtual void reload()
    {
        m_currentState = m_ReloadingState;
    }


    
    public abstract void dropWeapon();
    public abstract void attachWeapon(Player attachTo, bool asActive);
    

    }
