using UnityEngine;
using System.Collections;

public class PlayerHUD : PlayerBase {

    
	// Use this for initialization

    void OnGUI()
    {
        if (photonView.isMine) {

            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.blue;
            
            style.fontSize = 20;
            GUILayout.Label("Health: " + sCharacterClass.currentHealth.ToString() + " / "
                + sCharacterClass.maxHealth.ToString(), style);
            GUILayout.Label("Level: " + sCharacterClass.level.ToString(), style);
            GUILayout.Label("xp" + sCharacterClass.experience.ToString() + " / " + sCharacterClass.reqExperience.ToString(), style);
        }
    }
}
