using Photon.Pun;
using Photon.Voice;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Voice.PUN;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    #region NetworkManager Singleton

    private static NetworkManager instance;
    public static NetworkManager Instance
    {
        get
        {
            if (!instance)
                instance = FindObjectOfType<NetworkManager>();
            return instance;
        }
    }

    #endregion

    private const string GAME_VERSION = "1";
    [SerializeField]
    private GameObject m_PlayerXRRig;
    [SerializeField]
    private Transform m_SpawnPoint;

    public string m_Username;
    public string m_Roomcode;
    public int m_Maxplayers = 5;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        m_Username = PlayerInstanceManager.Instance.Username;
        m_Roomcode = PlayerInstanceManager.Instance.Roomcode;

        if(!PhotonNetwork.IsConnected)
        {
            Debug.Log("Connecting!");
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = GAME_VERSION;
            PhotonNetwork.NickName = m_Username;
        }
    }

    public void OnLeaveRoom()
    {
        if(PhotonNetwork.IsConnected)
        {
            PhotonNetwork.LeaveRoom();
        }
    }

    #region Networked Overrides

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        Debug.Log("Connected to Server");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        PhotonNetwork.JoinOrCreateRoom(m_Roomcode, new RoomOptions { MaxPlayers = m_Maxplayers }, null);
        Debug.Log("We are connected! and currently in a room now");
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        GameObject _xrInstance = PhotonNetwork.Instantiate(m_PlayerXRRig.name, m_SpawnPoint.position, Quaternion.identity);
        _xrInstance.GetComponent<NetworkedPlayerConfiguration>().IsLocalUser();
    }

    public override void OnPlayerEnteredRoom(Player player)
    {
        base.OnPlayerEnteredRoom(player);

        Debug.Log($"OnPlayerEnteredRoom(): {player.NickName}");

        if(PhotonNetwork.IsMasterClient)
        {
            Debug.Log($"OnPlayerEnteredRoom IsMasterClient {PhotonNetwork.IsMasterClient}");
        }

    }

    public override void OnPlayerLeftRoom(Player player)
    {
        base.OnPlayerLeftRoom(player);

        Debug.Log($"OnPlayerLeftRoom(): {player.NickName}");

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log($"OnPlayerLeftRoom IsMasterClient {PhotonNetwork.IsMasterClient}");
        }
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        Debug.Log("User Left The Room");

        SceneManager.LoadScene(0);
    }

    #endregion
}
