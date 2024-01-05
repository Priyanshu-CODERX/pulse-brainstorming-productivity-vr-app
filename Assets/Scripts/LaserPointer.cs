using UnityEngine;

public class LaserPointer : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Transform _laserOrigin;
    [SerializeField] private Transform _laserEndPoint;
    [SerializeField] private GameObject _pointerEnd;
    [SerializeField] private int _maxLength;

    private GameObject _insPointer = null;

    private void Update()
    {
        _lineRenderer.SetPosition(0, _laserOrigin.transform.position);
        _lineRenderer.SetPosition(1, _laserEndPoint.transform.position);
        ShootRaycast();
    }

    void ShootRaycast()
    {
        Vector3 origin = _laserOrigin.transform.position;
        Vector3 direction = _laserOrigin.transform.forward;

        RaycastHit hit;
        if (Physics.Raycast(origin, direction, out hit, _maxLength))
        {
            _laserEndPoint.transform.position = hit.point;

            if (hit.collider.gameObject.CompareTag("Board"))
            {
                // Instantiate _insPointer only if it's null
                if (_insPointer == null)
                {
                    _insPointer = Instantiate(_pointerEnd);
                }
                _insPointer.transform.position = hit.point;
            }
        }
        else
        {
            // Destroy _insPointer if the raycast doesn't hit anything
            DestroyPointer();
        }
    }

    void DestroyPointer()
    {
        if (_insPointer != null)
        {
            Destroy(_insPointer);
            _insPointer = null;
        }
    }
}
