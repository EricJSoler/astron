using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{

    // Use this for initialization
    DataHolder dataHolderObj;
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
        if (GUI.Button(buttonRect, "StartNewGame")) {
            dataHolderObj.LastScene = 3;
            Application.LoadLevel(1);
        }

        buttonRect = new Rect(0, Screen.height - buttonHeight * i--, buttonWidth, buttonHeight);
        if (GUI.Button(buttonRect, "Load Saved Game")) {
            dataHolderObj.load();
        }
        buttonRect = new Rect(0, Screen.height - buttonHeight * i--, buttonWidth, buttonHeight);
        if (GUI.Button(buttonRect, "Save Game")) {
            dataHolderObj.save();
        }
        buttonRect = new Rect(0, Screen.height - buttonHeight * i--, buttonWidth, buttonHeight);
        if (GUI.Button(buttonRect, "Quit")) {
            Debug.Log("trying to quit");
            Application.Quit();

        }
    }
}
