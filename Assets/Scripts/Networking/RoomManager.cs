using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public GameObject XRRig;
    public Transform spawnPoint;

    private void Start()
    {
        Debug.Log("Connecting!");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        Debug.Log("Connected to Server");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        PhotonNetwork.JoinOrCreateRoom("TestRoom", null, null);
        Debug.Log("We are connected! and currently in a room now");
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        GameObject _playerRig = PhotonNetwork.Instantiate(XRRig.name, spawnPoint.position, Quaternion.identity);
        _playerRig.GetComponent<NetworkedPlayerConfiguration>().IsLocalUser();
    }
}
