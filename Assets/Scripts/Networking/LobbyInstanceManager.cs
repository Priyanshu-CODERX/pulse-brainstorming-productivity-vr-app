using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class LobbyInstanceManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField usernameField;
    public TMP_InputField roomcodeField;

    [SerializeField]
    private int m_SceneIndex;

    public void EnterAndJoinRoom()
    {
        if(usernameField.text == "" || roomcodeField.text == "")
            return;

        PlayerInstanceManager.Instance.Username = usernameField.text;
        PlayerInstanceManager.Instance.Roomcode = roomcodeField.text;
        SceneManager.LoadScene(m_SceneIndex);
    }
}
