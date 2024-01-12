using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField] private Transform _followableObject;

    private void Update()
    {
        this.gameObject.transform.position = _followableObject.transform.position;
    }
}
