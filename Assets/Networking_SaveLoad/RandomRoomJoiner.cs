using UnityEngine;
using System.Collections;


public class RandomRoomJoiner : Photon.PunBehaviour
{

    //taken from an old version of project mocha
    // Use this for initialization
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings("v1.0");
    }

    void Update()
    {
        if (PhotonNetwork.connectedAndReady) {
            
            
        }
    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
        
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    public override void  OnJoinedLobby()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    void OnPhotonRandomJoinFailed()
    {
        PhotonNetwork.CreateRoom(null);
    }

    public override void OnJoinedRoom()
    {
        DataHolder dataObj = FindObjectOfType<DataHolder>();
        Debug.Log(dataObj.LastScene);
        PhotonNetwork.LoadLevel(3);
    }
}
