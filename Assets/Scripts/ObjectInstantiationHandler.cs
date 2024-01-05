using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInstantiationHandler : MonoBehaviour
{
    [SerializeField] private GameObject _object;
    [SerializeField] private Transform _instantiationOrigin;

    private GameObject _instantiatedObject = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hands"))
        {
            InstantiateObjectOnHoveringBubble();
        }
    }

    public void InstantiateObjectOnHoveringBubble()
    {
        if(_instantiatedObject == null)
        {
            _instantiatedObject = Instantiate(_object);
            _instantiatedObject.transform.position = _instantiationOrigin.transform.position;
        }
    }
}
