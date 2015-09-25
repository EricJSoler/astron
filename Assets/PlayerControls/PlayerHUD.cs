using UnityEngine;
using System.Collections;

public class PlayerHUD : PlayerBase {

	// Use this for initialization
	WeaponI collideWeapon;
	int currentlevel;
	int increaseStat;


    void OnGUI()
    {

        if (photonView.isMine) {

			listenForLevelUp();

            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.blue;
            style.fontSize = 20;
            GUILayout.Label("Health: " + sCharacterClass.currentHealth.ToString() + " / "
                + sCharacterClass.maxHealth.ToString(), style);
            GUILayout.Label("Level: " + sCharacterClass.level.ToString(), style);
            GUILayout.Label("xp" + sCharacterClass.experience.ToString() + " / " + sCharacterClass.reqExperience.ToString(), style);

			if(collideWeapon != null)
			{
				GUI.Label(new Rect(10, 200, 300, 20), "Press F to pick up " + collideWeapon.GunID, style);
			}

			if(increaseStat > 0)
			{
				levelUpStats();
			}

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
			if(!incOnce)
			{
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
		Rect button = new Rect (0, (Screen.height / 2) - buttonHeight * i--, 
		                 		buttonWidth, buttonHeight);
		if (GUI.Button (button, "+ Attack Damage")) {
			sCharacterClass.increaseAttackDamage(2);
			increaseStat--;
		}
		button = new Rect (0, (Screen.height / 2) - buttonHeight * i--, 
		                   buttonWidth, buttonHeight);
		if (GUI.Button (button, "+ Movement Speed")) {
			sCharacterClass.increaseMoveSpeed(10);
			increaseStat--;
		}
		button = new Rect (0, (Screen.height / 2) - buttonHeight * i--, 
		                   buttonWidth, buttonHeight);
		if (GUI.Button (button, "+ Jump")) {
			sCharacterClass.increaseAttackDamage(2);
			increaseStat--;
		}
		button = new Rect (0, (Screen.height / 2) - buttonHeight * i--, 
		                   buttonWidth, buttonHeight);
		if (GUI.Button (button, "+ Electronics")) {
			sCharacterClass.increaseElectronics(2);
			increaseStat--;
		}

	}







}
