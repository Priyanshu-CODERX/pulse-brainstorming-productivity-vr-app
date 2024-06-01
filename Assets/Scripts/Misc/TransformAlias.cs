using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformAlias : MonoBehaviour
{
    public Transform CameraTransform;
    public Transform TargetTransform;

    private void Update()
    {
        if(CameraTransform != null && TargetTransform != null)
        {
            TargetTransform.transform.position = CameraTransform.transform.position;
            TargetTransform.transform.rotation = CameraTransform.transform.rotation;
        }
    }
}
