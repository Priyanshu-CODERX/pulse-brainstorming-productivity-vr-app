using Photon.Pun;
using UnityEngine;  
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class NetworkedPlayerConfiguration : MonoBehaviourPunCallbacks, IPunObservable
{
    public TMP_Text Username;

    public CharacterController XRRigCharacterController;
    public CharacterControllerDriver XRRigCharacterControllerDriver;
    public XRBaseController XRLeftController;
    public XRBaseController XRRightController;
    public InputData XRRigInputData;
    public GameObject XRLocomotion;
    public GameObject XRRigCamera;

    private void Start()
    {
        Username.text = NetworkManager.Instance.m_Username;
    }

    public void IsLocalUser()
    {
        if(photonView.IsMine)
        {
            XRRigCharacterController.enabled = true;
            XRRigCharacterControllerDriver.enabled = true;
            XRLeftController.enabled = true;
            XRRightController.enabled = true;
            XRLocomotion.SetActive(true);
            XRRigInputData.enabled = true;
            XRRigCamera.SetActive(true);
        }
    }

    public void LeaveRoom()
    {
        if(photonView.IsMine)
        {
            NetworkManager.Instance.OnLeaveRoom();
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(NetworkManager.Instance.m_Username);
        }
        else
        {
            Username.text = (string)stream.ReceiveNext();
        }

        Debug.Log($"Photon Message Info: {info}");
    }
}
