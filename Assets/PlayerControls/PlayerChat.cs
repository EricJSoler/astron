using UnityEngine;
using System.Collections;

/// <summary>
/// PlayerChat handles the chat box that allows players to message eachother
/// across the network
/// </summary>
public class PlayerChat : PlayerBase
{

    //send and recieve values.
    private string sendString = "";
    private string recString = "";
    //name of the person over the chat.
    private string chatId = "";
    bool sendOnce;



    bool chatIsUp = false;
    PlayerChat[] chatObjects;

    // Use this for initialization
    void Start()
    {
        if (PhotonNetwork.isMasterClient) {
            chatId = "George";
        }
        else {
            chatId = "Sadam";
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    [PunRPC]
    public void messageHandle(string recieve, string chatName)
    {
        recString = chatName + ": " + recieve + "\n" + recString;
    }

    void OnGUI()
    {
        if (chatIsUp) {
            turnOnChat();
        }

    }


    void turnOnChat()
    {
        GUI.Label(new Rect(0, Screen.height / 1.5f - 20, 100, 30), "Talk that shit!");
        sendString = GUI.TextField(new Rect(0, Screen.height / 1.5f, 200, 25), sendString, 200);
        GUI.Label(new Rect(0, Screen.height / 1.5f + 25, 350, 9000), recString);
    }

    void OnPhotonSerializeView()
    { }

    public void recieveInput(float input)
    {
        if (input > 0 && chatIsUp) {
            sendCurrentMessage();
        }
        else if (input > 0) {
            chatIsUp = true;
        }
        else if (input < 0) {
            chatIsUp = false;
        }
    }

    public void sendCurrentMessage()
    {
        if (!sendString.Equals("")) {
            Debug.Log("sned");
            sendOnce = false;

            if (!sendOnce) {
                chatObjects = GameObject.FindObjectsOfType<PlayerChat>();
                foreach (PlayerChat element in chatObjects) {
                    string[] parameters = new string[] { sendString, chatId };
                    element.photonView.RPC("messageHandle", PhotonTargets.AllBufferedViaServer, parameters);
                    sendString = "";
                }
                sendOnce = true;
            }
        }
    }

}
