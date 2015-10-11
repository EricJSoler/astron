using UnityEngine;
using System.Collections;

public class ArionWeapon : WeaponI {

    GunFireControl firingSystem;
    Vector3 positionOnPlayer = new Vector3(.084f, 1.127f, .442f);
    
    void Start()
    {
        m_LoadedClipState = new ArionLoadedClip(this);
        m_EmptyClipState = new ArionEmptyClip(this);
        m_ReloadingState = new ArionReloading(this);
        m_NotOnPlayer = new ArionNotOnPlayer(this);
        m_InInventory = new ArionInInventory(this);
        firingSystem = new MachineGunFireControl(.1f, this);// FireControl(this);
        weaponStats.damage = 20;
        weaponStats.electric = 0;
        weaponStats.clipSize = 30;
        weaponStats.clipAmmo = 30;
        weaponStats.maxAmmo = 300;
        weaponStats.currentAmmo = 300;
        weaponStats.reloadTime = 1f;
        weaponStats.range = 300;
        m_currentState = m_NotOnPlayer;

        m_gunId = "Arion" + photonView.instantiationId;
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
    public override void switchToInInventoryState()
    {
        m_currentState = m_InInventory;
        Visuals.turnOffRenderers();
    }
    public override void switchToActiveOnPlayerState()
    {
        base.switchToActiveOnPlayerState();
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
        float amount = OwnedPlayer.sCharacterClass.attackDamage + weaponStats.damage;
        target.photonView.RPC("recieveDamage", PhotonTargets.All, amount);
    }
    public override void doDamage(Player target)
    {
        float amount = OwnedPlayer.sCharacterClass.attackDamage + weaponStats.damage;
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
        gameObject.transform.localEulerAngles = new Vector3(0f, -90f, 0f);
        if (asActive) {
            Visuals.turnOnRenderers();
            if (weaponStats.clipAmmo > 0) {
                m_currentState = m_LoadedClipState;
            }
            else {
                m_currentState = m_EmptyClipState;
            }
        }
        else {
            Visuals.turnOffRenderers();
            m_currentState = m_InInventory;
        }
    }
    public override void reload()
    {
        waitFor(weaponStats.reloadTime, m_currentState.reload, this.switchToLoadedClipState);
    }

    ///////////////////////////////////////////////////////////////////////////
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }

    void OnGUI()
    {
        if (OwnedPlayer != null) {
            if (OwnedPlayer.photonView.isMine)
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
