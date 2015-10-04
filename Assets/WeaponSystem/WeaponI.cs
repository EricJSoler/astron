using UnityEngine;
using System.Collections;

//Used for timing things in weapon mechanics
public delegate void weaponDelegate();

//All Weapon Classes should inherit from WeaponI
//WeaponI provides a base template that all WeaponsCanBeExpected To Implement 
public abstract class WeaponI : WeaponBase
{

    //CrossHair SEttings allows for us to create custom cross-hairs for weapons
    //By draging the 2D texture in from the inspector
    //The members of this class are referneced in implementations of WeaponI and
    //Within the different states each gun should have
    [System.Serializable]
    public class CrossHairSettings
    {
        public Texture2D fTexture;
        public Texture2D eTexture;
        public Vector2 location = Vector2.zero;
        public Vector2 scale = Vector2.zero;
    }

    //Weapon stats decide how much damage should be dealt
    //how much ammo a weapon has. These stats should be set in the implementation
    //of WeaponI and are referenced in the different states the weapon is in
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

    //Instance of CrossHairSettings that will be used by the weaopn to 
    //display the crosshairs that were loaded from the inspector
    public CrossHairSettings crossHairSettings = new CrossHairSettings();

    //weaponStats the stats for the given weapon these are initialized within
    //the instance of the weapon class upon awake.
    public WeaponStats weaponStats = new WeaponStats();

    //guiCrossHairTexture is the texture that will be displayed by the gui
    //this variable is set to different textures from the crosshair settings
    //depending on what the user is aiming at
    public Texture2D guiCrossHairTexture;

    //m_current state is the current state the weapon is in 
    //action methods called on instances of WeaponI are delegated to this state
    //and are handled differently based upon which state the weapon is in
    protected WeaponStateI m_currentState;

    //Returns the current state of the weapon. Users are not allowed to set
    //the current state because the weapon handles what state it is in internally
    public WeaponStateI currentState
    {
        get
        {
            return m_currentState;
        }
    }

    //All the different states a weapon can have. The weapon class will switch
    //between these states as the weapon does different things
    protected WeaponStateI m_LoadedClipState;
    protected WeaponStateI m_EmptyClipState;
    protected WeaponStateI m_ReloadingState;
    protected WeaponStateI m_NotOnPlayer;
    protected WeaponStateI m_InInventory;

    //m_ownedPlayer is the player who is carrying the weapon otherwise it is null
    //this allows the weapon to acces information about the player who is carrying it
    //like their stats and if the player is a local or remote player
    protected Player m_ownedPlayer;

    //returns the player who owns the weapon. If no one owns the weapon returns null
    public Player OwnedPlayer
    {
        get
        {
            return m_ownedPlayer;
        }
    }

    //GunId is set without the instance of the weaponI class and is used as a 
    //means to determine which weapon is which on the map so that remote players
    //can correctly pick up and drop weapons. all weapons should have different gun ids
    protected string m_gunId;

    //return the id of the weapon all weapons should have a different gun id.
    public string GunID
    {
        get
        {
            return m_gunId;
        }
    }

    ///Functions For Changing State. These functions should only be called within
    /// the states of the weapon or within the weapon instance itself
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
    /// ///////////////// End of functions that change state
    ///InputHandling
    
    //Weapons recieveInput from playerInventory that informs if player pressed reload
    public abstract void recieveInput(float input);
    
    //Listen for input informs the weapon that its fire control should be listening
    //for clicking the mouse button down.
    public abstract void listenForInput();
    ///Actions 
    
    //If a weapon shoots a player it sends an rpc to everyone informing which
    //player was hit and sends the amount of damage dealt
    public virtual void doDamage(Player target)
    {
        float amount = 0;
        target.photonView.RPC("recieveDamage", PhotonTargets.All, amount);
    }
    
    //If a weapon shoots an enemy it sends an rpc to everyone telling them to 
    //update that enemies health
    public virtual void doDamage(AIEnemy target)
    {
        float amount = 0;
        target.photonView.RPC("recieveDamage", PhotonTargets.All, amount);
    }

    //Adds the player wielding this weapon to the enemies xp timer to recive xp
    //if the enemy dies within a given time period
    public virtual void getKillCredit(AIEnemy shot)
    {
        shot.requestXPOnDeath(OwnedPlayer);
    }


    //Adds the player wielding this weapon to the player xp timer to recive xp
    //if the player dies within a given time period
    public virtual void getKillCredit(Player shot)
    {
        shot.requestXPOnDeath(OwnedPlayer);
    }

    //Reloads the weapon
    public virtual void reload()
    {
        m_currentState = m_ReloadingState;
    }

    //Removes this weapon from the owned players inventory and drops the weapon
    //This update is sent as an rpc
    public abstract void dropWeapon();

    //Attaches the given weapon to the player the paramter asActive determines whether
    //the weapon is placed in the players inventory or if it is currently being wielded
    public abstract void attachWeapon(Player attachTo, bool asActive);
}
