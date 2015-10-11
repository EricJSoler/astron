using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHUD : PlayerBase
{

    // Use this for initialization
    Slider healthSlider;
    Text[] messageText;
    Text messenger1;
    Text messenger2;
    string msg1;
    string msg2;
    WeaponI collideWeapon;
    int currentlevel;
    int increaseStat;
    GameObject uiOBJ;
    void Start()
    {
        if (photonView.isMine) {
            uiOBJ = GameObject.FindWithTag("PlayerUI");
            healthSlider = uiOBJ.GetComponentInChildren<Slider>();
            healthSlider.value = 1;
            messageText = uiOBJ.GetComponentsInChildren<Text>();
            foreach (Text element in messageText) {
                if (element.name == "Message")
                    messenger2 = element;
                else
                    messenger1 = element;
            }
        }
        else {
            this.enabled = false;
        }
    }

    public void recievePlayerHit()
    {
        healthSlider.value = sCharacterClass.currentHealth / sCharacterClass.maxHealth;
    }

    void LateUpdate()
    {

    }
    void OnGUI()
    {
        listenForLevelUp();
        msg1 = "@AstroBros:~$ ls \n";
        if (collideWeapon != null) {
            msg2 = "> Press F to pick up " + collideWeapon.GunID + "\n";
        }
        else
            msg2 = "";
        msg1 += "Level: " + sCharacterClass.level.ToString() + "\n" +
            "xp" + sCharacterClass.experience.ToString() + " / " + sCharacterClass.reqExperience.ToString() + "\n" + "0b1110001111";
        messenger1.text = msg1;
        messenger2.text = msg2;
        if (increaseStat > 0) {
            levelUpStats();
        }
    }

    public void pickUpGunVisual(WeaponI weapon)
    {
        collideWeapon = weapon;
    }



    private void listenForLevelUp()
    {
        bool incOnce = false;
        if (currentlevel < sCharacterClass.level) {
            if (!incOnce) {
                increaseStat++;
                currentlevel = sCharacterClass.level;
                incOnce = true;
            }
        }
    }


    void levelUpStats()
    {
        int i = 4;
        float buttonHeight = 50;
        float buttonWidth = 100;
        Rect button = new Rect(0, (Screen.height / 2) - buttonHeight * i--,
                                buttonWidth, buttonHeight);
        if (GUI.Button(button, "+ Attack Damage")) {
            sCharacterClass.increaseAttackDamage(2);
            increaseStat--;
        }
        button = new Rect(0, (Screen.height / 2) - buttonHeight * i--,
                           buttonWidth, buttonHeight);
        if (GUI.Button(button, "+ Movement Speed")) {
            sCharacterClass.increaseMoveSpeed(10);
            increaseStat--;
        }
        button = new Rect(0, (Screen.height / 2) - buttonHeight * i--,
                           buttonWidth, buttonHeight);
        if (GUI.Button(button, "+ Jump")) {
            sCharacterClass.increaseAttackDamage(2);
            increaseStat--;
        }
        button = new Rect(0, (Screen.height / 2) - buttonHeight * i--,
                           buttonWidth, buttonHeight);
        if (GUI.Button(button, "+ Electronics")) {
            sCharacterClass.increaseElectronics(2);
            increaseStat--;
        }

    }







}
