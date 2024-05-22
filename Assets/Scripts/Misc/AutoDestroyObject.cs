using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyObject : MonoBehaviour
{
    private bool isGrabbed = false;
    private bool scheduledForDestruction = false;
    [SerializeField] private float delayBeforeDestruction = 20f; // Adjust the delay as needed
    [SerializeField] private string objectTag = "";

    private void Update()
    {
        if (isGrabbed && gameObject.CompareTag(objectTag))
        {
            // The object is grabbed, cancel destruction
            CancelScheduledDestruction();
        }
    }

    public void GrabObject()
    {
        isGrabbed = true;
    }

    public void ReleaseObject()
    {
        isGrabbed = false;
        ScheduleDestruction();
    }

    private void ScheduleDestruction()
    {
        if (!scheduledForDestruction)
        {
            scheduledForDestruction = true;
            Invoke(nameof(DestroyObject), delayBeforeDestruction);
        }
    }

    private void CancelScheduledDestruction()
    {
        if (scheduledForDestruction)
        {
            scheduledForDestruction = false;
            CancelInvoke("DestroyObject");
        }
    }

    private void DestroyObject()
    {
        // Perform any cleanup or additional actions before destroying
        PhotonNetwork.Destroy(gameObject);
    }
}