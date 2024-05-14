using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class NetworkedPlayerConfiguration : MonoBehaviour
{
    public CharacterController XRRigCharacterController;
    public CharacterControllerDriver XRRigCharacterControllerDriver;
    public GameObject XRRigCamera;

    public void IsLocalUser()
    {
        XRRigCharacterController.enabled = true;
        XRRigCharacterControllerDriver.enabled = true;
        XRRigCamera.SetActive(true);
    }
}
