using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;

public class HandleUserOwnership : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private PhotonView m_PhotonView;

    [SerializeField]
    private XRGrabInteractable m_GrabInteractable;

    private void Start()
    {
        m_PhotonView = GetComponent<PhotonView>();
        m_GrabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void Update()
    {
        if (m_PhotonView != null && m_GrabInteractable != null)
            HandleIndividualGrab();
    }

    public void HandleIndividualGrab()
    {
        if(m_GrabInteractable.isSelected)
        {
            TransferOwnership(m_PhotonView);
        }
    }

    public void TransferOwnership(PhotonView photonView)
    {
        photonView.TransferOwnership(PhotonNetwork.LocalPlayer);
    }
}
