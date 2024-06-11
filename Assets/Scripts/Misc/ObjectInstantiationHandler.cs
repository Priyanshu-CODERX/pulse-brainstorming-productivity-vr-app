using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;

public class ObjectInstantiationHandler : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject _object;
    [SerializeField] private Transform _instantiationOrigin;

    [SerializeField] private UnityEvent _onCall;

    private GameObject _instantiatedObject = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hands"))
        {
            _onCall.Invoke();
        }
    }

    public void InstantiateObjectOnHoveringBubbleOneAtATime()
    {
        if(_instantiatedObject == null)
        {
            _instantiatedObject = PhotonNetwork.Instantiate(_object.name, _instantiationOrigin.transform.position, Quaternion.identity);
            _instantiatedObject.transform.position = _instantiationOrigin.transform.position;
            _instantiatedObject.transform.rotation = _instantiatedObject.transform.rotation;
        }
    }

    public void InstantiateObjectOnHoveringBubble()
    {
        _instantiatedObject = PhotonNetwork.Instantiate(_object.name, _instantiationOrigin.transform.position, Quaternion.identity);
        _instantiatedObject.transform.position = _instantiationOrigin.transform.position;
        _instantiatedObject.transform.rotation = _instantiatedObject.transform.rotation;
    }
}
