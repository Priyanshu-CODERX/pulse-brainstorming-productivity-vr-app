using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class XRHandController : MonoBehaviourPunCallbacks
{
    public InputActionReference gripInputActionReference;
    public InputActionReference triggerInputActionReference;
    public bool isNetworked = false;

    private Animator _handAnimator;
    private float _gripValue;
    private float _triggerValue;

    private void Start()
    {
        _handAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            AnimateGrip();
            AnimateTrigger();
        }
    }

    private void AnimateGrip()
    {
            _gripValue = gripInputActionReference.action.ReadValue<float>();
            _handAnimator.SetFloat("Grip", _gripValue);
    }

    private void AnimateTrigger()
    {
            _triggerValue = triggerInputActionReference.action.ReadValue<float>();
            _handAnimator.SetFloat("Trigger", _triggerValue);
    }
}
