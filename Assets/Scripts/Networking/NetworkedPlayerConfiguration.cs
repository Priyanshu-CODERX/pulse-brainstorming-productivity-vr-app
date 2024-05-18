using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class NetworkedPlayerConfiguration : MonoBehaviourPunCallbacks
{
    public CharacterController XRRigCharacterController;
    public CharacterControllerDriver XRRigCharacterControllerDriver;
    public XRBaseController XRLeftController;
    public XRBaseController XRRightController;
    public InputData XRRigInputData;
    public GameObject XRLocomotion;
    public GameObject XRRigCamera;

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
}
