using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour
{

    bool on;
    DataHolder dataHolderObj;
    // Use this for initialization
    void Start()
    {
        dataHolderObj = FindObjectOfType<DataHolder>();
    }

    public void turnOn()
    {
        on = true;
    }

    public void turnOff()
    {
        on = false;
    }

    void OnGUI()
    {
        if (on) {
            turnOnMenu();
        }
    }

    void turnOnMenu()
    {
        int i = 4;
        float buttonHeight = 50;
        float buttonWidth = 150;
        Rect buttonRect;
        buttonRect = new Rect(0, Screen.height - buttonHeight * i--, buttonWidth, buttonHeight);
        if (GUI.Button(buttonRect, "Save")) {
            dataHolderObj.LastScene = Application.loadedLevel;
            dataHolderObj.save();
        }
        buttonRect = new Rect(0, Screen.height - buttonHeight * i--, buttonWidth, buttonHeight);
        if (GUI.Button(buttonRect, "Quit To Main Menu")) {
            dataHolderObj.LastScene = Application.loadedLevel;
            dataHolderObj.save();
            PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.player);
            PhotonNetwork.Disconnect();
            Application.LoadLevel(0);
        }
    }
}
