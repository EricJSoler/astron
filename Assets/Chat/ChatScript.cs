using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChatScript : Photon.MonoBehaviour {
	//send and recieve values.
	private string sendString = "";
	private string recString = "";
	//name of the person over the chat.
	private string chatId = "";
	bool sendOnce;
	bool switchOnOff;

	ChatScript[] chatObjects;

	// Use this for initialization
	void Start () 
	{
		if (PhotonNetwork.isMasterClient) {
			chatId = "George";
		} else {
			chatId = "Sadam";
		}
	}
	
	// Update is called once per frame
	void Update () {
	
		chatObjects = GameObject.FindObjectsOfType<ChatScript>();
		chatControls ();
	}

	public void chatControls()
	{
		if (Input.GetKeyDown(KeyCode.DownArrow)) {
			
			if(!sendString.Equals(""))
			{
				Debug.Log ("sned");
				sendOnce = false;
				
				if(!sendOnce)
				{
					foreach(ChatScript element in chatObjects)
					{
						string[] parameters = new string[]{sendString, chatId};
						element.photonView.RPC("messageHandle",PhotonTargets.AllBufferedViaServer,parameters);
						sendString = "";
					}
					sendOnce = true;
				}
			}
		}

		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			switchOnOff = !switchOnOff;
		}
	}
	
	[PunRPC]
	public void messageHandle(string recieve, string chatName)
	{
		recString = chatName + ": " + recieve + "\n" + recString;
		switchOnOff = true;
	}

	void OnGUI()
	{
		if (switchOnOff) {
			turnOnChat ();
		}
	}

	void turnOnChat()
	{
		GUI.Label(new Rect(0,Screen.height/1.5f - 20,100,30),"Talk that shit!");
		sendString = GUI.TextField (new Rect (0, Screen.height / 1.5f, 200, 25), sendString, 200);
		GUI.Label(new Rect(0,Screen.height/1.5f + 25,350,9000),recString);
	}

	void OnPhotonSerializeView()
	{}



}
