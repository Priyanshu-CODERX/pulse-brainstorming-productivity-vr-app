using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StickyNoteManager : MonoBehaviour
{
    [SerializeField] private Transform _stickyNoteOrigin;
    [SerializeField] private GameObject _stickyNoteShadowObject;
    [SerializeField] private int _maxLength = 20;
    [SerializeField] private Transform _parentObject;
    [SerializeField] private UnityEvent OnEventCaptured;
    [SerializeField] private UnityEvent OnEventReleased;

    private GameObject _insShadowObject = null;
    private RaycastHit hit;
    private bool _canAttach = false;

    private void Update()
    {
        ShootRaycast();
    }

    public void OnGrabRelease()
    {
        if(_canAttach)
        {
            this.gameObject.transform.position = hit.point;
            this.gameObject.transform.rotation = Quaternion.identity;
            OnEventCaptured.Invoke();
        }
    }

    void ShootRaycast()
    {
        Vector3 origin = _stickyNoteOrigin.transform.position;
        Vector3 direction = _stickyNoteOrigin.transform.forward;

        if (Physics.Raycast(origin, direction, out hit, _maxLength))
        {
            _stickyNoteShadowObject.transform.position = hit.point;

            if (hit.collider.gameObject.CompareTag("Board"))
            {
                _canAttach = true;

                if (_insShadowObject == null)
                {
                    _insShadowObject = Instantiate(_stickyNoteShadowObject);
                    _insShadowObject.transform.parent = _parentObject.transform;
                }
                _insShadowObject.transform.position = hit.point;
            }
            else
            {
                _canAttach = false;
            }
        }
        else
        {
            _canAttach = false;
            DestroyStickyNoteShadow();
        }
    }

    void DestroyStickyNoteShadow()
    {
        if (_insShadowObject != null)
        {
            Destroy(_insShadowObject);
            _insShadowObject = null;
        }
    }
}
