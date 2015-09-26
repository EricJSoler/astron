using UnityEngine;
using System.Collections;

public class LeviathanWeapon : WeaponI {
    GunFireControl firingSystem;
    Vector3 positionOnPlayer = new Vector3(.23f, 1.2f, .2f);
	void Start()
    {
        m_LoadedClipState =  new LeviathanLoadedClip(this);
        m_EmptyClipState = new LeviathanEmptyClip(this);
        m_ReloadingState = new LeviathanReloading(this);
        m_NotOnPlayer = new LeviathanNotOnPlayer(this);
        m_InInventory = new LeviathanInInventory(this);
        firingSystem = new MachineGunFireControl(.3f, this);// FireControl(this);
        weaponStats.damage = 0;
        weaponStats.electric = 100;
        weaponStats.clipSize = 10;
        weaponStats.clipAmmo = 10;
        weaponStats.maxAmmo = 30;
        weaponStats.currentAmmo = 30;
        weaponStats.reloadTime = 2f;
        weaponStats.range = 1000;
        m_currentState = m_NotOnPlayer;
        
        m_gunId = "Leviathan" + GameObject.FindObjectsOfType<LeviathanWeapon>().Length.ToString();
    }
    ///Functions For Changing state shouldnt be called by 
    ///classes outside of this one except within the states
    public override void switchToEmptyClipState()
    {
        m_currentState = m_EmptyClipState;
    }
    public override void switchToLoadedClipState()
    {
        base.switchToLoadedClipState();
    }
    public override void switchToReloadState()
    {
        base.switchToReloadState();
    }
    ////////////////////////////////////////////////////////////////////////////
    ///Input Handling
    public override void listenForInput()
    {
        crossHairSettings.location.x = Screen.width / 2;
        crossHairSettings.location.y = (Screen.height / 2) - 10;
        firingSystem.gunControl();
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player") {
            PlayerController toNotify = col.gameObject.GetComponent<PlayerController>();
            toNotify.notifyPlayerOfPickUpAbleWeapon(this);
        }
    }
    public override void recieveInput(float input)
    {

    }
    ///Actions
    /////////////////////////////////////////////////////////////////////////////////
    public override void doDamage(AIEnemy target)
    {
        float amount = OwnedPlayer.sCharacterClass.electronics + weaponStats.electric;
        target.photonView.RPC("recieveDamage", PhotonTargets.All, amount);
    }
    public override void doDamage(Player target)
    {
        float amount = OwnedPlayer.sCharacterClass.electronics + weaponStats.electric;
        target.photonView.RPC("recieveDamage", PhotonTargets.All, amount);
    }
    public override void dropWeapon()
    {

    }
    public override void attachWeapon(Player attachTo, bool asActive)
    {
        m_ownedPlayer = attachTo;
        gameObject.transform.parent = m_ownedPlayer.gameObject.transform;
        gameObject.transform.localPosition = positionOnPlayer;
        gameObject.transform.rotation = m_ownedPlayer.gameObject.transform.rotation;
        if (asActive) {
            Visuals.turnOnRenderers();
            if (weaponStats.clipAmmo > 0) {
                m_currentState = m_LoadedClipState;
            }
            else {
                m_currentState = m_EmptyClipState;
            }
        }
    }
    public override void reload()
    {
        waitFor(weaponStats.reloadTime, m_currentState.reload,this.switchToLoadedClipState);
    }
    
    ///////////////////////////////////////////////////////////////////////////
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }

    void OnGUI()
    {
        if (OwnedPlayer != null) {
            if(OwnedPlayer.photonView.isMine)
                m_currentState.aim();
        }
        
    }




    ///Timing
    public void waitFor(float time, weaponDelegate doThisThenWait, weaponDelegate thenDoThis)
    {
        StartCoroutine(timedWait(time, doThisThenWait, thenDoThis));
    }

    IEnumerator timedWait(float time, weaponDelegate doThisThenWait, weaponDelegate thenDoThis)
    {
        doThisThenWait();
        yield return new WaitForSeconds(time);
        thenDoThis();
    }
    ///////////////////////////////////////////////////////////////////////////
}
