using UnityEngine;
using System.Collections;

public class CharacterCreation : MonoBehaviour
{

    DataHolder dataHolderObj;
    // Use this for initialization
    void Start()
    {
        dataHolderObj = FindObjectOfType<DataHolder>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        int i = 4;
        float buttonHeight = 50;
        float buttonWidth = 150;
        Rect buttonRect;
        buttonRect = new Rect(0, Screen.height - buttonHeight * i--, buttonWidth, buttonHeight);
        if (GUI.Button(buttonRect, "Choose Warrior")) {

            dataHolderObj.CharacterClass = new BaseWarrior();
            dataHolderObj.CharacterPrefab = "Kristoph2";
            dataHolderObj.LastScene = 2;
            Application.LoadLevel(2);
        }
    }
}
