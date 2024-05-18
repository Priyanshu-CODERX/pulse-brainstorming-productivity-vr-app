using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInstanceManager : MonoBehaviourPunCallbacks
{
    private static PlayerInstanceManager instance;
    public static PlayerInstanceManager Instance
    {
        get
        {
            if (!instance)
                instance = FindObjectOfType<PlayerInstanceManager>();
            return instance;
        }
    }
    public string Roomcode;
    public string Username;
    public string Maxplayers;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
