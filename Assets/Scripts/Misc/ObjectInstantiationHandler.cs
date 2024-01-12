using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectInstantiationHandler : MonoBehaviour
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
            _instantiatedObject = Instantiate(_object);
            _instantiatedObject.transform.position = _instantiationOrigin.transform.position;
        }
    }

    public void InstantiateObjectOnHoveringBubble()
    {
        _instantiatedObject = Instantiate(_object);
        _instantiatedObject.transform.position = _instantiationOrigin.transform.position;
    }
}
