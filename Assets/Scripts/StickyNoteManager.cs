using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyNoteManager : MonoBehaviour
{
    [SerializeField] private Transform _stickyNoteOrigin;
    [SerializeField] private GameObject _stickyNoteShadowObject;
    [SerializeField] private int _maxLength = 20;

    private GameObject _insShadowObject = null;
    private RaycastHit hit;

    private void Update()
    {
        ShootRaycast();
    }

    public void OnGrabRelease()
    {
        this.gameObject.transform.position = hit.point;
        this.gameObject.transform.rotation = Quaternion.identity;
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
                if (_insShadowObject == null)
                {
                    _insShadowObject = Instantiate(_stickyNoteShadowObject);
                }
                _insShadowObject.transform.position = hit.point;
            }
        }
        else
        {
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
